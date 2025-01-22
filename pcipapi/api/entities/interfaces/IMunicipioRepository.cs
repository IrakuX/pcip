using entities.entities;
using entities.models;
using entities.models.Municipio;

using LanguageExt;

namespace entities.interfaces
{
    public interface IMunicipioRepository : IGenericAddRepository<Municipio>,
        IGenericUpdateRepository<Municipio>,
        IGenericDeleteRepository<Municipio>,
        IDisposable
    {
        Task<ResponseModel<IReadOnlyList<MunicipioCmbViewModel>>> dsCmbMunicipiosAsync(int estadoId, string filtro);

        Task<ResponseModel<IReadOnlyList<MunicipioViewModel>>> GetAllAsync(int estadoId);

        Task<ResponseModel<Option<MunicipioViewModel>>> GetByIdAsync(int id);
    }
}