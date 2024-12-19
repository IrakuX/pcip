namespace entities.interfaces
{
    public interface IWorkRepository
    {
        IAccesoRepository Acceso { get; }
        IAlmacenRepository Almacen { get; }
        IArticuloRepository Articulo { get; }
        ICategoriaArticuloRepository CategoriaArticulo { get; }
        IEmpleadoRepository Empleado { get; }
        IEnvioCorreoRepository EnvioCorreo { get; }
        IObjetoRepository Objeto { get; }
        IObjetoTipoRepository ObjetoTipo { get; }
        IPerfilRepository Perfil { get; }
        IPuestoRepository Puesto { get; }
        IUnidadMedidaRepository UnidadMedida { get; }
        IUsuarioRepository Usuario { get; }
        IUsuarioTokenRepository UsuarioToken { get; }
    }
}