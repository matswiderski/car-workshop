using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.API.Models
{
    [Table("Services")]
    public class Service
    {
        public string Id { get; set; }
        public ICollection<WorkshopService> WorkshopServices { get; set; }
        public ICollection<RepairService> RepairServices { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
    }
}
