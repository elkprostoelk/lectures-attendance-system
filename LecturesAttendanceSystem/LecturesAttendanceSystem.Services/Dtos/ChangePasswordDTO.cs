namespace LecturesAttendanceSystem.Services.Dtos
{
    public class ChangePasswordDTO
    {
        public string CurrentPassword { get; set; }
        
        public string NewPassword { get; set; }
    }
}