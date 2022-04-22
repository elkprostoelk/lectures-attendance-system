using System.Collections.Generic;
using System.Threading.Tasks;
using LecturesAttendanceSystem.Data.Entities;
using LecturesAttendanceSystem.Data.Interfaces;
using LecturesAttendanceSystem.Services.Dtos;
using LecturesAttendanceSystem.Services.Interfaces;

namespace LecturesAttendanceSystem.Services.ServicesImplementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(
            IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        
        public async Task<ServiceResult> GetAllRoles()
        {
            var result = new ServiceResult();
            result.ResultObject = await _roleRepository.GetAllRoles();
            return result;
        }
    }
}