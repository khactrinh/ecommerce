namespace Ecommerce.Domain.ValueObjects;

public class OrderItem
{
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; } = default!;
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    private OrderItem() { }

    public OrderItem(Guid productId, string name, decimal price, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Invalid quantity");

        ProductId = productId;
        ProductName = name;
        Price = price;
        Quantity = quantity;
    }

    public decimal GetTotal() => Price * Quantity;
}