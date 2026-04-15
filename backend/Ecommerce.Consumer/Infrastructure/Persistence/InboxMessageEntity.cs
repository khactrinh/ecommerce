using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Consumer.Infrastructure.Persistence;

[Table("inbox_messages", Schema = "messaging")]
public class InboxMessageEntity
{
    [Key]
    public Guid Id { get; set; }

    public string Type { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime OccurredOn { get; set; }

    public DateTime? ProcessedOn { get; set; }
    public string? Error { get; set; }
}