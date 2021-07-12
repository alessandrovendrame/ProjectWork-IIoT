using Microsoft.Azure.Devices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RoomTech.CloudManager.ApplicationCore.Interfaces.Repositories;
using RoomTech.CloudManager.ApplicationCore.Interfaces.Services;
using RoomTech.CloudManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Message = RoomTech.CloudManager.Domain.Entities.Message;

namespace RoomTech.CloudManager.Web.Services
{
    public class AzureBackgroundService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<AzureBackgroundService> _logger;
        private Timer _timer;
        private readonly Microsoft.Extensions.DependencyInjection.IServiceScopeFactory _serviceScopeFactory;
        private IConfiguration _configuration;
        private readonly ISendMessageToServiceBus _sendMessageToServiceBus;
        private readonly ISendCloudToDeviceMessage _sendCloudToDeviceMessage;
        private readonly ServiceClient _serviceClient;

        public AzureBackgroundService(
            ILogger<AzureBackgroundService> logger,
            Microsoft.Extensions.DependencyInjection.IServiceScopeFactory serviceScopeFactory,
            IConfiguration configuration,
            ISendMessageToServiceBus sendMessageToServiceBus,
            ISendCloudToDeviceMessage sendCloudToDeviceMessage)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;
            _sendMessageToServiceBus = sendMessageToServiceBus;
            _sendCloudToDeviceMessage = sendCloudToDeviceMessage;
            _serviceClient = ServiceClient.CreateFromConnectionString(_configuration["IotDeviceConnectionString"]);
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(8)); // 8 secondi => manda mex
            var scope = _serviceScopeFactory.CreateScope();
            var contextService = scope.ServiceProvider.GetRequiredService<IReceiveMessageFromServiceBus>();
            var messageService = scope.ServiceProvider.GetRequiredService<ISendGmailEmail>();

            contextService.ReceiveMessages(
                async result =>
                {
                    Console.WriteLine(result);
                    var obj = JsonConvert.DeserializeObject<Message>(result.ToString());
                    //messageService.Send(obj.Caller); //manda qui la mail il resto metti password e mail address su user secrets il resto variabili ambiente
                },
                _configuration["IotRoute"]);
            return Task.CompletedTask;
        }

        private void DoWork(object state) // forse ho sbagliato qualche dato no solito indirizzo ip
        {
            var count = Interlocked.Increment(ref executionCount);
            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);
            //var message = new Message("PicName", "AntonioStaChiamando");
            //var messageSerialized = JsonConvert.SerializeObject(message);
            //_sendMessageToServiceBus.SendMessage(_configuration["IotHubConnectionString"], 1, messageSerialized);
            /*var scope = _serviceScopeFactory.CreateScope();
            var lessonService = scope.ServiceProvider.GetRequiredService<ILessonRepository>();
            var classroomService = scope.ServiceProvider.GetRequiredService<IClassroomRepository>();
            var allLessons = lessonService.GetAllAsync().Result;
            var allClassrooms = classroomService.GetAllAsync().Result;
            List<string> uniqueFloor = lessonService.GetUniqueFloors(allClassrooms);
            List<Lesson> result = new List<Lesson>();
            foreach (var floor in uniqueFloor)
            {
                foreach (var lesson in allLessons)
                {
                    if (floor == lesson.Floor)
                    {
                        result.Add(lesson);
                    }
                }
                _sendCloudToDeviceMessage.SendCloudToDeviceMessageAsync(JsonConvert.SerializeObject(result), _serviceClient, "pablo-device-one");
                result.Clear();
            }*/
        }


        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

/*
 *
 *
 *             Dictionary<string, string> Data = new Dictionary<string, string>();
            Data.Add("Workstation1", "Device-Iot-One");
            Data.Add("Workstation2", "Device-Iot-Two");
            Data.Add("Workstation3", "Device-Iot-Three");
            Data.Add("Workstation4", "Device-Iot-Four");
            DataKeys = new List<string>();
            foreach (var item in Data)
            {
                DataKeys.Add(item.Key);
            }*/
