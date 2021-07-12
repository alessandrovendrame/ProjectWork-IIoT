using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomTech.CloudManager.ApplicationCore.Interfaces.Services
{
    public interface IReadFromXml
    {
        public struct MailConfiguration { public string sender; public string recipient; };

        public MailConfiguration Read();
    }
}
