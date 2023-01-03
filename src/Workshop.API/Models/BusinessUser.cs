using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.API.Models
{
    [Table("BusinessUsers")]
    public class BusinessUser : WorkshopUser
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<Workshop>? Workshops { get; set; }
    }
}
