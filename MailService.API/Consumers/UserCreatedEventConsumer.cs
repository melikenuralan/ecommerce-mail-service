using MailService.API.Models;
using MailService.API.Services.Interfaces;
using MassTransit;
using Shared.Contracts.Events.Users;

namespace MailService.API.Consumers
{
    public class UserCreatedEventConsumer : IConsumer<UserCreatedEvent>
    {
        private readonly ISmtpMailService _smtpMailService;
        public UserCreatedEventConsumer(ISmtpMailService smtpMailService)
        {
            _smtpMailService = smtpMailService;
        }

        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            var message = context.Message;

            var mail = new MailRequestDto
            {
                To = message.Email,
                Subject = $"Hoş geldiniz, {message.UserName}",
                Body = "Hesabınızı başarıyla oluşturdunuz!"
            };

            await _smtpMailService.SendMailAsync(mail);
        }
    }
}
