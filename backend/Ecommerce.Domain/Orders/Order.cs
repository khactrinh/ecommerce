using Ecommerce.Domain.Common;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.Events;

namespace Ecommerce.Domain.Orders;

public class Order : BaseEntity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }

    public OrderStatus Status { get; private set; }
    
    public PaymentStatus PaymentStatus { get; private set; }

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

        PaymentStatus = PaymentStatus.Processing;
    }

    public void MarkAsPaid()
    {
        if (PaymentStatus != PaymentStatus.Processing)
            throw new InvalidOperationException("Invalid state");

        PaymentStatus = PaymentStatus.Paid;

        // 🔥 QUAN TRỌNG: sync với order flow
        if (Status == OrderStatus.Pending)
        {
            ConfirmOrder();
        }

        AddDomainEvent(new OrderPaidEvent(Id));
    }
    
    private void ConfirmOrder()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException();

        Status = OrderStatus.Confirmed;
    }

    public void FailPayment()
    {
        if (PaymentStatus != PaymentStatus.Processing)
            throw new InvalidOperationException("Invalid state");

        PaymentStatus = PaymentStatus.Failed;
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
        if (PaymentStatus != PaymentStatus.Paid)
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
        TotalAmount = _items.Sum(x => x.Total);

        if (TotalAmount <= 0)
            throw new InvalidOperationException("Invalid order total");
    }
}