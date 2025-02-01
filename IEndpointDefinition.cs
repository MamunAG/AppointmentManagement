using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentManagement
{
    public interface IEndpointDefinition
    {
        void RegisterEndpoints(WebApplication app);
    }
}