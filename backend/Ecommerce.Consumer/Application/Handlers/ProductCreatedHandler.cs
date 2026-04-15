using System.Text.Json;
using Ecommerce.Consumer.Application.Interfaces;
using Ecommerce.Domain.Catalog.Products.Events;

namespace Ecommerce.Consumer.Application.Handlers;

public class ProductCreatedHandler : IEventHandler
{
    public string EventType => "ProductCreatedEvent";

    public async Task HandleAsync(JsonElement data)
    {
        var evt = JsonSerializer.Deserialize<ProductCreatedEvent>(
            data.GetRawText()
        )!;

        Console.WriteLine($"🔥 Product created: {evt.ProductId}");

        await Task.CompletedTask;
    }
}