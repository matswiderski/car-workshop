using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Categories.API.Models
{
    public class Category
    {
        [BsonId]
        public string Id { get; set; }
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "Category name can't be longer than 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Category description is required")]
        [StringLength(200, ErrorMessage = "Category description can't be longer than 200 characters")]
        public string Description { get; set; }
        public ICollection<string> Parts { get; set; }
    }
}
