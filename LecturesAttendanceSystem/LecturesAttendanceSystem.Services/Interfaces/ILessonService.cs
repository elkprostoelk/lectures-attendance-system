using System.Threading.Tasks;
using LecturesAttendanceSystem.Services.Dtos;
using LecturesAttendanceSystem.Services.Enums;

namespace LecturesAttendanceSystem.Services.Interfaces
{
    public interface ILessonService
    {
        Task<ServiceResult> CreateLesson(NewLessonDTO newLessonDto);
        Task<ServiceResult> EditLesson(long lessonId, EditLessonDTO editLessonDto);
        Task<ServiceResult> DeleteLesson(long lessonId);
        Task<ServiceResult> CountAbsences(AbsencePeriods duration, long? userId = null);
    }
}