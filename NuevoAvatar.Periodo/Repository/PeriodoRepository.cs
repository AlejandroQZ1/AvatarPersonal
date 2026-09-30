using Dapper;
using PeriodoEntity = NuevoAvatar.Periodo.Entities.Periodo;

namespace NuevoAvatar.Periodo.Repository;

public class PeriodoRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public PeriodoRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<IEnumerable<PeriodoEntity>> GetAllAsync()
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                PeriodoId,
                Anio,
                NumeroPeriodo,
                FechaInicio,
                FechaFin
            FROM academico.Periodo
            ORDER BY PeriodoId;
            """;

        return await connection.QueryAsync<PeriodoEntity>(sql);
    }

    public async Task<PeriodoEntity?> GetByIdAsync(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                PeriodoId,
                Anio,
                NumeroPeriodo,
                FechaInicio,
                FechaFin
            FROM academico.Periodo
            WHERE PeriodoId = @id;
            """;

        return await connection.QueryFirstOrDefaultAsync<PeriodoEntity>(
            sql,
            new { id });
    }

    public async Task<int> CreateAsync(PeriodoEntity periodo)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        const string sql = """
            INSERT INTO academico.Periodo
            (
                Anio,
                NumeroPeriodo,
                FechaInicio,
                FechaFin
            )
            VALUES
            (
                @Anio,
                @NumeroPeriodo,
                @FechaInicio,
                @FechaFin
            );

            SELECT CAST(SCOPE_IDENTITY() AS int);
            """;

        return await connection.ExecuteScalarAsync<int>(
            sql,
            periodo);
    }

    public async Task<int> UpdateAsync(PeriodoEntity periodo)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        const string sql = """
            UPDATE academico.Periodo
            SET
                Anio = @Anio,
                NumeroPeriodo = @NumeroPeriodo,
                FechaInicio = @FechaInicio,
                FechaFin = @FechaFin
            WHERE PeriodoId = @PeriodoId;
            """;

        return await connection.ExecuteAsync(
            sql,
            periodo);
    }

    public async Task<int> DeleteAsync(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        const string sql = """
            DELETE FROM academico.Periodo
            WHERE PeriodoId = @id;
            """;

        return await connection.ExecuteAsync(
            sql,
            new { id });
    }
}