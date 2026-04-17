using Ecommerce.Application.Catalog.Categories.Dtos;

namespace Ecommerce.Application.Catalog.Categories.Queries.GetCategories;

using Dapper;
using Ecommerce.Shared.Pagination;
using MediatR;
using System.Data;
using System.Text;

public class GetCategoriesHandler 
    : IRequestHandler<GetCategoriesQuery, PagedResult<CategoryResponse>>
{
    private readonly IDbConnection _connection;

    public GetCategoriesHandler(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<PagedResult<CategoryResponse>> Handle(
        GetCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var filter = request.Filter;

        var sqlBuilder = new StringBuilder(@"
            FROM categories
            WHERE is_deleted = false
        ");

        var parameters = new DynamicParameters();

        // 🔍 Search
        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            sqlBuilder.Append(" AND LOWER(name) LIKE @Search");
            parameters.Add("Search", $"%{filter.Search.ToLower()}%");
        }

        // 🌳 Parent filter
        if (filter.ParentId.HasValue)
        {
            sqlBuilder.Append(" AND parent_id = @ParentId");
            parameters.Add("ParentId", filter.ParentId);
        }

        // 🔢 Count query
        var countSql = $"SELECT COUNT(*) {sqlBuilder}";

        var total = await _connection.ExecuteScalarAsync<int>(countSql, parameters);

        // 📄 Data query
        var dataSql = $@"
            SELECT id, name, slug, parent_id AS ParentId
            {sqlBuilder}
            ORDER BY name
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
        ";

        parameters.Add("Offset", (filter.PageNumber - 1) * filter.PageSize);
        parameters.Add("PageSize", filter.PageSize);

        var items = (await _connection.QueryAsync<CategoryResponse>(
            dataSql,
            parameters)).ToList();
        
        return items.ToPagedResult(total,
            filter.PageNumber,
            filter.PageSize);

        // return PagedResult<CategoryListItemResponse>.Create(
        //     items,
        //     total,
        //     filter.PageNumber,
        //     filter.PageSize
        // );
    }
}