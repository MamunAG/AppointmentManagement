using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentManagement.Model
{
    public class Appointment : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public required string PatientName { get; set; }
        public required string PatientContactInfo { get; set; }
        public DateTime AppointmentDate { get; set; }


        [ForeignKey(nameof(Doctors))]
        public Guid DoctorId { get; set; }
        public virtual Doctor? Doctors { get; set; }

    }
}