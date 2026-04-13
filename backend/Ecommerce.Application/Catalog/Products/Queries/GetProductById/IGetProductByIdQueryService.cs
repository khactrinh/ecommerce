namespace Ecommerce.Application.Catalog.Products.Queries.GetProductById;

public interface IGetProductByIdQueryService
{
    Task<ProductDetailDto?> Execute(Guid id);
}