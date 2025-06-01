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
        _channel.QueueDeclare(queue: nameof(PasswordResetEvent), durable: false, exclusive: false, autoDelete: false);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var messageJson = Encoding.UTF8.GetString(body);
            var mailEvent = JsonSerializer.Deserialize<PasswordResetEvent>(messageJson);

            await SendMailAsync(mailEvent);
        };

        _channel.BasicConsume(queue: nameof(PasswordResetEvent), autoAck: true, consumer: consumer);
        return Task.CompletedTask;
    }
    private Task SendMailAsync(PasswordResetEvent mail)
    {
        Console.WriteLine($"📧 Mail Gönderiliyor: {mail.To} -> {mail.Subject}");
      
        return Task.CompletedTask;
    }
}
