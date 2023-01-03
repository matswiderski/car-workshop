namespace Workshop.API.Models
{
    public class RepairService
    {
        public string RepairId { get; set; }
        public Repair Repair { get; set; }
        public string ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
