namespace LecturesAttendanceSystem.Services.Dtos
{
    public class NewUserDTO
    {
        public string Name { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Password { get; set; }
        
        public int RoleId { get; set; }
    }
}