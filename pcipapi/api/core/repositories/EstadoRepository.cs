using Dapper;

using entities.entities;
using entities.interfaces;
using entities.models;
using entities.models.Estado;
using entities.models.Puesto;

using LanguageExt;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using MySqlConnector;

using System.Data;
using System.Text.Json;

namespace core.repositories
{
    public class EstadoRepository : IEstadoRepository
    {
        private readonly IConfiguration _config;
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public EstadoRepository(ILogger<EstadoRepository> logger, IConfiguration configuration, IApplicationDbContext dbContext)
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

        public async Task<ResponseModel<Estado>> AddAsync(Estado entity)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
                var resultadoId = await GetMaxIdAsync().ConfigureAwait(false);
                entity.estadoId = (resultadoId == null ? 0 : resultadoId.Value) + 1;

                await _dbContext.Estados.AddAsync(entity).ConfigureAwait(false);
                await _dbContext.SaveChangesAsync(default).ConfigureAwait(false);
                tran.Commit();

                return new ResponseModel<Estado>()
                {
                    data = entity,
                    resultado = true
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<Estado>()
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
                return new ResponseModel<Estado>()
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

        public async Task<ResponseModel<Estado>> DeleteAsync(Estado entity)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
                _dbContext.Estados.Remove(entity);
                await _dbContext.SaveChangesAsync(default).ConfigureAwait(false);
                tran.Commit();

                return new ResponseModel<Estado>()
                {
                    resultado = true,
                    data = entity
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<Estado>()
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
                return new ResponseModel<Estado>()
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

        public async Task<ResponseModel<Estado>> DeleteAsync(int id)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
                var registro = await GetEstadoByIdAsync(id).ConfigureAwait(false);
                if (registro.data == Option<Estado>.None)
                {
                    throw new ArgumentException("Registro de objeto invalido.");
                }

                _dbContext.Estados.Remove((Estado)registro.data);
                await _dbContext.SaveChangesAsync(default).ConfigureAwait(false);
                tran.Commit();

                return new ResponseModel<Estado>()
                {
                    resultado = true,
                    data = (Estado)registro.data
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<Estado>()
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
                return new ResponseModel<Estado>()
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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private async Task<ResponseModel<Option<Estado>>> GetEstadoByIdAsync(int id)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spEstadosListadoPorId"
                    , new { _estadoId = id }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                if (resultadoCompleto != string.Empty)
                {
                    return new ResponseModel<Option<Estado>>()
                    {
                        resultado = true,
                        data = JsonSerializer.Deserialize<Estado>(resultadoCompleto)
                    };
                }
                else
                {
                    return new ResponseModel<Option<Estado>>()
                    {
                        resultado = false,
                        data = new Estado(),
                        error = new ErrorModel()
                        {
                            error = null,
                            errorCodigo = 100,
                            errorMensaje = "Error al intentar interpretar la respuesta del servidor"
                        }
                    };
                }
            }
            catch (MySqlException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "GetEstadoByIdAsync(int id)", detalleUsuario = new { _estadoId = id } });
                return new ResponseModel<Option<Estado>>()
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "GetEstadoByIdAsync(int id)", detalleUsuario = new { _estadoId = id } });
                return new ResponseModel<Option<Estado>>()
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

        public async Task<ResponseModel<IReadOnlyList<EstadoCmbViewModel>>> dsCmbEstadosAsync(string filtro)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spEstadosListadoPorCmb"
                    , new { _filtro = filtro }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                if (resultadoCompleto != String.Empty)
                {
                    return new ResponseModel<IReadOnlyList<EstadoCmbViewModel>>()
                    {
                        resultado = true,
                        data = JsonSerializer.Deserialize<IReadOnlyList<EstadoCmbViewModel>>(resultadoCompleto)
                    };
                }
                else
                {
                    return new ResponseModel<IReadOnlyList<EstadoCmbViewModel>>()
                    {
                        resultado = false,
                        data = new List<EstadoCmbViewModel>(),
                        error = new ErrorModel()
                        {
                            error = null,
                            errorCodigo = 100,
                            errorMensaje = "Error al intentar interpretar la respuesta del servidor"
                        }
                    };
                }
            }
            catch (MySqlException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "dsCmbEstadosAsync(string filtro)", detalleUsuario = new { _filtro = filtro } });
                return new ResponseModel<IReadOnlyList<EstadoCmbViewModel>>()
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "dsCmbEstadosAsync(string filtro)", detalleUsuario = new { _filtro = filtro } });
                return new ResponseModel<IReadOnlyList<EstadoCmbViewModel>>()
                {
                    resultado = false,
                    error = new ErrorModel()
                    {
                        error = ex,
                        errorCodigo = 200,
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

        public async Task<ResponseModel<IReadOnlyList<EstadoViewModel>>> GetAllAsync()
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spEstadosListado"
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                string resultadoCompleto = string.Concat(result);
                if (resultadoCompleto != string.Empty)
                {
                    return new ResponseModel<IReadOnlyList<EstadoViewModel>>()
                    {
                        resultado = true,
                        data = JsonSerializer.Deserialize<List<EstadoViewModel>>(resultadoCompleto)
                    };
                }
                else
                {
                    return new ResponseModel<IReadOnlyList<EstadoViewModel>>()
                    {
                        resultado = false,
                        data = new List<EstadoViewModel>(),
                        error = new ErrorModel()
                        {
                            error = null,
                            errorCodigo = 100,
                            errorMensaje = "Error al intentar interpretar la respuesta del servidor"
                        }
                    };
                }
            }
            catch (MySqlException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "GetAllAsync()" });
                return new ResponseModel<IReadOnlyList<EstadoViewModel>>()
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "GetAllAsync()" });
                return new ResponseModel<IReadOnlyList<EstadoViewModel>>()
                {
                    resultado = false,
                    error = new ErrorModel()
                    {
                        error = ex,
                        errorCodigo = 200,
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

        public async Task<ResponseModel<Option<EstadoViewModel>>> GetByIdAsync(int id)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spEstadosListadoPorId"
                    , new { _estadoId = id }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                if (resultadoCompleto != String.Empty)
                {
                    return new ResponseModel<Option<EstadoViewModel>>()
                    {
                        resultado = true,
                        data = JsonSerializer.Deserialize<EstadoViewModel>(resultadoCompleto)
                    };
                }
                else
                {
                    return new ResponseModel<Option<EstadoViewModel>>()
                    {
                        resultado = false,
                        data = new EstadoViewModel(),
                        error = new ErrorModel()
                        {
                            error = null,
                            errorCodigo = 100,
                            errorMensaje = "Error al intentar interpretar la respuesta del servidor"
                        }
                    };
                }
            }
            catch (MySqlException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "GetByIdAsync(int id)", detalleUsuario = new { _estadoId = id } });
                return new ResponseModel<Option<EstadoViewModel>>()
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "GetByIdAsync(int id)", detalleUsuario = new { _estadoId = id } });
                return new ResponseModel<Option<EstadoViewModel>>()
                {
                    resultado = false,
                    error = new ErrorModel()
                    {
                        error = ex,
                        errorCodigo = 200,
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
                string sql = @"SELECT IFNULL(MAX(estadoId), NULL) AS maxEstadoId FROM estados";
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

        public async Task<ResponseModel<Estado>> UpdateAsync(Estado entity)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();
            try
            {
                _dbContext.Estados.Update(entity);
                await _dbContext.SaveChangesAsync(default).ConfigureAwait(false);
                tran.Commit();

                return new ResponseModel<Estado>()
                {
                    data = entity,
                    resultado = true
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<Estado>()
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
                tran.Rollback();
                return new ResponseModel<Estado>()
                {
                    resultado = false,
                    error = new ErrorModel()
                    {
                        error = ex,
                        errorCodigo = 000,
                        errorMensaje = ex.Message
                    },
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
    }
}