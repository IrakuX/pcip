using Dapper;

using entities.entities;
using entities.interfaces;
using entities.models;
using entities.models.Acceso;

using LanguageExt;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using MySqlConnector;

using System.Data;
using System.Text.Json;

namespace core.repositories
{
    public class AccesoRepository : IAccesoRepository
    {
        private readonly IConfiguration _config;
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public AccesoRepository(ILogger<AccesoRepository> logger, IConfiguration configuration, IApplicationDbContext dbContext)
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

        public async Task<ResponseModel<IReadOnlyList<Acceso>>> AddRangeAsync(List<Acceso> entities)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
                await _dbContext.Accesos.AddRangeAsync(entities);
                await _dbContext.SaveChangesAsync(default).ConfigureAwait(false);
                tran.Commit();

                return new ResponseModel<IReadOnlyList<Acceso>>()
                {
                    data = entities,
                    resultado = true
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<IReadOnlyList<Acceso>>()
                {
                    error = new ErrorModel()
                    {
                        error = ex,
                        errorMensaje = ex.Message,
                        errorCodigo = (int)ex.ErrorCode,
                    },
                    resultado = false
                };
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return new ResponseModel<IReadOnlyList<Acceso>>()
                {
                    error = new ErrorModel()
                    {
                        errorCodigo = 000,
                        error = ex,
                        errorMensaje = ex.Message
                    },
                    resultado = false
                };
            }
            finally
            {
                db.Close();
                db.Dispose();
                MySqlConnection.ClearPool((MySqlConnection)db);
                this.Dispose();
            }
        }

        public async Task<ResponseModel<IReadOnlyList<Acceso>>> DeletePorPerfilAsync(int perfilId)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
                var registro = await GetByPerfilIdAsync(perfilId).ConfigureAwait(false);
                if (registro.data == Option<AccesoViewModel>.None)
                {
                    throw new ArgumentException("Registro de objeto invalido.");
                }

                List<Acceso> registrosEliminar = new List<Acceso>();
                var accesos = ((AccesoViewModel)registro.data).accesoDetalle;
                foreach (var item in accesos)
                {
                    registrosEliminar.Add(new Acceso() { perfilId = item.perfilId, objetoId = item.objetoId });
                }

                _dbContext.Accesos.RemoveRange(registrosEliminar);
                _dbContext.SaveChanges();
                tran.Commit();

                return new ResponseModel<IReadOnlyList<Acceso>>()
                {
                    resultado = true,
                    data = registrosEliminar
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<IReadOnlyList<Acceso>>()
                {
                    error = new ErrorModel()
                    {
                        error = ex,
                        errorMensaje = ex.Message,
                        errorCodigo = (int)ex.ErrorCode,
                    },
                    resultado = false
                };
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return new ResponseModel<IReadOnlyList<Acceso>>()
                {
                    error = new ErrorModel()
                    {
                        error = ex,
                        errorCodigo = 000,
                        errorMensaje = ex.Message
                    },
                    resultado = false
                };
            }
            finally
            {
                db.Close();
                db.Dispose();
                MySqlConnection.ClearPool((MySqlConnection)db);
                this.Dispose();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<ResponseModel<IReadOnlyList<AccesoDetalleViewModel>>> GetAccesoPorPerfilAsync(int perfilId)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spAccesosListadoPorPerfil"
                    , new { perfilId }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                var resultado = JsonSerializer.Deserialize<List<AccesoDetalleViewModel>>(resultadoCompleto);
                return new ResponseModel<IReadOnlyList<AccesoDetalleViewModel>>()
                {
                    resultado = true,
                    data = resultado
                };
            }
            catch (MySqlException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "GetAccesoPorPerfilAsync(int perfilId)", detalleUsuario = new { perfilId } });
                return new ResponseModel<IReadOnlyList<AccesoDetalleViewModel>>()
                {
                    resultado = false,
                    data = new List<AccesoDetalleViewModel>(),
                    error = new ErrorModel()
                    {
                        error = ex,
                        errorCodigo = (int)ex.ErrorCode,
                        errorMensaje = ex.Message
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "GetAccesoPorPerfilAsync(int perfilId)", detalleUsuario = new { perfilId } });
                return new ResponseModel<IReadOnlyList<AccesoDetalleViewModel>>()
                {
                    resultado = false,
                    data = new List<AccesoDetalleViewModel>(),
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

        public async Task<ResponseModel<IReadOnlyList<AccesoDetalleViewModel>>> GetAccesoPorPerfilAsync(int perfilId, int objetoTipoId)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spAccesosListadoPorObjetoTipo"
                    , new { perfilId, objetoTipoId }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                var resultado = JsonSerializer.Deserialize<List<AccesoDetalleViewModel>>(resultadoCompleto);
                return new ResponseModel<IReadOnlyList<AccesoDetalleViewModel>>()
                {
                    resultado = true,
                    data = resultado
                };
            }
            catch (MySqlException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "GetAccesoPorPerfilAsync(int perfilId, int objetoTipoId)", detalleUsuario = new { perfilId, objetoTipoId } });
                return new ResponseModel<IReadOnlyList<AccesoDetalleViewModel>>()
                {
                    resultado = false,
                    data = new List<AccesoDetalleViewModel>(),
                    error = new ErrorModel()
                    {
                        error = ex,
                        errorCodigo = (int)ex.ErrorCode,
                        errorMensaje = ex.Message
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "GetAccesoPorPerfilAsync(int perfilId, int objetoTipoId)", detalleUsuario = new { perfilId, objetoTipoId } });
                return new ResponseModel<IReadOnlyList<AccesoDetalleViewModel>>()
                {
                    resultado = false,
                    data = new List<AccesoDetalleViewModel>(),
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

        public async Task<ResponseModel<IReadOnlyList<AccesoViewModel>>> GetAccesosAsync()
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spAccesosListado"
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                var resultado = JsonSerializer.Deserialize<List<AccesoViewModel>>(resultadoCompleto);
                return new ResponseModel<IReadOnlyList<AccesoViewModel>>()
                {
                    resultado = true,
                    data = resultado
                };
            }
            catch (MySqlException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "GetAccesosAsync()" });
                return new ResponseModel<IReadOnlyList<AccesoViewModel>>()
                {
                    resultado = false,
                    data = new List<AccesoViewModel>(),
                    error = new ErrorModel()
                    {
                        error = ex,
                        errorCodigo = (int)ex.ErrorCode,
                        errorMensaje = ex.Message
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "GetAccesosAsync()" });
                return new ResponseModel<IReadOnlyList<AccesoViewModel>>()
                {
                    resultado = false,
                    data = new List<AccesoViewModel>(),
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

        public async Task<ResponseModel<Option<AccesoViewModel>>> GetByPerfilIdAsync(int perfilId)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spAccesosListadoPorPerfil"
                    , new { perfilId }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                var resultado = JsonSerializer.Deserialize<AccesoViewModel>(resultadoCompleto);
                return new ResponseModel<Option<AccesoViewModel>>()
                {
                    resultado = true,
                    data = resultado
                };
            }
            catch (MySqlException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "GetByPerfilIdAsync(int perfilId)", detalleUsuario = new { perfilId } });
                return new ResponseModel<Option<AccesoViewModel>>()
                {
                    resultado = false,
                    data = new Option<AccesoViewModel>(),
                    error = new ErrorModel()
                    {
                        error = ex,
                        errorCodigo = (int)ex.ErrorCode,
                        errorMensaje = ex.Message
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "GetByPerfilIdAsync(int perfilId)", detalleUsuario = new { perfilId } });
                return new ResponseModel<Option<AccesoViewModel>>()
                {
                    resultado = false,
                    data = new Option<AccesoViewModel>(),
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
    }
}