using System.Threading.Tasks;
using LecturesAttendanceSystem.Services.Dtos;

namespace LecturesAttendanceSystem.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult> RegisterUser(NewUserDTO newUserDto);
        Task<ServiceResult> Login(LoginDTO loginDto);
        Task<ServiceResult> EditUser(long userId, EditUserDTO editUserDto);
        Task<ServiceResult> DeleteUser(long userId);
        Task<ServiceResult> ChangePassword(long userId, ChangePasswordDTO changePasswordDto);
        Task<ServiceResult> GetAllUsers();
        Task<ServiceResult> GetStudentsAndTeachers();
    }
}