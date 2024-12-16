using entities.entities;
using entities.models;
using entities.models.CategoriaArticulo;

namespace entities.interfaces
{
    public interface ICategoriaArticuloRepository : IGenericAddRepository<CategoriaArticulo>,
        IGenericUpdateRepository<CategoriaArticulo>,
        IGenericDeleteRepository<CategoriaArticulo>,
        IGenericListRepository<CategoriaArticulo>,
        IDisposable
    {
        Task<ResponseModel<IReadOnlyList<CategoriaArticuloCmbViewModel>>> dsCmbCategoriasArticuloAsync(string filtro);
    }
}