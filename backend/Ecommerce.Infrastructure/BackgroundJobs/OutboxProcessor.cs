// using System.Text.Json;
// using Ecommerce.Application.Interfaces;
// using Ecommerce.Infrastructure.Persistence;
// using MediatR;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
//
// namespace Ecommerce.Infrastructure.BackgroundJobs;
//
// public class OutboxProcessor : BackgroundService
// {
//     private readonly IServiceProvider _serviceProvider;
//     private readonly IMessageBus _bus;
//
//     public OutboxProcessor(IServiceProvider serviceProvider)
//     {
//         _serviceProvider = serviceProvider;
//     }
//     
//     protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//     {
//         while (!stoppingToken.IsCancellationRequested)
//         {
//             using var scope = _serviceProvider.CreateScope();
//             var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//             var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
//
//             var messages = await context.OutboxMessages
//                 .Where(x => x.ProcessedOn == null)
//                 .OrderBy(x => x.OccurredOn)
//                 .Take(20)
//                 .ToListAsync(stoppingToken);
//
//             foreach (var message in messages)
//             {
//                 try
//                 {
//                     var type = Type.GetType($"Ecommerce.Domain.Events.{message.Type}");
//                     var domainEvent = JsonSerializer.Deserialize(message.Content, type!);
//
//                     await mediator.Publish((INotification)domainEvent!, stoppingToken);
//                     
//                     //TODO
//                     //await _bus.PublishAsync(message.Type, message.Content);
//                     
//                     message.ProcessedOn = DateTime.UtcNow;
//                 }
//                 catch (Exception ex)
//                 {
//                     message.Error = ex.Message;
//                 }
//             }
//
//             await context.SaveChangesAsync(stoppingToken);
//
//             await Task.Delay(2000, stoppingToken); // polling
//         }
//     }
// }