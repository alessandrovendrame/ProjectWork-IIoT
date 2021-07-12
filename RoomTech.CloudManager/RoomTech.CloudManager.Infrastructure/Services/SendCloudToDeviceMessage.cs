using Microsoft.Azure.Devices;
using RoomTech.CloudManager.ApplicationCore.Interfaces.Services;
using System.Text;
using System.Threading.Tasks;

namespace RoomTech.CloudManager.Infrastructure.Services
{
    public class SendCloudToDeviceMessage : ISendCloudToDeviceMessage
    {
        public async Task SendCloudToDeviceMessageAsync(string MessageSerialized, ServiceClient serviceClient, string targetDevice)
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes(MessageSerialized));
            await serviceClient.SendAsync(targetDevice, commandMessage);
        }
    }
}
