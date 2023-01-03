using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshop.API.Extensions;
using Workshop.API.Models;
using Workshop.API.Services;

namespace Workshop.API.Controllers
{
    [Route("api/service")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesRepository _servicesRepository;

        public ServicesController(IServicesRepository servicesRepository)
        {
            _servicesRepository = servicesRepository;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Service>))]
        [HttpGet, Route("get-all"), Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(_servicesRepository.GetServices().Select(s => s.AsDto()));
        }
    }
}
