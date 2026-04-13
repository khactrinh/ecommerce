// using Dapper;
// using Ecommerce.Application.Catalog.GetProducts;
// using Ecommerce.Application.Catalog.Products.Queries.GetProducts;
// using Ecommerce.Application.Common.Interfaces;
//
// namespace Ecommerce.Infrastructure.ReadServices;
//
// public class GetProductsQueryService : IGetProductsQueryService
// {
//     private readonly IDbConnectionFactory _connectionFactory;
//
//     public GetProductsQueryService(IDbConnectionFactory connectionFactory)
//     {
//         _connectionFactory = connectionFactory;
//     }
//
//     public async Task<(IEnumerable<ProductListItemDto> Items, int Total)> GetProductsAsync(ProductFilter filter)
//     {
//         using var connection = _connectionFactory.CreateConnection();
//
//         var offset = (filter.Page - 1) * filter.PageSize;
//
//         var whereConditions = new List<string>();
//         var parameters = new DynamicParameters();
//
//         // 🔍 Search
//         if (!string.IsNullOrWhiteSpace(filter.Search))
//         {
//             whereConditions.Add("name ILIKE @Search");
//             parameters.Add("Search", $"%{filter.Search}%");
//         }
//
//         // 📂 Category filter
//         if (filter.CategoryId.HasValue)
//         {
//             whereConditions.Add("category_id = @CategoryId");
//             parameters.Add("CategoryId", filter.CategoryId);
//         }
//
//         var whereClause = whereConditions.Any()
//             ? "WHERE " + string.Join(" AND ", whereConditions)
//             : "";
//
//         // 🔽 Sorting
//         var orderBy = filter.Sort switch
//         {
//             "price_asc" => "price ASC",
//             "price_desc" => "price DESC",
//             _ => "name ASC"
//         };
//
//         var sql = $@"
//             SELECT id, name, price, stock, image_url
//             FROM products
//             {whereClause}
//             ORDER BY {orderBy}
//             OFFSET @Offset LIMIT @Limit;
//
//             SELECT COUNT(*) FROM products {whereClause};
//         ";
//
//         parameters.Add("Offset", offset);
//         parameters.Add("Limit", filter.PageSize);
//
//         using var multi = await connection.QueryMultipleAsync(sql, parameters);
//
//         var items = await multi.ReadAsync<ProductListItemDto>();
//         var total = await multi.ReadFirstAsync<int>();
//
//         return (items, total);
//     }
// }



////VERSION 2

// using Dapper;
// using Ecommerce.Application.Catalog.GetProducts;
// using Ecommerce.Application.Catalog.Products.Queries.GetProducts;
// using Ecommerce.Application.Common.Interfaces;
//
// namespace Ecommerce.Infrastructure.ReadModels.Products;
//
// public class GetProductsQueryService : IGetProductsQueryService
// {
//     private readonly IDbConnectionFactory _connectionFactory;
//
//     public GetProductsQueryService(IDbConnectionFactory connectionFactory)
//     {
//         _connectionFactory = connectionFactory;
//     }
//
//     public async Task<(IEnumerable<ProductListItemDto> Items, int Total)> Execute(ProductFilter filter)
//     {
//         using var connection = _connectionFactory.CreateConnection();
//
//         var offset = (filter.Page - 1) * filter.PageSize;
//
//         var whereConditions = new List<string>();
//         var parameters = new DynamicParameters();
//
//         if (!string.IsNullOrWhiteSpace(filter.Search))
//         {
//             whereConditions.Add("name ILIKE @Search");
//             parameters.Add("Search", $"%{filter.Search}%");
//         }
//
//         if (filter.CategoryId.HasValue)
//         {
//             whereConditions.Add("category_id = @CategoryId");
//             parameters.Add("CategoryId", filter.CategoryId);
//         }
//
//         var whereClause = whereConditions.Any()
//             ? "WHERE " + string.Join(" AND ", whereConditions)
//             : "";
//
//         var orderBy = filter.Sort switch
//         {
//             "price_asc" => "price ASC",
//             "price_desc" => "price DESC",
//             _ => "name ASC"
//         };
//
//         var sql = $@"
//             SELECT id, name, price, stock, image_url AS ImageUrl
//             FROM products
//             {whereClause}
//             ORDER BY {orderBy}
//             OFFSET @Offset LIMIT @Limit;
//
//             SELECT COUNT(*) FROM products {whereClause};
//         ";
//
//         parameters.Add("Offset", offset);
//         parameters.Add("Limit", filter.PageSize);
//
//         using var multi = await connection.QueryMultipleAsync(sql, parameters);
//
//         var items = await multi.ReadAsync<ProductListItemDto>();
//         var total = await multi.ReadFirstAsync<int>();
//
//         return (items, total);
//     }
// }