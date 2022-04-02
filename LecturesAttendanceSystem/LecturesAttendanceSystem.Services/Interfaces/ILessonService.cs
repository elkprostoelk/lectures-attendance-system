using System.Threading.Tasks;
using LecturesAttendanceSystem.Services.Dtos;

namespace LecturesAttendanceSystem.Services.Interfaces
{
    public interface ILessonService
    {
        Task<ServiceResult> CreateLesson(NewLessonDTO newLessonDto);
    }
}