using System;
using System.Collections.Generic;
using LecturesAttendanceSystem.Data.Enums;

namespace LecturesAttendanceSystem.Services.Dtos
{
    public class EditLessonDTO
    {
        public string Name { get; set; }
        
        public DateTime ScheduledOn { get; set; }
        
        public LessonTypes LessonType { get; set; }
        
        public ICollection<long> Participants { get; set; }
    }
}