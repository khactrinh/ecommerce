using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Catalog.Products.Events;

public class ProductCreatedEvent : BaseDomainEvent
{
    public Guid ProductId { get; }
    public string Name { get; }
    public DateTime OccurredOn { get; }

    public ProductCreatedEvent(Guid productId, string name)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("ProductId cannot be empty"); // 🔥 GUARD

        ProductId = productId;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        OccurredOn = DateTime.UtcNow;
    }
}