using Ecommerce.Domain.Catalog.Products;

namespace Ecommerce.Application.Interfaces;

public interface IProductRepository
{
    Task AddAsync(Product product);
}