using NuevoAvatar.Periodo.Entities;

namespace NuevoAvatar.Periodo.Services;

public class PeriodoValidator
{
    public List<string> Validate(PeriodoRequest periodo)
    {
        var errores = new List<string>();

        if (periodo is null)
        {
            errores.Add("El periodo es requerido.");
            return errores;
        }

        if (periodo.Anio is null)
        {
            errores.Add("El año es requerido.");
        }

        if (periodo.NumeroPeriodo is null)
        {
            errores.Add("El número de periodo es requerido.");
        }

        if (periodo.FechaInicio is null)
        {
            errores.Add("La fecha de inicio es requerida.");
        }

        if (periodo.FechaFin is null)
        {
            errores.Add("La fecha de fin es requerida.");
        }

        return errores;
    }
}