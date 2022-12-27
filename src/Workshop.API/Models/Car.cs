using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.API.Models
{
    [Table("Cars")]
    public class Car
    {
        public string Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public int ProductionYear { get; set; }
        public string PersonalUserId { get; set; }
        public PersonalUser PersonalUser { get; set; }
    }
}
