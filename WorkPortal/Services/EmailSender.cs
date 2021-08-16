using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace WorkPortal.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration configuration;

        public EmailSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            return this.Execute(apiKey, email, subject, htmlMessage);
        }
        
        private Task Execute(string apiKey, string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(apiKey);
            var message = new SendGridMessage()
            {
                From = new EmailAddress("dtsonev91@abv.bg", "WP Administration"),
                Subject = subject,
                PlainTextContent = htmlMessage,
                HtmlContent = htmlMessage,
            };

            message.AddTo(new EmailAddress(email));
            message.SetClickTracking(false, false);
            return client.SendEmailAsync(message);
        }
    }
}
