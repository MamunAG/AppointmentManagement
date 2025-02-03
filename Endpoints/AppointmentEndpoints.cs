using AppointmentManagement.Model;
using AppointmentManagement.Service;
using AppointmentManagement.Validations;

namespace AppointmentManagement.EndpointDefinitions;

public class AppointmentEndpoints : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var posts = app.MapGroup("/api/appointments").RequireAuthorization();

        posts.MapGet("/", GetAllAppointments);
        posts.MapGet("/{id}", GetAppointmentById);
        posts.MapPost("/", Save);
        posts.MapPut("/{id}", Update);
        posts.MapDelete("/{id}", Delete);
    }

    private async Task<IResult> GetAllAppointments(
        ILogger<AppointmentEndpoints> _logger,
        IAppointmentService _appointmentService)
    {
        try
        {
            var res = await _appointmentService.GetAllAppointments();
            return Results.Ok(res);
        }
        catch (System.Exception err)
        {
            _logger.LogInformation("GetAllAppointments: " + err.Message);
            return Results.BadRequest(err.Message);
        }
    }

    private async Task<IResult> GetAppointmentById(
        ILogger<AppointmentEndpoints> _logger,
        IAppointmentService _appointmentService,
        int id)
    {
        try
        {
            if (id <= 0)
            {
                return Results.BadRequest("Please select a appointment.");
            }

            var res = await _appointmentService.GetAppointmentById(id);
            return Results.Ok(res);
        }
        catch (System.Exception err)
        {
            _logger.LogInformation("GetAppointmentById: " + err.Message);
            return Results.BadRequest(err.Message);
        }
    }

    private async Task<IResult> Save(
        ILogger<AppointmentEndpoints> _logger,
        IAppointmentService _appointmentService,
        AppointmentDto dto)
    {
        try
        {
            if (dto == null)
            {
                return Results.BadRequest("Data is required.");
            }
            AppointmentEntityValidation validator = new AppointmentEntityValidation();
            var validation = validator.Validate(dto);
            if (validation.IsValid)
            {
                var res = await _appointmentService.Save(dto);
                return Results.Ok(res);
            }
            else
            {
                return Results.BadRequest(validation.Errors);
            }
        }
        catch (System.Exception err)
        {
            _logger.LogInformation("Appointment-Save: " + err.Message);
            return Results.BadRequest(err.Message);
        }
    }

    private async Task<IResult> Update(
        ILogger<AppointmentEndpoints> _logger,
        IAppointmentService _appointmentService,
        int id,
        AppointmentDto dto)
    {
        try
        {
            if (dto == null)
            {
                return Results.BadRequest("Data is required.");
            }
            if (id != dto.Id)
            {
                return Results.BadRequest("Invalid request.");
            }

            AppointmentEntityValidation validator = new AppointmentEntityValidation();
            var validation = validator.Validate(dto);
            if (validation.IsValid)
            {
                var res = await _appointmentService.Update(id, dto);
                return Results.Ok(res);
            }
            else
            {
                return Results.BadRequest(validation.Errors);
            }
        }
        catch (System.Exception err)
        {
            _logger.LogInformation("Appointment-Update: " + err.Message);
            return Results.BadRequest(err.Message);
        }
    }

    private async Task<IResult> Delete(
        ILogger<AppointmentEndpoints> _logger,
        IAppointmentService _appointmentService,
        int id)
    {
        try
        {
            if (id <= 0)
            {
                return Results.BadRequest("Invalid request.");
            }

            await _appointmentService.Delete(id);
            return Results.NoContent();
        }
        catch (System.Exception err)
        {
            _logger.LogInformation("Appointment-Delete: " + err.Message);
            return Results.BadRequest(err.Message);
        }
    }

}