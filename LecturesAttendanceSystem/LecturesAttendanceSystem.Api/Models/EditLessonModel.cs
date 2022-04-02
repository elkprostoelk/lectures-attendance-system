using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LecturesAttendanceSystem.Api.Models
{
    public class EditLessonModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public DateTime ScheduledOn { get; set; }
        
        [Required]
        public ICollection<long> Participants { get; set; }
    }
}