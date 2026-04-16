using Dapper;
using Ecommerce.Application.Common.Interfaces;
using MediatR;

namespace Ecommerce.Application.Carts.Queries.GetCart;

public class GetCartHandler : IRequestHandler<GetCartQuery, List<CartResponse>>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public GetCartHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<List<CartResponse>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
            SELECT 
                ci.product_id AS ProductId,
                p.name AS ProductName,
                ci.price AS Price,
                ci.quantity AS Quantity,
                p.image_url AS ImageUrl
            FROM cart_items ci
            JOIN products p ON ci.product_id = p.id
            WHERE ci.cart_user_id = @UserId";

        var items = await connection.QueryAsync<CartResponse>(sql, new { UserId = request.UserId });

        return items.ToList();
    }
}
