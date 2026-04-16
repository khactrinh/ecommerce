// using Microsoft.IdentityModel.Tokens;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Security.Cryptography;
// using System.Text;
// using Ecommerce.Domain.Auth;
// using Ecommerce.Domain.Identity;
//
// namespace Ecommerce.Application.Auth;
//
// public class TokenService
// {
//     private readonly IConfiguration _config;
//
//     public TokenService(IConfiguration config)
//     {
//         _config = config;
//     }
//
//     public string GenerateAccessToken(User user, IList<string> roles)
//     {
//         var claims = new List<Claim>
//         {
//             new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//             new Claim(ClaimTypes.Email, user.Email)
//         };
//
//         claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
//
//         var key = new SymmetricSecurityKey(
//             Encoding.UTF8.GetBytes(_config["Jwt:Secret"]!)
//         );
//
//         var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//
//         var token = new JwtSecurityToken(
//             issuer: _config["Jwt:Issuer"],
//             audience: _config["Jwt:Audience"],
//             claims: claims,
//             expires: DateTime.UtcNow.AddMinutes(15),
//             signingCredentials: creds
//         );
//
//         return new JwtSecurityTokenHandler().WriteToken(token);
//     }
//
//     public (string raw, RefreshToken entity) GenerateRefreshToken(Guid userId)
//     {
//         var raw = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
//
//         var entity = new RefreshToken
//         {
//             UserId = userId,
//             TokenHash = BCrypt.Net.BCrypt.HashPassword(raw),
//             CreatedAt = DateTime.UtcNow,
//             ExpiresAt = DateTime.UtcNow.AddDays(30)
//         };
//
//         return (raw, entity);
//     }
//
//     public bool Verify(string raw, string hash)
//     {
//         return BCrypt.Net.BCrypt.Verify(raw, hash);
//     }
// }