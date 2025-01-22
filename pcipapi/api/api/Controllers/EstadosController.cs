using entities.entities;
using entities.interfaces;
using entities.models;
using entities.models.Ciudad;
using entities.models.Error;
using entities.models.Estado;

using LanguageExt;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.Annotations;

namespace api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosController(ILogger<EstadosController> logger, IConfiguration config, IOptions<AppSetting> appSettings, IWorkRepository unit) : ControllerBase
    {
        private readonly AppSetting _appSettings = appSettings.Value;
        private readonly IWorkRepository _unit = unit;
        private readonly IConfiguration _config = config;
        private ILogger<EstadosController> _logger = logger;

        [HttpGet, Authorize]
        [Route("[action]/{id:int}")]
        [SwaggerOperation(
            Summary = "Consulta",
            Description = "Consulta información sobre un estado especifica.",
            OperationId = "GetEstado",
            Tags = new[] { "Catálogo operativo", "Estados", "Consulta" })]
        [SwaggerResponse(200, "Estado", typeof(ResponseModel<EstadoViewModel>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> GetEstado([FromRoute, SwaggerParameter(description: "Identificador interno del estado.", Required = true)] int id)
        {
            try
            {
                var resultado = await _unit.Estado.GetByIdAsync(id).ConfigureAwait(false);
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
                return Problem(detail: ex.Message, instance: HttpContext.Request.Path, statusCode: 500, title: "Consulta de estado");
            }
        }

        [HttpGet, Authorize]
        [Route("[action]")]
        [SwaggerOperation(
                Summary = "Listado",
                Description = "Listado de estados.",
                OperationId = "GetEstados",
                Tags = new[] { "Catálogo operativo", "Estados", "Listado" })]
        [SwaggerResponse(200, "Estados", typeof(ResponseModel<List<EstadoViewModel>>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> GetEstados()
        {
            try
            {
                var resultado = await _unit.Estado.GetAllAsync().ConfigureAwait(false);
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
                return Problem(detail: ex.Message, instance: HttpContext.Request.Path, statusCode: 500, title: "Listado de estados");
            }
        }

        [HttpPost, Authorize]
        [Route("[action]")]
        [SwaggerOperation(
                    Summary = "Agregar",
                    Description = "Método para agregar un estado.",
                    OperationId = "EstadoAgregar",
                    Tags = new[] { "Catálogo operativo", "Estados", "Agregar" })]
        [SwaggerResponse(200, "Estado", typeof(ResponseModel<EstadoViewModel>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> EstadoAgregar([FromBody] Estado item)
        {
            if (!ModelState.IsValid)
            {
                return Problem(detail: "Error al intentar eliminar el registro.", instance: HttpContext.Request.Path, statusCode: 500, title: "Estado agregar");
            }

            try
            {
                item.estadoId = -1;
                var resultado = await _unit.Estado.AddAsync(item).ConfigureAwait(false);
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
                return Problem(detail: ex.Message, instance: HttpContext.Request.Path, statusCode: 500, title: "Estado agregar");
            }
        }

        [HttpPut, Authorize]
        [Route("[action]/{id:int}")]
        [SwaggerOperation(
                Summary = "Actualizar",
                Description = "Método para actualizar la información de un estado.",
                OperationId = "EstadoActualizar",
                Tags = new[] { "Catálogo operativo", "Estados", "Actualizar" })]
        [SwaggerResponse(200, "Estado", typeof(ResponseModel<EstadoViewModel>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> EstadoActualizar([FromRoute, SwaggerParameter(description: "Identificador interno de un estado.", Required = true)] int id, [FromBody] Estado item)
        {
            if (!ModelState.IsValid)
            {
                return Problem(detail: "Error al intentar eliminar el registro.", instance: HttpContext.Request.Path, statusCode: 500, title: "Estado actualizar");
            }

            try
            {
                item.estadoId = id;
                var resultado = await _unit.Estado.UpdateAsync(item).ConfigureAwait(false);
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
                return Problem(detail: ex.Message, instance: HttpContext.Request.Path, statusCode: 500, title: "Estado actualizar");
            }
        }

        [HttpDelete, Authorize]
        [Route("[action]/{id:int}")]
        [SwaggerOperation(
                Summary = "Eliminar",
                Description = "Método para eliminar un estado.",
                OperationId = "EstadoEliminar",
                Tags = new[] { "Catálogo operativo", "Estados", "Eliminar" })]
        [SwaggerResponse(200, "Estado", typeof(ResponseModel<EstadoViewModel>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> EstadoEliminar([FromRoute, SwaggerParameter(description: "Identificador interno de un estado.", Required = true)] int id)
        {
            Option<EstadoViewModel> item = null;
            if (id <= 0)
            {
                return Problem(detail: "Error al intentar eliminar el registro.", instance: HttpContext.Request.Path, statusCode: 404, title: "Estado eliminar");
            }
            else
            {
                var registro = await _unit.Estado.GetByIdAsync(id).ConfigureAwait(false);
                if (registro.resultado)
                {
                    item = registro.data;
                }
                else
                {
                    return Problem(detail: registro?.error?.errorMensaje, instance: HttpContext.Request.Path, statusCode: 404, title: "Estado eliminar");
                }
            }

            if (item == null)
            {
                return Problem(detail: "Error al buscar el registro, no ha encontrado algún registro que concida al solicitado.", instance: HttpContext.Request.Path, statusCode: 404, title: "Estado eliminar");
            }

            var resultado = await _unit.Estado.DeleteAsync((Estado)item).ConfigureAwait(false);

            return Ok(resultado);
        }
    }
}