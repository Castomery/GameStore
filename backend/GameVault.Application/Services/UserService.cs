using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using GameVault.Application.Dtos.User;
using GameVault.Application.Interfaces.Repositories;
using GameVault.Application.Interfaces.Services;
using GameVault.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace GameVault.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto)
        {
            var existing = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (existing != null)
            {
                throw new InvalidOperationException("Email is already registered.");
            }

            var passwordHash = HashPassword(registerDto.Password);

            var user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PasswordHash = passwordHash
            };

            var createdUser = await _userRepository.AddAsync(user);

            var token = GenerateSecureToken(createdUser);

            return new AuthResponseDto
            {
                Token = token,
                UserName = createdUser.UserName,
                Email = createdUser.Email
            };
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return null;
            }

            var verified = VerifyPassword(loginDto.Password, user.PasswordHash);
            if (!verified)
            {
                return null;
            }

            var token = GenerateSecureToken(user);

            return new AuthResponseDto
            {
                Token = token,
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public async Task<UserProfileDto?> GetUserProfileAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            return new UserProfileDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.UserName,
            };
        }


        #region Hashing Helper
        private string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations: 100000,
                hashAlgorithm: HashAlgorithmName.SHA256,
                outputLength: 32);

            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        private static bool VerifyPassword(string password, string hashedPassword)
        {
            var parts = hashedPassword.Split('.');
            if (parts.Length != 2) return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] hash = Convert.FromBase64String(parts[1]);

            byte[] testHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations: 100000,
                hashAlgorithm: HashAlgorithmName.SHA256,
                outputLength: 32);

            return CryptographicOperations.FixedTimeEquals(hash, testHash);
        }
        #endregion

        #region Token Helper
        private string GenerateSecureToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey is not configured"));

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                }),
                Expires = DateTime.UtcNow.AddHours(double.Parse(_configuration["Jwt:ExpiresHours"]
                                                                ?? throw new InvalidOperationException("JWT ExpiresHours is not configured"))),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                )
            };

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }

        
        #endregion
    }
}
