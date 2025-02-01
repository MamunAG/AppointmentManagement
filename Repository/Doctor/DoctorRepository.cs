using AppointmentManagement.lib;
using AppointmentManagement.Model;
using ClickErp.Api.Repository;

namespace AppointmentManagement.Repository
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        private readonly DatabaseContext context;

        public DoctorRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }
    }
}