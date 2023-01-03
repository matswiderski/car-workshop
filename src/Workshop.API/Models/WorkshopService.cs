namespace Workshop.API.Models
{
    public class WorkshopService
    {
        public string WorkshopId { get; set; }
        public Workshop Workshop { get; set; }
        public string ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
