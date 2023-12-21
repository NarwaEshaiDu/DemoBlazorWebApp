using Blazor2App.Database.Base;
using Blazor2App.ServiceBus;
using MassTransit;
using System.Reflection;

namespace Blazor2App.Server
{
    public static partial class DependencyInjection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void RegisterBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                var host = configuration["ConnectionStrings:Asb:Url"];

                x.SetInMemorySagaRepositoryProvider();

                //Different library class
                var entryAssembly = Assembly.GetAssembly(typeof(Worker));

                x.AddEntityFrameworkOutbox<RegistrationDbContext>(o =>
                {
                    o.QueryDelay = TimeSpan.FromSeconds(1);

                    o.UseSqlServer();
                    o.UseBusOutbox();
                });

                x.AddSagaStateMachines(entryAssembly);
                x.AddSagas(entryAssembly);
                x.AddActivities(entryAssembly);

                x.UsingAzureServiceBus((context, cfg) =>
                {
                    cfg.AutoStart = true;
                    cfg.Host(host);

                    IEndpointNameFormatter endpointNameFormatter = new KebabCaseEndpointNameFormatter(true);
                    IEntityNameFormatter entityNameFormatter = cfg.MessageTopology.EntityNameFormatter;
                    cfg.MessageTopology.SetEntityNameFormatter(entityNameFormatter);

                    cfg.ConfigureEndpoints(context, endpointNameFormatter);
                });

                x.AddConsumers(entryAssembly);

                services.AddHostedService<Worker>();
            });
        }
    }
}
