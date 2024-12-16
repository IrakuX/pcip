using entities.entities;
using entities.models;

using LanguageExt;

namespace entities.interfaces
{
    public interface IUsuarioTokenRepository : IGenericAddRepository<UsuarioToken>,
        IGenericUpdateRepository<UsuarioToken>,
        IDisposable
    {
        Task<ResponseModel<Option<UsuarioToken>>> GetByIdAsync(Guid usuarioToken);

        Task<ResponseModel<UsuarioToken>> DeleteAsync(Guid usuarioToken);
    }
}