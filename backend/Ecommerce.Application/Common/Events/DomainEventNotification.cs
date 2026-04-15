using MediatR;
using Ecommerce.Domain.Common;

namespace Ecommerce.Application.Common.Events;

public class DomainEventNotification<TDomainEvent> : INotification
    where TDomainEvent : BaseDomainEvent
{
    public TDomainEvent DomainEvent { get; }

    public DomainEventNotification(TDomainEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }
}