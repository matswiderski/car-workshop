namespace Workshop.API.Models
{
    public class RefreshToken
    {
        public string Id { get; set; }
        public WorkshopUser User { get; set; }
        public string Token { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsActive { get; set; }
    }
}
