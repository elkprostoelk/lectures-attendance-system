using System.Threading.Tasks;
using LecturesAttendanceSystem.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LecturesAttendanceSystem.Api
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task DatabaseEnsureCreated(this IApplicationBuilder app)
        {
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using var serviceScope = serviceScopeFactory.CreateScope();
            await using var dbContext = serviceScope.ServiceProvider.GetService<AttendanceSystemDbContext>();
            if (dbContext is not null)
            {
                await dbContext.Database.EnsureCreatedAsync();
            }
        }
    }
}