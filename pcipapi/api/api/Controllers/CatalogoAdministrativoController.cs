using entities.interfaces;
using entities.models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoAdministrativoController(ILogger<CatalogoAdministrativoController> logger, IConfiguration config, IOptions<AppSetting> appSettings, IWorkRepository unit) : ControllerBase
    {
        private readonly AppSetting _appSettings = appSettings.Value;
        private readonly IWorkRepository _unit = unit;
        private readonly IConfiguration _config = config;
        private ILogger<CatalogoAdministrativoController> _logger = logger;
    }
}