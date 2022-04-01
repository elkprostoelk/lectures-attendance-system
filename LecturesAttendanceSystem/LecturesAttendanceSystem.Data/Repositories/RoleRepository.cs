using System.Threading.Tasks;
using LecturesAttendanceSystem.Data.Entities;
using LecturesAttendanceSystem.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LecturesAttendanceSystem.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AttendanceSystemDbContext _context;

        public RoleRepository(AttendanceSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Role> GetRole(int roleId)
        {
            return await _context.Roles.SingleOrDefaultAsync(r => r.Id == roleId);
        }
    }
}