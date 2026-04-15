using System.Text.Json;

namespace Ecommerce.Consumer.Application.Interfaces;

public interface IEventHandler
{
    string EventType { get; }
    Task HandleAsync(JsonElement data);
}