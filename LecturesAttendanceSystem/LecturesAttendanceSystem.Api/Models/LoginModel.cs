using System.ComponentModel.DataAnnotations;

namespace LecturesAttendanceSystem.Api.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string UserName { get; set; }
        
        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; }
    }
}