using MediatR;

namespace Ecommerce.Domain.Common;

public abstract class BaseDomainEvent : INotification
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}