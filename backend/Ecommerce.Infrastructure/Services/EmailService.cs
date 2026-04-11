using Ecommerce.Application.Common.Interfaces;

namespace Ecommerce.Infrastructure.Services;

public class EmailService : IEmailService
{
    public Task SendAsync(string to, string subject, string body)
    {
        Console.WriteLine($"Sending email to {to}: {subject}");
        return Task.CompletedTask;
    }
}