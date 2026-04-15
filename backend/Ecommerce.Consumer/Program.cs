using Ecommerce.Application.Common.Messaging;
using Ecommerce.Consumer.Application.Dispatchers;
using Ecommerce.Consumer.Application.Handlers;
using Ecommerce.Consumer.Application.Interfaces;
using Ecommerce.Consumer.BackgroundJobs;
using Ecommerce.Consumer.Infrastructure.Persistence;
using Ecommerce.Infrastructure.Messaging;
using Microsoft.EntityFrameworkCore;

//using Ecommerce.Consumer.Messaging;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<RabbitMqConsumer>();

builder.Services.AddScoped<IInboxRepository, InboxRepository>();
builder.Services.AddScoped<ProductCreatedHandler>();

builder.Services.AddHostedService<RabbitMqConsumer>();

builder.Services.AddScoped<IInboxRepository, InboxRepository>();

builder.Services.AddScoped<IEventHandler, ProductCreatedHandler>();

builder.Services.AddScoped<EventDispatcher>();

builder.Services.AddHostedService<RabbitMqConsumer>();

builder.Services.AddScoped<IRabbitMqPublisher, RabbitMqPublisher>();

builder.Services.AddDbContext<ConsumerDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

builder.Services.AddScoped<IInboxRepository, InboxRepository>();

var host = builder.Build();
host.Run();