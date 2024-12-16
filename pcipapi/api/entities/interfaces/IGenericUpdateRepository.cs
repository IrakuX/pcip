using entities.models;

namespace entities.interfaces
{
    public interface IGenericUpdateRepository<T> where T : class
    {
        Task<ResponseModel<T>> UpdateAsync(T entity);
    }
}