using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Events;

public class ProductCreatedEvent : BaseDomainEvent
{
    public Guid ProductId { get; }

    public ProductCreatedEvent(Guid productId)
    {
        ProductId = productId;
    }
}