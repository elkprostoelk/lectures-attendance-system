using System;
using System.IO;
using LecturesAttendanceSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LecturesAttendanceSystem.Api
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AttendanceSystemDbContext>
    {
        public AttendanceSystemDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.{environment}.json", optional: false)
                .Build();
            var builder = new DbContextOptionsBuilder<AttendanceSystemDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new AttendanceSystemDbContext(builder.Options);
        }
    }
}