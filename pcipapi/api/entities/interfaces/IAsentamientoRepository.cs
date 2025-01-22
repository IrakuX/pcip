using entities.entities;
using entities.models;
using entities.models.Asentamiento;

namespace entities.interfaces
{
    public interface IAsentamientoRepository : IGenericAddRepository<Asentamiento>,
        IGenericUpdateRepository<Asentamiento>,
        IGenericDeleteRepository<Asentamiento>,
        IGenericListRepository<AsentamientoViewModel>,
        IDisposable
    {
        Task<ResponseModel<IReadOnlyList<AsentamientoCmbViewModel>>> dsCmbAsentamientosAsync(int? estadoId, int? municipioId, int? ciudadId, string asentamientoCodigoPostal);

        Task<ResponseModel<IReadOnlyList<AsentamientoViewModel>>> dsAsentamientosAsync(string filtro);
    }
}