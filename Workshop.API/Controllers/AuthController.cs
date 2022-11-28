using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;
using Workshop.API.Models;
using Workshop.API.Services;

namespace Workshop.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<IdentityUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest credentials)
        {
            var user = await _userManager.FindByEmailAsync(credentials.EmailAddress);
            if (user.Equals(null))
                return NotFound();

            bool loginSucceded = await _userManager.CheckPasswordAsync(user, credentials.Password);
            if (!loginSucceded)
                return BadRequest();

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, "admin")
            };

            return Ok(new AuthenticationResponse { Token = _tokenService.GenerateToken(user, claims) });
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Route("signup")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest credentials)
        {
            var result = await _userManager.CreateAsync(
                new IdentityUser { Email = credentials.EmailAddress, UserName = string.Empty },
                credentials.Password
            );

            return result.Succeeded ? Ok() : BadRequest(new { errors = result.Errors });
        }
    }
}
