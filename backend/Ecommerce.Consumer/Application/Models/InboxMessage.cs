namespace Ecommerce.Consumer.Application.Models;

public class InboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime OccurredOn { get; set; }
    public DateTime? ProcessedOn { get; set; }

    public int RetryCount { get; set; }
    public string? Error { get; set; }
}