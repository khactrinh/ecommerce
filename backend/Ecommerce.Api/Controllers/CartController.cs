using Ecommerce.Api.Extensions;
using Ecommerce.API.Models.Cart;
using Ecommerce.Application.Carts.Commands.AddToCart;
using Ecommerce.Application.Carts.Commands.RemoveItem;
using Ecommerce.Application.Carts.Commands.UpdateQuantity;
using Ecommerce.Application.Carts.Queries.GetCart;
using Ecommerce.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Authorize]
[ApiController]
[Route("api/cart")]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> AddToCart(AddToCartRequest request)
    {
        var userId = User.GetUserId();
        
        await _mediator.Send(new AddToCartCommand(userId, request.ProductId, request.Quantity));
        
        return Ok(ApiResponse<string>.Ok("Item added to cart"));
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var userId = User.GetUserId();
        
        var result = await _mediator.Send(new GetCartQuery(userId));
        
        return Ok(ApiResponse<List<CartResponse>>.Ok(result));
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateQuantity(AddToCartRequest request)
    {
        var userId = User.GetUserId();
        
        await _mediator.Send(new UpdateQuantityCommand(userId, request.ProductId, request.Quantity));
        
        return Ok(ApiResponse<string>.Ok("Quantity updated"));
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemoveItem([FromBody] RemoveItemRequest request)
    {
        var userId = User.GetUserId();
        
        await _mediator.Send(new RemoveItemCommand(userId, request.ProductId));
        
        return Ok(ApiResponse<string>.Ok("Item removed from cart"));
    }
}

public record RemoveItemRequest(Guid ProductId);