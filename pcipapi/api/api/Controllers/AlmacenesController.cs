using entities.entities;
using entities.interfaces;
using entities.models;
using entities.models.Almacen;
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
    public class AlmacenesController : ControllerBase
    {
        private readonly IWorkRepository _unit;
        private ILogger<AlmacenesController> _logger;

        public AlmacenesController(ILogger<AlmacenesController> logger, IWorkRepository unit)
        {
            this._unit = unit;
            this._logger = logger;
        }

        [HttpGet, Authorize]
        [Route("[action]/{id:int}")]
        [SwaggerOperation(
            Summary = "Almacén",
            Description = "Consulta información sobre un almacén en especifico.",
            OperationId = "GetAlmacen",
            Tags = new[] { "Catálogo operativo", "Almacenes", "Almacén" })]
        [SwaggerResponse(200, "Almacén.", typeof(ResponseModel<AlmacenViewModel>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> GetAlmacen([FromRoute, SwaggerParameter(description: "Identificador interno del almacén.", Required = true)] int id)
        {
            try
            {
                var resultado = await _unit.Almacen.GetByIdAsync(id).ConfigureAwait(false);
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
                return Problem(detail: ex.Message, instance: HttpContext.Request.Path, statusCode: 500, title: "Almacén");
            }
        }

        [HttpGet, Authorize]
        [Route("[action]")]
        [SwaggerOperation(
            Summary = "Almacenes",
            Description = "Listado de almacenes.",
            OperationId = "GetAlmacenes",
            Tags = new[] { "Catálogo operativo", "Almacenes" })]
        [SwaggerResponse(200, "Almacenes.", typeof(ResponseModel<List<Almacen>>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> GetAlmacenes()
        {
            try
            {
                var resultado = await _unit.Almacen.GetAllAsync().ConfigureAwait(false);
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
                return Problem(detail: ex.Message, instance: HttpContext.Request.Path, statusCode: 500, title: "Almacenes");
            }
        }

        [HttpPost, Authorize]
        [Route("[action]")]
        [SwaggerOperation(
            Summary = "Almacén agregar",
            Description = "Método para agregar un almacén.",
            OperationId = "AlmacenAgregar",
            Tags = new[] { "Catálogo operativo", "Almacenes" })]
        [SwaggerResponse(200, "Almacenes.", typeof(ResponseModel<Almacen>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> AlmacenAgregar([FromBody] Almacen almacen)
        {
            if (!ModelState.IsValid)
            {
                return Problem(detail: "Error al intentar eliminar el registro.", instance: HttpContext.Request.Path, statusCode: 500, title: "Almacenes");
            }

            try
            {
                almacen.almacenId = -1;
                var resultado = await _unit.Almacen.AddAsync(almacen).ConfigureAwait(false);
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
                return Problem(detail: ex.Message, instance: HttpContext.Request.Path, statusCode: 500, title: "Almacenes");
            }
        }

        [HttpPut, Authorize]
        [Route("[action]/{id:int}")]
        [SwaggerOperation(
            Summary = "Almacén actualizar",
            Description = "Método para actualizar la información un almacén.",
            OperationId = "AlmacenActualizar",
            Tags = new[] { "Catálogo operativo", "Almacenes" })]
        [SwaggerResponse(200, "Almacenes.", typeof(ResponseModel<Almacen>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> AlmacenActualizar([FromRoute, SwaggerParameter(description: "Identificador interno del almacén.", Required = true)] int id, [FromBody] Almacen almacen)
        {
            if (!ModelState.IsValid)
            {
                return Problem(detail: "Error al intentar eliminar el registro.", instance: HttpContext.Request.Path, statusCode: 500, title: "Almacenes");
            }

            try
            {
                almacen.almacenId = id;
                var resultado = await _unit.Almacen.UpdateAsync(almacen).ConfigureAwait(false);
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
                return Problem(detail: ex.Message, instance: HttpContext.Request.Path, statusCode: 500, title: "Almacenes");
            }
        }

        [HttpDelete, Authorize]
        [Route("[action]/{id:int}")]
        [SwaggerOperation(
            Summary = "Almacé eliminar",
            Description = "Método para eliminar un almacén.",
            OperationId = "AlmacenEliminar",
            Tags = new[] { "Catálogo operativo", "Almacenes" })]
        [SwaggerResponse(200, "Almacenes.", typeof(ResponseModel<Almacen>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> AlmacenEliminar([FromRoute, SwaggerParameter(description: "Identificador interno del almacén.", Required = true)] int id)
        {
            Option<Almacen> almacen = null;
            if (id <= 0)
            {
                return Problem(detail: "Error al intentar eliminar el registro.", instance: HttpContext.Request.Path, statusCode: 404, title: "Almacen eliminar");
            }
            else
            {
                var registro = await _unit.Almacen.GetByIdAsync(id).ConfigureAwait(false);
                if (registro.resultado)
                {
                    almacen = registro.data;
                }
                else
                {
                    return BadRequest(new { error = registro.error });
                }
            }

            if (almacen == null)
            {
                return Problem(detail: "Error al buscar el registro, no ha encontrado algún registro que concida al solicitado.", instance: HttpContext.Request.Path, statusCode: 404, title: "Almacen eliminar");
            }

            var resultado = await _unit.Almacen.DeleteAsync((Almacen)almacen).ConfigureAwait(false);

            return Ok(resultado);
        }
    }
}