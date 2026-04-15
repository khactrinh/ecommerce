namespace Ecommerce.Domain.Events;

public class ProductCreatedIntegrationEvent
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
}