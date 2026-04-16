using Ecommerce.Application.Carts.Interfaces;
using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Orders.Mappers;
using Ecommerce.Domain.Orders;
using MediatR;

namespace Ecommerce.Application.Orders.Commands.Checkout;

public class CheckoutHandler : IRequestHandler<CheckoutCommand, CheckoutResponse>
{
    private readonly ICartRepository _cartRepo;
    private readonly IOrderRepository _orderRepo;
    private readonly IPaymentService _paymentService;
    private readonly IUnitOfWork _uow;

    public CheckoutHandler(
        ICartRepository cartRepo,
        IOrderRepository orderRepo,
        IPaymentService paymentService,
        IUnitOfWork uow)
    {
        _cartRepo = cartRepo;
        _orderRepo = orderRepo;
        _paymentService = paymentService;
        _uow = uow;
    }

    public async Task<CheckoutResponse> Handle(CheckoutCommand request, CancellationToken ct)
    {
        var cart = await _cartRepo.GetByUserIdAsync(request.UserId);

        if (cart == null || !cart.Items.Any())
            throw new Exception("Cart is empty");

        var items = OrderMapper.ToOrderItems(cart);

        var order = Order.Create(request.UserId, items);

        order.StartPayment();

        await _orderRepo.AddAsync(order);
        await _uow.SaveChangesAsync(ct);


        var paymentUrl = _paymentService.CreatePaymentUrl(order, request.IpAddress);

        return new CheckoutResponse
        {
            OrderId = order.Id,
            PaymentUrl = paymentUrl
        };
    }
}