using entities.entities;
using entities.models;
using entities.models.Usuario;

using LanguageExt;

namespace entities.interfaces
{
    public interface IUsuarioRepository : IGenericAddRepository<Usuario>,
        IGenericUpdateRepository<Usuario>,
        IGenericDeleteRepository<Usuario>,
        IGenericListRepository<Usuario>,
        IDisposable
    {
        Task<ResponseModel<Option<UsuarioViewModel>>> UsuarioLoginAsync(string usuarioEmail, string usuarioContrasena);
    }
}