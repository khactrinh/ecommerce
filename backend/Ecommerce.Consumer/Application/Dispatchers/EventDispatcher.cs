using System.Text.Json;
using Ecommerce.Consumer.Application.Interfaces;

namespace Ecommerce.Consumer.Application.Dispatchers;

public class EventDispatcher
{
    private readonly IEnumerable<IEventHandler> _handlers;

    public EventDispatcher(IEnumerable<IEventHandler> handlers)
    {
        _handlers = handlers;
    }

    public async Task DispatchAsync(string type, JsonElement data)
    {
        var handler = _handlers.FirstOrDefault(x => x.EventType == type);

        if (handler == null)
            throw new Exception($"No handler for event {type}");

        await handler.HandleAsync(data);
    }
}