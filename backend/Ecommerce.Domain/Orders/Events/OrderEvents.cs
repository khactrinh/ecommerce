namespace Ecommerce.Domain.Orders.Events;

public record OrderCreatedEvent(Guid OrderId, decimal Amount);

public record PaymentSucceededEvent(Guid OrderId);
public record PaymentFailedEvent(Guid OrderId);

public record InventoryReservedEvent(Guid OrderId);
public record InventoryFailedEvent(Guid OrderId);

public record OrderCompletedEvent(Guid OrderId);
public record OrderCancelledEvent(Guid OrderId);