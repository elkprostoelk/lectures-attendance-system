using System.Threading.Tasks;
using LecturesAttendanceSystem.Data;
using LecturesAttendanceSystem.Data.Interfaces;
using LecturesAttendanceSystem.Services.Dtos;
using LecturesAttendanceSystem.Services.Interfaces;
using LecturesAttendanceSystem.Services.ServicesImplementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

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
                var httpAccessor = serviceScope.ServiceProvider.GetService<IHttpContextAccessor>();
                var claimDecorator = new ClaimDecorator(httpAccessor);
                var userRepository = serviceScope.ServiceProvider.GetService<IUserRepository>();
                var userService = serviceScope.ServiceProvider.GetService<IUserService>();
                if (userRepository is not null &&
                    userService is not null &&
                    !(await userRepository.UserExists("admin")))
                {
                    var adminUserDto = new NewUserDTO()
                    {
                        Name = "admin",
                        FirstName = "Administrator",
                        LastName = "Administrator",
                        Password = "THINKY7teeth",
                        RoleId = 1
                    };
                    var result = await userService.RegisterUser(adminUserDto);
                    if (result.IsSuccessful)
                    {
                        Log.Information("User \"admin\" is created successfully!");
                    }
                    else
                    {
                        Log.Error(string.Join("\n", "An error occured while creating the admin user", result.Errors.Values));
                    }
                }
            }
        }
    }
}