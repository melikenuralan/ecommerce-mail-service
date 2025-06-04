using MailService.API.Models;

namespace MailService.API.Services.Interfaces
{
    public interface ISmtpMailService
    {
        Task SendMailAsync(MailRequestDto mail);
    }

}
