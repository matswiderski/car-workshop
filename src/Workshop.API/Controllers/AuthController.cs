using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;
using Workshop.API.Extensions;
using Workshop.API.Models;
using Workshop.API.Services;
using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;

namespace Workshop.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepositoryService _userRepositoryService;
        private readonly IMailService _mailService;
        private IValidator<RegisterRequest> _registerRequestValidator;
        private IValidator<AuthenticationRequest> _authenticationRequestValidator;

        public AuthController(UserManager<WorkshopUser> userManager,
            ITokenService tokenService, IUserRepositoryService userRepositoryService,
            IMailService mailService,
            IValidator<RegisterRequest> registerRequestValidator,
            IValidator<AuthenticationRequest> authenticationRequestValidator)
        {
            _tokenService = tokenService;
            _userRepositoryService = userRepositoryService;
            _mailService = mailService;
            _registerRequestValidator = registerRequestValidator;
            _authenticationRequestValidator = authenticationRequestValidator;
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest credentials)
        {
            ValidationResult fresult = await _authenticationRequestValidator.ValidateAsync(credentials);
            if (!fresult.IsValid)
                return BadRequest(fresult.ToResponseObject(StatusCodes.Status400BadRequest));
            string accessToken = _tokenService.GenerateAccessToken(credentials);
            string refreshToken = (await _userRepositoryService.CreateUserRefreshTokenAsync(credentials.EmailAddress)).Token;
            await _userRepositoryService.SaveChangesAsync();
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(14)
            };
            Response.Cookies.Append("x-refresh-token", refreshToken, cookieOptions);
            return Ok(new { token = accessToken });
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Route("signup")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest credentials)
        {
            ValidationResult fresult = await _registerRequestValidator.ValidateAsync(credentials);
            if (!fresult.IsValid)
                return BadRequest(fresult.ToResponseObject(StatusCodes.Status400BadRequest));
            var result = await _userRepositoryService.CreateUserAsync(credentials);
            var user = await _userRepositoryService.GetUserByEmailAsync(credentials.EmailAddress);
            string token = await _userRepositoryService.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Link("ConfirmEmail", new { id = user.Id, token });
            await _mailService.SendAsync("noreplygworkshop@ws.com", user.Email,
                    "Confirm your email address",
                    $"In order to confirm your email address click on this link: {confirmationLink}");
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Route("logout"), Authorize]
        public async Task<IActionResult> Logout()
        {
            string? token = Request.Cookies["x-refresh-token"];
            bool removed = await _userRepositoryService.DeleteRefreshTokenAsync(token);
            await _userRepositoryService.SaveChangesAsync();
            return removed ? Ok() : BadRequest();
        }

        [ProducesResponseType(StatusCodes.Status308PermanentRedirect)]
        [HttpGet, Route("confirm/{id}", Name = "ConfirmEmail")]
        public async Task<RedirectResult> ConfirmEmail(string id, [FromQuery] string token)
        {
            var user = await _userRepositoryService.GetUserByIdAsync(id);
            bool confirmCheckResult = await _userRepositoryService.ValidateEmailConfirmAsync(user);
            if (confirmCheckResult)
                return new RedirectResult("http://localhost:3000/", true);
            bool confirmed = await _userRepositoryService.ConfirmEmailAsync(user, token);
            return new RedirectResult("http://localhost:3000/", true);
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Object))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost, Route("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string token)
        {
            var claimsPrincpal = _tokenService.GetClaimsPrincipalFromExpiredToken(token);
            var email = claimsPrincpal.Identity.Name;
            var user = await _userRepositoryService.GetUserByEmailAsync(email);
            if (user == null)
                return Unauthorized();
            var refreshToken = await _userRepositoryService.IsRefreshTokenValidAsync(user, Request.Cookies["x-refresh-token"]);
            if (refreshToken == null)
                return Unauthorized();
            if (refreshToken.CreationTime.AddDays(14) < DateTime.UtcNow)
                return Unauthorized();
            await _userRepositoryService.DeleteRefreshTokenAsync(refreshToken.Token);
            string newAccessToken = _tokenService.GenerateAccessToken(
                new AuthenticationRequest
                {
                    EmailAddress = email,
                    Password = user.PasswordHash
                });
            var newRefreshToken = await _userRepositoryService.CreateUserRefreshTokenAsync(email);
            await _userRepositoryService.SaveChangesAsync();
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(14)
            };
            Response.Cookies.Append("x-refresh-token", newRefreshToken.Token, cookieOptions);
            return Ok(new { token = newAccessToken });
        }
    }
}
