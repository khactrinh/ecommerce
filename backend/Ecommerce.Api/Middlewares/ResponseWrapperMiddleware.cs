using System.Text.Json;
using Ecommerce.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Ecommerce.Api.Middlewares;

public class ResponseWrapperMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JsonSerializerOptions _jsonOptions;

    public ResponseWrapperMiddleware(
        RequestDelegate next,
        IOptions<JsonOptions> jsonOptions)
    {
        _next = next;
        _jsonOptions = jsonOptions.Value.JsonSerializerOptions; // 🔥 LẤY GLOBAL CONFIG
    }

    public async Task Invoke(HttpContext context)
    {
        // 🔥 1. Chỉ apply cho API
        if (!context.Request.Path.StartsWithSegments("/api"))
        {
            await _next(context);
            return;
        }

        // 🔥 2. Tránh double wrap
        if (context.Items.ContainsKey("__wrapped"))
        {
            await _next(context);
            return;
        }

        var originalBodyStream = context.Response.Body;

        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

        // 🔥 3. Không phải JSON → skip
        if (context.Response.ContentType == null ||
            !context.Response.ContentType.Contains("application/json"))
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
            return;
        }

        // 🔥 4. Status != 200 → skip
        if (context.Response.StatusCode != StatusCodes.Status200OK)
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
            return;
        }

        responseBody.Seek(0, SeekOrigin.Begin);

        var bodyText = await new StreamReader(responseBody).ReadToEndAsync();

        object? data = null;

        if (!string.IsNullOrWhiteSpace(bodyText))
        {
            try
            {
                data = JsonSerializer.Deserialize<object>(bodyText, _jsonOptions);
            }
            catch
            {
                data = bodyText;
            }
        }

        var wrapped = ApiResponse<object>.Ok(data);

        context.Items["__wrapped"] = true;

        context.Response.Body = originalBodyStream;
        context.Response.ContentType = "application/json";

        // 🔥🔥 QUAN TRỌNG NHẤT
        await context.Response.WriteAsync(
            JsonSerializer.Serialize(wrapped, _jsonOptions)
        );
    }
}