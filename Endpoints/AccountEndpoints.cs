using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentManagement.EndpointDefinitions;

public class AccountEndpoints : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var posts = app.MapGroup("/api/account");

        // posts.MapPost("/", Register);
        // posts.MapPost("/", Login);

    }

    // private async Task<IResult> GetAllUsers()
    // {
    //     return Results.Ok("users");
    // }

}
