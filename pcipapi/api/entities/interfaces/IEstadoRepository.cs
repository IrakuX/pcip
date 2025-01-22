using entities.entities;
using entities.models;
using entities.models.Estado;

namespace entities.interfaces
{
    public interface IEstadoRepository : IGenericAddRepository<Estado>,
        IGenericUpdateRepository<Estado>,
        IGenericDeleteRepository<Estado>,
        IGenericListRepository<EstadoViewModel>,
        IDisposable
    {
        Task<ResponseModel<IReadOnlyList<EstadoCmbViewModel>>> dsCmbEstadosAsync(string filtro);
    }
}