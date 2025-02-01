using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentManagement.Model
{
    public class BaseModelDto
    {
        public string? CreatedBy { get; set; }
        public virtual AppUser? CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }
        public virtual AppUser? UpdatedByUser { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}