using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using RoomTech.CloudManager.ApplicationCore.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace RoomTech.CloudManager.Infrastructure.Services
{
    public class SendMessageToServiceBus : ISendMessageToServiceBus
    {
        public async Task SendMessage(string connectionString, int numberOfMessagesToSend, object messageToSend)
        {
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                ServiceBusSender sender = client.CreateSender(Environment.GetEnvironmentVariable("topicName"));
                var resultToSend = JsonConvert.SerializeObject(messageToSend);
                await sender.SendMessageAsync(new ServiceBusMessage(resultToSend));
            }
        }
    }
}
