using System;
using System.Collections.Generic;

namespace LecturesAttendanceSystem.Services.Dtos
{
    public class EditLessonDTO
    {
        public string Name { get; set; }
        
        public DateTime ScheduledOn { get; set; }
        
        public ICollection<long> Participants { get; set; }
    }
}