using Ecommerce.Application.Catalog.Products.DTOs;

namespace Ecommerce.Application.Catalog.Products.Interfaces;

public interface IProductReadService
{
    Task<IEnumerable<ProductDto>> GetProductsAsync(int page, int pageSize);
}