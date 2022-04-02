using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> UserExists(string name) => 
            await _context.Users.AnyAsync(u => u.Name == name);

        public async Task AddUser(User user)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        public async Task<User> GetUser(string userName) => 
            await _context.Users
                .Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.Name == userName);

        public async Task<User> GetUser(long userId) =>
            await _context.Users
                .SingleOrDefaultAsync(u => u.Id == userId);

        public async Task UpdateUser(User user)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        public async Task RemoveUser(User user)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        public async Task<ICollection<User>> GetUsers(ICollection<long> participantIds) =>
            await _context.Users
                .Include(u => u.Role)
                .Where(u => participantIds.Contains(u.Id))
                .ToListAsync();
    }
}