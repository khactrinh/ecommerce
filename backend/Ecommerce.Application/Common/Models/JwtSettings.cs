using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.Common.Models;

public class JwtSettings
{
    [Required]
    public string Secret { get; set; }
    [Required]
    public string Issuer { get; set; }
    [Required]
    public string Audience { get; set; }
}