using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Boilerplate.Common.Helpers.EmailHelper

{
    public class EmailClient : IEmailClient
    {
        public EmailSettings _emailSettings { get; }
        public ILogger<EmailClient> _logger { get; } 

        public EmailClient(IOptions<EmailSettings> mailSettings, ILogger<EmailClient> logger)
        {
            _emailSettings = mailSettings.Value;
            _logger = logger;
        }
        //public async Task<bool> SendEmail(Email email)
        //{
        //    MailMessage message = new MailMessage(_emailSettings.FromAddress, email.To);
        //    message.Subject = email.Subject;
        //    message.IsBodyHtml = true;
        //    message.Body = email.Body;

        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Host = "smtp.gmail.com";
        //    smtp.EnableSsl = true;

        //    smtp.UseDefaultCredentials = false;
        //    NetworkCredential cred = new NetworkCredential(_emailSettings.FromAddress, _emailSettings.Password);
        //    smtp.Credentials = cred;
        //    smtp.Port = 587;
        //    smtp.Send(message);

        //    return true;
        //}
        public async Task<bool> SendEmail(Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;

            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendGridMessage);

            _logger.LogInformation("Email sent");

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            _logger.LogError("Email sending failed");

            return false;
        }
    }
}
