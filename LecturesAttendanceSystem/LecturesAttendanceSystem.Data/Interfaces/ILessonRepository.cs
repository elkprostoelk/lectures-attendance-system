using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LecturesAttendanceSystem.Data.Entities;

namespace LecturesAttendanceSystem.Data.Interfaces
{
    public interface ILessonRepository
    {
        Task AddLesson(Lesson lesson);
        Task<Lesson> GetLesson(long lessonId);
        Task UpdateLesson(Lesson lesson);
        Task RemoveLesson(Lesson lesson);
        Task<ICollection<Lesson>> GetLessons(DateTime limitDate);
    }
}