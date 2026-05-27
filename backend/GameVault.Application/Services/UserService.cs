using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GameVault.Application.Dtos.User;
using GameVault.Application.Interfaces.Repositories;
using GameVault.Application.Interfaces.Services;
using GameVault.Domain.Models;

namespace GameVault.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

        #region Hashing Helper
        private static string HashPassword(string password)
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
        private static string GenerateSecureToken(User user)
        {
            var claims = $"{user.Id}:{user.Email}:{DateTime.UtcNow.AddHours(2).Ticks}";
            var claimsBytes = Encoding.UTF8.GetBytes(claims);
            
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes("GameVaultSuperSecretKey123456789"));
            var signatureBytes = hmac.ComputeHash(claimsBytes);

            return $"{Convert.ToBase64String(claimsBytes)}.{Convert.ToBase64String(signatureBytes)}";
        }
        #endregion
    }
}
