using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Workshop.API.Filters;
using Workshop.API.Models;

namespace Workshop.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }


        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(AuthenticatedResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginModel credentials)
        {
            if (credentials.UserName == "es")
                return Ok(new AuthenticatedResponse { Token = "xd" });

            return Ok("ok");
        }
    }
}
