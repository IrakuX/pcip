using entities.interfaces;
using entities.models;
using entities.models.Usuario;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppSetting _appSettings;
        private readonly IConfiguration _config;
        private readonly IIdentityService _identityService;
        private readonly IWorkRepository _unit;

        public AuthController(IConfiguration config, IOptions<AppSetting> appSettings, IWorkRepository unit, IIdentityService identityService)
        {
            this._unit = unit;
            this._appSettings = appSettings.Value;
            this._config = config;
            this._identityService = identityService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginViewModel login)
        {
            var result = await _identityService.LoginAsync(login);
            if (result.resultado)
            {
                HttpContext.Response.Cookies.Append("cjwtpcip", result.data.token, new CookieOptions() { Secure = true, Expires = DateTime.Now.AddDays(30) });
                return Ok(result);
            }
            else
            {
                return Problem(detail: result?.error.errorMensaje, instance: null, statusCode: 500, title: "Login");
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Refresh([FromBody] TokenModel request)
        {
            var result = await _identityService.RefreshTokenAsync(request);
            return Ok(result);
        }
    }
}