using Ecommerce.Application.Catalog.GetProducts;
using Ecommerce.Application.Catalog.Products.Queries.GetProductById;
using Ecommerce.Application.Catalog.Products.Queries.GetProducts;
using Ecommerce.Application.Common.Models;

namespace Ecommerce.Application.Common.Interfaces;

public interface IProductQueryService
{
    Task<ProductQueryResult> GetProducts(ProductFilter filter);

    Task<ProductDetailDto?> GetById(Guid id);
}