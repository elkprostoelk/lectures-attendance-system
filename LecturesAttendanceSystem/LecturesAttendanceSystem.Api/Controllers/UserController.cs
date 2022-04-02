using System.Threading.Tasks;
using AutoMapper;
using LecturesAttendanceSystem.Api.Models;
using LecturesAttendanceSystem.Services.Dtos;
using LecturesAttendanceSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LecturesAttendanceSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
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

        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <param name="newUserModel">User creating model</param>
        /// <returns>A newly created User</returns>
        /// <response code="201">Returns the newly created user</response>
        /// <response code="400">If the data is invalid or user already exists or role does not exist</response>
        /// <response code="500">Any exception thrown</response>
        [Authorize(Roles = "administrator")]
        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Edits a user.
        /// </summary>
        /// <param name="editUserModel">User editing model</param>
        /// <param name="userId">User ID</param>
        /// <returns>Empty result</returns>
        /// <response code="200">User is edited successfully</response>
        /// <response code="400">If the data is invalid or user/role does not exist</response>
        /// <response code="500">Any exception thrown</response>
        [Authorize(Roles = "administrator")]
        [HttpPut("{userId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Empty result</returns>
        /// <response code="204">User is deleted successfully</response>
        /// <response code="400">If the data is invalid or user/role does not exist or user tries to delete itself</response>
        /// <response code="500">Any exception thrown</response>
        [Authorize(Roles = "administrator")]
        [HttpDelete("{userId:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
            return BadRequest(ModelState);
        }
    }
}