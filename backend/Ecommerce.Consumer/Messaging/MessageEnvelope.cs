using System.Text.Json;

namespace Ecommerce.Consumer.Messaging;

public class MessageEnvelope
{
    public Guid MessageId { get; set; }
    public string Type { get; set; } = default!;
    public JsonElement Data { get; set; }
    public DateTime OccurredOn { get; set; }
    public string CorrelationId { get; set; } = default!;
}