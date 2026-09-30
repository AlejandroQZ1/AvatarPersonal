using PeriodoEntity = NuevoAvatar.Periodo.Entities.Periodo;
using NuevoAvatar.Periodo.Repository;

namespace NuevoAvatar.Periodo.Services;

public class PeriodoService : IPeriodoService
{
    private readonly PeriodoRepository _periodoRepository;

    public PeriodoService(PeriodoRepository periodoRepository)
    {
        _periodoRepository = periodoRepository;
    }

    public async Task<IEnumerable<PeriodoEntity>> GetAllAsync()
    {
        return await _periodoRepository.GetAllAsync();
    }

    public async Task<PeriodoEntity?> GetByIdAsync(int id)
    {
        return await _periodoRepository.GetByIdAsync(id);
    }

    public async Task<int> CreateAsync(PeriodoEntity periodo)
    {
        return await _periodoRepository.CreateAsync(periodo);
    }

    public async Task<int> UpdateAsync(PeriodoEntity periodo)
    {
        return await _periodoRepository.UpdateAsync(periodo);
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _periodoRepository.DeleteAsync(id);
    }
}