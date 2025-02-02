using System.Security.Claims;
using AppointmentManagement.Model;
using AutoMapper;
using ClickErp.Api.IRepository;

namespace AppointmentManagement.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private string _userId = string.Empty;
        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            var i = httpContextAccessor.HttpContext!.User;
            var dd = httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Name);
            _userId = httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        }
        public async Task<IEnumerable<DoctorDto>> GetAllDoctors()
        {
            var doctors = await _unitOfWork.Doctor.GetAllAsync();
            var res = _mapper.Map<IEnumerable<DoctorDto>>(doctors);
            return res;
        }
        public async Task<DoctorDto> GetDoctorById(string id)
        {
            var doctors = await _unitOfWork.Doctor.GetAsync(d => d.Id.ToString() == id);
            var res = _mapper.Map<DoctorDto>(doctors);
            return res;
        }
        public async Task<DoctorDto> Save(DoctorDto dto)
        {
            dto.CreatedBy = _userId;
            dto.CreatedDate = DateTime.Now;
            var doctor = _mapper.Map<Doctor>(dto);
            await _unitOfWork.Doctor.AddAsync(doctor);
            await _unitOfWork.CommitAsync();
            var res = _mapper.Map<DoctorDto>(doctor);
            return res;
        }
        public async Task<DoctorDto> Update(string id, DoctorDto dto)
        {
            dto.UpdatedBy = _userId;
            dto.UpdatedDate = DateTime.Now;

            var doctor = await _unitOfWork.Doctor.GetAsync(d => d.Id.ToString() == id);
            if (doctor == null)
            {
                throw new Exception("This doctor not found");
            }
            dto.CreatedBy = doctor.CreatedBy;
            dto.CreatedDate = doctor.UpdatedDate;

            doctor = _mapper.Map(dto, doctor);

            _unitOfWork.Doctor.Update(doctor);
            await _unitOfWork.CommitAsync();

            var res = _mapper.Map<DoctorDto>(doctor);
            return res;
        }

        public async Task Delete(string id)
        {
            if (id == null || id == "")
            {
                throw new Exception("Invalid request.");
            }

            var doctor = await _unitOfWork.Doctor.GetAsync(d => d.Id.ToString() == id);
            if (doctor == null)
            {
                throw new Exception("This doctor not found");
            }

            _unitOfWork.Doctor.Remove(doctor);
            await _unitOfWork.CommitAsync();
        }
    }
}