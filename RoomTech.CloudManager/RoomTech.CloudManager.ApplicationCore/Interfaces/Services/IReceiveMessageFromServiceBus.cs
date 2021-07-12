using System;
using System.Threading.Tasks;

namespace RoomTech.CloudManager.ApplicationCore.Interfaces.Services
{
    public interface IReceiveMessageFromServiceBus
    {
        Task ReceiveMessages(Action<Object> processMessageFunc, string connectionString);
    }
}
