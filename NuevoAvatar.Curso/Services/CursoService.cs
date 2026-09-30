using NuevoAvatar.Curso.Entities;
using NuevoAvatar.Curso.Repository;
using CursoEntity = NuevoAvatar.Curso.Entities.Curso;

namespace NuevoAvatar.Curso.Services;

public sealed class CursoService(ICursoRepository repository, CursoValidator validator) : ICursoService
{
    public Task<IReadOnlyList<CursoEntity>> GetAllAsync() => repository.GetAllAsync();

    public Task<CursoEntity?> GetByIdAsync(int id) => repository.GetByIdAsync(id);

    public Task<IReadOnlyList<CursoEntity>> GetByCarreraIdAsync(int carreraId) => repository.GetByCarreraIdAsync(carreraId);

    public async Task<int> CreateAsync(CursoRequest curso)
    {
        Validate(curso);
        return await repository.CreateAsync(curso);
    }

    public async Task<bool> UpdateAsync(int id, CursoRequest curso)
    {
        if (await repository.GetByIdAsync(id) is null)
        {
            return false;
        }

        Validate(curso);
        return await repository.UpdateAsync(id, curso);
    }

    public Task<bool> DeleteAsync(int id) => repository.DeleteAsync(id);

    private void Validate(CursoRequest curso)
    {
        var errors = validator.Validate(curso);
        if (errors.Count > 0)
        {
            throw new CursoValidationException(errors);
        }
    }
}

public sealed class CursoValidationException(IReadOnlyList<string> errors)
    : Exception("La solicitud contiene datos inválidos.")
{
    public IReadOnlyList<string> Errors { get; } = errors;
}
