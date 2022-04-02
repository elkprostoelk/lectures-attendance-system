using System.Threading.Tasks;
using LecturesAttendanceSystem.Data.Entities;
using LecturesAttendanceSystem.Data.Interfaces;

namespace LecturesAttendanceSystem.Data.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly AttendanceSystemDbContext _context;

        public LessonRepository(
            AttendanceSystemDbContext context)
        {
            _context = context;
        }
        
        public async Task AddLesson(Lesson lesson)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            await _context.Lessons.AddAsync(lesson);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
    }
}