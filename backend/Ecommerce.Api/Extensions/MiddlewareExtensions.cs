using Ecommerce.Api.Middlewares;

namespace Ecommerce.Api.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<CorrelationIdMiddleware>(); // 🔥 phải đứng đầu
        app.UseMiddleware<ExceptionMiddleware>();     // 🔥 catch lỗi
        app.UseMiddleware<ResponseWrapperMiddleware>(); // 🔥 wrap response

        return app;
    }
}