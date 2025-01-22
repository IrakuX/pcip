using entities.entities;
using entities.models;
using entities.models.FraccionArancelaria;

namespace entities.interfaces
{
    public interface IFraccionArancelariaRepository : IGenericAddRepository<FraccionArancelaria>,
        IGenericUpdateRepository<FraccionArancelaria>,
        IGenericDeleteRepository<FraccionArancelaria>,
        IGenericListRepository<FraccionArancelariaViewModel>,
        IDisposable
    {
        Task<ResponseModel<IReadOnlyList<FraccionArancelariaCmbViewModel>>> dsCmbFracccionesArancelariasAsync(string filtro);
    }
}