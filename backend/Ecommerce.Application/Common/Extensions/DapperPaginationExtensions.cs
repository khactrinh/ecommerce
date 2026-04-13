using System.Data;
using Dapper;
using Ecommerce.Shared.Pagination;

namespace Ecommerce.Application.Common.Extensions;

public static class DapperPaginationExtensions
{
    public static async Task<PagedResult<T>> QueryPagedAsync<T>(
        this IDbConnection connection,
        string selectSql,
        string fromSql,
        string orderBy,
        object? parameters,
        int page,
        int pageSize)
    {
        var countSql = $"SELECT COUNT(*) {fromSql}";

        var total = await connection.ExecuteScalarAsync<int>(countSql, parameters);

        var dataSql = $@"
        {selectSql}
        {fromSql}
        {orderBy}
        LIMIT @PageSize OFFSET @Offset
    ";

        var dynamicParams = new DynamicParameters(parameters);
        dynamicParams.Add("Offset", (page - 1) * pageSize);
        dynamicParams.Add("PageSize", pageSize);

        var items = (await connection.QueryAsync<T>(dataSql, dynamicParams)).ToList();

        return PagedResult<T>.Create(items, total, page, pageSize);
    }
}