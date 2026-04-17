namespace Ecommerce.Shared.Responses;

public class ApiResponse<T>
{
    public bool Success { get; init; }
    public string Message { get; init; } = "";
    public T Data { get; init; } = default!;
    public object? Errors { get; init; }
    public string? TraceId { get; init; }

    public static ApiResponse<T> Ok(T data, string message = "Success")
        => new()
        {
            Success = true,
            Message = message,
            Data = data
        };

    public static ApiResponse<T> Fail(
        string message,
        object? errors = null,
        string? traceId = null)
        => new()
        {
            Success = false,
            Message = message,
            Errors = errors,
            TraceId = traceId,
            Data = default! // 🔥 luôn set để không null runtime
        };
}