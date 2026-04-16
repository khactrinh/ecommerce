// namespace Ecommerce.Domain.Orders;
//
// public class OrderItem
// {
//     public Guid Id { get; private set; }
//     public Guid ProductId { get; private set; }
//
//     public string ProductName { get; private set; }
//     public decimal Price { get; private set; }
//     public int Quantity { get; private set; }
//
//     private OrderItem() { }
//
//     public OrderItem(Guid productId, string productName, decimal price, int quantity)
//     {
//         if (quantity <= 0)
//             throw new ArgumentException("Quantity must be > 0");
//
//         if (price < 0)
//             throw new ArgumentException("Price cannot be negative");
//
//         Id = Guid.NewGuid();
//         ProductId = productId;
//         ProductName = productName;
//         Price = price;
//         Quantity = quantity;
//     }
//
//     public decimal Total => Price * Quantity;
//
//     public void IncreaseQuantity(int amount)
//     {
//         if (amount <= 0)
//             throw new ArgumentException("Invalid amount");
//
//         Quantity += amount;
//     }
//
//     public void DecreaseQuantity(int amount)
//     {
//         if (amount <= 0 || Quantity - amount <= 0)
//             throw new ArgumentException("Invalid decrease");
//
//         Quantity -= amount;
//     }
// }

public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    private OrderItem() { }

    public OrderItem(Guid productId, decimal price, int quantity)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("Invalid productId");

        if (price < 0)
            throw new ArgumentException("Price cannot be negative");

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be > 0");

        Id = Guid.NewGuid();
        ProductId = productId;
        Price = price;
        Quantity = quantity;
    }

    public decimal Total => Price * Quantity;
}