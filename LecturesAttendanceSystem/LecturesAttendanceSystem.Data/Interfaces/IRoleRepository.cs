using System.Collections.Generic;
using System.Threading.Tasks;
using LecturesAttendanceSystem.Data.Entities;

namespace LecturesAttendanceSystem.Data.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> GetRole(int roleId);
        Task<ICollection<Role>> GetAllRoles();
    }
}