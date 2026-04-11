using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Events;

public class ProductStockDecreasedEvent : BaseDomainEvent
{
    public Guid ProductId { get; }
    public int Quantity { get; }

    public ProductStockDecreasedEvent(Guid productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }
}