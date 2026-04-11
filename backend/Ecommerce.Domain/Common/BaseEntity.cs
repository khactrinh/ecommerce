namespace Ecommerce.Domain.Common;

public abstract class BaseEntity
{
    private readonly List<BaseDomainEvent> _domainEvents = new();

    public IReadOnlyCollection<BaseDomainEvent> DomainEvents => _domainEvents;

    protected void AddDomainEvent(BaseDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}