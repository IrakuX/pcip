using Dapper;

using entities.entities;
using entities.interfaces;
using entities.models;
using entities.models.Moneda;
using entities.models.Municipio;

using LanguageExt;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using MySqlConnector;

using System.Data;
using System.Text.Json;

namespace core.repositories
{
    public class MunicipioRepository : IMunicipioRepository
    {
        private readonly IConfiguration _config;
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public MunicipioRepository(ILogger<MunicipioRepository> logger, IConfiguration configuration, IApplicationDbContext dbContext)
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

        public async Task<ResponseModel<Municipio>> AddAsync(Municipio entity)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
                var resultadoId = await GetMaxIdAsync().ConfigureAwait(false);
                entity.municipioId = (resultadoId == null ? 0 : resultadoId.Value) + 1;

                await _dbContext.Municipios.AddAsync(entity).ConfigureAwait(false);
                await _dbContext.SaveChangesAsync(default).ConfigureAwait(false);
                tran.Commit();

                return new ResponseModel<Municipio>()
                {
                    data = entity,
                    resultado = true
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<Municipio>()
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
                return new ResponseModel<Municipio>()
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

        public async Task<ResponseModel<Municipio>> DeleteAsync(Municipio entity)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
                _dbContext.Municipios.Remove(entity);
                await _dbContext.SaveChangesAsync(default).ConfigureAwait(false);
                tran.Commit();

                return new ResponseModel<Municipio>()
                {
                    resultado = true,
                    data = entity
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<Municipio>()
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
                return new ResponseModel<Municipio>()
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
                return new ResponseModel<Municipio>()
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

        public async Task<ResponseModel<Municipio>> DeleteAsync(int id)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
                var registro = await GetMunicipioByIdAsync(id);
                if (registro.data == Option<Municipio>.None)
                {
                    throw new ArgumentException("Registro de objeto invalido.");
                }

                _dbContext.Municipios.Remove((Municipio)registro.data);
                await _dbContext.SaveChangesAsync(default);
                tran.Commit();

                return new ResponseModel<Municipio>()
                {
                    resultado = true,
                    data = (Municipio)registro.data
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<Municipio>()
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
                return new ResponseModel<Municipio>()
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

        public async Task<ResponseModel<IReadOnlyList<MunicipioCmbViewModel>>> dsCmbMunicipiosAsync(int estadoId, string filtro)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spMonedasListadoCmb"
                    , new { _estadoId = estadoId, _filtro = filtro }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                if (resultadoCompleto != String.Empty)
                {
                    return new ResponseModel<IReadOnlyList<MunicipioCmbViewModel>>()
                    {
                        resultado = true,
                        data = JsonSerializer.Deserialize<IReadOnlyList<MunicipioCmbViewModel>>(resultadoCompleto)
                    };
                }
                else
                {
                    return new ResponseModel<IReadOnlyList<MunicipioCmbViewModel>>()
                    {
                        resultado = false,
                        data = new List<MunicipioCmbViewModel>(),
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "dsCmbMunicipiosAsync(int estadoId, string filtro)", detalleUsuario = new { _estadoId = estadoId, _filtro = filtro } });
                return new ResponseModel<IReadOnlyList<MunicipioCmbViewModel>>()
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "dsCmbMunicipiosAsync(int estadoId, string filtro)", detalleUsuario = new { _estadoId = estadoId, _filtro = filtro } });
                return new ResponseModel<IReadOnlyList<MunicipioCmbViewModel>>()
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

        public async Task<ResponseModel<IReadOnlyList<MunicipioViewModel>>> GetAllAsync(int estadoId)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spMunicipiosListadoPorEstadoId"
                    , new { _estadoId = estadoId }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                if (resultadoCompleto != string.Empty)
                {
                    return new ResponseModel<IReadOnlyList<MunicipioViewModel>>()
                    {
                        resultado = true,
                        data = JsonSerializer.Deserialize<IReadOnlyList<MunicipioViewModel>>(resultadoCompleto)
                    };
                }
                else
                {
                    return new ResponseModel<IReadOnlyList<MunicipioViewModel>>()
                    {
                        resultado = false,
                        data = new List<MunicipioViewModel>(),
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "GetAllAsync(int estadoId)" });
                return new ResponseModel<IReadOnlyList<MunicipioViewModel>>()
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "GetAllAsync(int estadoId)" });
                return new ResponseModel<IReadOnlyList<MunicipioViewModel>>()
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

        public async Task<ResponseModel<Option<MunicipioViewModel>>> GetByIdAsync(int id)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spMunicipiosListadoPorId"
                    , new { _municipioId = id }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                if (resultadoCompleto != String.Empty)
                {
                    return new ResponseModel<Option<MunicipioViewModel>>()
                    {
                        resultado = true,
                        data = JsonSerializer.Deserialize<MunicipioViewModel>(resultadoCompleto)
                    };
                }
                else
                {
                    return new ResponseModel<Option<MunicipioViewModel>>()
                    {
                        resultado = false,
                        data = new MunicipioViewModel(),
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "GetByIdAsync(int id)", detalleUsuario = new { _municipioId = id } });
                return new ResponseModel<Option<MunicipioViewModel>>()
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "GetByIdAsync(int id)", detalleUsuario = new { _municipioId = id } });
                return new ResponseModel<Option<MunicipioViewModel>>()
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
                string sql = @"SELECT IFNULL(MAX(municipioId), NULL) AS maxMunicipioId FROM municipios";
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

        public async Task<ResponseModel<Municipio>> UpdateAsync(Municipio entity)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();
            try
            {
                _dbContext.Municipios.Update(entity);
                await _dbContext.SaveChangesAsync(default).ConfigureAwait(false);
                tran.Commit();

                return new ResponseModel<Municipio>()
                {
                    data = entity,
                    resultado = true
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<Municipio>()
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
                return new ResponseModel<Municipio>()
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

        private async Task<ResponseModel<Option<Municipio>>> GetMunicipioByIdAsync(int id)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spMunicipiosListadoPorId"
                    , new { _municipioId = id }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                if (resultadoCompleto != string.Empty)
                {
                    return new ResponseModel<Option<Municipio>>()
                    {
                        resultado = true,
                        data = JsonSerializer.Deserialize<Municipio>(resultadoCompleto)
                    };
                }
                else
                {
                    return new ResponseModel<Option<Municipio>>()
                    {
                        resultado = false,
                        data = new Municipio(),
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "GetMunicipioByIdAsync(int id)", detalleUsuario = new { _municipioId = id } });
                return new ResponseModel<Option<Municipio>>()
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "GetMunicipioByIdAsync(int id)", detalleUsuario = new { _municipioId = id } });
                return new ResponseModel<Option<Municipio>>()
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