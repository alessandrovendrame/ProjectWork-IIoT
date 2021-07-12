using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RoomTech.CloudManager.ApplicationCore.Interfaces.Repositories;
using RoomTech.CloudManager.ApplicationCore.Interfaces.Services;
using RoomTech.CloudManager.Infrastructure.Persistence.DatabaseContext;
using RoomTech.CloudManager.Infrastructure.Persistence.Repositories;
using RoomTech.CloudManager.Infrastructure.Services;
using RoomTech.CloudManager.WebScheduler.Services;

namespace RoomTech.CloudManager.WebScheduler
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RoomTechDbContext>(options =>
            options.UseSqlServer(Configuration["SqlConnectionString"]));
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IClassroomRepository, ClassroomRepository>();
            services.AddHostedService<SchedulerBackgroundService>();

            services.AddSingleton<ISendCloudToDeviceMessage, SendCloudToDeviceMessage>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
