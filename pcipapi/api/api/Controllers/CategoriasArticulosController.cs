using entities.entities;
using entities.interfaces;
using entities.models;
using entities.models.Error;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.Annotations;

namespace api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasArticulosController(ILogger<CategoriasArticulosController> logger, IConfiguration config, IOptions<AppSetting> appSettings, IWorkRepository unit) : ControllerBase
    {
        private readonly AppSetting _appSettings = appSettings.Value;
        private readonly IWorkRepository _unit = unit;
        private readonly IConfiguration _config = config;
        private ILogger<CategoriasArticulosController> _logger = logger;

        [HttpGet, Authorize]
        [Route("[action]/{id:int}")]
        [SwaggerOperation(
            Summary = "Consulta",
            Description = "Consulta información sobre una categoría de articulo.",
            OperationId = "GetCategoriaArticulo",
            Tags = new[] { "Catálogo operativo", "Categorias articulos", "Consulta" })]
        [SwaggerResponse(200, "Categoría de articulo", typeof(ResponseModel<CategoriaArticulo>))]
        [SwaggerResponse(400, "Solicitud incorrecta", typeof(Error400))]
        [SwaggerResponse(401, "Accedo denegado. Token inválido", typeof(Error401))]
        [SwaggerResponse(500, "Error interno en el servidor", typeof(Error500))]
        [Produces("application/json")]
        public async Task<IActionResult> GetCategoriaArticulo([FromRoute, SwaggerParameter(description: "Identificador interno del categoría de articulo.", Required = true)] int id)
        {
            try
            {
                var resultado = await _unit.CategoriaArticulo.GetByIdAsync(id).ConfigureAwait(false);
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
                return Problem(detail: ex.Message, instance: HttpContext.Request.Path, statusCode: 500, title: "Consulta de categoría de articulo");
            }
        }
    }
}