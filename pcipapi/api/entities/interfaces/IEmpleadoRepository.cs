using entities.entities;
using entities.models;
using entities.models.Empleado;

namespace entities.interfaces
{
    public interface IEmpleadoRepository : IGenericAddRepository<Empleado>,
        IGenericUpdateRepository<Empleado>,
        IGenericDeleteRepository<Empleado>,
        IGenericListRepository<Empleado>,
        IDisposable
    {
        Task<ResponseModel<IReadOnlyList<EmpleadoCmbViewModel>>> dsCmbEmpleadosAsync(string filtro);
    }
}