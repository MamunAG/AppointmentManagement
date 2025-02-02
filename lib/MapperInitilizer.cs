using AppointmentManagement.Model;
using AppointmentManagement.Model.Dto;
using AutoMapper;

namespace AppointmentManagement.lib
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<AppUser, LoginUserDTO>().ReverseMap();
            CreateMap<Doctor, DoctorDto>().ReverseMap();
            CreateMap<Appointment, AppointmentDto>().ReverseMap();
        }
    }
}