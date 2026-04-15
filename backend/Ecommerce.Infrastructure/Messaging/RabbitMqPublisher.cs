using System.Text;
using System.Text.Json;
using Ecommerce.Application.Common.Messaging;
using RabbitMQ.Client;

namespace Ecommerce.Infrastructure.Messaging;

public class RabbitMqPublisher : IRabbitMqPublisher, IAsyncDisposable
{
    private readonly ConnectionFactory _factory;

    private IConnection? _connection;
    private IChannel? _channel;

    public RabbitMqPublisher()
    {
        _factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
    }

    // 🔥 Lazy init
    private async Task EnsureConnectedAsync()
    {
        if (_connection != null && _channel != null)
            return;

        _connection = await _factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        await _channel.ExchangeDeclareAsync(
            exchange: "ecommerce.events",
            type: ExchangeType.Topic,
            durable: true
        );
    }

    // 🔥 IMPLEMENT INTERFACE (QUAN TRỌNG)
    public async Task PublishAsync(string eventType, object data)
    {
        await EnsureConnectedAsync();

        var envelope = new
        {
            MessageId = Guid.NewGuid(),
            Type = eventType,
            OccurredOn = DateTime.UtcNow,
            CorrelationId = Guid.NewGuid().ToString(),
            Data = data
        };

        var body = Encoding.UTF8.GetBytes(
            JsonSerializer.Serialize(envelope)
        );

        var properties = new BasicProperties
        {
            Persistent = true
        };

        await _channel!.BasicPublishAsync(
            exchange: "ecommerce.events",
            routingKey: eventType, // 🔥 routing theo type
            mandatory: false,
            basicProperties: properties,
            body: body
        );
    }

    public async ValueTask DisposeAsync()
    {
        if (_channel != null)
            await _channel.CloseAsync();

        if (_connection != null)
            await _connection.CloseAsync();
    }
}