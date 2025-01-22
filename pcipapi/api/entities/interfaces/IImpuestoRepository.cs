using entities.entities;
using entities.models;
using entities.models.Impuesto;

namespace entities.interfaces
{
    public interface IImpuestoRepository : IGenericAddRepository<Impuesto>,
        IGenericUpdateRepository<Impuesto>,
        IGenericDeleteRepository<Impuesto>,
        IGenericListRepository<ImpuestoViewModel>,
        IDisposable
    {
        Task<ResponseModel<IReadOnlyList<ImpuestoCmbViewModel>>> dsCmbImpuestosAsync(string filtro);
    }
}