using AppointmentManagement.lib;
using AppointmentManagement.Model;
using AppointmentManagement.Model.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagement.Service.Auth
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public AccountService(
            UserManager<AppUser> userManager,
            IMapper mapper,
            IAuthManager authManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _authManager = authManager;
        }

        public async Task Register(LoginUserDTO userDTO)
        {
            var user = _mapper.Map<AppUser>(userDTO);
            user.UserName = userDTO.Email;
            var result = await _userManager.CreateAsync(user, userDTO.Password!);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.FirstOrDefault()?.Description);
            }
        }

        public async Task<TokenRequest> Login([FromBody] LoginUserDTO userDTO)
        {
            if (!await _authManager.ValidateUser(userDTO))
            {
                throw new Exception("Unauthorized");
            }

            var tokenReq = new TokenRequest();
            tokenReq.AccessToken = await _authManager.CreateToken();
            tokenReq.RefreshToken = await _authManager.CreateRefreshToken();
            return tokenReq;
        }

        public async Task<TokenRequest> RefreshToken([FromBody] TokenRequest request)
        {
            var tokenRequest = await _authManager.VerifyRefreshToken(request);
            return tokenRequest;
        }

    }
}