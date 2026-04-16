using System.Security.Cryptography;
using System.Text;
using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.Orders;
using Microsoft.Extensions.Options;

namespace Ecommerce.Infrastructure.Payments;

public class VnPayService : IPaymentService
{
    private readonly VnPayOptions _options;

    public VnPayService(
        IOptions<VnPayOptions> options)
    {
        _options = options.Value;
    }

    public string CreatePaymentUrl(Order order, string ipAddress)
    {
        var ip = string.IsNullOrEmpty(ipAddress) ? "127.0.0.1" : ipAddress;

        var data = new SortedDictionary<string, string>
        {
            ["vnp_Version"] = "2.1.0",
            ["vnp_Command"] = "pay",
            ["vnp_TmnCode"] = _options.TmnCode,
            ["vnp_Amount"] = ((long)(order.TotalAmount * 100)).ToString(),
            ["vnp_CreateDate"] = DateTime.UtcNow.ToString("yyyyMMddHHmmss"),
            ["vnp_CurrCode"] = "VND",
            ["vnp_IpAddr"] = ip,
            ["vnp_Locale"] = "vn",
            ["vnp_OrderInfo"] = $"Order {order.Id}",
            ["vnp_OrderType"] = "other",
            ["vnp_ReturnUrl"] = _options.ReturnUrl,
            ["vnp_TxnRef"] = order.Id.ToString(),
            ["vnp_SecureHashType"] = "HmacSHA512"
        };

        var query = string.Join("&", data.Select(x =>
            $"{x.Key}={Uri.EscapeDataString(x.Value)}"));

        var hash = HmacSHA512(_options.HashSecret, query);

        return $"{_options.Url}?{query}&vnp_SecureHash={hash}";
    }

    private static string HmacSHA512(string key, string input)
    {
        using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }
    
    public bool VerifyPayment(Dictionary<string, string> query)
    {
        var secret = _options.HashSecret;

        var vnpData = query
            .Where(x => x.Key.StartsWith("vnp_") && x.Key != "vnp_SecureHash")
            .ToDictionary(x => x.Key, x => x.Value);

        var sortedData = new SortedDictionary<string, string>(vnpData);

        var rawData = string.Join("&", sortedData.Select(x =>
            $"{x.Key}={Uri.EscapeDataString(x.Value)}"));

        var computedHash = HmacSHA512(secret, rawData);

        var vnpSecureHash = query["vnp_SecureHash"];

        return computedHash.Equals(vnpSecureHash, StringComparison.OrdinalIgnoreCase);
    }
}