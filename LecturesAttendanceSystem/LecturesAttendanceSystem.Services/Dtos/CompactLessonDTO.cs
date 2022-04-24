namespace LecturesAttendanceSystem.Services.Dtos
{
    public class CompactLessonDTO
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string LessonType { get; set; }
        
        public string TeacherName { get; set; }
    }
}