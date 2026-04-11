using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.Catalog.Products.EventHandlers;

public class ProductCreatedEventHandler 
    : INotificationHandler<ProductCreatedEvent>
{
    //private readonly IIntegrationEventService _service;
    
    private readonly ILogger<ProductCreatedEventHandler> _logger;
    private readonly IEmailService _emailService;

    public ProductCreatedEventHandler(
        ILogger<ProductCreatedEventHandler> logger,
        IEmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    public async Task Handle(
        ProductCreatedEvent notification,
        CancellationToken cancellationToken)
    {
        // var integrationEvent = new ProductCreatedIntegrationEvent
        // {
        //     ProductId = notification.ProductId
        // };

        //await _service.AddToOutboxAsync(integrationEvent);
        
        Console.WriteLine($"🔥 Product created: {notification.ProductId}");
        
        _logger.LogInformation(
            "Product created with Id: {ProductId}",
            notification.ProductId);
        
        // ✅ email (mock)
        await _emailService.SendAsync(
            "admin@shop.com",
            "New product created",
            $"Product Id: {notification.ProductId}");
    }
}