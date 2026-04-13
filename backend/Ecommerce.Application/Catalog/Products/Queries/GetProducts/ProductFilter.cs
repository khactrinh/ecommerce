using Ecommerce.Shared.Pagination;

namespace Ecommerce.Application.Catalog.Products.Queries.GetProducts;

public class ProductFilter : PaginationParams
{
    public string? Search { get; set; }
    public Guid? CategoryId { get; set; }
    public string? SortBy { get; set; } = "name"; // name, price, created
    public string? SortDirection { get; set; } = "asc"; // asc, desc
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}