using entities.entities;
using entities.models;
using entities.models.Ciudad;

using LanguageExt;

namespace entities.interfaces
{
    public interface ICiudadRepository : IGenericAddRepository<Ciudad>,
        IGenericUpdateRepository<Ciudad>,
        IGenericDeleteRepository<Ciudad>,
        IDisposable
    {
        Task<ResponseModel<Option<CiudadViewModel>>> GetByIdAsync(int id);

        Task<ResponseModel<IReadOnlyList<CiudadViewModel>>> GetAllAsync(int municipioId);

        Task<ResponseModel<IReadOnlyList<CiudadCmbViewModel>>> dsCmbCiudadesAsync(int municipioId, string filtro);
    }
}