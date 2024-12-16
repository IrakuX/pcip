using Dapper;

using entities.entities;
using entities.interfaces;
using entities.models;

using LanguageExt;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using MySqlConnector;

using System.Data;
using System.Text.Json;

namespace core.repositories
{
    public class ObjetoTipoRepository : IObjetoTipoRepository
    {
        private readonly IConfiguration _config;
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public ObjetoTipoRepository(ILogger<ObjetoTipoRepository> logger, IConfiguration configuration, IApplicationDbContext dbContext)
        {
            _logger = logger;
            _config = configuration;
            _dbContext = dbContext;
        }

        private IDbConnection db
        {
            get
            {
                return new MySqlConnection(_config.GetConnectionString("cnnConexion"));
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<ResponseModel<IReadOnlyList<ObjetoTipo>>> GetAllAsync(string filtro, int pagina, int paginaTamanio)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spObjetoTiposListado"
                    , new { filtro, pagina, paginaTamanio }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                var resultado = JsonSerializer.Deserialize<IReadOnlyList<ObjetoTipo>>(resultadoCompleto);
                return new ResponseModel<IReadOnlyList<ObjetoTipo>>()
                {
                    resultado = true,
                    data = resultado
                };
            }
            catch (MySqlException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "GetAllAsync(string filtro, int pagina, int paginaTamanio)", detalleUsuario = new { filtro, pagina, paginaTamanio } });
                return new ResponseModel<IReadOnlyList<ObjetoTipo>>()
                {
                    resultado = false,
                    error = new ErrorModel()
                    {
                        error = ex,
                        errorMensaje = ex.Message,
                        errorCodigo = (int)ex.ErrorCode,
                    },
                };
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "GetAllAsync(string filtro, int pagina, int paginaTamanio)", detalleUsuario = new { filtro, pagina, paginaTamanio } });
                return new ResponseModel<IReadOnlyList<ObjetoTipo>>()
                {
                    resultado = false,
                    data = new List<ObjetoTipo>(),
                    error = new ErrorModel()
                    {
                        error = ex,
                        errorCodigo = 000,
                        errorMensaje = ex.Message
                    }
                };
            }
            finally
            {
                this.db.Close();
                this.db.Dispose();
                MySqlConnection.ClearPool((MySqlConnection)db);
                this.Dispose();
            }
        }

        public async Task<ResponseModel<Option<ObjetoTipo>>> GetByIdAsync(int id)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spObjetoTiposListadoPorObjetoTipoId"
                    , new { objetoTipoId = id }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                var resultado = JsonSerializer.Deserialize<Option<ObjetoTipo>>(resultadoCompleto);
                return new ResponseModel<Option<ObjetoTipo>>()
                {
                    resultado = true,
                    data = resultado
                };
            }
            catch (MySqlException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "GetByIdAsync(int objetoTipoId)", detalleUsuario = new { objetoTipoId = id } });
                return new ResponseModel<Option<ObjetoTipo>>()
                {
                    resultado = false,
                    error = new ErrorModel()
                    {
                        error = ex,
                        errorMensaje = ex.Message,
                        errorCodigo = (int)ex.ErrorCode,
                    },
                };
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "GetByIdAsync(int objetoTipoId)", detalleUsuario = new { objetoTipoId = id } });
                return new ResponseModel<Option<ObjetoTipo>>()
                {
                    resultado = false,
                    error = new ErrorModel()
                    {
                        error = ex,
                        errorCodigo = 000,
                        errorMensaje = ex.Message
                    }
                };
            }
            finally
            {
                this.db.Close();
                this.db.Dispose();
                MySqlConnection.ClearPool((MySqlConnection)db);
                this.Dispose();
            }
        }

        public async Task<int?> GetMaxIdAsync()
        {
            try
            {
                db.Open();
                string sql = @"SELECT IFNULL(MAX(objetoTipoId), NULL) AS maxObjetoTipoId FROM objetoTipos";
                return await db.QuerySingleOrDefaultAsync<int?>(sql).ConfigureAwait(false);
            }
            catch (MySqlException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "GetMaxIdAsync()" });
                return -1;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "GetMaxIdAsync()" });
                return -1;
            }
            finally
            {
                this.db.Close();
                this.db.Dispose();
                MySqlConnection.ClearPool((MySqlConnection)db);
                this.Dispose();
            }
        }
    }
}