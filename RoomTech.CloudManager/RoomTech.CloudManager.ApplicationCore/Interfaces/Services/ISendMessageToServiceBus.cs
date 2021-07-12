using System.Threading.Tasks;

namespace RoomTech.CloudManager.ApplicationCore.Interfaces.Services
{
    public interface ISendMessageToServiceBus
    {
        Task SendMessage(string connectionString, int numberOfMessagesToSend, object messageToSend);
    }
}
