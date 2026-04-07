namespace Ecommerce.Application.Catalog.GetProducts;

public interface IProductReadService
{
    Task<(IEnumerable<ProductDto> Items, int Total)> GetProductsAsync(
        int page,
        int pageSize,
        string? search
    );
}