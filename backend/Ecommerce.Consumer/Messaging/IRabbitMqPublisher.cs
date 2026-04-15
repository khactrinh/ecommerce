namespace Ecommerce.Consumer.Messaging;

public interface IRabbitMqPublisher
{
    Task PublishAsync(string eventType, object data);
}