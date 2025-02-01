using System.ComponentModel.DataAnnotations;

namespace AppointmentManagement.Model
{
    public class DoctorDto : BaseModelDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}