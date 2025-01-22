using Dapper;

using entities.entities;
using entities.interfaces;
using entities.models;
using entities.models.Moneda;

using LanguageExt;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using MySqlConnector;

using System.Data;
using System.Text.Json;

namespace core.repositories
{
    public class MonedaRepository : IMonedaRepository
    {
        private readonly IConfiguration _config;
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public MonedaRepository(ILogger<MonedaRepository> logger, IConfiguration configuration, IApplicationDbContext dbContext)
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

        public async Task<ResponseModel<Moneda>> AddAsync(Moneda entity)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
                var resultadoId = await GetMaxIdAsync().ConfigureAwait(false);
                entity.monedaId = (resultadoId == null ? 0 : resultadoId.Value) + 1;

                await _dbContext.Monedas.AddAsync(entity).ConfigureAwait(false);
                await _dbContext.SaveChangesAsync(default).ConfigureAwait(false);
                tran.Commit();

                return new ResponseModel<Moneda>()
                {
                    data = entity,
                    resultado = true
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<Moneda>()
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
                return new ResponseModel<Moneda>()
                {
                    resultado = false,
                    error = new ErrorModel()
                    {
                        errorCodigo = 000,
                        error = ex,
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

        public async Task<ResponseModel<Moneda>> DeleteAsync(Moneda entity)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
                _dbContext.Monedas.Remove(entity);
                await _dbContext.SaveChangesAsync(default).ConfigureAwait(false);
                tran.Commit();

                return new ResponseModel<Moneda>()
                {
                    resultado = true,
                    data = entity
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<Moneda>()
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
            catch (DbUpdateException ex)
            {
                tran.Rollback();
                return new ResponseModel<Moneda>()
                {
                    resultado = false,
                    error = new ErrorModel()
                    {
                        errorCodigo = ex.Message == "Reference constraint violation" ? 101 : 000,
                        error = ex.Message == "Reference constraint violation" ? ex.InnerException : ex,
                        errorMensaje = ex.Message == "Reference constraint violation" ? ex.InnerException.Message : ex.Message
                    },
                };
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return new ResponseModel<Moneda>()
                {
                    resultado = false,
                    error = new ErrorModel()
                    {
                        errorCodigo = 000,
                        error = ex,
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

        public async Task<ResponseModel<Moneda>> DeleteAsync(int id)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
                var Puesto = await GetMonedaByIdAsync(id);
                if (Puesto.data == Option<Moneda>.None)
                {
                    throw new ArgumentException("Registro de objeto invalido.");
                }

                _dbContext.Monedas.Remove((Moneda)Puesto.data);
                await _dbContext.SaveChangesAsync(default);
                tran.Commit();

                return new ResponseModel<Moneda>()
                {
                    resultado = true,
                    data = (Moneda)Puesto.data
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<Moneda>()
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
                return new ResponseModel<Moneda>()
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

        public async Task<ResponseModel<IReadOnlyList<MonedaCmbViewModel>>> dsCmbMonedasAsync(string filtro)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spMonedasListadoCmb"
                    , new { _filtro = filtro }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                if (resultadoCompleto != String.Empty)
                {
                    return new ResponseModel<IReadOnlyList<MonedaCmbViewModel>>()
                    {
                        resultado = true,
                        data = JsonSerializer.Deserialize<IReadOnlyList<MonedaCmbViewModel>>(resultadoCompleto)
                    };
                }
                else
                {
                    return new ResponseModel<IReadOnlyList<MonedaCmbViewModel>>()
                    {
                        resultado = false,
                        data = new List<MonedaCmbViewModel>(),
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "dsCmbMonedasAsync(string filtro)", detalleUsuario = new { _filtro = filtro } });
                return new ResponseModel<IReadOnlyList<MonedaCmbViewModel>>()
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "dsCmbMonedasAsync(string filtro)", detalleUsuario = new { _filtro = filtro } });
                return new ResponseModel<IReadOnlyList<MonedaCmbViewModel>>()
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

        public async Task<ResponseModel<IReadOnlyList<MonedaViewModel>>> GetAllAsync()
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spMonedasListado"
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                if (resultadoCompleto != string.Empty)
                {
                    return new ResponseModel<IReadOnlyList<MonedaViewModel>>()
                    {
                        resultado = true,
                        data = JsonSerializer.Deserialize<IReadOnlyList<MonedaViewModel>>(resultadoCompleto)
                    };
                }
                else
                {
                    return new ResponseModel<IReadOnlyList<MonedaViewModel>>()
                    {
                        resultado = false,
                        data = new List<MonedaViewModel>(),
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
                return new ResponseModel<IReadOnlyList<MonedaViewModel>>()
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
                return new ResponseModel<IReadOnlyList<MonedaViewModel>>()
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

        public async Task<ResponseModel<Option<MonedaViewModel>>> GetByIdAsync(int id)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spMonedasListadoPorId"
                    , new { _monedaId = id }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                if (resultadoCompleto != String.Empty)
                {
                    return new ResponseModel<Option<MonedaViewModel>>()
                    {
                        resultado = true,
                        data = JsonSerializer.Deserialize<MonedaViewModel>(resultadoCompleto)
                    };
                }
                else
                {
                    return new ResponseModel<Option<MonedaViewModel>>()
                    {
                        resultado = false,
                        data = new MonedaViewModel(),
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "GetByIdAsync(int id)", detalleUsuario = new { _monedaId = id } });
                return new ResponseModel<Option<MonedaViewModel>>()
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "GetByIdAsync(int id)", detalleUsuario = new { _monedaId = id } });
                return new ResponseModel<Option<MonedaViewModel>>()
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
                string sql = @"SELECT IFNULL(MAX(monedaId), NULL) AS maxMonedaId FROM monedas";
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

        public async Task<ResponseModel<Moneda>> UpdateAsync(Moneda entity)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();
            try
            {
                _dbContext.Monedas.Update(entity);
                await _dbContext.SaveChangesAsync(default).ConfigureAwait(false);
                tran.Commit();

                return new ResponseModel<Moneda>()
                {
                    data = entity,
                    resultado = true
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<Moneda>()
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
                return new ResponseModel<Moneda>()
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

        private async Task<ResponseModel<Option<Moneda>>> GetMonedaByIdAsync(int id)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spMonedasListadoPorId"
                    , new { _monedaId = id }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                if (resultadoCompleto != string.Empty)
                {
                    return new ResponseModel<Option<Moneda>>()
                    {
                        resultado = true,
                        data = JsonSerializer.Deserialize<Moneda>(resultadoCompleto)
                    };
                }
                else
                {
                    return new ResponseModel<Option<Moneda>>()
                    {
                        resultado = false,
                        data = new Moneda(),
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "GetMonedaByIdAsync(int id)", detalleUsuario = new { _monedaId = id } });
                return new ResponseModel<Option<Moneda>>()
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "GetMonedaByIdAsync(int id)", detalleUsuario = new { _monedaId = id } });
                return new ResponseModel<Option<Moneda>>()
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
    }
}