using System;
using LecturesAttendanceSystem.Data.Enums;

namespace LecturesAttendanceSystem.Services.Dtos
{
    public class LessonDTO
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string LessonType { get; set; }
        
        public string TeacherName { get; set; }
        
        public DateTime ScheduledOn { get; set; }
    }
}