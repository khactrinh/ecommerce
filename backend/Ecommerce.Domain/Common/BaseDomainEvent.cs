namespace Ecommerce.Domain.Common;

public abstract class BaseDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}