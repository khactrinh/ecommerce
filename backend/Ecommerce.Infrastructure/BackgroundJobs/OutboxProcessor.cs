using System.Text.Json;
using Ecommerce.Application.Common.Messaging;
using Ecommerce.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.BackgroundJobs;

public class OutboxProcessor : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OutboxProcessor> _logger;

    private const int MAX_RETRY = 5;

    public OutboxProcessor(
        IServiceProvider serviceProvider,
        ILogger<OutboxProcessor> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("🚀 Outbox Processor started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();

                var publisher = scope.ServiceProvider
                    .GetRequiredService<IRabbitMqPublisher>();

                var db = scope.ServiceProvider
                    .GetRequiredService<AppDbContext>();

                // 🔥 lấy message chưa xử lý
                var messages = await db.Set<OutboxMessage>()
                    .Where(x => x.ProcessedOn == null)
                    .OrderBy(x => x.OccurredOn)
                    .Take(20)
                    .ToListAsync(stoppingToken);

                if (!messages.Any())
                {
                    await Task.Delay(2000, stoppingToken);
                    continue;
                }

                foreach (var msg in messages)
                {
                    try
                    {
                        // ======================================
                        // 🔥 STEP 1: resolve type
                        // ======================================
                        var type = ResolveType(msg.Type);

                        if (type == null)
                        {
                            msg.Error = $"Type not found: {msg.Type}";
                            _logger.LogWarning("⚠️ {Error}", msg.Error);
                            continue;
                        }

                        // ======================================
                        // 🔥 STEP 2: deserialize domain event
                        // ======================================
                        var domainEvent = JsonSerializer.Deserialize(
                            msg.Content,
                            type
                        );

                        if (domainEvent == null)
                        {
                            msg.Error = "Deserialize failed";
                            _logger.LogWarning("⚠️ Deserialize failed for {Type}", msg.Type);
                            continue;
                        }

                        // ======================================
                        // 🔥 STEP 3: publish (NO envelope here)
                        // ======================================
                        await publisher.PublishAsync(
                            type.Name,
                            domainEvent
                        );

                        // ======================================
                        // 🔥 STEP 4: mark processed
                        // ======================================
                        msg.ProcessedOn = DateTime.UtcNow;

                        _logger.LogInformation(
                            "✅ Published event: {Type} (OutboxId: {Id})",
                            type.Name,
                            msg.Id
                        );
                    }
                    catch (Exception ex)
                    {
                        msg.RetryCount++;
                        msg.Error = ex.Message;

                        _logger.LogError(
                            ex,
                            "❌ Failed to process OutboxId: {Id}, Retry: {Retry}",
                            msg.Id,
                            msg.RetryCount
                        );

                        // 🔥 max retry → bỏ luôn
                        if (msg.RetryCount >= MAX_RETRY)
                        {
                            msg.ProcessedOn = DateTime.UtcNow;

                            _logger.LogError(
                                "💀 Dropped message after max retry: {Id}",
                                msg.Id
                            );
                        }
                    }
                }

                await db.SaveChangesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Outbox processor error");
            }

            await Task.Delay(2000, stoppingToken);
        }
    }

    // 🔥 FIX Type.GetType() issue
    private static Type? ResolveType(string typeName)
    {
        return AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a =>
            {
                try { return a.GetTypes(); }
                catch { return Array.Empty<Type>(); }
            })
            .FirstOrDefault(t => t.Name == typeName);
    }
}