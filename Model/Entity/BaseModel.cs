using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentManagement.Model
{
    public class BaseModel
    {
        [ForeignKey(nameof(CreatedByUser))]
        public required string CreatedBy { get; set; }
        public virtual AppUser? CreatedByUser { get; set; }


        public DateTime CreatedDate { get; set; }


        [ForeignKey(nameof(UpdatedByUser))]
        public string? UpdatedBy { get; set; }
        public virtual AppUser? UpdatedByUser { get; set; }


        public DateTime UpdatedDate { get; set; }
    }
}