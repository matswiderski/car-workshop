using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;
using Workshop.API.Dtos;
using Workshop.API.Extensions;
using Workshop.API.Models;
using Workshop.API.Services;

namespace Workshop.API.Controllers
{
    [Route("api/workshop")]
    [ApiController]
    public class WorkshopController: ControllerBase
    {
        private readonly IWorkshopRepositoryService _workshopRepositoryService;
        private readonly ITokenService _tokenService;
        private readonly IUserRepositoryService _userRepositoryService;
        public WorkshopController(
            ITokenService tokenService, 
            IWorkshopRepositoryService workshopRepositoryService, 
            IUserRepositoryService userRepositoryService)
        {
            _workshopRepositoryService = workshopRepositoryService;
            _tokenService = tokenService;
            _userRepositoryService = userRepositoryService;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WorkshopDto>))]
        [HttpGet, Route("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(_workshopRepositoryService.GetWorkshopsAsync().Select(w => w.AsDto()));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkshopDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet, Route("get/{id:length(36)}"), Authorize]
        public async Task<IActionResult> GetWorkshopAsync(string id)
        {
            Models.Workshop? workshop = await _workshopRepositoryService.GetWorkshopAsync(id);
            if (workshop == null)
                return NotFound();
            return Ok(workshop.AsDto());
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RepairDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Route("create"), Authorize]
        public async Task<IActionResult> CreateWorkshop([FromBody] WorkshopDto workshop)
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", string.Empty);
            var claimsPrincpal = _tokenService.GetClaimsPrincipalFromToken(token);
            var user = await _userRepositoryService.GetUserByEmailAsync(claimsPrincpal.Identity?.Name);
            if (user == null)
                return BadRequest();
            Models.Workshop newWorkshop = await _workshopRepositoryService.CreateWorkshopAsync(workshop, user);
            await _userRepositoryService.SaveChangesAsync();
            return CreatedAtAction("GetWorkshop", new { id = newWorkshop.Id }, newWorkshop.AsDto());
        }
    }
}
