using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Ecommerce.Consumer.Messaging;

public class RabbitMqPublisher : IRabbitMqPublisher
{
    private readonly IChannel _channel;

    public RabbitMqPublisher(IChannel channel)
    {
        _channel = channel;
    }

    public async Task PublishAsync(string eventType, object data)
    {
        var envelope = new
        {
            MessageId = Guid.NewGuid(),
            Type = eventType,
            OccurredOn = DateTime.UtcNow,
            CorrelationId = Guid.NewGuid().ToString(),
            Data = data
        };

        var json = JsonSerializer.Serialize(envelope);
        var body = Encoding.UTF8.GetBytes(json);

        await _channel.BasicPublishAsync(
            exchange: "",
            routingKey: "ecommerce.consumer",
            mandatory: false,
            basicProperties: new BasicProperties { Persistent = true },
            body: body
        );
    }
}