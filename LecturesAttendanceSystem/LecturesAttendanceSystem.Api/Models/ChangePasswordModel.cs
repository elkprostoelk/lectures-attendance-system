using System.ComponentModel.DataAnnotations;

namespace LecturesAttendanceSystem.Api.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string CurrentPassword { get; set; }
        
        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string NewPassword { get; set; }
    }
}