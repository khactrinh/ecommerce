namespace Ecommerce.Application.Interfaces;

public interface IMessageBus
{
    Task PublishAsync(string topic, object message);
}