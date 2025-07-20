using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.Property(p => p.LessonCode)
             .HasMaxLength(3)
             .IsRequired();

            builder.Property(p => p.LessonName)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(p => p.Class)
                .HasPrecision(2, 0)
                .IsRequired();

            builder.Property(p => p.TeacherFirstName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.TeacherLastName)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasMany(l => l.Students)
                .WithMany(s => s.Lessons);
        }
    }
}