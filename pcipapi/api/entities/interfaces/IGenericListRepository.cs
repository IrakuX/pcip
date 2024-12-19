using entities.models;

namespace entities.interfaces
{
    public interface IGenericListRepository<T> where T : class
    {
        //Task<ResponseModel<IReadOnlyList<T>>> GetAllAsync(string filtro, int pagina, int paginaTamanio);
        Task<ResponseModel<IReadOnlyList<T>>> GetAllAsync();

        Task<ResponseModel<LanguageExt.Option<T>>> GetByIdAsync(int id);

        Task<int?> GetMaxIdAsync();
    }
}