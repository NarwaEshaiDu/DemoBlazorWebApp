using Blazor2App.Database.Configuration;
using Blazor2App.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blazor2App.Database.Base
{
    public class DataContext : DbContext, IDatabaseContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DataContext()
        {
            Context = this;
        }
        public DbContext Context { get; }

        public DbSet<StudentEntity> StudentEntities { get; set; }
        public DbSet<BookEntity> BookEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyConfiguration(modelBuilder);
        }
        private void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BookEntityConfiguration());
        }
    }
}
