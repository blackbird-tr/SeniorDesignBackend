using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;

namespace AccountService.Infrastructure.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string htmlContent);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;
        private readonly string _fromEmail;
        private readonly string _fromName;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiKey = _configuration["SendGrid:ApiKey"];
            _fromEmail = _configuration["SendGrid:FromEmail"];
            _fromName = _configuration["SendGrid:FromName"];
        }

        public async Task SendEmailAsync(string to, string subject, string htmlContent)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_fromEmail, _fromName);
            var toAddress = new EmailAddress(to);
            var msg = MailHelper.CreateSingleEmail(from, toAddress, subject, null, htmlContent);
            var response = await client.SendEmailAsync(msg);
            
            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new Exception($"Failed to send email. Status code: {response.StatusCode}");
            }
        }
    }
} 