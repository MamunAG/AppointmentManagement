using System.Security.Claims;
using AppointmentManagement.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AppointmentManagement.Model.Dto;

namespace AppointmentManagement.Service.Auth
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private AppUser _user;
        public AuthManager(UserManager<AppUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var token = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
             {
                 new Claim(ClaimTypes.Name, _user.UserName),
                 new Claim(ClaimTypes.NameIdentifier, _user.Id)
             };

            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var expiration = DateTime.Now.AddHours(Convert.ToDouble(
                jwtSettings.GetSection("lifetime").Value));

            var token = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("Issuer").Value,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials);

            return token;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = jwtSettings.GetSection("key").Value;
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
            var signingCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

            return signingCredentials;
        }

        public async Task<bool> ValidateUser(LoginUserDTO userDTO)
        {
            _user = new AppUser();
            _user.Email = userDTO.Email;
            _user.UserName = userDTO.Email;


            var user = await _userManager.FindByNameAsync(userDTO.Email);
            if (user != null)
            {
                var validPassword = await _userManager.CheckPasswordAsync(user, userDTO.Password);
                _user = user;
                return (_user != null && validPassword);
            }
            return false;
        }

        public async Task<string> CreateRefreshToken(bool isSkip = false)
        {
            await _userManager.RemoveAuthenticationTokenAsync(_user, "AM.Jwt.API", "RefreshToken");
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, TokenOptions.DefaultProvider, "RefreshToken");
            if (!isSkip)
            {
                var result = await _userManager.SetAuthenticationTokenAsync(_user, TokenOptions.DefaultProvider, "RefreshToken", newRefreshToken);
            }
            return newRefreshToken;
        }

        public async Task<TokenRequest> VerifyRefreshToken(TokenRequest request)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.AccessToken);
            var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == ClaimTypes.Name)?.Value;
            _user = await _userManager.FindByNameAsync(username);
            try
            {
                var isValid = await _userManager.VerifyUserTokenAsync(_user, TokenOptions.DefaultProvider, "RefreshToken", request.RefreshToken);
                if (isValid)
                {
                    return new TokenRequest { AccessToken = await CreateToken(), RefreshToken = await CreateRefreshToken() };
                }
                await _userManager.UpdateSecurityStampAsync(_user);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }
    }
}