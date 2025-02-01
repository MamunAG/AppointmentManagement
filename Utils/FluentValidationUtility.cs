using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentManagement.Utils
{
    public class FluentValidationUtility
    {
        public string GetErrorMessage(IList<FluentValidation.Results.ValidationFailure> Errors)
        {
            string msg = "";
            foreach (var item in Errors)
            {
                msg += item.PropertyName + ": " + item.ErrorMessage + Environment.NewLine;
            }
            return msg;
        }
    }
}