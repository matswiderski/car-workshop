namespace Workshop.API.Services
{
    public interface IMailService
    {
        Task SendAsync(string from, string to, string subject, string body);
    }
}