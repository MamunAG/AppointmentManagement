using AppointmentManagement.lib;
using AppointmentManagement.Model;
using ClickErp.Api.Repository;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManagement.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly DatabaseContext _context;

        public AppointmentRepository(DatabaseContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointment() =>
            await _context.Appointment.Include(_ => _.Doctors).ToListAsync();

        public async Task<Appointment> GetAppointmentById(int id) =>
            await _context.Appointment.Include(_ => _.Doctors).FirstOrDefaultAsync(_ => _.Id.Equals(id));
    }
}