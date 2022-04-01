using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using LecturesAttendanceSystem.Api.Models;
using LecturesAttendanceSystem.Services.Dtos;
using LecturesAttendanceSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LecturesAttendanceSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpPost("login")]
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
            return BadRequest();
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