using Blazor2App.Database.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Blazor2App.Migrator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets(Assembly.GetExecutingAssembly());

            var connectionString = builder.Build().GetConnectionString("DefaultConnection");

            var services = new ServiceCollection();
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
            var provider = services.BuildServiceProvider();
            Console.WriteLine($"ConnectionString :{connectionString}");
            var context = provider.GetRequiredService<DataContext>();
            var pendingMigrations = context.Database.GetPendingMigrations();
            Console.WriteLine($"Count Pending migrations :{pendingMigrations.Count()}");
            if (pendingMigrations.Any())
            {
                context.Database.Migrate();
                Console.WriteLine($"Migrations Applied OK");
            }
        }
    }
}
