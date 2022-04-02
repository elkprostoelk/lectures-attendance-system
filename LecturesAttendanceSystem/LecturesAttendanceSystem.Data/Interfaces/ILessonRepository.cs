using System.Threading.Tasks;
using LecturesAttendanceSystem.Data.Entities;

namespace LecturesAttendanceSystem.Data.Interfaces
{
    public interface ILessonRepository
    {
        Task AddLesson(Lesson lesson);
    }
}