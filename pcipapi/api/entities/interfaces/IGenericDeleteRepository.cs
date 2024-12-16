using entities.models;

namespace entities.interfaces
{
    public interface IGenericDeleteRepository<T> where T : class
    {
        Task<ResponseModel<T>> DeleteAsync(T entity);

        Task<ResponseModel<T>> DeleteAsync(int id);
    }
}