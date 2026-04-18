namespace Ecommerce.Api.Middlewares;

public class CorrelationIdMiddleware
{
    private const string HeaderKey = "X-Correlation-Id";

    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(HeaderKey, out var correlationId))
        {
            correlationId = Guid.NewGuid().ToString();
        }

        context.TraceIdentifier = correlationId!;

        context.Response.Headers[HeaderKey] = correlationId!;

        await _next(context);
    }
}