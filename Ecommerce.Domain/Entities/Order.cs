namespace Ecommerce.Domain.Entities;

public class Order
{
    private readonly List<OrderItem> _items = new();

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Status { get; private set; }
    public decimal TotalAmount { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items;

    public Order(Guid userId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Status = "Pending";
    }

    public void AddItem(Guid variantId, string productName, decimal price, int quantity)
    {
        if (quantity <= 0)
            throw new Exception("Invalid quantity");

        _items.Add(new OrderItem(variantId, productName, price, quantity));
        TotalAmount += price * quantity;
    }

    public void MarkAsPaid()
    {
        Status = "Paid";
    }
}