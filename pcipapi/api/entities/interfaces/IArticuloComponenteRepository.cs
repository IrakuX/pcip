using entities.models;

namespace entities.interfaces
{
    public interface IArticuloComponenteRepository : IGenericAddRepository<ArticuloCompuesto>,
        IGenericUpdateRepository<ArticuloCompuesto>,
        IGenericDeleteRepository<ArticuloCompuesto>,
        IDisposable
    {
        Task<ResponseModel<IReadOnlyList<ArticuloCompuestoViewModel>>> dsArticuloCompuestosAsync(int articuloId);

        ResponseModel<IReadOnlyList<ArticuloCompuestoViewModel>> dsArticuloCompuestos(int articuloId);

        ResponseModel<IReadOnlyList<ArticuloCompuestoViewModel>> dsCmbArticuloCompuestos(int articuloId, string filtro);

        Task<ResponseModel<IReadOnlyList<ArticuloCompuestoViewModel>>> dsCmbArticuloCompuestosAsync(int articuloId, string filtro);
    }
}