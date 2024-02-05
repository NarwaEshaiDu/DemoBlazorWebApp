using Asp.Versioning.ApiExplorer;
using Blazor2App.Database.Base;
using Blazor2App.Database.OutboxDb;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Blazor2App.Server
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        private static void Main(string[] args)
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