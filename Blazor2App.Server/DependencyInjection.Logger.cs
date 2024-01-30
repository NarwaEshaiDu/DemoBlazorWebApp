using MediatR;
using Serilog.Core;
using Serilog;

namespace Blazor2App.Server
{
    public partial class DependencyInjection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void RegisterLogger(this IServiceCollection services, IConfiguration configuration)
        {
            var levelSwitch = new LoggingLevelSwitch(Serilog.Events.LogEventLevel.Information);
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.ControlledBy(levelSwitch)
               .WriteTo.Console(levelSwitch: levelSwitch).CreateLogger();

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(Infra.Mediator.LoggingBehavior<,>));
        }
    }
}
