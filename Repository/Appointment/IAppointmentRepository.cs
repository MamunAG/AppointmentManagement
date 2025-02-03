using AppointmentManagement.Model;
using ClickErp.Api.IRepository;

namespace AppointmentManagement.Repository
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAllAppointment();
        Task<Appointment> GetAppointmentById(int id);
    }
}