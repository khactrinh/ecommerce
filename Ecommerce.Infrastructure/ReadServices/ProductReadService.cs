using Dapper;
using Ecommerce.Application.Catalog.GetProducts;
using Ecommerce.Application.Common.Interfaces;

namespace Ecommerce.Infrastructure.ReadServices;

public class ProductReadService : IProductReadService
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ProductReadService(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<(IEnumerable<ProductDto> Items, int Total)> GetProductsAsync(
        int page,
        int pageSize,
        string? search)
    {
        using var connection = _connectionFactory.CreateConnection();
        //await connection.OpenAsync();

        var offset = (page - 1) * pageSize;

        // dynamic filter
        var whereClause = "";
        if (!string.IsNullOrWhiteSpace(search))
        {
            whereClause = "WHERE name ILIKE @Search";
        }

        var sql = $@"
            SELECT id, name, price, stock
            FROM products
            {whereClause}
            ORDER BY name
            OFFSET @Offset LIMIT @Limit;

            SELECT COUNT(*) FROM products {whereClause};
        ";

        using var multi = await connection.QueryMultipleAsync(sql, new
        {
            Offset = offset,
            Limit = pageSize,
            Search = $"%{search}%"
        });

        var items = await multi.ReadAsync<ProductDto>();
        var total = await multi.ReadFirstAsync<int>();

        return (items, total);
    }
}