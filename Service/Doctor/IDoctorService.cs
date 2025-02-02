using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentManagement.Model;

namespace AppointmentManagement.Service
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDto>> GetAllDoctors();
        Task<DoctorDto> GetDoctorById(string id);
        Task<DoctorDto> Save(DoctorDto dto);
        Task<DoctorDto> Update(string id, DoctorDto dto);
        Task Delete(string id);
    }
}