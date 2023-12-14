using Blazor2App.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazor2App.Database.Configuration
{
    public class BookEntityConfiguration : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.ToTable(Constants.Database.TableBook, Constants.Database.Schema);

            builder.Property<DateTime>("LastModified")
               .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(c => c.BookName)
             .IsRequired()
             .HasMaxLength(64);

            builder.HasOne(e => e.Student)
               .WithMany(e => e.Books);
        }
    }
}
