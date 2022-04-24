using System;
using LecturesAttendanceSystem.Data.Entities;

namespace LecturesAttendanceSystem.Services.Dtos
{
    public class WorkWeekDTO
    {
        public DateTime LessonTime { get; set; }

        public CompactLessonDTO MondayLesson { get; set; }
        
        public CompactLessonDTO TuesdayLesson { get; set; }
        
        public CompactLessonDTO WednesdayLesson { get; set; }
        
        public CompactLessonDTO ThursdayLesson { get; set; }
        
        public CompactLessonDTO FridayLesson { get; set; }
    }
}