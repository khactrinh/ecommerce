using Ecommerce.Shared.Pagination;

namespace Ecommerce.Application.Catalog.Products.Queries;

public class ProductFilter : PaginationParams
{
    public string? Search { get; set; }
    public Guid? CategoryId { get; set; }
    public string? Sort { get; set; } // price_asc, price_desc
}