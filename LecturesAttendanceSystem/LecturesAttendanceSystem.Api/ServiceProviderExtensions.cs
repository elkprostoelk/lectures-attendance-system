using System;
using System.Security.Cryptography;
using AutoMapper;
using LecturesAttendanceSystem.Data.Interfaces;
using LecturesAttendanceSystem.Data.Repositories;
using LecturesAttendanceSystem.Services.Interfaces;
using LecturesAttendanceSystem.Services.ServicesImplementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace LecturesAttendanceSystem.Api
{
    public static class ServiceProviderExtensions
    {
        public static void AddRsaAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(provider =>
            {
                var rsa = RSA.Create();
                rsa.ImportRSAPublicKey(
                    source: Convert.FromBase64String(configuration["Jwt:PublicKey"]),
                    bytesRead: out _);
                return new RsaSecurityKey(rsa);
            });
            services.AddAuthentication(auth=>
                {
                    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer("Bearer", options =>
                {
                    SecurityKey rsa = services.BuildServiceProvider().GetRequiredService<RsaSecurityKey>();
                    options.IncludeErrorDetails = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = rsa,
                        ValidAudience = "lecture-system",
                        ValidIssuer = "lecture-system",
                        RequireSignedTokens = true,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidateIssuer = true
                    };
                });
        }

        public static void AddServices(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddHttpContextAccessor();
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();

            services.AddScoped<IClaimDecorator, ClaimDecorator>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILessonService, LessonService>();
        }
    }
}