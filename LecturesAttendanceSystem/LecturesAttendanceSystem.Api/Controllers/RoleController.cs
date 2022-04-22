using System.Threading.Tasks;
using LecturesAttendanceSystem.Services.Dtos;
using LecturesAttendanceSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LecturesAttendanceSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(
            IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize(Roles = "administrator")]
        [HttpGet("all")]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _roleService.GetAllRoles();
            if (result.IsSuccessful)
            {
                return Ok(result.ResultObject);
            }
            foreach (var (key, value) in result.Errors)
            {
                ModelState.AddModelError(key, value);
            }
            return BadRequest(ModelState);
        }
    }
}