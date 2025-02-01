using System.ComponentModel.DataAnnotations;

namespace AppointmentManagement.Model
{
    public class Doctor : BaseModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }
    }
}