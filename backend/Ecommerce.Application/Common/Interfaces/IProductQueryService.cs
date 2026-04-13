using Ecommerce.Application.Catalog.GetProducts;
using Ecommerce.Application.Catalog.Products.Queries.GetProductById;
using Ecommerce.Application.Catalog.Products.Queries.GetProducts;

namespace Ecommerce.Application.Common.Interfaces;

public interface IProductQueryService
{
    Task<(IEnumerable<ProductListItemDto> Items, int Total)> GetProducts(ProductFilter filter);

    Task<ProductDetailDto?> GetById(Guid id);
}