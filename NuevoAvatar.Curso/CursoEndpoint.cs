using NuevoAvatar.Curso.Entities;
using NuevoAvatar.Curso.Repository;
using NuevoAvatar.Curso.Services;

namespace NuevoAvatar.Curso;

public static class CursoEndpoint
{
    public static IEndpointRouteBuilder MapCursoEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/Curso").WithTags("Curso");

        group.MapGet("", async (ICursoService service) => Results.Ok(await service.GetAllAsync()))
            .WithName("GetCursos").WithSummary("Obtiene todos los cursos.");

        group.MapGet("/carrera/{carreraId:int}", async (int carreraId, ICursoService service) =>
        {
            if (carreraId <= 0) return Results.BadRequest(new { errors = new[] { "CarreraId debe ser mayor que cero." } });
            return Results.Ok(await service.GetByCarreraIdAsync(carreraId));
        }).WithName("GetCursosPorCarrera").WithSummary("Obtiene los cursos de una carrera.");

        group.MapGet("/{id:int}", async (int id, ICursoService service) =>
        {
            if (id <= 0) return Results.BadRequest(new { errors = new[] { "El id debe ser mayor que cero." } });
            var curso = await service.GetByIdAsync(id);
            return curso is null ? Results.NotFound() : Results.Ok(curso);
        }).WithName("GetCursoPorId").WithSummary("Obtiene un curso por su llave primaria.");

        group.MapPost("", async (CursoRequest? curso, ICursoService service) =>
        {
            try
            {
                if (curso is null) return Results.BadRequest(new { errors = new[] { "El curso es requerido." } });
                var id = await service.CreateAsync(curso);
                return Results.CreatedAtRoute("GetCursoPorId", new { id }, new { CursoId = id });
            }
            catch (CursoValidationException exception)
            {
                return Results.BadRequest(new { errors = exception.Errors });
            }
            catch (CarreraInexistenteException)
            {
                return Results.Conflict(new { error = "CarreraId no corresponde a una carrera existente." });
            }
        }).WithName("CreateCurso").WithSummary("Crea un curso.");

        group.MapPut("/{id:int}", async (int id, CursoRequest? curso, ICursoService service) =>
        {
            if (id <= 0) return Results.BadRequest(new { errors = new[] { "El id debe ser mayor que cero." } });
            if (curso is null) return Results.BadRequest(new { errors = new[] { "El curso es requerido." } });
            try
            {
                return await service.UpdateAsync(id, curso)
                    ? Results.Ok(await service.GetByIdAsync(id))
                    : Results.NotFound();
            }
            catch (CursoValidationException exception)
            {
                return Results.BadRequest(new { errors = exception.Errors });
            }
            catch (CarreraInexistenteException)
            {
                return Results.Conflict(new { error = "CarreraId no corresponde a una carrera existente." });
            }
        }).WithName("UpdateCurso").WithSummary("Modifica un curso.");

        group.MapDelete("/{id:int}", async (int id, ICursoService service) =>
        {
            if (id <= 0) return Results.BadRequest(new { errors = new[] { "El id debe ser mayor que cero." } });
            try
            {
                return await service.DeleteAsync(id) ? Results.NoContent() : Results.NotFound();
            }
            catch (CursoReferenciadoException)
            {
                return Results.Conflict(new { error = "El curso está relacionado con otros registros y no puede eliminarse." });
            }
        }).WithName("DeleteCurso").WithSummary("Elimina un curso.");

        return endpoints;
    }
}
