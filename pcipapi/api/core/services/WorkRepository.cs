using entities.interfaces;

namespace core.services
{
    public class WorkRepository : IWorkRepository
    {
        public WorkRepository(
            IAccesoRepository accesoRepository
            , IArticuloRepository articuloRepository
            , IAlmacenRepository almacenRepository
            , ICategoriaArticuloRepository categoriaArticuloRepository
            , IEmpleadoRepository empleadoRepository
            , IEnvioCorreoRepository envioCorreoRepository
            , IObjetoRepository objetoRepository
            , IObjetoTipoRepository objetoTipoRepository
            , IPerfilRepository perfilRepository
            , IPuestoRepository puestoRepository
            , IUnidadMedidaRepository unidadMedidaRepository
            , IUsuarioRepository usuarioRepository
            , IUsuarioTokenRepository usuarioTokenRepository)
        {
            Acceso = accesoRepository;
            Almacen = almacenRepository;
            Articulo = articuloRepository;
            CategoriaArticulo = categoriaArticuloRepository;
            Empleado = empleadoRepository;
            EnvioCorreo = envioCorreoRepository;
            Objeto = objetoRepository;
            ObjetoTipo = objetoTipoRepository;
            Perfil = perfilRepository;
            Puesto = puestoRepository;
            UnidadMedida = unidadMedidaRepository;
            Usuario = usuarioRepository;
            UsuarioToken = usuarioTokenRepository;
        }

        public IAccesoRepository Acceso { get; }
        public IAlmacenRepository Almacen { get; }
        public IArticuloRepository Articulo { get; }
        public ICategoriaArticuloRepository CategoriaArticulo { get; }
        public IEmpleadoRepository Empleado { get; }
        public IEnvioCorreoRepository EnvioCorreo { get; }
        public IObjetoRepository Objeto { get; }
        public IObjetoTipoRepository ObjetoTipo { get; }
        public IPerfilRepository Perfil { get; }
        public IPuestoRepository Puesto { get; }
        public IUnidadMedidaRepository UnidadMedida { get; }
        public IUsuarioRepository Usuario { get; }
        public IUsuarioTokenRepository UsuarioToken { get; }
    }
}