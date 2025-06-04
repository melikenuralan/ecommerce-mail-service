using MailService.API.Services.Interfaces;
using MailService.API.Services;
using MassTransit;
using MailService.API.Consumers;

namespace MailService.API.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ISmtpMailService, SmtpMailService>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<PasswordResetEventConsumer>();
                x.AddConsumer<UserCreatedEventConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost");

                    cfg.ReceiveEndpoint("PasswordResetEvent", e =>
                    {
                        e.ConfigureConsumer<PasswordResetEventConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("UserCreatedEvent", e =>
                    {
                        e.ConfigureConsumer<UserCreatedEventConsumer>(context);
                    });
                });
            });

            return services;
        }
    }
}
