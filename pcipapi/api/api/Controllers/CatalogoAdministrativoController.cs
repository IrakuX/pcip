using entities.interfaces;
using entities.models;
using entities.models.Usuario;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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

        #endregion ALMACENES
    }
}