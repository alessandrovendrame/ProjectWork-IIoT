using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RoomTech.CloudManager.ApplicationCore.Interfaces.Repositories;
using RoomTech.CloudManager.ApplicationCore.Interfaces.Services;
using RoomTech.CloudManager.Infrastructure.Persistence.DatabaseContext;
using RoomTech.CloudManager.Infrastructure.Persistence.Repositories;
using RoomTech.CloudManager.Infrastructure.Services;
using RoomTech.CloudManager.Web.Services;

namespace RoomTech.CloudManager.Web
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
            services.AddRazorPages();
            services.AddMvc().AddRazorPagesOptions(options => options.Conventions.AddPageRoute("/Accesso/Accesso", ""));
            services.AddScoped<IClassroomRepository, ClassroomRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddSingleton<IReceiveMessageFromServiceBus, ReceiveMessageFromServiceBus>();
            services.AddSingleton<ISendMessageToServiceBus, SendMessageToServiceBus>();
            services.AddSingleton<ISendCloudToDeviceMessage, SendCloudToDeviceMessage>();
            services.AddSingleton<ISendGmailEmail, SendGmailEmail>();
            services.AddSingleton<IReadFromXml, ReadFromXml>();
            services.AddHostedService<AzureBackgroundService>();
            services.AddDbContext<RoomTechDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("SqlConnectionString")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
