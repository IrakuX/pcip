using entities.entities;
using entities.interfaces;
using entities.models;
using entities.models.Usuario;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace core.services
{
    public class IdentityService : IIdentityService
    {
        private readonly AppSetting _appSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IWorkRepository _unit;

        public IdentityService(IOptions<AppSetting> settings, TokenValidationParameters tokenValidationParameters, IWorkRepository unit)
        {
            this._appSettings = settings.Value;
            this._unit = unit;
            this._tokenValidationParameters = tokenValidationParameters;
        }

        public async Task<AuthenticationResult> AuthenticateAsync(UsuarioViewModel usuario)
        {
            AuthenticationResult authenticationResult = new AuthenticationResult();
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var perfil = await _unit.Perfil.GetByIdAsync(usuario.perfilId.Value);
                var key = Encoding.ASCII.GetBytes(_appSettings.key);

                ClaimsIdentity Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("usuarioId", usuario.usuarioId.ToString()),
                    new Claim("usuarioNombre", usuario.usuarioNombre),
                    new Claim("usuarioApellidos", usuario.usuarioApellidos),
                    new Claim("usuarioEmail", usuario.usuarioEmail),
                    new Claim("perfilId", usuario.perfilId.ToString()),
                    new Claim("empleadoId", usuario.empleadoId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Aud, _appSettings.audience),
                    new Claim(JwtRegisteredClaimNames.Iss, _appSettings.issuer),
                    new Claim(JwtRegisteredClaimNames.UniqueName, usuario.usuarioEmail),
                    new Claim(JwtRegisteredClaimNames.Email, usuario.usuarioEmail),
                    new Claim(JwtRegisteredClaimNames.GivenName, string.Format("{0} {1}", usuario.usuarioNombre,usuario.usuarioApellidos))
                });

                if (perfil.resultado)
                {
                    Subject.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.usuarioEmail));
                    Subject.AddClaim(new Claim(ClaimTypes.Email, usuario.usuarioEmail));
                    Subject.AddClaim(new Claim(ClaimTypes.Role, ((Perfil)perfil.data).perfilNombre));
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = Subject,
                    Expires = DateTime.UtcNow.Add(_appSettings.tokenLifetime),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = _appSettings.issuer,
                    Audience = _appSettings.audience
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                authenticationResult.token = tokenHandler.WriteToken(token);

                var refreshToken = new UsuarioToken
                {
                    usuarioToken = Guid.NewGuid(),
                    usuarioJwtId = Guid.Parse(token.Id),
                    usuarioId = usuario.usuarioId,
                    usuarioTokenFechaCreacion = DateTime.UtcNow,
                    usuarioTokenFechaExpiracion = DateTime.UtcNow.AddMonths(6)
                };

                await _unit.UsuarioToken.AddAsync(refreshToken);

                authenticationResult.refreshToken = refreshToken.usuarioToken;
                authenticationResult.success = true;
                return authenticationResult;
            }
            catch (Exception ex)
            {
                return new AuthenticationResult()
                {
                    errors = new string[] { ex.Message },
                    refreshToken = Guid.Empty,
                    success = false,
                    token = string.Empty
                };
            }
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ResponseModel<TokenModel>> LoginAsync(UsuarioLoginViewModel login)
        {
            ResponseModel<TokenModel> response = new ResponseModel<TokenModel>();
            try
            {
                var usuario = await _unit.Usuario.UsuarioLoginAsync(login.usuarioEmail, login.usuarioContrasena).ConfigureAwait(false);
                if (!usuario.resultado)
                {
                    response.error = new ErrorModel()
                    {
                        error = null,
                        errorCodigo = 000,
                        errorMensaje = $"Nombre de usuario y contraseña inválidos. Detalle: {usuario?.error?.errorMensaje}."
                    };
                    return response;
                }

                AuthenticationResult authenticationResult = await AuthenticateAsync((UsuarioViewModel)usuario.data);
                if (authenticationResult != null && authenticationResult.success)
                {
                    response.resultado = true;
                    response.data = new TokenModel() { token = authenticationResult.token, refreshToken = authenticationResult.refreshToken };
                }
                else
                {
                    response.error = new ErrorModel()
                    {
                        errorCodigo = 000,
                        errorMensaje = "Error al generar la autentificación."
                    };
                }
            }
            catch (Exception ex)
            {
                response.error = new ErrorModel()
                {
                    errorCodigo = 000,
                    error = ex,
                    errorMensaje = ex.Message
                };
            }

            return response;
        }

        public async Task<ResponseModel<TokenModel>> RefreshTokenAsync(TokenModel request)
        {
            ResponseModel<TokenModel> response = new ResponseModel<TokenModel>();
            try
            {
                var authResponse = await GetRefreshTokenAsync(request.token, request.refreshToken);
                if (!authResponse.success)
                {
                    response.resultado = false;
                    response.error = new ErrorModel()
                    {
                        errorCodigo = 000,
                        errorMensaje = string.Join(",", authResponse.errors)
                    };
                    return response;
                }
                TokenModel refreshTokenModel = new TokenModel();
                refreshTokenModel.token = authResponse.token;
                refreshTokenModel.refreshToken = authResponse.refreshToken;
                response.data = refreshTokenModel;
                return response;
            }
            catch (Exception ex)
            {
                response.resultado = false;
                response.error = new ErrorModel()
                {
                    errorCodigo = 000,
                    error = ex,
                    errorMensaje = ex.Message
                };
                return response;
            }
        }

        private async Task<AuthenticationResult> GetRefreshTokenAsync(string token, Guid refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);

            if (validatedToken == null)
            {
                return new AuthenticationResult { errors = new[] { "Token no valido." } };
            }

            var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult { errors = new[] { "Este token aún no ha caducado." } };
            }

            var jti = new Guid(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value);

            var storedRefreshToken = await _unit.UsuarioToken.GetByIdAsync(refreshToken);

            if (storedRefreshToken.data == LanguageExt.Option<UsuarioToken>.None)
            {
                return new AuthenticationResult { errors = new[] { "Este token de actualización no existe." } };
            }

            if (DateTime.UtcNow > ((UsuarioToken)storedRefreshToken.data).usuarioTokenFechaExpiracion)
            {
                return new AuthenticationResult { errors = new[] { "Este token de actualización ha caducado." } };
            }

            if (((UsuarioToken)storedRefreshToken.data).usuarioTokenUsado == true)
            {
                return new AuthenticationResult { errors = new[] { "Este token de actualización se ha utilizado." } };
            }

            if (((UsuarioToken)storedRefreshToken.data).usuarioJwtId != jti)
            {
                return new AuthenticationResult { errors = new[] { "Este token de actualización no coincide con este JWT." } };
            }

            ((UsuarioToken)storedRefreshToken.data).usuarioTokenUsado = true;
            await _unit.UsuarioToken.UpdateAsync((UsuarioToken)storedRefreshToken.data);
            string strUserId = validatedToken.Claims.Single(x => x.Type == "usuarioId").Value;
            int userId = 0;
            int.TryParse(strUserId, out userId);

            var user = await _unit.Usuario.GetByIdAsync(userId).ConfigureAwait(false);
            if (user.data == LanguageExt.Option<Usuario>.None)
            {
                return new AuthenticationResult { errors = new[] { "Usuario no encontrado." } };
            }

            return await AuthenticateAsync((UsuarioViewModel)user.data);
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }
    }
}