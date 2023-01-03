using Categories.API.Dtos;
using Categories.API.Extensions;
using Categories.API.Models;
using Categories.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Categories.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;
        public CategoriesController(ICategoriesService categoriesService) =>
            _categoriesService = categoriesService;

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<Category>>> GetAsync() =>
            await _categoriesService.GetAsync();

        [HttpGet("{id:length(36)}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(string id)
        {
            var category = await _categoriesService.GetAsync(id);
            return category is null ? NotFound() : Ok(category);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CategoryDto categoryDto)
        {
            var newCategory = new Category
            {
                Id = Guid.NewGuid().ToString(),
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                Parts = categoryDto.Parts
            };
            await _categoriesService.CreateAsync(newCategory);
            return CreatedAtAction(nameof(GetAsync), new { id = newCategory.Id }, newCategory);
        }

        [HttpPut("{id:length(36)}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(string id, [FromBody] CategoryDto categoryDto)
        {
            var category = await _categoriesService.GetAsync(id);

            if (category is null)
                return BadRequest();

            await _categoriesService.UpdateAsync(id, categoryDto.AsCategory(id));

            return NoContent();
        }

        [HttpDelete("{id:length(36)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(string id)
        {
            var category = await _categoriesService.GetAsync(id);

            if (category is null)
                BadRequest();

            await _categoriesService.RemoveAsync(id);

            return NoContent();
        }
    }
}
