using System.Data;
using Ecommerce.Application.Cart.Interfaces;
using Ecommerce.Application.Catalog.GetProducts;
using Ecommerce.Application.Catalog.Products.Queries.GetProductById;
using Ecommerce.Application.Catalog.Products.Queries.GetProducts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Interfaces;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Identity;
using Ecommerce.Infrastructure.Persistence;
using Ecommerce.Infrastructure.Persistence.Repositories;
using Ecommerce.Infrastructure.ReadModels.Products;
using Ecommerce.Infrastructure.Services;
using Npgsql;
using StackExchange.Redis;

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
            ).UseSnakeCaseNamingConvention()
        );
        
        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<AppDbContext>());
        
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
        
        services.AddScoped<IGetProductByIdQueryService, GetProductByIdQueryService>();
        services.AddScoped<IGetProductsQueryService, GetProductsQueryService>();
        

        // 🧱 Write side (EF Core)
        services.AddScoped<IProductRepository, ProductRepository>();
        
        // =============================
        // 🔐 JWT CONFIG (BEST PRACTICE)
        // =============================
        services.AddOptions<JwtSettings>()
            .Bind(configuration.GetSection("Jwt"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<IJwtService, JwtService>();
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var config = configuration.GetConnectionString("Redis");
            return ConnectionMultiplexer.Connect(config);
        });

        services.AddScoped<IEmailService, EmailService>();
        
        //// services.AddSingleton<IMessageBus, RabbitMqBus>();
        
        return services;
    }
}