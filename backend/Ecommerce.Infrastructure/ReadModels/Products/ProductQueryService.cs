// using Dapper;
// using Ecommerce.Application.Catalog.GetProducts;
// using Ecommerce.Application.Catalog.Products.Queries.GetProductById;
// using Ecommerce.Application.Catalog.Products.Queries.GetProducts;
// using Ecommerce.Application.Common.Interfaces;
//
// namespace Ecommerce.Infrastructure.ReadModels.Products;
//
// public class ProductQueryService : IProductQueryService
// {
//     private readonly IDbConnectionFactory _connectionFactory;
//     private readonly ICategoryQueryService _categoryService;
//     private readonly ICacheService _cache;
//
//     public ProductQueryService(IDbConnectionFactory connectionFactory,  ICategoryQueryService categoryService, ICacheService cache)
//     {
//         _connectionFactory = connectionFactory;
//         _categoryService = categoryService;
//         _cache = cache;
//     }
//
//     public async Task<(IEnumerable<ProductListItemDto> Items, int Total)> GetProducts(ProductFilter filter)
//     {
//         var cacheKey = BuildCacheKey(filter);
//
//         var cached = await _cache.GetAsync<(List<ProductListItemDto>, int)?>(cacheKey);
//         if (cached.HasValue)
//             return cached.Value;
//
//         using var connection = _connectionFactory.CreateConnection();
//
//         var offset = (filter.Page - 1) * filter.PageSize;
//
//         var where = new List<string> { "p.is_deleted = false" };
//         var param = new DynamicParameters();
//
//         // Search
//         if (!string.IsNullOrWhiteSpace(filter.Search))
//         {
//             where.Add("p.name ILIKE @Search");
//             param.Add("Search", $"%{filter.Search}%");
//         }
//
//         // 🌳 Category Tree
//         if (filter.CategoryId.HasValue)
//         {
//             var categoryIds = await _categoryService.GetDescendantIds(filter.CategoryId.Value);
//
//             where.Add("p.category_id = ANY(@CategoryIds)");
//             param.Add("CategoryIds", categoryIds);
//         }
//
//         // Price
//         if (filter.MinPrice.HasValue)
//         {
//             where.Add("p.price >= @MinPrice");
//             param.Add("MinPrice", filter.MinPrice);
//         }
//
//         if (filter.MaxPrice.HasValue)
//         {
//             where.Add("p.price <= @MaxPrice");
//             param.Add("MaxPrice", filter.MaxPrice);
//         }
//
//         var whereClause = "WHERE " + string.Join(" AND ", where);
//         var orderBy = BuildOrderBy(filter.SortBy, filter.SortDirection);
//
//         var sql = $@"
//             SELECT p.id, p.name, p.price, p.stock, p.image_url AS ImageUrl
//             FROM products p
//             {whereClause}
//             {orderBy}
//             OFFSET @Offset LIMIT @PageSize;
//
//             SELECT COUNT(*) FROM products p {whereClause};
//         ";
//
//         param.Add("Offset", offset);
//         param.Add("PageSize", filter.PageSize);
//
//         using var multi = await connection.QueryMultipleAsync(sql, param);
//
//         var items = (await multi.ReadAsync<ProductListItemDto>()).ToList();
//         var total = await multi.ReadFirstAsync<int>();
//
//         var result = (items, total);
//
//         await _cache.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5));
//
//         return result;
//     }
//
//     public async Task<ProductDetailDto?> GetById(Guid id)
//     {
//         using var connection = _connectionFactory.CreateConnection();
//
//         var sql = @"
//             SELECT 
//                 p.id,
//                 p.name,
//                 p.price,
//                 p.stock,
//                 p.image_url AS ImageUrl,
//                 p.category_id AS CategoryId
//             FROM products p
//             WHERE p.id = @Id AND p.is_deleted = false
//         ";
//
//         return await connection.QueryFirstOrDefaultAsync<ProductDetailDto>(
//             sql,
//             new { Id = id }
//         );
//     }
//
//     private static string BuildCacheKey(ProductFilter f)
//     {
//         return $"products:" +
//                $"cat:{f.CategoryId}:" +
//                $"search:{f.Search}:" +
//                $"price:{f.MinPrice}-{f.MaxPrice}:" +
//                $"sort:{f.SortBy}_{f.SortDirection}:" +
//                $"page:{f.Page}_{f.PageSize}";
//     }
//     
//     private static string BuildOrderBy(string? sortBy, string? direction)
//     {
//         var dir = direction?.ToLower() == "desc" ? "DESC" : "ASC";
//
//         return sortBy?.ToLower() switch
//         {
//             "price" => $"ORDER BY p.price {dir}",
//             "created" => $"ORDER BY p.created_at {dir}",
//             "stock" => $"ORDER BY p.stock {dir}",
//             _ => $"ORDER BY p.name {dir}"
//         };
//     }
// }


using Dapper;
using Ecommerce.Application.Catalog.Products.Queries.GetProductById;
using Ecommerce.Application.Catalog.Products.Queries.GetProducts;
using Ecommerce.Application.Common.Caching;
using Ecommerce.Application.Common.Interfaces;

namespace Ecommerce.Infrastructure.ReadModels.Products;

public class ProductQueryService : IProductQueryService
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly ICategoryQueryService _categoryService;
    private readonly ICacheService _cache;

    public ProductQueryService(IDbConnectionFactory connectionFactory,  ICategoryQueryService categoryService, ICacheService cache)
    {
        _connectionFactory = connectionFactory;
        _categoryService = categoryService;
        _cache = cache;
    }

    public async Task<(IEnumerable<ProductListItemDto> Items, int Total)> GetProducts(ProductFilter filter)
    {
        var cacheKey = CacheKeys.ProductList(filter);

        return await _cache.GetOrSetAsync(
            cacheKey,
            async () =>
            {
                using var connection = _connectionFactory.CreateConnection();

                var offset = (filter.Page - 1) * filter.PageSize;

                var where = new List<string> { "p.is_deleted = false" };
                var param = new DynamicParameters();

                if (!string.IsNullOrWhiteSpace(filter.Search))
                {
                    where.Add("p.name ILIKE @Search");
                    param.Add("Search", $"%{filter.Search}%");
                }

                if (filter.CategoryId.HasValue)
                {
                    var categoryIds = await _cache.GetOrSetAsync(
                        CacheKeys.CategoryTree(filter.CategoryId.Value),
                        () => _categoryService.GetDescendantIds(filter.CategoryId.Value),
                        TimeSpan.FromMinutes(30)
                    );

                    where.Add("p.category_id = ANY(@CategoryIds)");
                    param.Add("CategoryIds", categoryIds);
                }

                if (filter.MinPrice.HasValue)
                {
                    where.Add("p.price >= @MinPrice");
                    param.Add("MinPrice", filter.MinPrice);
                }

                if (filter.MaxPrice.HasValue)
                {
                    where.Add("p.price <= @MaxPrice");
                    param.Add("MaxPrice", filter.MaxPrice);
                }

                var whereClause = "WHERE " + string.Join(" AND ", where);
                var orderBy = BuildOrderBy(filter.SortBy, filter.SortDirection);

                var sql = $@"
                    SELECT p.id, p.name, p.price, p.stock, p.image_url AS ImageUrl
                    FROM products p
                    {whereClause}
                    {orderBy}
                    OFFSET @Offset LIMIT @PageSize;

                    SELECT COUNT(*) FROM products p {whereClause};
                ";

                param.Add("Offset", offset);
                param.Add("PageSize", filter.PageSize);

                using var multi = await connection.QueryMultipleAsync(sql, param);

                var items = (await multi.ReadAsync<ProductListItemDto>()).ToList();
                var total = await multi.ReadFirstAsync<int>();

                return (items, total);
            },
            TimeSpan.FromMinutes(5)
        );
    }

    public async Task<ProductDetailDto?> GetById(Guid id)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
            SELECT 
                p.id,
                p.name,
                p.price,
                p.stock,
                p.image_url AS ImageUrl,
                p.category_id AS CategoryId
            FROM products p
            WHERE p.id = @Id AND p.is_deleted = false
        ";

        return await connection.QueryFirstOrDefaultAsync<ProductDetailDto>(
            sql,
            new { Id = id }
        );
    }

    private static string BuildCacheKey(ProductFilter f)
    {
        return $"products:" +
               $"cat:{f.CategoryId}:" +
               $"search:{f.Search}:" +
               $"price:{f.MinPrice}-{f.MaxPrice}:" +
               $"sort:{f.SortBy}_{f.SortDirection}:" +
               $"page:{f.Page}_{f.PageSize}";
    }
    
    private static string BuildOrderBy(string? sortBy, string? direction)
    {
        var dir = direction?.ToLower() == "desc" ? "DESC" : "ASC";

        return sortBy?.ToLower() switch
        {
            "price" => $"ORDER BY p.price {dir}",
            "created" => $"ORDER BY p.created_at {dir}",
            "stock" => $"ORDER BY p.stock {dir}",
            _ => $"ORDER BY p.name {dir}"
        };
    }
}

