using System.Data;
using Ecommerce.Application.Cart.Interfaces;
using Ecommerce.Application.Catalog.GetProducts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Interfaces;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Persistence;
using Ecommerce.Infrastructure.Persistence.Repositories;
using Ecommerce.Infrastructure.ReadServices;
using Npgsql;

namespace Ecommerce.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // =============================
        // 🔥 EF Core (WRITE SIDE)
        // =============================
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")
            )//.UseSnakeCaseNamingConvention()
        );
        
        // =============================
        // 🔥 Repositories (WRITE)
        // =============================
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        
        
        // =============================
        // 🔥 Dapper Connection (READ SIDE)
        // =============================
        services.AddScoped<IDbConnection>(sp =>
            new NpgsqlConnection(
                sp.GetRequiredService<IConfiguration>()
                    .GetConnectionString("DefaultConnection")
            )
        );
        
        // =============================
        // 🔥 Connection Factory (BEST PRACTICE)
        // =============================
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        // =============================
        // 🔥 Read Services (CQRS)
        // =============================
        services.AddScoped<IProductReadService, ProductReadService>();
        

        // 🧱 Write side (EF Core)
        services.AddScoped<IProductRepository, ProductRepository>();
        
        return services;
    }
}