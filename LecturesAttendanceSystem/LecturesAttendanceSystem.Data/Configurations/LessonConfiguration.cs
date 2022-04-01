using LecturesAttendanceSystem.Data.Entities;
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
        }
    }
}