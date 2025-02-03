using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManagement.Model
{
    [Index(nameof(Name), IsUnique = true)]
    public class Doctor : BaseModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }
    }
}