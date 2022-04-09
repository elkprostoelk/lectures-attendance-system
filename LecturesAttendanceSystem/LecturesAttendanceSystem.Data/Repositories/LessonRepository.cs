using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LecturesAttendanceSystem.Data.Entities;
using LecturesAttendanceSystem.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Lesson> GetLesson(long lessonId) =>
            await _context.Lessons
                .Include(l => l.Participants)
                .SingleOrDefaultAsync(l => l.Id == lessonId);

        public async Task UpdateLesson(Lesson lesson)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            _context.Lessons.Update(lesson);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        public async Task RemoveLesson(Lesson lesson)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        public async Task<ICollection<Lesson>> GetLessons(DateTime limitDate) => 
            await _context.Lessons
                .Include(l => l.LessonParticipants)
                    .ThenInclude(lp => lp.Participant)
                    .ThenInclude(p => p.Role)
                .Where(l =>
                    l.ScheduledOn <= DateTime.Now
                    && l.ScheduledOn >= limitDate)
                .ToListAsync();
    }
}