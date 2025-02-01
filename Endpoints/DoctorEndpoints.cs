using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentManagement.EndpointDefinitions;

public class DoctorEndpoints : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var posts = app.MapGroup("/api/Doctors").RequireAuthorization();

        // posts.MapGet("/", GetAllDoctors);
        // posts.MapGet("/{id}", GetDoctorById);
        // posts.MapPost("/", Save);
        // posts.MapPut("/{id}", Update);
        // posts.MapDelete("/{id}", Delete);
    }

    // private async Task<IResult> GetAllDoctors()
    // {
    //     return Results.Ok("doctors");
    // }

}
