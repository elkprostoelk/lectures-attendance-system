using AutoMapper;
using LecturesAttendanceSystem.Api.Models;
using LecturesAttendanceSystem.Services.Dtos;

namespace LecturesAttendanceSystem.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NewUserModel, NewUserDTO>();
            CreateMap<LoginModel, LoginDTO>();
        }
    }
}