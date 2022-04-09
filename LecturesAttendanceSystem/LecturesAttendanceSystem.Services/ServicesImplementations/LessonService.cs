using System;
using System.Linq;
using System.Threading.Tasks;
using LecturesAttendanceSystem.Data.Entities;
using LecturesAttendanceSystem.Data.Interfaces;
using LecturesAttendanceSystem.Services.Dtos;
using LecturesAttendanceSystem.Services.Enums;
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

        public async Task<ServiceResult> EditLesson(long lessonId, EditLessonDTO editLessonDto)
        {
            var result = new ServiceResult();
            var lesson = await _lessonRepository.GetLesson(lessonId);
            if (lesson is null)
            {
                result.IsSuccessful = false;
                result.Errors.Add("LessonNotFound", "Lesson was not found!");
            }
            else
            {
                lesson.Name = editLessonDto.Name;
                lesson.ScheduledOn = editLessonDto.ScheduledOn;
                lesson.Participants = await _userRepository.GetUsers(editLessonDto.Participants);
                await _lessonRepository.UpdateLesson(lesson);
            }

            return result;
        }

        public async Task<ServiceResult> DeleteLesson(long lessonId)
        {
            var result = new ServiceResult();
            var lesson = await _lessonRepository.GetLesson(lessonId);
            if (lesson is null)
            {
                result.IsSuccessful = false;
                result.Errors.Add("LessonNotFound", "Lesson was not found!");
            }
            else
            {
                await _lessonRepository.RemoveLesson(lesson);
            }

            return result;
        }

        public async Task<ServiceResult> CountAbsences(AbsencePeriods duration, long? userId = null)
        {
            var result = new ServiceResult();
            var now = DateTime.Now;
            var limitDate = duration switch
            {
                AbsencePeriods.Daily => now.AddDays(-1),
                AbsencePeriods.Week => now.AddDays(-7),
                AbsencePeriods.Month => now.AddMonths(-1),
                AbsencePeriods.Year => now.AddYears(-1),
                _ => now
            };
            var filteredLessons = await _lessonRepository.GetLessons(limitDate);
            var absencesCount = filteredLessons.SelectMany(l => l.LessonParticipants
                        .Where(lp => lp.Participant.IsStudent && !lp.Present),
                    (l, lp) => new LessonParticipantDTO {LessonId = l.Id, ParticipantId = lp.ParticipantId})
                .GroupBy(lp => lp.ParticipantId, (partId, abs) =>
                    new AbsencesCountDTO {ParticipantId = partId, AbsencesCount = abs.Count()});
            result.ResultObject = absencesCount;
            if (!userId.HasValue)
            {
                return result;
            }

            var user = await _userRepository.GetUser(userId.Value);
            if (user is not null && user.IsStudent)
            {
                result.ResultObject = absencesCount.SingleOrDefault(ac => ac.ParticipantId == userId);
            }
            else
            {
                result.IsSuccessful = false;
                result.Errors.Add("UserNotFound", "User was not found!");
            }

            return result;
        }
    }
}