using Ecommerce.Application.Common.Events;
using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.Catalog.Products.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.Catalog.Products.EventHandlers;

public class ProductCreatedEventHandler 
    : INotificationHandler<DomainEventNotification<ProductCreatedEvent>>
{
    //private readonly IIntegrationEventService _service;
    
    private readonly ILogger<ProductCreatedEventHandler> _logger;
    private readonly IEmailService _emailService;
    private INotificationHandler<DomainEventNotification<ProductCreatedEvent>> _notificationHandlerImplementation;

    public ProductCreatedEventHandler(
        ILogger<ProductCreatedEventHandler> logger,
        IEmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    public async Task Handle(
        DomainEventNotification<ProductCreatedEvent> notification,
        CancellationToken cancellationToken)
    {
        // var integrationEvent = new ProductCreatedIntegrationEvent
        // {
        //     ProductId = notification.ProductId
        // };

        //await _service.AddToOutboxAsync(integrationEvent);
        
        var domainEvent = notification.DomainEvent;
        
        Console.WriteLine($"🔥 Product created: {domainEvent.ProductId}");
        
        _logger.LogInformation(
            "Product created with Id: {ProductId}",
            domainEvent.ProductId);
        
        // ✅ email (mock)
        await _emailService.SendAsync(
            "admin@shop.com",
            "New product created",
            $"Product Id: {domainEvent.ProductId}");
    }

    
}