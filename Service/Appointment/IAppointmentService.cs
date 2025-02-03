using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentManagement.Model;

namespace AppointmentManagement.Service
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetAllAppointments();
        Task<AppointmentDto> GetAppointmentById(int id);
        Task<AppointmentDto> Save(AppointmentDto dto);
        Task<AppointmentDto> Update(int id, AppointmentDto dto);
        Task Delete(int id);
    }
}