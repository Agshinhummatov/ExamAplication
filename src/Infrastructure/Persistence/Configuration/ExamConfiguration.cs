using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.Property(p => p.StudentNumber)
           .HasPrecision(5, 0)
           .IsRequired();

            builder.Property(p => p.ExamDate)
                .HasConversion(
                    v => v.AddHours(4),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                .IsRequired();

            builder.Property(p => p.Grade)
                .HasPrecision(1, 0)
                .IsRequired();

            builder.HasMany(e => e.Students)
                .WithMany(s => s.Exams);

            builder.HasOne(e => e.Lesson)
                .WithMany(l => l.Exams)
                .HasForeignKey(e => e.LessonId)
                .IsRequired();
        }
    }
}