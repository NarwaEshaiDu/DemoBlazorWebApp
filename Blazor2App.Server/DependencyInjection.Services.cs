using Blazor2App.Application.Bus;
using Blazor2App.Application.Repositories;
using Blazor2App.Application.Services;
using Blazor2App.Repository.Repositories;
using Blazor2App.ServiceBus.Infra;
using Blazor2App.Services.Features;
using Microsoft.Extensions.Caching.Memory;
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
            services.AddTransient<IBusPublisher, BusPublisher>();

            services.AddScoped<IPokemonApiService, PokemonApiService>();

            services.AddScoped<StudentRepository>();
            services.AddScoped<IStudentRepository>(provider =>
            {
                var studentRepository = provider.GetService<StudentRepository>();
                return new CachedStudentRepository(studentRepository, provider.GetService<IMemoryCache>()!);
            });
        }
    }
}
