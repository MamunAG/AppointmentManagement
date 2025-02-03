using AppointmentManagement.Model;
using AppointmentManagement.Service;
using AppointmentManagement.Validations;

namespace AppointmentManagement.EndpointDefinitions;

public class DoctorEndpoints : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var posts = app.MapGroup("/api/Doctors").RequireAuthorization();

        posts.MapGet("/", GetAllDoctors);
        posts.MapGet("/{id}", GetDoctorById);
        posts.MapPost("/", Save);
        posts.MapPut("/{id}", Update);
        posts.MapDelete("/{id}", Delete);
    }

    private async Task<IResult> GetAllDoctors(
        ILogger<DoctorEndpoints> _logger,
        IDoctorService _doctorService)
    {
        try
        {
            var res = await _doctorService.GetAllDoctors();
            return Results.Ok(res);
        }
        catch (System.Exception err)
        {
            _logger.LogInformation("GetAllDoctors: " + err.Message);
            return Results.BadRequest(err.Message);
        }

    }

    private async Task<IResult> GetDoctorById(
        ILogger<DoctorEndpoints> _logger,
        IDoctorService _doctorService,
        string id)
    {
        try
        {
            if (id == null || id == "")
            {
                return Results.BadRequest("Please select a doctor.");
            }

            var res = await _doctorService.GetDoctorById(id);
            return Results.Ok(res);
        }
        catch (System.Exception err)
        {
            _logger.LogInformation("GetDoctorById: " + err.Message);
            return Results.BadRequest(err.Message);
        }
    }

    private async Task<IResult> Save(
        ILogger<DoctorEndpoints> _logger,
        IDoctorService _doctorService,
        DoctorDto dto)
    {
        try
        {
            if (dto == null)
            {
                return Results.BadRequest("Data is required.");
            }
            DoctorEntityValidation validator = new DoctorEntityValidation();
            var validation = validator.Validate(dto);
            if (validation.IsValid)
            {
                var res = await _doctorService.Save(dto);
                return Results.Ok(res);
            }
            else
            {
                return Results.BadRequest(validation.Errors);
            }
        }
        catch (System.Exception err)
        {
            _logger.LogInformation("Doctor-Save: " + err.Message);
            return Results.BadRequest(err.Message);
        }

    }

    private async Task<IResult> Update(
        ILogger<DoctorEndpoints> _logger,
        IDoctorService _doctorService,
        string id,
        DoctorDto dto)
    {
        try
        {
            if (dto == null)
            {
                return Results.BadRequest("Data is required.");
            }
            if (id != dto.Id.ToString())
            {
                return Results.BadRequest("Invalid request.");
            }
            DoctorEntityValidation validator = new DoctorEntityValidation();
            var validation = validator.Validate(dto);
            if (validation.IsValid)
            {
                var res = await _doctorService.Update(id, dto);
                return Results.Ok(res);
            }
            else
            {
                return Results.BadRequest(validation.Errors);
            }
        }
        catch (System.Exception err)
        {
            _logger.LogInformation("Doctor-Update: " + err.Message);
            return Results.BadRequest(err.Message);
        }
    }

    private async Task<IResult> Delete(
        ILogger<DoctorEndpoints> _logger,
        IDoctorService _doctorService,
        string id)
    {
        try
        {
            if (id == null || id == "")
            {
                return Results.BadRequest("Invalid request.");
            }

            await _doctorService.Delete(id);
            return Results.NoContent();
        }
        catch (System.Exception err)
        {
            _logger.LogInformation("Doctor-Delete: " + err.Message);
            return Results.BadRequest(err.Message);
        }

    }

}
