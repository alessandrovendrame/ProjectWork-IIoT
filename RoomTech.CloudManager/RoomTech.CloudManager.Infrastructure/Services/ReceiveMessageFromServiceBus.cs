using Azure.Messaging.ServiceBus;
using RoomTech.CloudManager.ApplicationCore.Interfaces.Repositories;
using RoomTech.CloudManager.ApplicationCore.Interfaces.Services;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace RoomTech.CloudManager.Infrastructure.Services
{
    public class ReceiveMessageFromServiceBus : IReceiveMessageFromServiceBus
    {
        private static readonly string _topicName = Environment.GetEnvironmentVariable("topicName");
        private static readonly string _subscriptionName = Environment.GetEnvironmentVariable("subscriptionName");
        private ServiceBusProcessor _processor;

        public async Task ReceiveMessages(Action<Object> processMessageFunc, string connectionString)
        {
            var client = new ServiceBusClient(connectionString);
            _processor = client.CreateProcessor(_topicName, _subscriptionName, new ServiceBusProcessorOptions());
            _processor.ProcessMessageAsync += async args =>
            {
                var body = args.Message.Body.ToString();
                var message = JsonSerializer.Deserialize<Object>(body);
                processMessageFunc.Invoke(message);
                await args.CompleteMessageAsync(args.Message);
            };
            _processor.ProcessErrorAsync += ErrorHandler;
            await _processor.StartProcessingAsync();
        }

        private static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
