namespace Ecommerce.Domain.Cart;

public class CartItem
{
    public Guid Id { get; private set; }
    public Guid VariantId { get; private set; }
    public string ProductName { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    public CartItem(Guid variantId, string productName, decimal price, int quantity)
    {
        Id = Guid.NewGuid();
        VariantId = variantId;
        ProductName = productName;
        Price = price;
        Quantity = quantity;
    }
}