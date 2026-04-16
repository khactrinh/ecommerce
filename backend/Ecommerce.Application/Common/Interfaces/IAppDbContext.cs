using Ecommerce.Domain.Catalog.Categories;
using Ecommerce.Domain.Catalog.Products;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Category> Categories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}