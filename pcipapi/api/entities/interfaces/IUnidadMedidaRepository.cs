using entities.entities;
using entities.models;
using entities.models.UnidadMedida;

namespace entities.interfaces
{
    public interface IUnidadMedidaRepository : IGenericAddRepository<UnidadMedida>,
        IGenericUpdateRepository<UnidadMedida>,
        IGenericDeleteRepository<UnidadMedida>,
        IGenericListRepository<UnidadMedida>,
        IDisposable
    {
        Task<ResponseModel<IReadOnlyList<UnidadMedidaCmbViewModel>>> dsCmbUnidadesMedidaAsync(string filtro);
    }
}