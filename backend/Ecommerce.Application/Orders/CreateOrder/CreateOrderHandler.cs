using Ecommerce.Application.Cart.Interfaces;
using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.Entities;
using MediatR;

namespace Ecommerce.Application.Orders.CreateOrder;

// public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
// {
//     private readonly ICartRepository _cartRepo;
//     private readonly IOrderRepository _orderRepo;
//
//     public CreateOrderHandler(ICartRepository cartRepo, IOrderRepository orderRepo)
//     {
//         _cartRepo = cartRepo;
//         _orderRepo = orderRepo;
//     }
//
//     public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
//     {
//         var cart = await _cartRepo.GetByUserIdAsync(request.UserId);
//
//         if (cart == null || !cart.Items.Any())
//             throw new Exception("Cart is empty");
//
//         var order = new Order(request.UserId);
//
//         foreach (var item in cart.Items)
//         {
//             order.AddItem(
//                 item.VariantId,
//                 item.ProductName,
//                 item.Price,
//                 item.Quantity
//             );
//         }
//
//         await _orderRepo.AddAsync(order);
//         
//         await _cartRepo.ClearAsync(request.UserId);
//
//         return order.Id;
//     }
// }