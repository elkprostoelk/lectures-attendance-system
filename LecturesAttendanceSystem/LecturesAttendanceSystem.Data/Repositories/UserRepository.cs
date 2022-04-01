using System.Threading.Tasks;
using LecturesAttendanceSystem.Data.Entities;
using LecturesAttendanceSystem.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LecturesAttendanceSystem.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AttendanceSystemDbContext _context;
        
        public UserRepository(
            AttendanceSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExists(string name)
        {
            return await _context.Users.AnyAsync(u => u.Name == name);
        }

        public async Task AddUser(User user)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
    }
}