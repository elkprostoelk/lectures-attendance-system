using System.Linq;
using AutoMapper;
using LecturesAttendanceSystem.Api.Models;
using LecturesAttendanceSystem.Data.Entities;
using LecturesAttendanceSystem.Services.Dtos;

namespace LecturesAttendanceSystem.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NewUserModel, NewUserDTO>();
            CreateMap<LoginModel, LoginDTO>();
            CreateMap<ChangePasswordModel, ChangePasswordDTO>();
            CreateMap<NewLessonModel, NewLessonDTO>();
            CreateMap<EditLessonModel, EditLessonDTO>();
            CreateMap<User, UserForAdminPanelDTO>();
            CreateMap<Lesson, CompactLessonDTO>()
                .ForMember(dto => dto.TeacherName,
                    opt => opt.MapFrom(
                        l => l.Participants.SingleOrDefault(p => p.IsTeacher).FullName));
            CreateMap<Lesson, LessonDTO>()
                .ForMember(dto => dto.TeacherName,
                    opt => opt.MapFrom(
                        l => l.Participants.SingleOrDefault(p => p.IsTeacher).FullName));
            CreateMap<User, ShortUserDTO>()
                .ForMember(dto => dto.Name,
                    opt => opt.MapFrom(
                        u => $"{u.FirstName} {u.LastName}"));
        }
    }
}