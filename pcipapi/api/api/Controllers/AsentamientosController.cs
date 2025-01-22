using entities.entities;
using entities.interfaces;
using entities.models;
using entities.models.Articulo;
using entities.models.Error;

using LanguageExt;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AsentamientosController : ControllerBase
    {
        private readonly IWorkRepository _unit;
        private ILogger<AsentamientosController> _logger;

        public AsentamientosController(ILogger<AsentamientosController> logger, IWorkRepository unit)
        {
            this._unit = unit;
            this._logger = logger;
        }

        [HttpGet, Authorize]
        [Route("[action]/{id:int}")]
        [SwaggerOperation(
            Summary = "Consulta",
            Description = "Consulta información sobre un asentamiento en especifico.",
            OperationId = "GetAsentamiento",
            Tags = new[] { "Catálogo operativo", "Asentamientos", "Consulta" })]
        [SwaggerResponse(200, "Asentamiento", typeof(ResponseModel<ArticuloViewModel>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> GetAsentamiento([FromRoute, SwaggerParameter(description: "Identificador interno del asentamiento.", Required = true)] int id)
        {
            try
            {
                var resultado = await _unit.Asentamiento.GetByIdAsync(id).ConfigureAwait(false);
                if (resultado.resultado)
                {
                    return Ok(resultado);
                }
                else
                {
                    throw new ArgumentException(resultado.error.errorMensaje);
                }
            }
            catch (System.Exception ex)
            {
                return Problem(detail: ex.Message, instance: HttpContext.Request.Path, statusCode: 500, title: "Consulta de articulo");
            }
        }

        [HttpGet, Authorize]
        [Route("[action]")]
        [SwaggerOperation(
            Summary = "Listado",
            Description = "Listado de Asentamientos.",
            OperationId = "GetArticulos",
            Tags = new[] { "Catálogo operativo", "Asentamientos", "Listado" })]
        [SwaggerResponse(200, "Asentamientos", typeof(ResponseModel<List<ArticuloViewModel>>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> GetArticulos()
        {
            try
            {
                var resultado = await _unit.Articulo.GetAllAsync().ConfigureAwait(false);
                if (resultado.resultado)
                {
                    return Ok(resultado);
                }
                else
                {
                    throw new ArgumentException(resultado.error.errorMensaje);
                }
            }
            catch (System.Exception ex)
            {
                return Problem(detail: ex.Message, instance: HttpContext.Request.Path, statusCode: 500, title: "Listado de Asentamientos");
            }
        }

        [HttpPost, Authorize]
        [Route("[action]")]
        [SwaggerOperation(
                Summary = "Agregar",
                Description = "Método para agregar un articulo.",
                OperationId = "ArticuloAgregar",
                Tags = new[] { "Catálogo operativo", "Asentamientos", "Agregar" })]
        [SwaggerResponse(200, "Articulo", typeof(ResponseModel<ArticuloViewModel>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> ArticuloAgregar([FromBody] Articulo item)
        {
            if (!ModelState.IsValid)
            {
                return Problem(detail: "Error al intentar eliminar el registro.", instance: HttpContext.Request.Path, statusCode: 500, title: "Articulo agregar");
            }

            try
            {
                item.articuloId = -1;
                var resultado = await _unit.Articulo.AddAsync(item).ConfigureAwait(false);
                if (resultado.resultado)
                {
                    return Ok(resultado);
                }
                else
                {
                    throw new ArgumentException(resultado.error.errorMensaje);
                }
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, instance: HttpContext.Request.Path, statusCode: 500, title: "Articulo agregar");
            }
        }

        [HttpPut, Authorize]
        [Route("[action]/{id:int}")]
        [SwaggerOperation(
            Summary = "Actualizar",
            Description = "Método para actualizar la información un articulo.",
            OperationId = "ArticuloActualizar",
            Tags = new[] { "Catálogo operativo", "Asentamientos", "Actualizar" })]
        [SwaggerResponse(200, "Articulo", typeof(ResponseModel<ArticuloViewModel>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> ArticuloActualizar([FromRoute, SwaggerParameter(description: "Identificador interno del articulo.", Required = true)] int id, [FromBody] Articulo item)
        {
            if (!ModelState.IsValid)
            {
                return Problem(detail: "Error al intentar eliminar el registro.", instance: HttpContext.Request.Path, statusCode: 500, title: "Articulo actualizar");
            }

            try
            {
                item.articuloId = id;
                var resultado = await _unit.Articulo.UpdateAsync(item).ConfigureAwait(false);
                if (resultado.resultado)
                {
                    return Ok(resultado);
                }
                else
                {
                    throw new ArgumentException(resultado.error.errorMensaje);
                }
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, instance: HttpContext.Request.Path, statusCode: 500, title: "Articulo actualizar");
            }
        }

        [HttpDelete, Authorize]
        [Route("[action]/{id:int}")]
        [SwaggerOperation(
            Summary = "Eliminar",
            Description = "Método para eliminar un articulo.",
            OperationId = "ArticuloEliminar",
            Tags = new[] { "Catálogo operativo", "Asentamientos", "Eliminar" })]
        [SwaggerResponse(200, "Articulo", typeof(ResponseModel<ArticuloViewModel>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> ArticuloEliminar([FromRoute, SwaggerParameter(description: "Identificador interno del articulo.", Required = true)] int id)
        {
            Option<Articulo> item = null;
            if (id <= 0)
            {
                return Problem(detail: "Error al intentar eliminar el registro.", instance: HttpContext.Request.Path, statusCode: 404, title: "Articulo eliminar");
            }
            else
            {
                var registro = await _unit.Articulo.GetByIdAsync(id).ConfigureAwait(false);
                if (registro.resultado)
                {
                    item = registro.data;
                }
                else
                {
                    return Problem(detail: registro.error.errorMensaje, instance: HttpContext.Request.Path, statusCode: 404, title: "Articulo eliminar");
                }
            }

            if (item == null)
            {
                return Problem(detail: "Error al buscar el registro, no ha encontrado algún registro que concida al solicitado.", instance: HttpContext.Request.Path, statusCode: 404, title: "Articulo eliminar");
            }

            var resultado = await _unit.Articulo.DeleteAsync((Articulo)item).ConfigureAwait(false);

            return Ok(resultado);
        }
    }
}