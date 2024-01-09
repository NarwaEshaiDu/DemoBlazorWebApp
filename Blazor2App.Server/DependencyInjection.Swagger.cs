using Blazor2App.Server.Infra.Options;
using Microsoft.OpenApi.Models;
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
        public static void RegisterSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureOptions<ConfigureSwaggerOptions>();

            services.AddSwaggerGen(c =>
            {
                var tokenUrl = $"{configuration["AzureAd:Instance"]}{configuration["AzureAd:TenantId"]}/oauth2/v2.0";

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow //use this flow when user connects to your API directly (url is same as tokenurl but with /authorize at the end instead of /token)
                        {
                            AuthorizationUrl = new Uri($"{tokenUrl}/authorize"),
                            TokenUrl = new Uri($"{tokenUrl}/token"),
                            Scopes = new Dictionary<string, string> { { $"api://{configuration["AzureAd:ClientId"]}/Student.Read", "" } }
                        }
                    }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                            {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            },
                            Scheme = "oauth2",
                            Name = "oauth2",
                            In = ParameterLocation.Header
                        },
                        Array.Empty<string>()
        }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
