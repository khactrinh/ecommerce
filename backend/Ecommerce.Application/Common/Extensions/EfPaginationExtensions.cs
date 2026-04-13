using Ecommerce.Shared.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Common.Extensions;

public static class EfPaginationExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        int page,
        int pageSize)
    {
        var total = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return PagedResult<T>.Create(items, total, page, pageSize);
    }
}