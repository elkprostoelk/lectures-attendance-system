using System.Collections.Generic;

namespace LecturesAttendanceSystem.Data.Entities
{
    public class User
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public System.DateTime RegisteredOn { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string HashingSalt { get; set; }
        
        public string PasswordHash { get; set; }
        
        public int RoleId { get; set; }
        
        public Role Role { get; set; }

        public ICollection<Lesson> Lessons { get; set; }

        public bool IsTeacher => Role.Name == "teacher";

        public bool IsStudent => Role.Name == "student";
        
        public ICollection<LessonParticipant> LessonParticipants { get; set; }
    }
}