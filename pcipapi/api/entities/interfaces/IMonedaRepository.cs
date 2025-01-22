using entities.entities;
using entities.models;
using entities.models.Moneda;

namespace entities.interfaces
{
    public interface IMonedaRepository : IGenericAddRepository<Moneda>,
        IGenericUpdateRepository<Moneda>,
        IGenericDeleteRepository<Moneda>,
        IGenericListRepository<MonedaViewModel>,
        IDisposable
    {
        Task<ResponseModel<IReadOnlyList<MonedaCmbViewModel>>> dsCmbMonedasAsync(string filtro);
    }
}