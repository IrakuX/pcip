using entities.entities;
using entities.interfaces;
using entities.models;
using entities.models.Error;
using entities.models.Municipio;

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
    public class MunicipiosController(ILogger<MunicipiosController> logger, IConfiguration config, IOptions<AppSetting> appSettings, IWorkRepository unit) : ControllerBase
    {
        private readonly AppSetting _appSettings = appSettings.Value;
        private readonly IWorkRepository _unit = unit;
        private readonly IConfiguration _config = config;
        private ILogger<MunicipiosController> _logger = logger;

        [HttpGet, Authorize]
        [Route("[action]/{id:int}")]
        [SwaggerOperation(
            Summary = "Consulta",
            Description = "Consulta información sobre un municipio especifica.",
            OperationId = "GetMunicipio",
            Tags = new[] { "Catálogo operativo", "Municipios", "Consulta" })]
        [SwaggerResponse(200, "Municipio", typeof(ResponseModel<MunicipioViewModel>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> GetMunicipio([FromRoute, SwaggerParameter(description: "Identificador interno del municipio.", Required = true)] int id)
        {
            try
            {
                var resultado = await _unit.Municipio.GetByIdAsync(id).ConfigureAwait(false);
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
                return Problem(detail: ex.Message, instance: HttpContext.Request.Path, statusCode: 500, title: "Consulta de municipio");
            }
        }

        [HttpGet, Authorize]
        [Route("[action]/{id:int}")]
        [SwaggerOperation(
                    Summary = "Listado",
                    Description = "Listado de municipios.",
                    OperationId = "GetMunicipios",
                    Tags = new[] { "Catálogo operativo", "Municipios", "Listado" })]
        [SwaggerResponse(200, "Municipios", typeof(ResponseModel<List<MunicipioViewModel>>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> GetMunicipios([FromRoute, SwaggerParameter(description: "Identificador interno del municipio.", Required = true)] int id)
        {
            try
            {
                var resultado = await _unit.Municipio.GetAllAsync(id).ConfigureAwait(false);
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
                return Problem(detail: ex.Message, instance: HttpContext.Request.Path, statusCode: 500, title: "Listado de municipios");
            }
        }

        [HttpPost, Authorize]
        [Route("[action]")]
        [SwaggerOperation(
                        Summary = "Agregar",
                        Description = "Método para agregar un municipio.",
                        OperationId = "MunicipioAgregar",
                        Tags = new[] { "Catálogo operativo", "Municipios", "Agregar" })]
        [SwaggerResponse(200, "Municipio", typeof(ResponseModel<MunicipioViewModel>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> MunicipioAgregar([FromBody] Municipio item)
        {
            if (!ModelState.IsValid)
            {
                return Problem(detail: "Error al intentar eliminar el registro.", instance: HttpContext.Request.Path, statusCode: 500, title: "Municipio agregar");
            }

            try
            {
                item.estadoId = -1;
                var resultado = await _unit.Municipio.AddAsync(item).ConfigureAwait(false);
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
                return Problem(detail: ex.Message, instance: HttpContext.Request.Path, statusCode: 500, title: "Municipio agregar");
            }
        }

        [HttpPut, Authorize]
        [Route("[action]/{id:int}")]
        [SwaggerOperation(
                    Summary = "Actualizar",
                    Description = "Método para actualizar la información de un municipio.",
                    OperationId = "MunicipioActualizar",
                    Tags = new[] { "Catálogo operativo", "Municipios", "Actualizar" })]
        [SwaggerResponse(200, "Municipio", typeof(ResponseModel<MunicipioViewModel>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> MunicipioActualizar([FromRoute, SwaggerParameter(description: "Identificador interno del municipio.", Required = true)] int id, [FromBody] Municipio item)
        {
            if (!ModelState.IsValid)
            {
                return Problem(detail: "Error al intentar eliminar el registro.", instance: HttpContext.Request.Path, statusCode: 500, title: "Municipio actualizar");
            }

            try
            {
                item.estadoId = id;
                var resultado = await _unit.Municipio.UpdateAsync(item).ConfigureAwait(false);
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
                return Problem(detail: ex.Message, instance: HttpContext.Request.Path, statusCode: 500, title: "Municipio actualizar");
            }
        }

        [HttpDelete, Authorize]
        [Route("[action]/{id:int}")]
        [SwaggerOperation(
                    Summary = "Eliminar",
                    Description = "Método para eliminar un municipio.",
                    OperationId = "MunicipioEliminar",
                    Tags = new[] { "Catálogo operativo", "Municipios", "Eliminar" })]
        [SwaggerResponse(200, "Municipio", typeof(ResponseModel<MunicipioViewModel>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> MunicipioEliminar([FromRoute, SwaggerParameter(description: "Identificador interno del municipio.", Required = true)] int id)
        {
            Option<MunicipioViewModel> item = null;
            if (id <= 0)
            {
                return Problem(detail: "Error al intentar eliminar el registro.", instance: HttpContext.Request.Path, statusCode: 404, title: "Municipio eliminar");
            }
            else
            {
                var registro = await _unit.Municipio.GetByIdAsync(id).ConfigureAwait(false);
                if (registro.resultado)
                {
                    item = registro.data;
                }
                else
                {
                    return Problem(detail: registro?.error?.errorMensaje, instance: HttpContext.Request.Path, statusCode: 404, title: "Municipio eliminar");
                }
            }

            if (item == null)
            {
                return Problem(detail: "Error al buscar el registro, no ha encontrado algún registro que concida al solicitado.", instance: HttpContext.Request.Path, statusCode: 404, title: "Municipio eliminar");
            }

            var resultado = await _unit.Municipio.DeleteAsync((Municipio)item).ConfigureAwait(false);

            return Ok(resultado);
        }
    }
}