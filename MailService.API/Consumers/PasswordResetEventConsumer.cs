using MailService.API.Models;
using MailService.API.Services.Interfaces;
using MassTransit;
using Shared.Contracts.Events.Users;

namespace MailService.API.Consumers
{
    public class PasswordResetEventConsumer : IConsumer<PasswordResetEvent>
    {
        private readonly ISmtpMailService _mailService;

        public PasswordResetEventConsumer(ISmtpMailService mailService)
        {
            _mailService = mailService;
        }
        public async Task Consume(ConsumeContext<PasswordResetEvent> context)
        {
            var mailRequest = new MailRequestDto
            {
                To = context.Message.Email,
                Subject = "Şifre Sıfırlama",
                Body = $"Şifre sıfırlamak için bağlantıya tıklayın: {context.Message.ResetLink}"
            };

            await _mailService.SendMailAsync(mailRequest);
        }
    }
}
