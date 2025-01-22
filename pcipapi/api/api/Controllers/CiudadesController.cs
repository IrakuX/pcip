using entities.entities;
using entities.interfaces;
using entities.models.Error;
using entities.models;

using LanguageExt;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Options;
using entities.models.Ciudad;

namespace api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadesController(ILogger<CiudadesController> logger, IConfiguration config, IOptions<AppSetting> appSettings, IWorkRepository unit) : ControllerBase
    {
        private readonly AppSetting _appSettings = appSettings.Value;
        private readonly IWorkRepository _unit = unit;
        private readonly IConfiguration _config = config;
        private ILogger<CiudadesController> _logger = logger;

        [HttpGet, Authorize]
        [Route("[action]/{id:int}")]
        [SwaggerOperation(
            Summary = "Consulta",
            Description = "Consulta información sobre una ciudad especifica.",
            OperationId = "GetCiudad",
            Tags = new[] { "Catálogo operativo", "Ciudades", "Consulta" })]
        [SwaggerResponse(200, "Ciudad", typeof(ResponseModel<CiudadViewModel>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> GetCiudad([FromRoute, SwaggerParameter(description: "Identificador interno de la ciudad.", Required = true)] int id)
        {
            try
            {
                var resultado = await _unit.Ciudad.GetByIdAsync(id).ConfigureAwait(false);
                if (resultado.resultado)
                {
                    return Ok(resultado);
                }
                else
                {
                    throw new ArgumentException(resultado?.error?.errorMensaje);
                }
            }
            catch (System.Exception ex)
            {
                return Problem(detail: ex.Message, instance: HttpContext.Request.Path, statusCode: 500, title: "Consulta de ciudad");
            }
        }

        [HttpGet, Authorize]
        [Route("[action]/{id:int}")]
        [SwaggerOperation(
            Summary = "Listado",
            Description = "Listado de ciudades.",
            OperationId = "GetCiudades",
            Tags = new[] { "Catálogo operativo", "Ciudades", "Listado" })]
        [SwaggerResponse(200, "Ciudades", typeof(ResponseModel<List<CiudadViewModel>>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> GetCiudades([FromQuery, SwaggerParameter(description: "Identificador interno del municipio.", Required = true)] int id)
        {
            try
            {
                var resultado = await _unit.Ciudad.GetAllAsync(id).ConfigureAwait(false);
                if (resultado.resultado)
                {
                    return Ok(resultado);
                }
                else
                {
                    throw new ArgumentException(resultado?.error?.errorMensaje);
                }
            }
            catch (System.Exception ex)
            {
                return Problem(detail: ex.Message, instance: HttpContext.Request.Path, statusCode: 500, title: "Listado de ciudades");
            }
        }

        [HttpPost, Authorize]
        [Route("[action]")]
        [SwaggerOperation(
                Summary = "Agregar",
                Description = "Método para agregar una ciudad.",
                OperationId = "CiudadAgregar",
                Tags = new[] { "Catálogo operativo", "Ciudades", "Agregar" })]
        [SwaggerResponse(200, "Ciudad", typeof(ResponseModel<CiudadViewModel>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> CiudadAgregar([FromBody] Ciudad item)
        {
            if (!ModelState.IsValid)
            {
                return Problem(detail: "Error al intentar eliminar el registro.", instance: HttpContext.Request.Path, statusCode: 500, title: "Ciudad agregar");
            }

            try
            {
                item.ciudadId = -1;
                var resultado = await _unit.Ciudad.AddAsync(item).ConfigureAwait(false);
                if (resultado.resultado)
                {
                    return Ok(resultado);
                }
                else
                {
                    throw new ArgumentException(resultado?.error?.errorMensaje);
                }
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, instance: HttpContext.Request.Path, statusCode: 500, title: "Ciudad agregar");
            }
        }

        [HttpPut, Authorize]
        [Route("[action]/{id:int}")]
        [SwaggerOperation(
            Summary = "Actualizar",
            Description = "Método para actualizar la información de una ciudad.",
            OperationId = "CiudadActualizar",
            Tags = new[] { "Catálogo operativo", "Ciudades", "Actualizar" })]
        [SwaggerResponse(200, "Articulo", typeof(ResponseModel<CiudadViewModel>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> CiudadActualizar([FromRoute, SwaggerParameter(description: "Identificador interno de la ciudad.", Required = true)] int id, [FromBody] Ciudad item)
        {
            if (!ModelState.IsValid)
            {
                return Problem(detail: "Error al intentar eliminar el registro.", instance: HttpContext.Request.Path, statusCode: 500, title: "Ciudad actualizar");
            }

            try
            {
                item.ciudadId = id;
                var resultado = await _unit.Ciudad.UpdateAsync(item).ConfigureAwait(false);
                if (resultado.resultado)
                {
                    return Ok(resultado);
                }
                else
                {
                    throw new ArgumentException(resultado?.error?.errorMensaje);
                }
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, instance: HttpContext.Request.Path, statusCode: 500, title: "Ciudad actualizar");
            }
        }

        [HttpDelete, Authorize]
        [Route("[action]/{id:int}")]
        [SwaggerOperation(
            Summary = "Eliminar",
            Description = "Método para eliminar una ciudad.",
            OperationId = "CiudadEliminar",
            Tags = new[] { "Catálogo operativo", "Ciudades", "Eliminar" })]
        [SwaggerResponse(200, "Articulo", typeof(ResponseModel<CiudadViewModel>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> CiudadEliminar([FromRoute, SwaggerParameter(description: "Identificador interno de la ciudad.", Required = true)] int id)
        {
            Option<CiudadViewModel> item = null;
            if (id <= 0)
            {
                return Problem(detail: "Error al intentar eliminar el registro.", instance: HttpContext.Request.Path, statusCode: 404, title: "Ciudad eliminar");
            }
            else
            {
                var registro = await _unit.Ciudad.GetByIdAsync(id).ConfigureAwait(false);
                if (registro.resultado)
                {
                    item = registro.data;
                }
                else
                {
                    return Problem(detail: registro?.error?.errorMensaje, instance: HttpContext.Request.Path, statusCode: 404, title: "Ciudad eliminar");
                }
            }

            if (item == null)
            {
                return Problem(detail: "Error al buscar el registro, no ha encontrado algún registro que concida al solicitado.", instance: HttpContext.Request.Path, statusCode: 404, title: "Ciudad eliminar");
            }

            var resultado = await _unit.Ciudad.DeleteAsync((Ciudad)item).ConfigureAwait(false);

            return Ok(resultado);
        }
    }
}