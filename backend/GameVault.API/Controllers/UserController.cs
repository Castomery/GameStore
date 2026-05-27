using System.Security.Claims;
using GameVault.Application.Dtos.Review;
using GameVault.Application.Dtos.User;
using GameVault.Application.Interfaces.Services;
using GameVault.Application.Services;
using GameVault.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameVault.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("profile")]
        public async Task<ActionResult<UserProfileDto>> GetUserProfile()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            if (userId <= 0)
            {
                return Unauthorized("User not authenticated.");
            }

            try
            {
                var userProfile = await _userService.GetUserProfileAsync(userId);
                if (userProfile == null) return NotFound();
                return Ok(userProfile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
