using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ecommerce.Application.Catalog.Products.Interfaces;
using Ecommerce.Application.Interfaces;
using Ecommerce.Infrastructure.Persistence;
using Ecommerce.Infrastructure.Persistence.Repositories;
using Ecommerce.Infrastructure.Persistence.ReadServices;

namespace Ecommerce.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // 🗄️ Database (PostgreSQL)
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")
            )
        );

        // 🧱 Write side (EF Core)
        services.AddScoped<IProductRepository, ProductRepository>();

        // ⚡ Read side (Dapper)
        services.AddScoped<IProductReadService, ProductReadService>();

        return services;
    }
}