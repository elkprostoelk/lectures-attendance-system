using LecturesAttendanceSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecturesAttendanceSystem.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasIndex(r => r.Name)
                .IsUnique();

            builder.HasData(
                new Role {Id = 1, Name = "administrator"},
                new Role {Id = 2, Name = "student"},
                new Role {Id = 3, Name = "teacher"}
            );
        }
    }
}