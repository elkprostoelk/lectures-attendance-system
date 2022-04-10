using LecturesAttendanceSystem.Data.Entities;
using LecturesAttendanceSystem.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecturesAttendanceSystem.Data.Configurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(l => l.Name);

            builder.Property(l => l.ScheduledOn)
                .IsRequired();

            builder.Property(l => l.LessonType)
                .IsRequired()
                .HasDefaultValue(LessonTypes.Lecture);
        }
    }
}