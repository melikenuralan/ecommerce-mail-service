using MailService.API.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

public class RabbitMQConsumerService : BackgroundService
{
    private readonly IModel _channel;
    private readonly IConnection _connection;

    public RabbitMQConsumerService()
    {
        var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: nameof(UserCreatedEvent),
            durable: false,
            exclusive: false,
            autoDelete: false
        );
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            var userCreated = JsonSerializer.Deserialize<UserCreatedEvent>(json);

            await SendWelcomeMailAsync(userCreated);
        };

        _channel.BasicConsume(
            queue: nameof(UserCreatedEvent),
            autoAck: true,
            consumer: consumer
        );

        return Task.CompletedTask;
    }

    private Task SendWelcomeMailAsync(UserCreatedEvent mail)
    {
        Console.WriteLine($"📧 Hoş Geldin Maili Gönderildi: {mail.Email}");
        Console.WriteLine($"Konu: Hoş Geldiniz, {mail.UserName}!");
        Console.WriteLine("İçerik: Hesabınızı başarıyla oluşturdunuz.");
        return Task.CompletedTask;
    }
}
