using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Carts.Interfaces;
using Ecommerce.Application.Interfaces;
using MediatR;

namespace Ecommerce.Application.Carts.Commands.AddToCart;

public class AddToCartHandler : IRequestHandler<AddToCartCommand>
{
    private readonly IProductQueryService _productQueryService;
    private readonly ICartRepository _cartRepo;
    private readonly IUnitOfWork _uow;

    public AddToCartHandler(
        IProductQueryService productQueryService,
        ICartRepository cartRepo,
        IUnitOfWork uow)
    {
        _productQueryService = productQueryService;
        _cartRepo = cartRepo;
        _uow = uow;
    }

    public async Task Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        // =========================
        // 1. Validate input
        // =========================
        if (request.Quantity <= 0)
            throw new Exception("Quantity must be greater than 0");

        // =========================
        // 2. Lấy product (READ - Dapper)
        // =========================
        var product = await _productQueryService.GetById(request.ProductId);

        if (product == null)
            throw new Exception("Product not found");

        // =========================
        // 3. Optional: check stock
        // =========================
        if (product.Stock < request.Quantity)
            throw new Exception("Not enough stock");

        // =========================
        // 4. Lấy cart (WRITE - Repository)
        // =========================
        var cart = await _cartRepo.GetByUserIdAsync(request.UserId);

        // =========================
        // 5. Nếu chưa có cart → tạo mới
        // =========================
        if (cart == null)
        {
            cart = new Domain.Cart.Cart(request.UserId);
            await _cartRepo.Add(cart);
        }

        // =========================
        // 6. Add item (Domain logic)
        // =========================
        cart.AddItem(
            product.Id,
            product.Price,
            request.Quantity
        );

        // =========================
        // 7. Save
        // =========================
        await _uow.SaveChangesAsync();
    }
}