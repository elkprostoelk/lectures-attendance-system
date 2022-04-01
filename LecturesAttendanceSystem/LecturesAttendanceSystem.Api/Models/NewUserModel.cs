using System.ComponentModel.DataAnnotations;

namespace LecturesAttendanceSystem.Api.Models
{
    public class NewUserModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string LastName { get; set; }
        
        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; }
        
        [Required]
        public int RoleId { get; set; }
    }
}