using System.Text;
using Ecommerce.Api.Middleware;
using Ecommerce.Application;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Orders.Sagas;
using Ecommerce.Infrastructure;
using Ecommerce.Infrastructure.Messaging;
//using Ecommerce.Infrastructure.BackgroundJobs;
using Ecommerce.Infrastructure.Persistence;
using Ecommerce.Infrastructure.Persistence.Dapper;
using Ecommerce.Infrastructure.Persistence.Sagas;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // 🔐 Define Bearer
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Chỉ cần paste nguyên chuỗi Token vào đây (Swagger sẽ tự thêm chữ Bearer)",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    // Cú pháp chuẩn .NET 10: Phải truyền 'document' vào Reference
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer", document),
            new List<string>()
        }
    });
});

builder.Services.AddInfrastructure(builder.Configuration);

//builder.Services.AddHostedService<OutboxProcessor>();

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowFrontend",
//         policy =>
//         {
//             policy.WithOrigins("http://localhost:3000")
//                 .AllowAnyHeader()
//                 .AllowAnyMethod();
//         });
// });

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));


var jwt = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwt.Issuer,

            ValidateAudience = true,
            ValidAudience = jwt.Audience,

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwt.Secret))
        };

        // 🔥 DEBUG
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = ctx =>
            {
                Console.WriteLine("AUTH FAILED: " + ctx.Exception.Message);
                return Task.CompletedTask;
            },
            OnTokenValidated = ctx =>
            {
                Console.WriteLine("TOKEN OK");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


builder.Services.AddScoped<IOrderSagaRepository, OrderSagaRepository>();
builder.Services.AddScoped<OrderSaga>();


// MediatR
//builder.Services.AddMediatR(Assembly.Load("Ecommerce.Application"));
builder.Services.AddApplication();

// Controllers
builder.Services.AddControllers();



var app = builder.Build();

DapperConfig.Configure();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // if (app.Environment.IsDevelopment())
    // {
        await context.Database.MigrateAsync();
    //}
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// if (app.Environment.IsDevelopment())
// {
//     
// }

app.UseSwagger();
app.UseSwaggerUI();
    
app.Run();


