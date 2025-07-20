using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(p => p.StudentNumber)
                    .HasPrecision(5, 0)
                    .IsRequired();

            builder.Property(p => p.FirstName)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(p => p.LastName)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(p => p.Class)
                   .HasPrecision(2, 0)
                   .IsRequired();

            builder.HasMany(s => s.Lessons)
            .WithMany(l => l.Students);

            builder.HasMany(s => s.Exams)
               .WithMany(e => e.Students);
        }
    }
}