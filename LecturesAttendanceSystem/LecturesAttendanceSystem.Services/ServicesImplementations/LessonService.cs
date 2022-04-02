using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LecturesAttendanceSystem.Data.Entities;
using LecturesAttendanceSystem.Data.Interfaces;
using LecturesAttendanceSystem.Services.Dtos;
using LecturesAttendanceSystem.Services.Interfaces;

namespace LecturesAttendanceSystem.Services.ServicesImplementations
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IUserRepository _userRepository;

        public LessonService(
            ILessonRepository lessonRepository,
            IUserRepository userRepository)
        {
            _lessonRepository = lessonRepository;
            _userRepository = userRepository;
        }
        
        public async Task<ServiceResult> CreateLesson(NewLessonDTO newLessonDto)
        {
            var result = new ServiceResult();
            var participants = await _userRepository.GetUsers(newLessonDto.ParticipantIds);
            var lesson = new Lesson
            {
                Name = newLessonDto.Name,
                ScheduledOn = newLessonDto.ScheduledOn,
                Participants = participants
            };
            var containsTeacher = participants.Any(u => u.IsTeacher);
            var containsStudents = participants.Any(u => u.IsStudent);
            if (!containsTeacher)
            {
                result.IsSuccessful = false;
                result.Errors.Add("NoTeacher", "Lesson should contain a teacher!");
            }
            if (!containsStudents)
            {
                result.IsSuccessful = false;
                result.Errors.Add("NoStudents", "Lesson should contain at least one student!");
            }
            if (result.IsSuccessful)
            {
                await _lessonRepository.AddLesson(lesson);
            }
            
            return result;
        }
    }
}