using Asp.Versioning.ApiExplorer;
using Blazor2App.Database.Base;
using Blazor2App.Database.OutboxDb;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Polly;
using Serilog;
namespace Blazor2App.Server
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.RegisterDependencies(builder.Configuration);
            builder.Host.UseSerilog();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<OutboxDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("OutBoxConnection"));
            });

            builder.Services.AddMemoryCache();
            builder.Services.AddResiliencePipeline<string, HttpResponseMessage>("my-pipeline", builder =>
            {
                builder.AddRetry(new()
                {
                    MaxRetryAttempts = 2,
                    ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                        .Handle<HttpRequestException>()
                        .HandleResult(response => response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                })
                .AddTimeout(TimeSpan.FromSeconds(2));
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.ApiVersion.ToString());
                }

                c.OAuthClientId($"{builder.Configuration["AzureAd:ClientId"]}");
                c.OAuthScopes(new[] { $"api://{builder.Configuration["AzureAd:ClientId"]}/Student.Read" });
                c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSerilogRequestLogging();

            app.MapHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = (check) => true,
                ResponseWriter = DependencyInjection.WriteResponse
            });

            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");
            app.MapGraphQL("/graphql");
            await app.RunAsync();
        }
    }
}