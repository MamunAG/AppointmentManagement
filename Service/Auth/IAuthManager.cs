using AppointmentManagement.Model.Dto;

namespace AppointmentManagement.Service.Auth
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO userDTO);
        Task<string> CreateToken();
        Task<string> CreateRefreshToken(bool isSkip = false);
        Task<TokenRequest> VerifyRefreshToken(TokenRequest request);
    }
}