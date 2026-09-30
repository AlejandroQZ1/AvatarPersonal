using PeriodoEntity = NuevoAvatar.Periodo.Entities.Periodo;

namespace NuevoAvatar.Periodo.Services;

public interface IPeriodoService
{
    Task<IEnumerable<PeriodoEntity>> GetAllAsync();

    Task<PeriodoEntity?> GetByIdAsync(int id);

    Task<int> CreateAsync(PeriodoEntity periodo);

    Task<int> UpdateAsync(PeriodoEntity periodo);

    Task<int> DeleteAsync(int id);
}