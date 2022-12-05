using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.API.Models
{
    [Table("BusinessUsers")]
    public class BusinessUser : WorkshopUser
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? OwnerFirstName { get; set; }
        public string? OwnerLastName { get; set; }
        public Localization? Localization { get; set; }
    }
}
