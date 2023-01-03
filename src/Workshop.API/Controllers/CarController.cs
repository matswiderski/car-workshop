using FluentValidation;
using FluentValidation.Results;
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
        private readonly IValidator<CarDto> _carValidator;

        public CarController(UserManager<WorkshopUser> userManager,
            ITokenService tokenService,
            IUserRepositoryService userRepositoryService,
            ICarRepositoryService carRepositoryService,
            IValidator<CarDto> carValidator)
        {
            _tokenService = tokenService;
            _userRepositoryService = userRepositoryService;
            _carRepositoryService = carRepositoryService;
            _carValidator = carValidator;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CarDto>))]
        [HttpGet, Route("get-all"), Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", string.Empty);
            var claimsPrincpal = _tokenService.GetClaimsPrincipalFromToken(token);
            return Ok((await _carRepositoryService.GetCarsAsync(claimsPrincpal.GetNameIdentifierId())).Select(c => c.AsDto()));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet, Route("get/{id:length(36)}"), Authorize]
        public async Task<IActionResult> GetCarAsync(string id)
        {
            Car? car = await _carRepositoryService.GetCarAsync(id);
            if (car == null)
                return NotFound();
            return Ok(car.AsDto());
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CarDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Route("create"), Authorize]
        public async Task<IActionResult> CreateCarAsync([FromBody] CarDto car)
        {
            ValidationResult fresult = await _carValidator.ValidateAsync(car);
            if (!fresult.IsValid)
                return BadRequest(fresult.ToResponseObject(StatusCodes.Status400BadRequest));
            string token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", string.Empty);
            var claimsPrincpal = _tokenService.GetClaimsPrincipalFromToken(token);
            var user = await _userRepositoryService.GetUserByEmailAsync(claimsPrincpal.Identity?.Name);
            if (user == null)
                return BadRequest();
            Car newCar = await _carRepositoryService.CreateCarAsync(car, user);
            await _userRepositoryService.SaveChangesAsync();
            return CreatedAtAction("GetCar", new { id = newCar.Id }, newCar.AsDto());
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch, Route("update"), Authorize]
        public async Task<IActionResult> UpdateCarAsync([FromBody] CarDto car)
        {
            ValidationResult fresult = await _carValidator.ValidateAsync(car);
            if (!fresult.IsValid)
                return BadRequest(fresult.ToResponseObject(StatusCodes.Status400BadRequest));
            var updatedCar = await _carRepositoryService.UpdateCarAsync(car);
            if (updatedCar == null)
                return BadRequest();
            await _userRepositoryService.SaveChangesAsync();
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Route("delete"), Authorize]
        public async Task<IActionResult> DeleteCarAsync([FromBody] CarDto[] cars)
        {
            foreach (var car in cars)
                await _carRepositoryService.DeleteCarAsync(car.id);
            await _userRepositoryService.SaveChangesAsync();
            return Ok();
        }
    }
}
