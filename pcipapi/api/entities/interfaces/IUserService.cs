using entities.entities;
using entities.models;

namespace entities.interfaces
{
    public interface IUserService
    {
        Task<ResponseModel<Usuario>> GetAsync(int usuarioId);
    }
}