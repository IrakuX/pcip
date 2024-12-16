using entities.models;
using entities.models.Usuario;

using System.Security.Claims;

namespace entities.interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> AuthenticateAsync(UsuarioViewModel usuario);

        ClaimsPrincipal GetPrincipalFromToken(string token);

        Task<ResponseModel<TokenModel>> LoginAsync(UsuarioLoginViewModel login);

        Task<ResponseModel<TokenModel>> RefreshTokenAsync(TokenModel request);
    }
}