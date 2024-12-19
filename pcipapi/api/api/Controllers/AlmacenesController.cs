using core.utils;

using entities.interfaces;
using entities.models;
using entities.models.Almacen;
using entities.models.Error;
using entities.models.Usuario;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.Annotations;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlmacenesController : ControllerBase
    {
        private readonly AppSetting _appSettings;
        private readonly IWorkRepository _unit;
        private readonly IConfiguration _config;
        private UsuarioSesionViewModel _usuarioSesion;

        public AlmacenesController(ILogger<AlmacenesController> logger, IConfiguration config, IOptions<AppSetting> appSettings, IWorkRepository unit)
        {
            this._unit = unit;
            this._appSettings = appSettings.Value;
            this._config = config;
        }

        [HttpGet, Authorize]
        [Route("[action]/{id:int}")]
        [SwaggerOperation(
            Summary = "Consulta de almacén",
            Description = "Consulta información sobre un almacén en especifico.",
            OperationId = "GetAlmacen",
            Tags = new[] { "Catálogo operativo", "Almacenes", "Consulta de almacén" })]
        [SwaggerResponse(200, "Almacén.", typeof(ResponseModel<AlmacenViewModel>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> GetAlmacen([FromRoute, SwaggerParameter(description: "Identificador interno del almacén.", Required = true)] int id)
        {
            this._usuarioSesion = CUtilidades.ObtenerUsuario(HttpContext.User.Identity);
            var resultado = await _unit.Almacen.GetByIdAsync(id).ConfigureAwait(false);
            return Ok(resultado);
        }
    }
}