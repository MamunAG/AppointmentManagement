using AppointmentManagement.Model.Dto;
using AppointmentManagement.Service.Auth;

namespace AppointmentManagement.EndpointDefinitions;

public class AccountEndpoints : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var posts = app.MapGroup("/api/account");

        posts.MapPost("/register", Register);
        posts.MapPost("/login", Login);
        posts.MapPost("/refresh-token", RefreshToken);
    }

    private async Task<IResult> Register(
        ILogger<AccountEndpoints> _logger,
        IAccountService _accountService,
        LoginUserDTO userDTO)
    {
        try
        {
            _logger.LogInformation($"Registration Attempt for {userDTO.Email} ");
            if (userDTO.Email == null || userDTO.Email == "")
            {
                return Results.BadRequest("Email is required.");
            }
            if (userDTO.Password == null || userDTO.Password == "")
            {
                return Results.BadRequest("Password is required.");
            }

            await _accountService.Register(userDTO);

            return Results.Accepted();
        }
        catch (System.Exception err)
        {
            _logger.LogInformation(err.Message);
            return Results.BadRequest(err.Message);
        }
    }

    private async Task<IResult> Login(
        ILogger<AccountEndpoints> _logger,
        IAccountService _accountService,
        LoginUserDTO userDTO)
    {
        try
        {
            _logger.LogInformation($"Login Attempt for {userDTO.Email} ");
            if (userDTO.Email == null || userDTO.Email == "")
            {
                return Results.BadRequest("Email is required.");
            }
            if (userDTO.Password == null || userDTO.Password == "")
            {
                return Results.BadRequest("Password is required.");
            }

            var res = await _accountService.Login(userDTO);

            return Results.Ok(res);
        }
        catch (System.Exception err)
        {
            _logger.LogInformation(err.Message);
            return Results.BadRequest("Username or Password is not incorrect. Please try again.");
        }
    }

    private async Task<IResult> RefreshToken(
        ILogger<AccountEndpoints> _logger,
        IAccountService _accountService,
        TokenRequest request)
    {
        try
        {
            var res = await _accountService.RefreshToken(request);
            return Results.Ok(res);
        }
        catch (System.Exception err)
        {
            _logger.LogInformation(err.Message);
            return Results.BadRequest("Please try again.");
        }
    }

}
