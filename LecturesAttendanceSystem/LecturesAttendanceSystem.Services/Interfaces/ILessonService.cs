using System.Threading.Tasks;
using LecturesAttendanceSystem.Services.Dtos;

namespace LecturesAttendanceSystem.Services.Interfaces
{
    public interface ILessonService
    {
        Task<ServiceResult> CreateLesson(NewLessonDTO newLessonDto);
        Task<ServiceResult> EditLesson(long lessonId, EditLessonDTO editLessonDto);
        Task<ServiceResult> DeleteLesson(long lessonId);
    }
}