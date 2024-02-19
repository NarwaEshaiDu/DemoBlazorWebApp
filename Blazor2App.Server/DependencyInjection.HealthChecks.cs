using Blazor2App.Database.Base;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace Blazor2App.Server
{
    public static partial class DependencyInjection
    {
        public static void RegisterHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                    .AddDbContextCheck<DataContext>();
        }
        public static Task WriteResponse(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";

            var obj = new
            {
                status = result.Status.ToString(),
                results = result.Entries.Select(pair => new
                {
                    source = pair.Key,
                    status = pair.Value.Status.ToString(),
                    description = pair.Value.Description
                })
            };

            return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(obj, Formatting.Indented));
        }
    }
}
