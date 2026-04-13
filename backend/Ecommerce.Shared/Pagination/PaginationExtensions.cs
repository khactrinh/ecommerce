namespace Ecommerce.Shared.Pagination;

public static class PaginationExtensions
{
    public static PagedResult<T> ToPagedResult<T>(
        this List<T> items,
        int total,
        int page,
        int pageSize)
    {
        return PagedResult<T>.Create(items, total, page, pageSize);
    }
}