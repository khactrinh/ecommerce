namespace Ecommerce.Domain.Identity;

public class RefreshToken
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }

    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }

    public bool IsRevoked { get; set; }

    public string CreatedByIp { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string? RevokedByIp { get; set; }
}