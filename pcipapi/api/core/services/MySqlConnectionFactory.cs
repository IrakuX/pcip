using entities.interfaces;

using Microsoft.Extensions.Configuration;

using MySqlConnector;

using System.Data;

namespace core.services
{
    public class MySqlConnectionFactory : IDbConnectionFactory, IDisposable
    {
        private readonly string _connectionString;
        private bool _disposed;

        public MySqlConnectionFactory(IConfiguration config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            _connectionString = config.GetConnectionString("cnnConexion")
                ?? throw new InvalidOperationException("La cadena de conexión 'cnnConexion' no está configurada.");
        }

        public MySqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        // Método síncrono para crear la conexión
        public IDbConnection CreateConnection()
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new InvalidOperationException("La cadena de conexión no está configurada.");

            return new MySqlConnection(_connectionString);
        }

        // Método asíncrono para crear y abrir la conexión
        public async Task<IDbConnection> CreateConnectionAsync()
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new InvalidOperationException("La cadena de conexión no está configurada.");

            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        // Implementación de IDisposable para liberar recursos si es necesario
        public void Dispose()
        {
            if (!this._disposed)
            {
                // Aquí puedes liberar cualquier recurso si es necesario
                this._disposed = true;
            }
        }
    }
}