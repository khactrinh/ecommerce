using Dapper;
using MediatR;
using Ecommerce.Application.Common.Interfaces;

namespace Ecommerce.Application.Catalog.GetProductById;

public class GetProductByIdHandler 
    : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public GetProductByIdHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<ProductDto?> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
            SELECT id, name, price, stock, image_url
            FROM products
            WHERE id = @Id
            LIMIT 1;
        ";

        var product = await connection.QueryFirstOrDefaultAsync<ProductDto>(
            sql,
            new { request.Id }
        );

        return product;
    }
}