using Ecommerce.Application.Catalog.Products.Queries.GetProducts;

namespace Ecommerce.Application.Common.Models;

public class ProductQueryResult
{
    public List<ProductListItemDto> Items { get; set; } = new();
    public int Total { get; set; }
}