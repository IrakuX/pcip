using entities.entities;
using entities.models;
using entities.models.Objeto;

namespace entities.interfaces
{
    public interface IObjetoRepository : IGenericListRepository<Objeto>, IDisposable
    {
        Task<ResponseModel<LanguageExt.Option<ObjetoViewModel>>> GetObjetoAsyn(int objetoId);
    }
}