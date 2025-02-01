using AppointmentManagement.lib;
using AppointmentManagement.Model;
using ClickErp.Api.Repository;

namespace AppointmentManagement.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly DatabaseContext context;

        public AppointmentRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }
    }
}