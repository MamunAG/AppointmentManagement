using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentManagement.Model;
using FluentValidation;

namespace AppointmentManagement.Validations
{
    public class AppointmentEntityValidation : AbstractValidator<AppointmentDto>
    {
        public AppointmentEntityValidation()
        {
            RuleFor(x => x.PatientName).NotNull().NotEmpty().WithName("Patient Name").WithMessage("Please Input Patient Name.");
            RuleFor(x => x.PatientContactInfo).NotNull().NotEmpty().WithName("Contact Info").WithMessage("Please Input Patient Contact Info.");
            RuleFor(x => x.AppointmentDate).GreaterThan(DateTime.Now).WithName("Appointment Date").WithMessage("Please Input a valid Appointment Date.");
            RuleFor(x => x.DoctorId).NotNull().NotEmpty().WithName("Doctor").WithMessage("Please Select a Doctor.");
        }
    }
}