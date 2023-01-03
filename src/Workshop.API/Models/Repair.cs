using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.API.Models
{
    [Table("Repairs")]
    public class Repair
    {
        public string Id { get; set; }
        public string PersonalUserId { get; set; }
        public PersonalUser PersonalUser { get; set; }
        public string CarId { get; set; }
        public Car Car { get; set; }
        public string WorkshopId { get; set; }
        public Workshop Workshop { get; set; }
        public string? Message { get; set; }
        public ICollection<RepairService> RepairServices { get; set; }
    }
}
