using Microsoft.Data.SqlClient;

namespace NuevoAvatar.Curso.Repository;

public interface IDbConnectionFactory
{
    SqlConnection CreateConnection();
}
