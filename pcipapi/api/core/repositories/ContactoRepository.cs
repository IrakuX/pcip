using Dapper;

using entities.entities;
using entities.interfaces;
using entities.models;
using entities.models.Contacto;

using LanguageExt;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using MySqlConnector;

using System.Data;
using System.Text.Json;

namespace core.repositories
{
    public class ContactoRepository : IContactoRepository
    {
        private readonly IConfiguration _config;
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public ContactoRepository(ILogger<ContactoRepository> logger, IConfiguration configuration, IApplicationDbContext dbContext)
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

        public async Task<ResponseModel<Contacto>> AddAsync(Contacto entity)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
                var resultadoId = await GetMaxIdAsync().ConfigureAwait(false);
                entity.contactoId = (resultadoId == null ? 0 : resultadoId.Value) + 1;

                await _dbContext.Contactos.AddAsync(entity).ConfigureAwait(false);
                await _dbContext.SaveChangesAsync(default).ConfigureAwait(false);
                tran.Commit();

                return new ResponseModel<Contacto>()
                {
                    data = entity,
                    resultado = true
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<Contacto>()
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
                return new ResponseModel<Contacto>()
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

        public async Task<ResponseModel<Contacto>> DeleteAsync(Contacto entity)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
                _dbContext.Contactos.Remove(entity);
                await _dbContext.SaveChangesAsync(default).ConfigureAwait(false);
                tran.Commit();

                return new ResponseModel<Contacto>()
                {
                    resultado = true,
                    data = entity
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<Contacto>()
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
                return new ResponseModel<Contacto>()
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

        public async Task<ResponseModel<Contacto>> DeleteAsync(int id)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();

            try
            {
                var registro = await GetContactoByIdAsync(id).ConfigureAwait(false);
                if (registro.data == Option<Contacto>.None)
                {
                    throw new ArgumentException("Registro de objeto invalido.");
                }

                _dbContext.Contactos.Remove((Contacto)registro.data);
                await _dbContext.SaveChangesAsync(default).ConfigureAwait(false);
                tran.Commit();

                return new ResponseModel<Contacto>()
                {
                    resultado = true,
                    data = (Contacto)registro.data
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<Contacto>()
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
                return new ResponseModel<Contacto>()
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

        private async Task<ResponseModel<Option<Contacto>>> GetContactoByIdAsync(int id)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spContactosListadoPorId"
                    , new { _contactoId = id }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                if (resultadoCompleto != string.Empty)
                {
                    return new ResponseModel<Option<Contacto>>()
                    {
                        resultado = true,
                        data = JsonSerializer.Deserialize<Contacto>(resultadoCompleto)
                    };
                }
                else
                {
                    return new ResponseModel<Option<Contacto>>()
                    {
                        resultado = false,
                        data = new Contacto(),
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "GetContactoByIdAsync(int id)", detalleUsuario = new { _contactoId = id } });
                return new ResponseModel<Option<Contacto>>()
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "GetContactoByIdAsync(int id)", detalleUsuario = new { _contactoId = id } });
                return new ResponseModel<Option<Contacto>>()
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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<ResponseModel<IReadOnlyList<ContactoCmbViewModel>>> dsCmbContactosAsync(string filtro)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spContactosListadoCmb"
                    , new { filtro }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                if (resultadoCompleto != String.Empty)
                {
                    return new ResponseModel<IReadOnlyList<ContactoCmbViewModel>>()
                    {
                        resultado = true,
                        data = JsonSerializer.Deserialize<IReadOnlyList<ContactoCmbViewModel>>(resultadoCompleto)
                    };
                }
                else
                {
                    return new ResponseModel<IReadOnlyList<ContactoCmbViewModel>>()
                    {
                        resultado = false,
                        data = new List<ContactoCmbViewModel>(),
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "dsCmbContactosAsync(string filtro)", detalleUsuario = new { filtro } });
                return new ResponseModel<IReadOnlyList<ContactoCmbViewModel>>()
                {
                    resultado = false,
                    data = new List<ContactoCmbViewModel>(),
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "dsCmbContactosAsync(string filtro)", detalleUsuario = new { filtro } });
                return new ResponseModel<IReadOnlyList<ContactoCmbViewModel>>()
                {
                    resultado = false,
                    data = new List<ContactoCmbViewModel>(),
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

        public async Task<ResponseModel<IReadOnlyList<ContactoViewModel>>> GetAllAsync()
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spContactosListado"
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                if (resultadoCompleto != String.Empty)
                {
                    return new ResponseModel<IReadOnlyList<ContactoViewModel>>()
                    {
                        resultado = true,
                        data = JsonSerializer.Deserialize<IReadOnlyList<ContactoViewModel>>(resultadoCompleto)
                    };
                }
                else
                {
                    return new ResponseModel<IReadOnlyList<ContactoViewModel>>()
                    {
                        resultado = false,
                        data = new List<ContactoViewModel>(),
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
                return new ResponseModel<IReadOnlyList<ContactoViewModel>>()
                {
                    resultado = false,
                    data = new List<ContactoViewModel>(),
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
                return new ResponseModel<IReadOnlyList<ContactoViewModel>>()
                {
                    resultado = false,
                    data = new List<ContactoViewModel>(),
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

        public async Task<ResponseModel<Option<ContactoViewModel>>> GetByIdAsync(int id)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spContactosListadoPorId"
                    , new { _puestoId = id }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                if (resultadoCompleto != String.Empty)
                {
                    return new ResponseModel<Option<ContactoViewModel>>()
                    {
                        resultado = true,
                        data = JsonSerializer.Deserialize<ContactoViewModel>(resultadoCompleto)
                    };
                }
                else
                {
                    return new ResponseModel<Option<ContactoViewModel>>()
                    {
                        resultado = false,
                        data = new ContactoViewModel(),
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "GetByIdAsync(int id)", detalleUsuario = new { _puestoId = id } });
                return new ResponseModel<Option<ContactoViewModel>>()
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "GetByIdAsync(int id)", detalleUsuario = new { _puestoId = id } });
                return new ResponseModel<Option<ContactoViewModel>>()
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
                string sql = @"SELECT IFNULL(MAX(contactoId), NULL) AS max_contactoId FROM contactos";
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

        public async Task<ResponseModel<Contacto>> UpdateAsync(Contacto entity)
        {
            using IDbConnection cn = db;
            cn.Open();
            using var tran = cn.BeginTransaction();
            try
            {
                _dbContext.Contactos.Update(entity);
                await _dbContext.SaveChangesAsync(default).ConfigureAwait(false);
                tran.Commit();

                return new ResponseModel<Contacto>()
                {
                    data = entity,
                    resultado = true
                };
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                return new ResponseModel<Contacto>()
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
                return new ResponseModel<Contacto>()
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