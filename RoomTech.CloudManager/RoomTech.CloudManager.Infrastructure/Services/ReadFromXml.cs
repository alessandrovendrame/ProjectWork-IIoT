using RoomTech.CloudManager.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RoomTech.CloudManager.Infrastructure.Services
{
    public class ReadFromXml : IReadFromXml
    {
        public struct MailConfiguration
        {
            public string sender;
            public string recipient;

        }
        public IReadFromXml.MailConfiguration Read()
        {
            IReadFromXml.MailConfiguration c;
            var configurationFile = @"C:\Users\Michele\Desktop\project-work\sln\RoomTech.CloudManager\RoomTech.CloudManager.Infrastructure\Persistence\Settings\mailSettings.xml";
            var xDoc = XDocument.Load(configurationFile);
            c.sender = xDoc.Descendants("MailSender").First().Value;
            c.recipient = xDoc.Descendants("MailRecipient").First().Value;

            return c;
        }
    }
}
