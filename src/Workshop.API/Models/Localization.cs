using System.ComponentModel.DataAnnotations.Schema;

namespace Workshop.API.Models
{
    [Table("Localizations")]
    public class Localization
    {
        public string Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string City { get; set; }
        public string BusinessUserId { get; set; }
        public BusinessUser BusinessUser { get; set; }
    }
}
