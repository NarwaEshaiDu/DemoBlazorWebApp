using Microsoft.EntityFrameworkCore;

namespace Blazor2App.Database.Base
{
    public sealed class FakeDbContext : DataContext
    {
        public FakeDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var randomUniqueInMemoryDatabaseInstanceName = System.Guid.NewGuid().ToString();

            optionsBuilder.UseInMemoryDatabase(randomUniqueInMemoryDatabaseInstanceName);
        }

        public override void Dispose()
        {
            Database.EnsureDeleted();
        }
    }
}
