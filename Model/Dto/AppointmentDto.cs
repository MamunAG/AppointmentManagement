using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentManagement.Model
{
    public class AppointmentDto : BaseModelDto
    {
        public int Id { get; set; }
        public string? PatientName { get; set; }
        public string? PatientContactInfo { get; set; }
        public DateTime AppointmentDate { get; set; }

        public Guid DoctorId { get; set; }
        public virtual Doctor? Doctors { get; set; }

    }
}