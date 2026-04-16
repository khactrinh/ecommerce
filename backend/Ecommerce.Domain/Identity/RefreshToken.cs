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

    public Guid? ParentTokenId { get; set; }
    public Guid FamilyId { get; set; }
    
    public bool IsUsed { get; set; }

    // =========================
    // ADD BEHAVIOR (IMPORTANT)
    // =========================

    public bool IsExpired => DateTime.UtcNow >= ExpiryDate;

    public bool IsActive => !IsRevoked && !IsExpired;

    public void Revoke(string ip)
    {
        IsRevoked = true;
        RevokedAt = DateTime.UtcNow;
        RevokedByIp = ip;
    }

    public void Rotate(Guid newTokenId)
    {
        ParentTokenId = newTokenId;
    }
}