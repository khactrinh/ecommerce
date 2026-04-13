using Dapper;
using Ecommerce.Application.Catalog.Products.Queries.GetProductById;
using Ecommerce.Application.Common.Interfaces;

namespace Ecommerce.Infrastructure.ReadModels.Products;

public class GetProductByIdQueryService :  IGetProductByIdQueryService
{
    private readonly IDbConnectionFactory _connectionFactory;

    public GetProductByIdQueryService(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<ProductDetailDto?> Execute(Guid id)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
            SELECT 
                id,
                name,
                price,
                stock,
                image_url AS ImageUrl,
                category_id AS CategoryId
            FROM products
            WHERE id = @Id
        ";

        return await connection.QueryFirstOrDefaultAsync<ProductDetailDto>(
            sql,
            new { Id = id }
        );
    }
}