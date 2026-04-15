using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Events;

public class OrderPaidEvent : BaseDomainEvent
{
    public Guid OrderId { get; }

    public OrderPaidEvent(Guid orderId)
    {
        OrderId = orderId;
    }
}