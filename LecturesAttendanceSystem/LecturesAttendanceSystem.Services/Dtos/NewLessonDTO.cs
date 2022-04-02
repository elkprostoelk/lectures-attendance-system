using System;
using System.Collections.Generic;

namespace LecturesAttendanceSystem.Services.Dtos
{
    public class NewLessonDTO
    {
        public string Name { get; set; }
        
        public DateTime ScheduledOn { get; set; }
        
        public ICollection<long> ParticipantIds { get; set; }
    }
}