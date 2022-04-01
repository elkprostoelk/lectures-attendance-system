namespace LecturesAttendanceSystem.Services.Dtos
{
    public class LoginDTO
    {
        public string UserName { get; set; }
        
        public string Password { get; set; }
        
        public int RoleId { get; set; }
    }
}