using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Workshop.API.Models;
using Workshop.API.Services;

namespace Workshop.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepositoryService _userRepositoryService;

        public UsersController(UserManager<WorkshopUser> userManager, ITokenService tokenService, IUserRepositoryService userRepositoryService)
        {
            _tokenService = tokenService;
            _userRepositoryService = userRepositoryService;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkshopUser))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet, Route("details"), Authorize]
        public async Task<IActionResult> GetUserDetails()
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", string.Empty);
            var claimsPrincpal = _tokenService.GetClaimsPrincipalFromExpiredToken(token);
            var user = await _userRepositoryService.GetUserByEmailAsync(claimsPrincpal.Identity?.Name);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
