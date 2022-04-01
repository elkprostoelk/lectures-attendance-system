using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using LecturesAttendanceSystem.Data.Entities;
using LecturesAttendanceSystem.Data.Interfaces;
using LecturesAttendanceSystem.Services.Dtos;
using LecturesAttendanceSystem.Services.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace LecturesAttendanceSystem.Services.ServicesImplementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult> RegisterUser(NewUserDTO newUserDto)
        {
            var result = new ServiceResult();
            var userExists = await _userRepository.UserExists(newUserDto.Name);
            if (userExists)
            {
                result.IsSuccessful = false;
                result.Errors.Add("UserExists", $"User {newUserDto.Name} already exists!");
            }
            else
            {
                var role = await _roleRepository.GetRole(newUserDto.RoleId);
                if (role is null)
                {
                    result.IsSuccessful = false;
                    result.Errors.Add("RoleNotExists", "Role does not exist!");
                }
                else
                {
                    var user = new User
                    {
                        Name = newUserDto.Name,
                        FirstName = newUserDto.FirstName,
                        LastName = newUserDto.LastName,
                        RoleId = role.Id
                    };
                    var hashedPassword = HashPassword(user, newUserDto.Password);
                    user.PasswordHash = hashedPassword;
                    await _userRepository.AddUser(user);
                    result.ResultObject = new UserDTO
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Role = role.Name
                    };
                }
            }
            return result;
        }

        public async Task<ServiceResult> Login(LoginDTO loginDto)
        {
            var user = await _userRepository.GetUser(loginDto.UserName);
            var hashedPassword = HashPassword(user, loginDto.Password);
            var result = new ServiceResult();
            if (user is null)
            {
                result.IsSuccessful = false;
                result.Errors.Add("UserNotExists", $"User {loginDto.UserName} doesn't exist!");
            }
            else
            {
                if (user.PasswordHash != hashedPassword)
                {
                    result.IsSuccessful = false;
                    result.Errors.Add("WrongPassword", "Wrong password!");
                }
                else
                {
                    if (user.RoleId != loginDto.RoleId)
                    {
                        result.IsSuccessful = false;
                        result.Errors.Add("NotInRole", $"User {loginDto.UserName} does not have this role!");
                    }
                    else
                    {
                        result.IsSuccessful = true;
                        result.ResultObject = new UserDTO
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Role = user.Role.Name
                        };
                    }
                }
            }
            return result;
        }

        public async Task<ServiceResult> EditUser(long userId, EditUserDTO editUserDto)
        {
            var result = new ServiceResult();
            var user = await _userRepository.GetUser(userId);
            if (user is null)
            {
                result.IsSuccessful = false;
                result.Errors.Add("UserNotExists", $"User does not exist!");
            }
            else
            {
                user = _mapper.Map<User>(editUserDto);
                await _userRepository.UpdateUser(user);
            }
            return result;
        }

        private string HashPassword(User user, string password)
        {
            const int hashSize = 256 / 8;
            byte[] salt;
            var isRegistered = user.Id != 0;
            if (isRegistered)
            {
                salt = Convert.FromBase64String(user.HashingSalt);
            }
            else
            {
                salt = new byte[hashSize];
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetNonZeroBytes(salt);
                }
                user.HashingSalt = Convert.ToBase64String(salt);
            }
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: hashSize));
            return hashed;
        }
    }
}