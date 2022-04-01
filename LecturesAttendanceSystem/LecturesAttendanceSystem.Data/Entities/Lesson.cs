using System;
using System.Collections.Generic;

namespace LecturesAttendanceSystem.Data.Entities
{
    public class Lesson
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public DateTime ScheduledOn { get; set; }
        
        public ICollection<User> Participants { get; set; }
        
        public ICollection<User> PresentParticipants { get; set; }
    }
}