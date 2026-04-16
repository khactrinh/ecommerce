// using Ecommerce.Domain.Cart;
// using Ecommerce.Domain.Orders;
//
// namespace  Ecommerce.Application.Orders.Mappers;
//
// public static class OrderMapper
// {
//     public static List<OrderItem> ToOrderItems(Domain.Cart.Cart cart)
//     {
//         if (cart == null || !cart.Items.Any())
//             throw new ArgumentException("Cart is empty");
//
//         return cart.Items.Select(MapItem).ToList();
//     }
//
//     private static OrderItem MapItem(CartItem x)
//     {
//         return new OrderItem(
//             x.VariantId,
//             x.ProductName,
//             x.Price,
//             x.Quantity
//         );
//     }
// }

using Ecommerce.Domain.Cart;

namespace Ecommerce.Application.Orders.Mappers;

public static class OrderMapper
{
    public static List<OrderItem> ToOrderItems(Cart cart)
    {
        if (cart == null || !cart.Items.Any())
            throw new InvalidOperationException("Cart is empty");

        return cart.Items.Select(MapItem).ToList();
    }

    private static OrderItem MapItem(CartItem x)
    {
        return new OrderItem(
            x.ProductId,
            x.Price,
            x.Quantity
        );
    }
}