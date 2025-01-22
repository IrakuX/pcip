using entities.entities;
using entities.models;
using entities.models.Contacto;

namespace entities.interfaces
{
    public interface IContactoRepository : IGenericAddRepository<Contacto>,
        IGenericUpdateRepository<Contacto>,
        IGenericDeleteRepository<Contacto>,
        IGenericListRepository<ContactoViewModel>,
        IDisposable
    {
        Task<ResponseModel<IReadOnlyList<ContactoCmbViewModel>>> dsCmbContactosAsync(string filtro);
    }
}