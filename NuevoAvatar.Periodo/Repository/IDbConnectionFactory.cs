using System.Data;

namespace NuevoAvatar.Periodo.Repository;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}