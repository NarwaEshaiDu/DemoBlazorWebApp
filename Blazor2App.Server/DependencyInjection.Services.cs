using Blazor2App.Application;
using Blazor2App.Application.Bus;
using Blazor2App.Application.Repositories;
using Blazor2App.Database.Base;
using Blazor2App.Database.Entities;
using Blazor2App.Repository.Repositories;
using Blazor2App.ServiceBus.Infra;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
namespace Blazor2App.Server
{
    public static partial class DependencyInjection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDatabaseContext, DataContext>();
            services.AddScoped<IStudentRepository, StudentRepository>();


            services.AddSingleton<IBusPublisher, BusPublisher>();
        }
    }
}
