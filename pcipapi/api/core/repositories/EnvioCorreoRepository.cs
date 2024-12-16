using Dapper;

using entities.entities;
using entities.interfaces;
using entities.models;
using entities.models.Usuario;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using MySqlConnector;

using SendGrid;
using SendGrid.Helpers.Mail;

using System.Data;
using System.Net.Mail;
using System.Text.Json;

namespace core.repositories
{
    public class EnvioCorreoRepository : IEnvioCorreoRepository
    {
        private readonly IConfiguration _config;
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public EnvioCorreoRepository(ILogger<EnvioCorreoRepository> logger, IConfiguration configuration, IApplicationDbContext dbContext)
        {
            _logger = logger;
            _config = configuration;
            _dbContext = dbContext;
        }

        private string apiKey
        {
            get
            {
                return (_config.GetSection("AppSettings:sendGridKey").Value);
            }
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

        public async Task<bool> EnviarCorreoAsync(EnumNotificacion tipoNotificacion, params object[] args)
        {
            string titulo = string.Empty,
                mensaje = string.Empty;

            switch (tipoNotificacion)
            {
                case EnumNotificacion.UsuarioNuevo:
                    List<string> categoria = new List<string>() { "usuarioNuevo" };
                    Usuario? usuario = args.Length > 0 ? (Usuario)args[0] : null;
                    if (usuario != null)
                    {
                        var objetoCorreo = await GeneraCorreoBienvenidaAsync(usuario.usuarioId).ConfigureAwait(false);
                        if (objetoCorreo.resultado)
                        {
                            titulo = objetoCorreo.data.titulo;
                            mensaje = objetoCorreo.data.mensaje;

                            var client = new SendGridClient(apiKey);
                            var msg = new SendGridMessage()
                            {
                                From = new EmailAddress("notificaciones@pcip.mx", "Notificaciones PCIP"),
                                Subject = titulo,
                                HtmlContent = mensaje,
                                Categories = categoria
                            };

                            msg.AddTo(new EmailAddress(usuario.usuarioEmail, String.Format("{0} {1}", usuario.usuarioNombre, usuario.usuarioApellidos)));

                            var resultado = await client.SendEmailAsync(msg).ConfigureAwait(false);
                            return resultado.IsSuccessStatusCode;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    break;

                default:
                    return false;
                    break;
            }
        }

        public async Task<ResponseModel<UsuarioEmailViewModel>> GeneraCorreoBienvenidaAsync(int usuarioId)
        {
            try
            {
                db.Open();
                var result = await db.QueryAsync<string>("spUsuariosBienvenida"
                    , new { usuarioId }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                string resultadoCompleto = string.Concat(result);
                var resultado = JsonSerializer.Deserialize<UsuarioEmailViewModel>(resultadoCompleto);
                return new ResponseModel<UsuarioEmailViewModel>()
                {
                    resultado = true,
                    data = resultado
                };
            }
            catch (MySqlException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error sql", error = ex, detalleMetodo = "GeneraCorreoBienvenidaAsync(int usuarioId)", detalleUsuario = new { usuarioId } });
                return new ResponseModel<UsuarioEmailViewModel>()
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
                _logger.Log(LogLevel.Error, ex.Message, new { errorTipo = "Error no sql", error = ex, detalleMetodo = "GeneraCorreoBienvenidaAsync(int usuarioId)", detalleUsuario = new { usuarioId } });
                return new ResponseModel<UsuarioEmailViewModel>()
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