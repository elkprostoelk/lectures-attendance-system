using System.Threading.Tasks;
using LecturesAttendanceSystem.Services.Dtos;

namespace LecturesAttendanceSystem.Services.Interfaces
{
    public interface IRoleService
    {
        Task<ServiceResult> GetAllRoles();
    }
}