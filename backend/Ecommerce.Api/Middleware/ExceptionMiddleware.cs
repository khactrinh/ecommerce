using System.Net;
using System.Text.Json;
using Ecommerce.Shared.Responses;
using FluentValidation;

namespace Ecommerce.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled Exception");

            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var traceId = context.TraceIdentifier;

        var response = exception switch
        {
            ValidationException validationException => HandleValidationException(validationException, traceId),
            KeyNotFoundException => BuildResponse("Resource not found", traceId, HttpStatusCode.NotFound),
            UnauthorizedAccessException => BuildResponse("Unauthorized", traceId, HttpStatusCode.Unauthorized),
            _ => BuildResponse("Internal server error", traceId, HttpStatusCode.InternalServerError)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.statusCode;

        return context.Response.WriteAsync(JsonSerializer.Serialize(response.body));
    }

    private static (HttpStatusCode statusCode, object body) HandleValidationException(
        ValidationException ex,
        string traceId)
    {
        var errors = ex.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage).ToArray()
            );

        var body = ApiResponse<object>.FailResponse(
            "Validation failed",
            errors,
            traceId
        );

        return (HttpStatusCode.BadRequest, body);
    }

    private static (HttpStatusCode statusCode, object body) BuildResponse(
        string message,
        string traceId,
        HttpStatusCode statusCode)
    {
        var body = ApiResponse<object>.FailResponse(message, null, traceId);

        return (statusCode, body);
    }
}