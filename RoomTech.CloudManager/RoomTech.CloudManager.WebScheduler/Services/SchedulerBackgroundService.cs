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

namespace RoomTech.CloudManager.WebScheduler.Services
{
    public class SchedulerBackgroundService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<SchedulerBackgroundService> _logger;
        private Timer _timer;
        private readonly Microsoft.Extensions.DependencyInjection.IServiceScopeFactory _serviceScopeFactory;
        private IConfiguration _configuration;
        private readonly ISendCloudToDeviceMessage _sendCloudToDeviceMessage;
        private readonly ServiceClient _serviceClient;

        public SchedulerBackgroundService(
            ILogger<SchedulerBackgroundService> logger,
            Microsoft.Extensions.DependencyInjection.IServiceScopeFactory serviceScopeFactory,
            IConfiguration configuration,
            ISendCloudToDeviceMessage sendCloudToDeviceMessage)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;
            _sendCloudToDeviceMessage = sendCloudToDeviceMessage;
            _serviceClient = ServiceClient.CreateFromConnectionString(_configuration["iotHubConnectionString"]);
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(8)); // 8 secondi => manda mex
            var scope = _serviceScopeFactory.CreateScope();
            var lessonService = scope.ServiceProvider.GetRequiredService<ILessonRepository>();
            var classroomService = scope.ServiceProvider.GetRequiredService<IClassroomRepository>();
            var interval = Environment.GetEnvironmentVariable("WorkerSchedulerInterval");
            while (true)
            {
                var timeParts = interval.Split(new char[1] { ':' });
                var dateNow = DateTime.Now;
                var date = new DateTime(
                    dateNow.Year,
                    dateNow.Month,
                    dateNow.Day,
                    int.Parse(timeParts[0]),
                    int.Parse(timeParts[1]),
                    int.Parse(timeParts[2]));
                TimeSpan ts;
                if (date > dateNow)
                    ts = date - dateNow;
                else
                {
                    date = date.AddDays(1);
                    ts = date - dateNow;
                }
                Task.Delay(ts, stoppingToken).
                    ContinueWith((x) =>
                    SendJson()
                    , stoppingToken);
                return Task.CompletedTask;
            }
        }

        private void DoWork(object state)
        {

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

        public async Task SendJson()
        {
            var scope = _serviceScopeFactory.CreateScope();
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
                await _sendCloudToDeviceMessage.SendCloudToDeviceMessageAsync(
                    JsonConvert.SerializeObject(result),
                    _serviceClient,
                    "pablo-device-one").ConfigureAwait(false);
                result.Clear();
            }
        }
    }
}
