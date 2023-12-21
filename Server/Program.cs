using Asp.Versioning.ApiExplorer;
using Blazor2App.Database.Base;
using Blazor2App.Services;
using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

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

            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            builder.Services.AddDbContext<RegistrationDbContext>(x =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

                x.UseSqlServer(connectionString, options =>
                {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                    options.MigrationsHistoryTable($"__{nameof(RegistrationDbContext)}");

                    options.EnableRetryOnFailure(5);
                    options.MinBatchSize(1);
                });
            });

            builder.Services.AddScoped<IRegistrationService, RegistrationService>();

            builder.Services.AddSingleton<ILockStatementProvider, SqlServerLockStatementProvider>();

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
            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");
            app.MapGraphQL("/graphql");
            app.Run();
        }
    }
}