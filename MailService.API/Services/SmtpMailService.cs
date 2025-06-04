using MailService.API.Models;
using System.Net.Mail;
using System.Net;
using MailService.API.Services.Interfaces;

namespace MailService.API.Services
{
    public class SmtpMailService : ISmtpMailService
    {
        private readonly IConfiguration _config;

        public SmtpMailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendMailAsync(MailRequestDto mail)
        {
            using var smtp = new SmtpClient();
               smtp.Host = _config["Smtp:Host"];
             smtp.Port = int.Parse(_config["Smtp:Port"]);
             smtp.Credentials = new NetworkCredential(_config["Smtp:Username"], _config["Smtp:Password"]);
            smtp.EnableSsl = true;

            var message = new MailMessage(_config["Smtp:Username"], mail.To, mail.Subject, mail.Body);
            message.IsBodyHtml = true;

              await smtp.SendMailAsync(message);
        }
    }

}
