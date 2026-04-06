using Ecommerce.Application.Catalog.Products.DTOs;
using Ecommerce.Application.Catalog.Products.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Infrastructure.Persistence.ReadServices;

using Dapper;
using Npgsql;
using System.Data;

public class ProductReadService : IProductReadService
{
    private readonly string _connectionString;

    public ProductReadService(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")!;
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync(int page, int pageSize)
    {
        using IDbConnection conn = new NpgsqlConnection(_connectionString);

        var sql = @"
            SELECT id, name, price
            FROM ""Products""
            ORDER BY name
            OFFSET @Offset LIMIT @Limit
        ";

        return await conn.QueryAsync<ProductDto>(sql, new
        {
            Offset = (page - 1) * pageSize,
            Limit = pageSize
        });
    }
}