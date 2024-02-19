using Blazor2App.Application.Bus;
using Blazor2App.Application.Repositories;
using Blazor2App.Application.Services;
using Blazor2App.Repository.Repositories;
using Blazor2App.ServiceBus.Infra;
using Blazor2App.Services.Features;
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
            services.AddScoped<IStudentRepository, StudentRepository>();

            services.AddTransient<IBusPublisher, BusPublisher>();

            services.AddScoped<IPokemonApiService, PokemonApiService>();
        }
    }
}
