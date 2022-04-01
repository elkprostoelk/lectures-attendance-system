using System.Threading.Tasks;
using AutoMapper;
using LecturesAttendanceSystem.Api.Models;
using LecturesAttendanceSystem.Services.Dtos;
using LecturesAttendanceSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LecturesAttendanceSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(
            IMapper mapper,
            IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [Authorize(Roles = "administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(NewUserModel newUserModel)
        {
            var newUserDto = _mapper.Map<NewUserDTO>(newUserModel);
            var result = await _userService.RegisterUser(newUserDto);
            if (result.IsSuccessful)
            {
                return Created(nameof(CreateUser), result.ResultObject);
            }
            foreach (var (key, value) in result.Errors)
            {
                ModelState.AddModelError(key, value);
            }
            return BadRequest();
        }

        [Authorize(Roles = "administrator")]
        [HttpPut("{userId:long}")]
        public async Task<IActionResult> EditUser(long userId, EditUserModel editUserModel)
        {
            var editUserDto = _mapper.Map<EditUserDTO>(editUserModel);
            var result = await _userService.EditUser(userId, editUserDto);
            if (result.IsSuccessful)
            {
                return Ok();
            }
            foreach (var (key, value) in result.Errors)
            {
                ModelState.AddModelError(key, value);
            }
            return BadRequest();
        }

        [Authorize(Roles = "administrator")]
        [HttpDelete("{userId:long}")]
        public async Task<IActionResult> DeleteUser(long userId)
        {
            var result = await _userService.DeleteUser(userId);
            if (result.IsSuccessful)
            {
                return NoContent();
            }
            foreach (var (key, value) in result.Errors)
            {
                ModelState.AddModelError(key, value);
            }
            return BadRequest();
        }
    }
}