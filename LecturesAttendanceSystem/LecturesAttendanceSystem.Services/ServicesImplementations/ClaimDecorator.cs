using System;
using System.Linq;
using System.Security.Claims;
using LecturesAttendanceSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LecturesAttendanceSystem.Services.ServicesImplementations
{
    public class ClaimDecorator : IClaimDecorator
    {
        public long Id =>
            Convert.ToInt64(_user.Claims.SingleOrDefault(c => 
                c.Type == ClaimTypes.NameIdentifier)?.Value);

        public string Name => _user.Claims.SingleOrDefault(c =>
            c.Type == ClaimTypes.Name)?.Value;

        public string Role => _user.Claims.SingleOrDefault(c =>
            c.Type == ClaimTypes.Role)?.Value;

        private readonly ClaimsPrincipal _user;

        public ClaimDecorator(IHttpContextAccessor httpContextAccessor)
        {
            _user = httpContextAccessor?.HttpContext?.User;
        }
    }
}