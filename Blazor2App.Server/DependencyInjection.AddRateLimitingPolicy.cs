using Polly;

namespace Blazor2App.Server
{
    public partial class DependencyInjection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddRateLimitingPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            var rateLimitPolicy = Policy
                        .Handle<HttpRequestException>()  // Adjust based on your needs
                        .WaitAndRetryAsync(10, retryAttempt => TimeSpan.FromSeconds(6));

            services.AddSingleton(rateLimitPolicy);
        }
    }
}
