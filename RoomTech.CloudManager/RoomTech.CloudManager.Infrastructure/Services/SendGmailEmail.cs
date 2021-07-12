using EASendMail;
using Microsoft.Extensions.Configuration;
using RoomTech.CloudManager.ApplicationCore.Interfaces.Services;
using System;

namespace RoomTech.CloudManager.Infrastructure.Services
{
    public class SendGmailEmail : ISendGmailEmail
    {
        private readonly IConfiguration _configuration;
        private readonly IReadFromXml _readFromXml;
        public SendGmailEmail(IConfiguration configuration, IReadFromXml readFromXml)
        {
            _configuration = configuration;
            _readFromXml = readFromXml;
        }
        public void Send(string classroom)
        {
            SmtpMail oMail = new SmtpMail("TryIt");
            oMail.From = _readFromXml.Read().sender;
            oMail.To = _readFromXml.Read().recipient;
            oMail.Subject = $"Richiesta di assistenza - Aula {classroom}";
            oMail.TextBody = $"È richiesta assistenza nell'aula {classroom}";
            SmtpServer oServer = new SmtpServer("smtp.gmail.com");
            oServer.User = _configuration["Email"];
            oServer.Password = _configuration["Password"];
            oServer.Port = 587;
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
            SmtpClient oSmtp = new SmtpClient();
            oSmtp.SendMail(oServer, oMail);
        }
    }
}
