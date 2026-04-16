namespace Ecommerce.Domain.Cart;

// public class Cart
// {
//     private readonly List<CartItem> _items = new();
//
//     public Guid Id { get; private set; }
//     public Guid UserId { get; private set; }
//
//     public IReadOnlyCollection<CartItem> Items => _items;
//
//     public Cart(Guid userId)
//     {
//         Id = Guid.NewGuid();
//         UserId = userId;
//     }
//
//     public void AddItem(Guid variantId, string productName, decimal price, int quantity)
//     {
//         _items.Add(new CartItem(variantId, productName, price, quantity));
//     }
//
//     public void Clear()
//     {
//         _items.Clear();
//     }
// }

public class Cart
{
    public Guid UserId { get; private set; }

    private readonly List<CartItem> _items = new();
    public IReadOnlyCollection<CartItem> Items => _items;

    public Cart(Guid userId)
    {
        UserId = userId;
    }

    public void AddItem(Guid productId, decimal price, int quantity)
    {
        var existing = _items.FirstOrDefault(x => x.ProductId == productId);

        if (existing != null)
        {
            existing.Increase(quantity);
        }
        else
        {
            _items.Add(new CartItem(productId, price, quantity));
        }
    }

    public void UpdateQuantity(Guid productId, int quantity)
    {
        var existing = _items.FirstOrDefault(x => x.ProductId == productId);
        if (existing != null)
        {
            existing.SetQuantity(quantity);
        }
    }

    public void RemoveItem(Guid productId)
    {
        var existing = _items.FirstOrDefault(x => x.ProductId == productId);
        if (existing != null)
        {
            _items.Remove(existing);
        }
    }
}