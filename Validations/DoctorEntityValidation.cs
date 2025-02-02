using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentManagement.Model;
using FluentValidation;

namespace AppointmentManagement.Validations
{
    public class DoctorEntityValidation : AbstractValidator<DoctorDto>
    {
        public DoctorEntityValidation()
        {
            RuleFor(x => x.Name)
                .NotNull().NotEmpty().WithName("Name").WithMessage("Please Input Name.")
                .MinimumLength(2).WithMessage("Name at least have 2 characters.");
        }
    }
}