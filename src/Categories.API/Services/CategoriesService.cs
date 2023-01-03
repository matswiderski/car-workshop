using Categories.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Categories.API.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IMongoCollection<Category> _categoriesCollection;

        public CategoriesService(IOptions<CategoriesDatabaseSettings> categoriesDatabaseSettings)
        {
            var mongoClient = new MongoClient(categoriesDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(categoriesDatabaseSettings.Value.DatabaseName);
            _categoriesCollection = mongoDatabase.GetCollection<Category>(categoriesDatabaseSettings.Value.CategoriesCollectionName);
        }

        public async Task<List<Category>> GetAsync() =>
            await _categoriesCollection.Find(c => true).ToListAsync();

        public async Task<Category?> GetAsync(string id) =>
            await _categoriesCollection.Find(c => c.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Category newCategory) =>
            await _categoriesCollection.InsertOneAsync(newCategory);

        public async Task UpdateAsync(string id, Category updatedCategory) =>
            await _categoriesCollection.ReplaceOneAsync(c => c.Id == id, updatedCategory);

        public async Task RemoveAsync(string id) =>
            await _categoriesCollection.DeleteOneAsync(c => c.Id == id);
    }
}
