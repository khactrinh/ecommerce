namespace Ecommerce.Domain.Common;

public interface IHasDomainEvents
{
    IReadOnlyCollection<BaseDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}