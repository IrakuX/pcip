using entities.entities;
using entities.models;
using entities.models.Articulo;

namespace entities.interfaces
{
    public interface IArticuloRepository : IGenericAddRepository<Articulo>,
        IGenericUpdateRepository<Articulo>,
        IGenericDeleteRepository<Articulo>,
        IGenericListRepository<Articulo>,
        IDisposable
    {
        Task<ResponseModel<ArticuloDetalleViewModel>> ArticuloDetalleAsync(int id);

        Task<ResponseModel<IReadOnlyList<ArticuloCmbViewModel>>> dsCmbArticulosAsync(int categoriaArticuloId, string filtro);
    }
}