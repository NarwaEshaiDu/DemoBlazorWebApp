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
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(Infra.Mediator.LoggingBehavior<,>));
        }
    }
}
