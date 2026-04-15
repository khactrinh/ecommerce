using System.Text;
using System.Text.Json;
using Ecommerce.Consumer.Application.Dispatchers;
using Ecommerce.Consumer.Application.Interfaces;
using Ecommerce.Consumer.Application.Models;
using Ecommerce.Application.Orders.Sagas;
using Ecommerce.Consumer.Messaging;
using Ecommerce.Domain.Orders.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace Ecommerce.Consumer.BackgroundJobs;

public class RabbitMqConsumer : BackgroundService
{
    private readonly IServiceProvider _sp;
    private readonly ILogger<RabbitMqConsumer> _logger;

    private const string ExchangeName = "ecommerce.events";
    private const string MainQueue = "ecommerce.consumer";
    private const string DlqQueue = "ecommerce.consumer.dlq";

    public RabbitMqConsumer(IServiceProvider sp, ILogger<RabbitMqConsumer> logger)
    {
        _sp = sp;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
        };

        var connection = await factory.CreateConnectionAsync(stoppingToken);
        var channel = await connection.CreateChannelAsync();

        // ===============================
        // 🔥 EXCHANGE (IMPORTANT FIX)
        // ===============================
        await channel.ExchangeDeclareAsync(
            exchange: ExchangeName,
            type: ExchangeType.Topic,
            durable: true
        );

        // ===============================
        // 🔥 MAIN QUEUE
        // ===============================
        await channel.QueueDeclareAsync(
            queue: MainQueue,
            durable: true,
            exclusive: false,
            autoDelete: false
        );

        await channel.QueueBindAsync(
            queue: MainQueue,
            exchange: ExchangeName,
            routingKey: "#"
        );

        // ===============================
        // 🔥 DLQ
        // ===============================
        await channel.QueueDeclareAsync(
            queue: DlqQueue,
            durable: true,
            exclusive: false,
            autoDelete: false
        );

        _logger.LogInformation("🚀 RabbitMQ Consumer started");

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (_, ea) =>
        {
            using var scope = _sp.CreateScope();

            var inbox = scope.ServiceProvider.GetRequiredService<IInboxRepository>();
            var dispatcher = scope.ServiceProvider.GetRequiredService<EventDispatcher>();
            var saga = scope.ServiceProvider.GetRequiredService<OrderSaga>();

            var body = Encoding.UTF8.GetString(ea.Body.ToArray());

            try
            {
                var envelope = JsonSerializer.Deserialize<MessageEnvelope>(body);

                if (envelope == null)
                    throw new Exception("Invalid message envelope");

                _logger.LogInformation(
                    "📥 {Type} | CorrelationId: {CorrelationId}",
                    envelope.Type,
                    envelope.CorrelationId
                );

                // ===============================
                // 🔥 IDEMPOTENCY (INBOX)
                // ===============================
                if (await inbox.ExistsAsync(envelope.MessageId))
                {
                    _logger.LogWarning("⚠️ Duplicate message: {Id}", envelope.MessageId);
                    await channel.BasicAckAsync(ea.DeliveryTag, false);
                    return;
                }

                // Save inbox first
                await inbox.AddAsync(new InboxMessage
                {
                    Id = envelope.MessageId,
                    Type = envelope.Type,
                    Content = body,
                    OccurredOn = envelope.OccurredOn
                });

                var json = envelope.Data.GetRawText();

                // ===============================
                // 🔥 SAGA FLOW
                // ===============================
                switch (envelope.Type)
                {
                    case nameof(OrderCreatedEvent):
                        var orderCreated = JsonSerializer.Deserialize<OrderCreatedEvent>(json);
                        if (orderCreated == null) throw new Exception("Invalid OrderCreatedEvent");
                        await saga.Handle(orderCreated);
                        break;

                    case nameof(PaymentSucceededEvent):
                        var paymentOk = JsonSerializer.Deserialize<PaymentSucceededEvent>(json);
                        if (paymentOk == null) throw new Exception("Invalid PaymentSucceededEvent");
                        await saga.Handle(paymentOk);
                        break;

                    case nameof(PaymentFailedEvent):
                        var paymentFail = JsonSerializer.Deserialize<PaymentFailedEvent>(json);
                        if (paymentFail == null) throw new Exception("Invalid PaymentFailedEvent");
                        await saga.Handle(paymentFail);
                        break;

                    case nameof(InventoryReservedEvent):
                        var inventoryOk = JsonSerializer.Deserialize<InventoryReservedEvent>(json);
                        if (inventoryOk == null) throw new Exception("Invalid InventoryReservedEvent");
                        await saga.Handle(inventoryOk);
                        break;

                    case nameof(InventoryFailedEvent):
                        var inventoryFail = JsonSerializer.Deserialize<InventoryFailedEvent>(json);
                        if (inventoryFail == null) throw new Exception("Invalid InventoryFailedEvent");
                        await saga.Handle(inventoryFail);
                        break;

                    // ===============================
                    // 🔥 NORMAL EVENT DISPATCH
                    // ===============================
                    default:
                        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

                        await dispatcher.DispatchAsync(envelope.Type, envelope.Data)
                            .WaitAsync(cts.Token);
                        break;
                }

                await inbox.MarkProcessedAsync(envelope.MessageId);

                await channel.BasicAckAsync(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error processing message");

                // ===============================
                // 🔥 RETRY + DLQ
                // ===============================
                int retryCount = 0;

                if (ea.BasicProperties.Headers != null &&
                    ea.BasicProperties.Headers.TryGetValue("x-retry-count", out var value))
                {
                    retryCount = Convert.ToInt32(value);
                }

                retryCount++;

                var delay = retryCount switch
                {
                    1 => 5000,
                    2 => 30000,
                    3 => 300000,
                    _ => 0
                };

                var props = new BasicProperties
                {
                    Persistent = true,
                    Headers = new Dictionary<string, object?>
                    {
                        { "x-retry-count", retryCount }
                    }
                };

                if (delay > 0)
                {
                    var retryQueue = $"retry-{delay}";

                    await channel.QueueDeclareAsync(
                        queue: retryQueue,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: new Dictionary<string, object?>
                        {
                            { "x-message-ttl", delay },
                            { "x-dead-letter-exchange", "" },
                            { "x-dead-letter-routing-key", MainQueue }
                        }
                    );

                    _logger.LogWarning("🔁 Retry {Retry} after {Delay}ms", retryCount, delay);

                    await channel.BasicPublishAsync(
                        exchange: "",
                        routingKey: retryQueue,
                        mandatory: false,
                        basicProperties: props,
                        body: ea.Body
                    );
                }
                else
                {
                    _logger.LogError("💀 Send to DLQ");

                    await channel.BasicPublishAsync(
                        exchange: "",
                        routingKey: DlqQueue,
                        mandatory: false,
                        basicProperties: props,
                        body: ea.Body
                    );
                }

                await channel.BasicAckAsync(ea.DeliveryTag, false);
            }
        };

        await channel.BasicConsumeAsync(
            queue: MainQueue,
            autoAck: false,
            consumer: consumer
        );

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}