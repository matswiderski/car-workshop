using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Logging;
using Workshop.API.Settings;

namespace Workshop.API.Services
{
    public class MailService : IMailService
    {
        private readonly IOptions<MailServiceSettings> _mailSenderSettings;
        private readonly ILogger _logger;
        public MailService(IOptions<MailServiceSettings> mailSenderSettings, ILogger<MailService> logger)
        {
            _mailSenderSettings = mailSenderSettings;
            _logger = logger;
        }

        public async Task SendAsync(string from, string to, string subject, string body)
        {
            MailMessage message = new(from, to, subject, body);
            SmtpClient client = new(_mailSenderSettings.Value.Host, _mailSenderSettings.Value.Port);
            client.Credentials = new NetworkCredential(_mailSenderSettings.Value.Login, _mailSenderSettings.Value.Password);
            client.EnableSsl = true;
            try
            {
                await client.SendMailAsync(message);
                throw new Exception();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception caught while sending e-mail: {ex.Message}",
                    DateTime.UtcNow.ToLongTimeString());
            }
            finally
            {
                client.Dispose();
            }
        }
    }
}
