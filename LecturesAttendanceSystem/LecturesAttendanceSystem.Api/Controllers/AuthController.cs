using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using LecturesAttendanceSystem.Api.Models;
using LecturesAttendanceSystem.Services.Dtos;
using LecturesAttendanceSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LecturesAttendanceSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public AuthController(
            IConfiguration configuration,
            IMapper mapper,
            IUserService userService)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userService = userService;
        }

        /// <summary>
        /// Log in.
        /// </summary>
        /// <param name="loginModel">Login model</param>
        /// <returns>JWT token</returns>
        /// <response code="200">User is logged in successfully</response>
        /// <response code="400">If the data is invalid or user/role does not exist</response>
        /// <response code="500">Any exception thrown</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var loginDto = _mapper.Map<LoginDTO>(loginModel);
            var result = await _userService.Login(loginDto);
            if (result.IsSuccessful && result.ResultObject is UserDTO user)
            {
                var token = GenerateToken(user.Name, user.Id, user.Role);
                return Ok(new {jwt = token});
            }
            foreach (var (key, value) in result.Errors)
            {
                ModelState.AddModelError(key, value);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Change user's password.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="changePasswordModel">Password changing model</param>
        /// <returns>Empty result</returns>
        /// <response code="200">User password has been changed successfully</response>
        /// <response code="400">If the data is invalid or user does not exist
        /// or user is not an administrator and tries to change other user's password</response>
        /// <response code="500">Any exception thrown</response>
        [Authorize]
        [HttpPatch("change-password/{userId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangePassword(long userId, ChangePasswordModel changePasswordModel)
        {
            var changePasswordDto = _mapper.Map<ChangePasswordDTO>(changePasswordModel);
            var result = await _userService.ChangePassword(userId, changePasswordDto);
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

        private string GenerateToken(string userName, long id, string role)
        {
            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(
                source: Convert.FromBase64String(_configuration["Jwt:PrivateKey"]),
                bytesRead: out _); 

            var signingCredentials = new SigningCredentials(
                key: new RsaSecurityKey(rsa),
                algorithm: SecurityAlgorithms.RsaSha256
            );
            var jwtDate = DateTime.Now;

            var jwt = new JwtSecurityToken(
                audience: "lecture-system",
                issuer: "lecture-system",
                claims: new Claim[] 
                { 
                    new Claim(ClaimTypes.NameIdentifier, id.ToString(), ClaimValueTypes.Integer64),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, role)
                },
                notBefore: jwtDate,
                expires: jwtDate.AddHours(24),
                signingCredentials: signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }
    }
}