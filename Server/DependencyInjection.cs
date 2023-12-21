using Asp.Versioning;
using Blazor2App.Application.Features.Schema;
using Blazor2App.Application.Features.Schema.Mutations;
using Blazor2App.Application.Features.Schema.Queries;
using Blazor2App.Application.Features.Students.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
namespace Blazor2App.Server
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class DependencyInjection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterServices(services, configuration);
            RegisterBus(services, configuration);
            RegisterLogger(services, configuration);
            RegisterSwagger(services, configuration);

            services.AddRazorPages();

          
    services.AddGraphQLServer()
            .AddQueryType<Query>()
            .AddMutationType<Mutation>();


            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(configuration.GetSection("AzureAd"));

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.Configure<JwtBearerOptions>(
               JwtBearerDefaults.AuthenticationScheme, options =>
               {
                   options.TokenValidationParameters.NameClaimType = "name";
               });
        }
    }
}
