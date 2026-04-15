namespace Ecommerce.Application.Common.Messaging;

public interface IRabbitMqPublisher
{
    Task PublishAsync(string eventType, object data);
}