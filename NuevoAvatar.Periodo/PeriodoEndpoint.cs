using NuevoAvatar.Periodo.Entities;
using NuevoAvatar.Periodo.Services;
using PeriodoEntity = NuevoAvatar.Periodo.Entities.Periodo;

namespace NuevoAvatar.Periodo;

public static class PeriodoEndpoint
{
    public static void MapPeriodoEndpoints(this WebApplication app)
    {
        // GET /api/Periodo
        app.MapGet("/api/Periodo", async (
            IPeriodoService service) =>
        {
            var periodos = await service.GetAllAsync();

            return Results.Ok(periodos);
        });

        // GET /api/Periodo/{id}
        app.MapGet("/api/Periodo/{id:int}", async (
            int id,
            IPeriodoService service) =>
        {
            var periodo = await service.GetByIdAsync(id);

            if (periodo is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(periodo);
        });

        // POST /api/Periodo
        app.MapPost("/api/Periodo", async (
            PeriodoRequest request,
            IPeriodoService service,
            PeriodoValidator validator) =>
        {
            var errores = validator.Validate(request);

            if (errores.Count > 0)
            {
                return Results.BadRequest(new
                {
                    errores
                });
            }

            var periodo = new PeriodoEntity
            {
                Anio = request.Anio!.Value,
                NumeroPeriodo = request.NumeroPeriodo!.Value,
                FechaInicio = request.FechaInicio!.Value,
                FechaFin = request.FechaFin!.Value
            };

            var periodoId = await service.CreateAsync(periodo);

            if (periodoId <= 0)
            {
                return Results.Problem(
                    title: "No se pudo crear el periodo.");
            }

            var creado = await service.GetByIdAsync(periodoId);

            return Results.Created(
                $"/api/Periodo/{periodoId}",
                creado);
        });

        // PUT /api/Periodo/{id}
        app.MapPut("/api/Periodo/{id:int}", async (
            int id,
            PeriodoRequest request,
            IPeriodoService service,
            PeriodoValidator validator) =>
        {
            var errores = validator.Validate(request);

            if (errores.Count > 0)
            {
                return Results.BadRequest(new
                {
                    errores
                });
            }

            var existente = await service.GetByIdAsync(id);

            if (existente is null)
            {
                return Results.NotFound();
            }

            var periodo = new PeriodoEntity
            {
                PeriodoId = id,
                Anio = request.Anio!.Value,
                NumeroPeriodo = request.NumeroPeriodo!.Value,
                FechaInicio = request.FechaInicio!.Value,
                FechaFin = request.FechaFin!.Value
            };

            var resultado = await service.UpdateAsync(periodo);

            if (resultado <= 0)
            {
                return Results.Problem(
                    title: "No se pudo modificar el periodo.");
            }

            var actualizado = await service.GetByIdAsync(id);

            return Results.Ok(actualizado);
        });

        // DELETE /api/Periodo/{id}
        app.MapDelete("/api/Periodo/{id:int}", async (
            int id,
            IPeriodoService service) =>
        {
            var existente = await service.GetByIdAsync(id);

            if (existente is null)
            {
                return Results.NotFound();
            }

            var resultado = await service.DeleteAsync(id);

            if (resultado <= 0)
            {
                return Results.Problem(
                    title: "No se pudo eliminar el periodo.");
            }

            return Results.NoContent();
        });
    }
}