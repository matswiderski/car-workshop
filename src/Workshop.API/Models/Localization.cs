using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.API.Models
{
    [Table("Localizations")]
    public class Localization
    {
        public string Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? WorkshopId { get; set; }
        public Workshop? Workshop { get; set; }
    }
}
