using Blazor2App.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blazor2App.Database.Configuration
{
    public class StudentEntityConfiguration : IEntityTypeConfiguration<StudentEntity>
    {
        public void Configure(EntityTypeBuilder<StudentEntity> builder)
        {
            builder.ToTable(Constants.Database.TableStudent, Constants.Database.Schema);

            //Shadow Property
            builder.Property<DateTime>("LastModified")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(64);

            builder.HasMany(e => e.Books)
                .WithOne(e => e.Student)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
