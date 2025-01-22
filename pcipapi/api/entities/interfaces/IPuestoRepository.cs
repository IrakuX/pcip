using entities.entities;
using entities.models;
using entities.models.Puesto;

namespace entities.interfaces
{
    public interface IPuestoRepository : IGenericAddRepository<Puesto>,
        IGenericUpdateRepository<Puesto>,
        IGenericDeleteRepository<Puesto>,
        IGenericListRepository<PuestoViewModel>,
        IDisposable
    {
        Task<ResponseModel<IReadOnlyList<PuestoCmbViewModel>>> dsCmbPuestosAsync(string filtro);
    }
}