using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentManagement.Model.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagement.Service.Auth
{
    public interface IAccountService
    {
        Task Register(LoginUserDTO userDTO);
        Task<TokenRequest> Login([FromBody] LoginUserDTO userDTO);
        Task<TokenRequest> RefreshToken([FromBody] TokenRequest request);
    }
}