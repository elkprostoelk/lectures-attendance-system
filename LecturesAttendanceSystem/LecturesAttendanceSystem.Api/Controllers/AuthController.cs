using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
        
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        private string GenerateToken(string userName, int id, string role)
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