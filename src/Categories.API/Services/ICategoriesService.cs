using Categories.API.Models;

namespace Categories.API.Services
{
    public interface ICategoriesService
    {
        Task CreateAsync(Category newCategory);
        Task<List<Category>> GetAsync();
        Task<Category?> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, Category updatedCategory);
    }
}