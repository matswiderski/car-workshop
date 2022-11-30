using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Workshop.API.Models;
using Workshop.API.Services;

namespace Workshop.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepositoryService _userRepositoryService;

        public AuthController(UserManager<WorkshopUser> userManager, ITokenService tokenService, IUserRepositoryService userRepositoryService)
        {
            _tokenService = tokenService;
            _userRepositoryService = userRepositoryService;
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationTokens))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest credentials)
        {
            bool userValid = await _userRepositoryService.IsUserValidAsync(credentials);
            if (!userValid)
                return BadRequest();
            string accesToken = _tokenService.GenerateAccesToken(credentials);
            string refreshToken = (await _userRepositoryService.CreateUserRefreshTokenAsync(credentials.EmailAddress)).Token;
            await _userRepositoryService.SaveChangesAsync();
            return Ok(new AuthenticationTokens { Token = accesToken, RefreshToken = refreshToken });
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Route("signup")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest credentials)
        {
            bool userCreated = await _userRepositoryService.CreateUserAsync(credentials);
            return userCreated ? Ok() : BadRequest();
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost, Route("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] AuthenticationTokens tokens)
        {
            var claimsPrincpal = _tokenService.GetClaimsPrincipalFromExpiredToken(tokens.Token);
            var email = claimsPrincpal.Identity?.Name;
            var user = await _userRepositoryService.GetUserByEmailAsync(email);
            if (user == null)
                return Unauthorized();
            var refreshToken = await _userRepositoryService.IsRefreshTokenValidAsync(user, tokens.RefreshToken);
            if (refreshToken == null)
                return Unauthorized();
            if (refreshToken.CreationTime.AddMinutes(1) < DateTime.UtcNow)
                return Unauthorized();
            await _userRepositoryService.DeleteTokenAsync(refreshToken.Token);
            string newAccesToken = _tokenService.GenerateAccesToken(
                new AuthenticationRequest
                {
                    EmailAddress = email,
                    Password = user.PasswordHash
                });
            var newRefreshToken = await _userRepositoryService.CreateUserRefreshTokenAsync(email);
            await _userRepositoryService.SaveChangesAsync();
            return Ok(
                new AuthenticationTokens { Token = newAccesToken, RefreshToken = newRefreshToken.Token });
        }
    }
}
