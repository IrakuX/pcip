using entities.entities;
using entities.models;
using entities.models.UnidadMedidaSAT;

namespace entities.interfaces
{
    public interface IUnidadMedidaSATRepository : IGenericAddRepository<UnidadMedidaSAT>,
        IGenericUpdateRepository<UnidadMedidaSAT>,
        IGenericDeleteRepository<UnidadMedidaSAT>,
        IGenericListRepository<UnidadMedidaSATViewModel>,
        IDisposable
    {
        Task<ResponseModel<IReadOnlyList<UnidadMedidaSATCmbViewModel>>> dsCmbUnidadesMedidaAsync(string filtro);
    }
}