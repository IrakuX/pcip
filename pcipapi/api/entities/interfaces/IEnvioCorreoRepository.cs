using entities.models;
using entities.models.Usuario;

namespace entities.interfaces
{
    public interface IEnvioCorreoRepository : IDisposable
    {
        Task<bool> EnviarCorreoAsync(EnumNotificacion tipoNotificacion, params object[] args);

        Task<ResponseModel<UsuarioEmailViewModel>> GeneraCorreoBienvenidaAsync(int usuarioId);
    }
}