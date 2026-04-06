namespace Ecommerce.Domain.Cart;

public class Cart
{
    private readonly List<CartItem> _items = new();

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }

    public IReadOnlyCollection<CartItem> Items => _items;

    public Cart(Guid userId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
    }

    public void AddItem(Guid variantId, string productName, decimal price, int quantity)
    {
        _items.Add(new CartItem(variantId, productName, price, quantity));
    }

    public void Clear()
    {
        _items.Clear();
    }
}