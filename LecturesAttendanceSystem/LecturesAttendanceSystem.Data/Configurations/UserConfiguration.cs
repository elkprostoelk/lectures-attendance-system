using LecturesAttendanceSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecturesAttendanceSystem.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.RegisteredOn)
                .IsRequired();

            builder.HasIndex(u => u.Name)
                .IsUnique();
            
            builder.Property(u => u.FirstName)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(u => u.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            builder.HasMany(u => u.Lessons)
                .WithMany(l => l.Participants)
                .UsingEntity<LessonParticipant>(j =>
                    j.HasOne(lp => lp.Lesson)
                        .WithMany(l => l.LessonParticipants)
                        .HasForeignKey(lp => lp.LessonId),

                    j =>
                    j.HasOne(lp => lp.Participant)
                        .WithMany(p => p.LessonParticipants)
                        .HasForeignKey(lp => lp.ParticipantId),
                    
                    j =>
                    {
                        j.HasKey(lp => new {lp.LessonId, lp.ParticipantId});
                        j.Property(lp => lp.Present)
                            .IsRequired()
                            .HasDefaultValue(false);
                        j.ToTable("LessonParticipants");
                    }
                );
        }
    }
}