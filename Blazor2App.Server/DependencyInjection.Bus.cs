﻿using Blazor2App.Application.Bus;
using Blazor2App.ConsumerDemo.Features;
using Blazor2App.ServiceBus;
using Blazor2App.ServiceBus.Infra;
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

                var entryAssembly = Assembly.GetAssembly(typeof(Worker));

                x.AddSagaStateMachines(entryAssembly);
                x.AddSagas(entryAssembly);
                x.AddActivities(entryAssembly);

                x.UsingAzureServiceBus((context, cfg) =>
                {
                    //cfg.UseSendFilter(typeof(BusMessageAuthFilter<>), context);
                    cfg.AutoStart = true;
                    cfg.Host(host);

                    MessageCorrelation.UseCorrelationId<BaseMessage>(msg => msg.CorrelationId);

                    IEndpointNameFormatter endpointNameFormatter = new KebabCaseEndpointNameFormatter(true);
                    IEntityNameFormatter entityNameFormatter = cfg.MessageTopology.EntityNameFormatter;
                    cfg.MessageTopology.SetEntityNameFormatter(entityNameFormatter);

                    cfg.ConfigureEndpoints(context, endpointNameFormatter);
                });

                x.AddConsumer<DemoConsumer>();

                services.AddHostedService<Worker>();
            });
        }
    }
}
