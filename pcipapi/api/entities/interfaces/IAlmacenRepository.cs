using entities.entities;
using entities.models;
using entities.models.Almacen;

namespace entities.interfaces
{
    public interface IAlmacenRepository : IGenericAddRepository<Almacen>,
        IGenericUpdateRepository<Almacen>,
        IGenericDeleteRepository<Almacen>,
        IGenericListRepository<Almacen>,
        IDisposable
    {
        Task<ResponseModel<IReadOnlyList<AlmacenViewModel>>> AlmacenDetalleAsync(int id);

        Task<ResponseModel<IReadOnlyList<AlmacenViewModel>>> dsCmbAlmacenesAsync(bool almacenParaActivos);
    }
}