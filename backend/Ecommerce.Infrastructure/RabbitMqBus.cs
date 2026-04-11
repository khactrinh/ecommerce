// using System.Text;
// using System.Text.Json;
// using Ecommerce.Application.Interfaces;
//
// namespace Ecommerce.Infrastructure;
//
// public class RabbitMqBus : IMessageBus
// {
//     private readonly IConnection _connection;
//
//     public RabbitMqBus()
//     {
//         var factory = new ConnectionFactory { HostName = "localhost" };
//         _connection = factory.CreateConnection();
//     }
//
//     public Task PublishAsync(string queue, object message)
//     {
//         using var channel = _connection.CreateModel();
//
//         channel.QueueDeclare(queue, durable: true, exclusive: false);
//
//         var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
//
//         channel.BasicPublish(
//             exchange: "",
//             routingKey: queue,
//             body: body);
//
//         return Task.CompletedTask;
//     }
// }