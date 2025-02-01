using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentManagement.EndpointDefinitions;

public class AppointmentEndpoints : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var posts = app.MapGroup("/api/appointment").RequireAuthorization();

        // posts.MapGet("/", GetAllAppointments);
        // posts.MapGet("/{id}", GetAppointmentById);
        // posts.MapPost("/", Save);
        // posts.MapPut("/{id}", Update);
        // posts.MapDelete("/{id}", Delete);
    }

    // private async Task<IResult> GetAllAppointments()
    // {
    //     return Results.Ok("users");
    // }

}
