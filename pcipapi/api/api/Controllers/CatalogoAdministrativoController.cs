using core.utils;

using entities.interfaces;
using entities.models;
using entities.models.Usuario;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.Annotations;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoAdministrativoController : ControllerBase
    {
        #region ALMACENES

        private readonly AppSetting _appSettings;
        private readonly IWorkRepository _unit;
        private readonly IConfiguration _config;
        private UsuarioSesionViewModel _usuarioSesion;

        public CatalogoAdministrativoController(ILogger<CatalogoAdministrativoController> logger, IConfiguration config, IOptions<AppSetting> appSettings, IWorkRepository unit)
        {
            this._unit = unit;
            this._appSettings = appSettings.Value;
            this._config = config;
        }

        [HttpGet, Authorize]
        [Route("[action]")]
        [SwaggerOperation(
    Summary = "Consulta el saldo de los contactos",
    Description = "Listado de contactos registrados con su saldo y limite de credito asignado.<br /><b>Limitado a 30 registros por petición como maximo por petición 50.</b>",
    OperationId = "GetContactosSaldo",
    Tags = new[] { "Configuración", "Catalogos - Contactos", "Consulta el saldo de los contactos" })]
        [SwaggerResponse(200, "Listado de contactos con saldo y limite de credito.", typeof(ResponseModel<ResponseData<IReadOnlyList<ContactoSaldo>>>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(entities.models.Error.Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(entities.models.Error.Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(entities.models.Error.Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> ContactosSaldo([FromQuery, SwaggerParameter(description: "Número de pagina a la que se desea consultar.", Required = false)] int? opcionPagina = 1
    , [FromQuery, SwaggerParameter(description: "Número de registros por pagina a la que se desea obtener. <br /> Como valor inicial es 30 pero puedes tener un tamaño maximo hasta 50.", Required = false)] int? opcionPaginaTamanio = 30)
        {
            int pagina = 1
                , paginaTamanio = 30;

            if ((opcionPagina != null) && (opcionPagina.Value > 0))
            {
                pagina = opcionPagina.Value;
            }

            if ((opcionPaginaTamanio != null) && ((opcionPaginaTamanio.Value < 0) || (opcionPaginaTamanio.Value >= 51)))
            {
                paginaTamanio = 50;
            }
            else if (opcionPagina == null)
            {
                paginaTamanio = 30;
            }
            else
            {
                paginaTamanio = opcionPaginaTamanio.Value;
            }

            this._usuarioSesion = CUtilidades.ObtenerUsuario(HttpContext.User.Identity, _config.GetConnectionString("cnnBase"));
            _unit.Contacto._cadenaConexion = _usuarioSesion.despachoConexion;
            var resultado = await _unit.Contacto.GetContactosSaldo(_usuarioSesion.organizacionId, pagina, paginaTamanio);
            return Ok(resultado);
        }

        #endregion ALMACENES
    }
}