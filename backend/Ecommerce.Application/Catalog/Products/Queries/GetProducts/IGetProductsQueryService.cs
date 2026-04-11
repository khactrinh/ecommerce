using Ecommerce.Application.Catalog.GetProducts;

namespace Ecommerce.Application.Catalog.Products.Queries.GetProducts;

public interface IProductReadService
{
    // Task<(IEnumerable<ProductDetailDto> Items, int Total)> GetProductsAsync(
    //     int page,
    //     int pageSize,
    //     string? search
    // );

    Task<(IEnumerable<ProductListItemDto> Items, int Total)> GetProductsAsync(
        ProductFilter filter);
}