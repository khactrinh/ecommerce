using Ecommerce.Application.Catalog.GetProducts;

namespace Ecommerce.Application.Catalog.Products.Queries.GetProducts;

public interface IGetProductsQueryService
{
    Task<(IEnumerable<ProductListItemDto> Items, int Total)> Execute(ProductFilter filter);
}