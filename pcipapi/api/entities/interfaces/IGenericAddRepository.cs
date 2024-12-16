using entities.models;

namespace entities.interfaces
{
    public interface IGenericAddRepository<T> where T : class
    {
        Task<ResponseModel<T>> AddAsync(T entity);
    }
}