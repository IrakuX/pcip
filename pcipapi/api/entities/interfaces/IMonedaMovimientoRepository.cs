using entities.entities;
using entities.models;
using entities.models.MonedaMovimiento;

using LanguageExt;

namespace entities.interfaces
{
    public interface IMonedaMovimientoRepository : IGenericAddRepository<MonedaMovimiento>,
        IGenericUpdateRepository<MonedaMovimiento>,
        IGenericDeleteRepository<MonedaMovimiento>,
        IDisposable
    {
        Task<ResponseModel<IReadOnlyList<MonedaMovimientoViewModel>>> GetAllAsync(int monedaId);

        Task<ResponseModel<Option<MonedaMovimientoViewModel>>> GetByIdAsync(int id);

        Task<int?> GetMaxIdAsync();
    }
}