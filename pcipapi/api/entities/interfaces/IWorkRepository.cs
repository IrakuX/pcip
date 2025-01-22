namespace entities.interfaces
{
    public interface IWorkRepository
    {
        IAccesoRepository Acceso { get; }
        IAlmacenRepository Almacen { get; }
        IArticuloRepository Articulo { get; }
        IAsentamientoRepository Asentamiento { get; }
        ICategoriaArticuloRepository CategoriaArticulo { get; }
        ICiudadRepository Ciudad { get; }
        IContactoRepository Contacto { get; }
        IEmpleadoRepository Empleado { get; }
        IEnvioCorreoRepository EnvioCorreo { get; }
        IEstadoRepository Estado { get; }
        IFraccionArancelariaRepository FraccionArancelaria { get; }
        IImpuestoRepository Impuesto { get; }
        IMonedaRepository Moneda { get; }
        IMonedaMovimientoRepository MonedaMovimiento { get; }
        IMunicipioRepository Municipio { get; }
        IObjetoRepository Objeto { get; }
        IObjetoTipoRepository ObjetoTipo { get; }
        IPerfilRepository Perfil { get; }
        IPuestoRepository Puesto { get; }
        ITipoAsentamientoRepository TipoAsentamiento { get; }
        IUnidadMedidaRepository UnidadMedida { get; }
        IUnidadMedidaSATRepository UnidadMedidaSAT { get; }
        IUsuarioRepository Usuario { get; }
        IUsuarioTokenRepository UsuarioToken { get; }
    }
}