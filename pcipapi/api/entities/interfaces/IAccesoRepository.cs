using entities.entities;
using entities.models;
using entities.models.Acceso;

using LanguageExt;

namespace entities.interfaces
{
    public interface IAccesoRepository : IDisposable
    {
        Task<ResponseModel<IReadOnlyList<Acceso>>> AddRangeAsync(List<Acceso> entities);

        Task<ResponseModel<IReadOnlyList<Acceso>>> DeletePorPerfilAsync(int perfilId);

        Task<ResponseModel<IReadOnlyList<AccesoDetalleViewModel>>> GetAccesoPorPerfilAsync(int perfilId);

        Task<ResponseModel<IReadOnlyList<AccesoDetalleViewModel>>> GetAccesoPorPerfilAsync(int perfilId, int objetoTipoId);

        Task<ResponseModel<IReadOnlyList<AccesoViewModel>>> GetAccesosAsync();

        Task<ResponseModel<Option<AccesoViewModel>>> GetByPerfilIdAsync(int perfilId);
    }
}