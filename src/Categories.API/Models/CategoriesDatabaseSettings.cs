namespace Categories.API.Models
{
    public class CategoriesDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CategoriesCollectionName { get; set; } = null!;
    }
}
