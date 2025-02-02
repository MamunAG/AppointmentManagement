using System.ComponentModel.DataAnnotations;

namespace AppointmentManagement.Model.Dto
{
    public class LoginUserDTO
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}