using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Events;

public class ProductPriceUpdatedEvent : BaseDomainEvent
{
    public Guid ProductId { get; }
    public decimal NewPrice { get; }

    public ProductPriceUpdatedEvent(Guid productId, decimal newPrice)
    {
        ProductId = productId;
        NewPrice = newPrice;
    }
}