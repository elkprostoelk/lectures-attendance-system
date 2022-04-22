namespace LecturesAttendanceSystem.Services.Dtos
{
    public class UserForAdminPanelDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string FullName { get; set; }
        
        public string Role { get; set; }
        
        public System.DateTime RegisteredOn { get; set; }
    }
}