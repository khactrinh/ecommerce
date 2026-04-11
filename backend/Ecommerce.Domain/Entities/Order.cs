using Ecommerce.Domain.Common;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.Events;

namespace Ecommerce.Domain.Entities;

public class Order : BaseEntity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }

    public OrderStatus Status { get; private set; }

    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items;

    public decimal TotalAmount { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private Order() { }

    private Order(Guid userId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Status = OrderStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    // 🔥 CREATE ORDER
    public static Order Create(Guid userId, List<OrderItem> items)
    {
        if (items == null || !items.Any())
            throw new ArgumentException("Order must have items");

        var order = new Order(userId);

        foreach (var item in items)
            order._items.Add(item);

        order.RecalculateTotal();

        order.AddDomainEvent(new OrderCreatedEvent(order.Id));

        return order;
    }

    // 🔥 BUSINESS METHODS

    public void StartPayment()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Invalid state");

        Status = OrderStatus.PaymentProcessing;
    }

    public void MarkAsPaid()
    {
        if (Status != OrderStatus.PaymentProcessing)
            throw new InvalidOperationException("Invalid state");

        Status = OrderStatus.Paid;

        AddDomainEvent(new OrderPaidEvent(Id));
    }

    public void FailPayment()
    {
        if (Status != OrderStatus.PaymentProcessing)
            throw new InvalidOperationException("Invalid state");

        Status = OrderStatus.Failed;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Shipped || Status == OrderStatus.Completed)
            throw new InvalidOperationException("Cannot cancel");

        Status = OrderStatus.Cancelled;

        //AddDomainEvent(new OrderCancelledEvent(Id));
    }

    public void Ship()
    {
        if (Status != OrderStatus.Paid)
            throw new InvalidOperationException("Order not paid");

        Status = OrderStatus.Shipped;
    }

    public void Complete()
    {
        if (Status != OrderStatus.Shipped)
            throw new InvalidOperationException("Not shipped");

        Status = OrderStatus.Completed;
    }

    private void RecalculateTotal()
    {
        //TotalAmount = _items.Sum(x => x.GetTotal());
    }
}