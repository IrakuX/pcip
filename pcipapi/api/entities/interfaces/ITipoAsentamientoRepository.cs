using entities.entities;
using entities.models;
using entities.models.TipoAsentamiento;

namespace entities.interfaces
{
    public interface ITipoAsentamientoRepository : IGenericAddRepository<TipoAsentamiento>,
        IGenericUpdateRepository<TipoAsentamiento>,
        IGenericDeleteRepository<TipoAsentamiento>,
        IGenericListRepository<TipoAsentamientoViewModel>,
        IDisposable
    {
        Task<ResponseModel<IReadOnlyList<TipoAsentamientoCmbViewModel>>> dsCmbTiposAsentamientosAsync(string filtro);
    }
}