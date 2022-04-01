using System.Threading.Tasks;
using LecturesAttendanceSystem.Data.Entities;

namespace LecturesAttendanceSystem.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UserExists(string name);
        Task AddUser(User user);
        Task<User> GetUser(string userName);
    }
}