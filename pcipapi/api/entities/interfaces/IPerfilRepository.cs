using entities.entities;
using entities.models;

namespace entities.interfaces
{
    public interface IPerfilRepository : IGenericAddRepository<Perfil>,
        IGenericUpdateRepository<Perfil>,
        IGenericDeleteRepository<Perfil>,
        IGenericListRepository<Perfil>,
        IDisposable
    {
        Task<ResponseModel<IReadOnlyList<Perfil>>> dsCmbPerfilesAsync(string filtro);
    }
}