using Dapper;

using entities.entities;
using entities.interfaces;
using entities.models;
using entities.models.Asentamiento;

using LanguageExt;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using MySqlConnector;

using System.Data;
using System.Text.Json;

namespace core.repositories
{
    public class AsentamientoRepository : IAsentamientoRepository
    {
        private readonly IConfiguration _config;
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public AsentamientoRepository(ILogger<AsentamientoRepository> logger, IConfiguration configuration, IApplicationDbContext dbContext)
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

        public async Task<ResponseModel<Asentamiento>> AddAsync(Asentamiento entity)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
                var resultadoId = await GetMaxIdAsync().ConfigureAwait(false);
                entity.asentamientoId = (resultadoId == null ? 0 : resultadoId.Value) + 1;

                await _dbContext.Asentamientos.AddAsync(entity).ConfigureAwait(false);
                await _dbContext.SaveChangesAsync(default).ConfigureAwait(false);
                tran.Commit();

                return new ResponseModel<Asentamiento>()
                {
                    data = entity,
                    resultado = true
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<Asentamiento>()
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
                return new ResponseModel<Asentamiento>()
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

        public async Task<ResponseModel<Asentamiento>> DeleteAsync(Asentamiento entity)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
                _dbContext.Asentamientos.Remove(entity);
                await _dbContext.SaveChangesAsync(default).ConfigureAwait(false);
                tran.Commit();
                return new ResponseModel<Asentamiento>()
                {
                    resultado = true,
                    data = entity
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<Asentamiento>()
                {
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
                return new ResponseModel<Asentamiento>()
                {
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
                return new ResponseModel<Asentamiento>()
                {
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

        public async Task<ResponseModel<Asentamiento>> DeleteAsync(int id)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
                var registro = await GetAsentamientoByIdAsync(id);
                if (registro.data == Option<Asentamiento>.None)
                {
                    throw new ArgumentException("Registro de objeto invalido.");
                }

                _dbContext.Asentamientos.Remove((Asentamiento)registro.data);
                await _dbContext.SaveChangesAsync(default);
                tran.Commit();

                return new ResponseModel<Asentamiento>()
                {
                    resultado = true,
                    data = (Asentamiento)registro.data
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<Asentamiento>()
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
                return new ResponseModel<Asentamiento>()
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

        public async Task<ResponseModel<IReadOnlyList<AsentamientoViewModel>>> dsAsentamientosAsync(string filtro)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spAsentamientosListadoPorFiltro"
                    , new { _filtro = filtro }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                if (resultadoCompleto != String.Empty)
                {
                    return new ResponseModel<IReadOnlyList<AsentamientoViewModel>>()
                    {
                        resultado = true,
                        data = JsonSerializer.Deserialize<IReadOnlyList<AsentamientoViewModel>>(resultadoCompleto)
                    };
                }
                else
                {
                    return new ResponseModel<IReadOnlyList<AsentamientoViewModel>>()
                    {
                        resultado = false,
                        data = new List<AsentamientoViewModel>(),
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "dsAsentamientosAsync(string filtro)", detalleUsuario = new { _filtro = filtro } });
                return new ResponseModel<IReadOnlyList<AsentamientoViewModel>>()
                {
                    resultado = false,
                    data = new List<AsentamientoViewModel>(),
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "dsAsentamientosAsync(string filtro)", detalleUsuario = new { _filtro = filtro } });
                return new ResponseModel<IReadOnlyList<AsentamientoViewModel>>()
                {
                    resultado = false,
                    data = new List<AsentamientoViewModel>(),
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

        public async Task<ResponseModel<IReadOnlyList<AsentamientoCmbViewModel>>> dsCmbAsentamientosAsync(int? estadoId, int? municipioId, int? ciudadId, string asentamientoCodigoPostal)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spAsentamientosListadoCmb"
                    , new { _estadoId = estadoId, _municipioId = municipioId, _ciudadId = ciudadId, _asentamientoCodigoPostal = asentamientoCodigoPostal }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                if (resultadoCompleto != String.Empty)
                {
                    return new ResponseModel<IReadOnlyList<AsentamientoCmbViewModel>>()
                    {
                        resultado = true,
                        data = JsonSerializer.Deserialize<IReadOnlyList<AsentamientoCmbViewModel>>(resultadoCompleto)
                    };
                }
                else
                {
                    return new ResponseModel<IReadOnlyList<AsentamientoCmbViewModel>>()
                    {
                        resultado = false,
                        data = new List<AsentamientoCmbViewModel>(),
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "dsCmbAsentamientosAsync(int? estadoId, int? municipioId, int? ciudadId, string asentamientoCodigoPostal)", detalleUsuario = new { _estadoId = estadoId, _municipioId = municipioId, _ciudadId = ciudadId, _asentamientoCodigoPostal = asentamientoCodigoPostal } });
                return new ResponseModel<IReadOnlyList<AsentamientoCmbViewModel>>()
                {
                    resultado = false,
                    data = new List<AsentamientoCmbViewModel>(),
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "dsCmbAsentamientosAsync(int? estadoId, int? municipioId, int? ciudadId, string asentamientoCodigoPostal)", detalleUsuario = new { _estadoId = estadoId, _municipioId = municipioId, _ciudadId = ciudadId, _asentamientoCodigoPostal = asentamientoCodigoPostal } });
                return new ResponseModel<IReadOnlyList<AsentamientoCmbViewModel>>()
                {
                    resultado = false,
                    data = new List<AsentamientoCmbViewModel>(),
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

        public async Task<ResponseModel<IReadOnlyList<AsentamientoViewModel>>> GetAllAsync()
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spAsentamientosListado"
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                if (resultadoCompleto != string.Empty)
                {
                    return new ResponseModel<IReadOnlyList<AsentamientoViewModel>>()
                    {
                        resultado = true,
                        data = JsonSerializer.Deserialize<IReadOnlyList<AsentamientoViewModel>>(resultadoCompleto)
                    };
                }
                else
                {
                    return new ResponseModel<IReadOnlyList<AsentamientoViewModel>>()
                    {
                        resultado = true,
                        data = new List<AsentamientoViewModel>()
                    };
                }
            }
            catch (MySqlException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "GetAllAsync()" });
                return new ResponseModel<IReadOnlyList<AsentamientoViewModel>>()
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
                return new ResponseModel<IReadOnlyList<AsentamientoViewModel>>()
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

        public async Task<ResponseModel<Option<AsentamientoViewModel>>> GetByIdAsync(int id)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spAsentamientosListadoPorId"
                    , new { _asentamientoId = id }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                if (resultadoCompleto != string.Empty)
                {
                    return new ResponseModel<Option<AsentamientoViewModel>>()
                    {
                        resultado = true,
                        data = JsonSerializer.Deserialize<AsentamientoViewModel>(resultadoCompleto)
                    };
                }
                else
                {
                    return new ResponseModel<Option<AsentamientoViewModel>>()
                    {
                        resultado = true,
                        data = new AsentamientoViewModel()
                    };
                }
            }
            catch (MySqlException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "GetByIdAsync(int id)", detalleUsuario = new { _asentamientoId = id } });
                return new ResponseModel<Option<AsentamientoViewModel>>()
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "GetByIdAsync(int id)", detalleUsuario = new { _asentamientoId = id } });
                return new ResponseModel<Option<AsentamientoViewModel>>()
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
                string sql = @"SELECT IFNULL(MAX(asentamientoId), NULL) AS max_asentamientoloId FROM asentamientos";
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

        public async Task<ResponseModel<Asentamiento>> UpdateAsync(Asentamiento entity)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();
            try
            {
                _dbContext.Asentamientos.Update(entity);
                await _dbContext.SaveChangesAsync(default).ConfigureAwait(false);
                tran.Commit();

                return new ResponseModel<Asentamiento>()
                {
                    data = entity,
                    resultado = true
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<Asentamiento>()
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
                return new ResponseModel<Asentamiento>()
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

        private async Task<ResponseModel<Option<Asentamiento>>> GetAsentamientoByIdAsync(int id)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spAsentamientosListadoPorId"
                    , new { _asentamientoId = id }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                if (resultadoCompleto != string.Empty)
                {
                    return new ResponseModel<Option<Asentamiento>>()
                    {
                        resultado = true,
                        data = JsonSerializer.Deserialize<Asentamiento>(resultadoCompleto)
                    };
                }
                else
                {
                    return new ResponseModel<Option<Asentamiento>>()
                    {
                        resultado = true,
                        data = new Asentamiento()
                    };
                }
            }
            catch (MySqlException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "GetAsentamientoByIdAsync(int id)", detalleUsuario = new { _asentamientoId = id } });
                return new ResponseModel<Option<Asentamiento>>()
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "GetAsentamientoByIdAsync(int id)", detalleUsuario = new { _asentamientoId = id } });
                return new ResponseModel<Option<Asentamiento>>()
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