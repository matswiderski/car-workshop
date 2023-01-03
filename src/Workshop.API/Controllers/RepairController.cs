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
    [Route("api/repair")]
    [ApiController]
    public class RepairController : ControllerBase
    {
        private readonly IRepairRepositoryService _repairRepositoryService;
        private readonly ITokenService _tokenService;
        private readonly IUserRepositoryService _userRepositoryService;
        public RepairController(ITokenService tokenService, IRepairRepositoryService repairRepositoryService, IUserRepositoryService userRepositoryService)
        {
            _repairRepositoryService = repairRepositoryService;
            _tokenService = tokenService;
            _userRepositoryService = userRepositoryService;
        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RepairDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet, Route("get/{id:length(36)}"), Authorize]
        public async Task<IActionResult> GetRepairAsync(string id)
        {
            Repair? repair = await _repairRepositoryService.GetRepairAsync(id);
            if (repair == null)
                return NotFound();
            return Ok(repair.AsDto());
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RepairDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Route("create"), Authorize]
        public async Task<IActionResult> CreateRepair([FromBody] RepairDto repair)
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", string.Empty);
            var claimsPrincpal = _tokenService.GetClaimsPrincipalFromToken(token);
            var user = await _userRepositoryService.GetUserByEmailAsync(claimsPrincpal.Identity?.Name);
            if (user == null)
                return BadRequest();
            Repair newRepair = await _repairRepositoryService.CreateRepairAsync(repair, user);
            await _userRepositoryService.SaveChangesAsync();
            return CreatedAtAction("GetRepair", new { id = newRepair.Id }, newRepair.AsDto());
        }
    }
}
