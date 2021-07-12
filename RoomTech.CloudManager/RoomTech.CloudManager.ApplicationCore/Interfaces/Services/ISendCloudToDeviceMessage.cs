using Microsoft.Azure.Devices;
using System.Threading.Tasks;

namespace RoomTech.CloudManager.ApplicationCore.Interfaces.Services
{
    public interface ISendCloudToDeviceMessage
    {
        Task SendCloudToDeviceMessageAsync(string MessageSerialized, ServiceClient serviceClient, string targetDevice);
    }
}
