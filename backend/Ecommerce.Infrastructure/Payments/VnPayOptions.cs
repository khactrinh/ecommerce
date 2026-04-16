namespace Ecommerce.Infrastructure.Payments;

public class VnPayOptions
{
    public string Url { get; set; } = default!;
    public string TmnCode { get; set; } = default!;
    public string HashSecret { get; set; } = default!;
    public string ReturnUrl { get; set; } = default!;
}