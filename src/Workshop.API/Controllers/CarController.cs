using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;
using Workshop.API.Dtos;
using Workshop.API.Extensions;
using Workshop.API.Models;
using Workshop.API.Services;

namespace Workshop.API.Controllers
{
    [Route("api/car")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepositoryService _userRepositoryService;
        private readonly ICarRepositoryService _carRepositoryService;

        public CarController(UserManager<WorkshopUser> userManager,
            ITokenService tokenService,
            IUserRepositoryService userRepositoryService,
            ICarRepositoryService carRepositoryService)
        {
            _tokenService = tokenService;
            _userRepositoryService = userRepositoryService;
            _carRepositoryService = carRepositoryService;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CarDto>))]
        [HttpGet, Route("get-all"), Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", string.Empty);
            var claimsPrincpal = _tokenService.GetClaimsPrincipalFromToken(token);
            return Ok((await _carRepositoryService.GetCarsAsync(claimsPrincpal.GetNameIdentifierId())).Select(c => c.AsDto()));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Car))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet, Route("get/{id:length(36)}"), Authorize]
        public async Task<IActionResult> GetCarAsync(string id)
        {
            Car car = await _carRepositoryService.GetCarAsync(id);
            if (car == null)
                return NotFound();
            return Ok(car.AsDto());
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Car))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Route("create"), Authorize]
        public async Task<IActionResult> CreateCarAsync([FromBody] CarDto car)
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", string.Empty);
            var claimsPrincpal = _tokenService.GetClaimsPrincipalFromToken(token);
            var user = await _userRepositoryService.GetUserByEmailAsync(claimsPrincpal.Identity?.Name);
            if (user == null)
                return BadRequest();
            Car newCar = await _carRepositoryService.CreateCarAsync(car, user);
            await _userRepositoryService.SaveChangesAsync();
            return CreatedAtAction("GetCar", new { id = newCar.Id }, newCar.AsDto());
        }
    }
}
