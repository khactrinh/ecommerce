namespace Ecommerce.Shared.Pagination;

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public bool HasNext => Page < TotalPages;
    public bool HasPrevious => Page > 1;

    public static PagedResult<T> Create(List<T> items, int count, int page, int pageSize)
    {
        return new PagedResult<T>
        {
            Items = items,
            TotalItems = count,
            Page = page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize)
        };
    }
}