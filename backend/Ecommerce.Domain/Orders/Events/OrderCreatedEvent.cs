using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Events;

public class OrderCreatedEvent : BaseDomainEvent
{
    public Guid OrderId { get; }

    public OrderCreatedEvent(Guid orderId)
    {
        OrderId = orderId;
    }
}