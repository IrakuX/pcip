namespace entities.models.Articulo
{
    public class ArticuloViewModel : entities.Articulo
    {
        public ArticuloViewModel()
        {
            this.categoriaArticuloCodigoNombre = string.Empty;
            this.articuloCodigoNombre = string.Empty;
            this.unidadMedidaCodigoNombre = string.Empty;
        }

        public string categoriaArticuloCodigoNombre { get; set; }
        public string articuloCodigoNombre { get; set; }
        public string unidadMedidaCodigoNombre { get; set; }
    }
}