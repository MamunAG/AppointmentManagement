using System.Security.Claims;
using AppointmentManagement.Model;
using AutoMapper;
using ClickErp.Api.IRepository;

namespace AppointmentManagement.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private string _userId = string.Empty;
        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userId = httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        }
        public async Task<IEnumerable<AppointmentDto>> GetAllAppointments()
        {
            var appointments = await _unitOfWork.Appointment.GetAllAppointment();
            var res = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
            return res;
        }
        public async Task<AppointmentDto> GetAppointmentById(int id)
        {
            var appointments = await _unitOfWork.Appointment.GetAppointmentById(id);
            if (appointments == null)
            {
                throw new Exception("This appointment not found.");
            }
            var res = _mapper.Map<AppointmentDto>(appointments);
            return res;
        }
        public async Task<AppointmentDto> Save(AppointmentDto dto)
        {

            var doctor = await _unitOfWork.Doctor.GetAsync(_ => _.Id.ToString().Equals(dto.DoctorId));
            if (doctor == null)
            {
                throw new Exception("This doctor is not available.");
            }

            dto.CreatedBy = _userId;
            dto.CreatedDate = DateTime.Now;
            var appointment = _mapper.Map<Appointment>(dto);
            appointment.Doctors = null;
            appointment.CreatedByUser = null;
            appointment.UpdatedByUser = null;
            await _unitOfWork.Appointment.AddAsync(appointment);
            await _unitOfWork.CommitAsync();
            var res = _mapper.Map<AppointmentDto>(appointment);
            return res;
        }
        public async Task<AppointmentDto> Update(int id, AppointmentDto dto)
        {
            var doctor = await _unitOfWork.Doctor.GetAsync(_ => _.Id.ToString().Equals(dto.DoctorId));
            if (doctor == null)
            {
                throw new Exception("This doctor is not available.");
            }

            dto.UpdatedBy = _userId;
            dto.UpdatedDate = DateTime.Now;

            var appointment = await _unitOfWork.Appointment.GetAppointmentById(id);
            if (appointment == null)
            {
                throw new Exception("This appointment not found");
            }
            dto.CreatedBy = appointment.CreatedBy;
            dto.CreatedDate = appointment.CreatedDate;

            appointment = _mapper.Map(dto, appointment);
            appointment.Doctors = null;
            appointment.CreatedByUser = null;
            appointment.UpdatedByUser = null;
            _unitOfWork.Appointment.Update(appointment);
            await _unitOfWork.CommitAsync();

            var res = _mapper.Map<AppointmentDto>(appointment);
            return res;
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Invalid request.");
            }

            var appointment = await _unitOfWork.Appointment.GetAppointmentById(id);
            if (appointment == null)
            {
                throw new Exception("This appointment not found");
            }

            _unitOfWork.Appointment.Remove(appointment);
            await _unitOfWork.CommitAsync();
        }
    }
}