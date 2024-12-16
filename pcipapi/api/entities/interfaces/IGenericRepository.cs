using entities.models;

namespace entities.interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<ResponseModel<T>> AddAsync(T entity);

        Task<ResponseModel<T>> DeleteAsync(T entity);

        Task<ResponseModel<T>> DeleteAsync(int id);

        Task<ResponseModel<IReadOnlyList<T>>> GetAllAsync(string filtro, int page, int pageSizes);

        Task<ResponseModel<LanguageExt.Option<T>>> GetByIdAsync(int id);

        Task<int?> GetMaxIdAsync();

        Task<ResponseModel<T>> UpdateAsync(T entity);
    }
}