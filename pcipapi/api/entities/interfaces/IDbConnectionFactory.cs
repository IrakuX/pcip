using System.Data;

namespace entities.interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();

        Task<IDbConnection> CreateConnectionAsync();
    }
}