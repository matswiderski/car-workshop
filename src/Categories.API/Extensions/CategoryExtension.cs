using Categories.API.Dtos;
using Categories.API.Models;

namespace Categories.API.Extensions
{
    public static class CategoryExtension
    {
        public static Category AsCategory(this CategoryDto categoryDto, string id) => new()
        {
            Id = id,
            Name = categoryDto.Name,
            Description = categoryDto.Description,
            Parts = categoryDto.Parts
        };
    }
}
