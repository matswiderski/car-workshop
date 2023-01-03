using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.API.Models
{
    [Table("Workshops")]
    public class Workshop
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public BusinessUser Owner { get; set; }
        public string LocalizationId { get; set; }
        public Localization Localization { get; set; }
        public ICollection<Repair> Repairs { get; set; }
        public ICollection<WorkshopService> WorkshopServices { get; set; }
    }
}
