using System.Reflection;
using LecturesAttendanceSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LecturesAttendanceSystem.Data
{
    public class AttendanceSystemDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<Role> Roles { get; set; }
        
        public DbSet<Lesson> Lessons { get; set; }

        public AttendanceSystemDbContext(DbContextOptions<AttendanceSystemDbContext> options) : base(options)
        {   
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}