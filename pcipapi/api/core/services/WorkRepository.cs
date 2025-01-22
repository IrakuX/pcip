using entities.interfaces;

namespace core.services
{
    public class WorkRepository : IWorkRepository
    {
        public WorkRepository(
            IAccesoRepository accesoRepository
            , IArticuloRepository articuloRepository
            , IAsentamientoRepository asentamientoRepository
            , IAlmacenRepository almacenRepository
            , ICategoriaArticuloRepository categoriaArticuloRepository
            , ICiudadRepository ciudadRepository
            , IContactoRepository contactoRepository
            , IEmpleadoRepository empleadoRepository
            , IEnvioCorreoRepository envioCorreoRepository
            , IEstadoRepository estadoRepository
            , IFraccionArancelariaRepository fraccionArancelariaRepository
            , IImpuestoRepository impuestoRepository
            , IMonedaMovimientoRepository monedaMovimientoRepository
            , IMonedaRepository monedaRepository
            , IMunicipioRepository municipioRepository
            , IObjetoRepository objetoRepository
            , IObjetoTipoRepository objetoTipoRepository
            , IPerfilRepository perfilRepository
            , IPuestoRepository puestoRepository
            , ITipoAsentamientoRepository tipoAsentamientoRepository
            , IUnidadMedidaRepository unidadMedidaRepository
            , IUnidadMedidaSATRepository unidadMedidaSATRepository
            , IUsuarioRepository usuarioRepository
            , IUsuarioTokenRepository usuarioTokenRepository)
        {
            Acceso = accesoRepository;
            Almacen = almacenRepository;
            Articulo = articuloRepository;
            Asentamiento = asentamientoRepository;
            CategoriaArticulo = categoriaArticuloRepository;
            Ciudad = ciudadRepository;
            Contacto = contactoRepository;
            Empleado = empleadoRepository;
            EnvioCorreo = envioCorreoRepository;
            Estado = estadoRepository;
            FraccionArancelaria = fraccionArancelariaRepository;
            Impuesto = impuestoRepository;
            MonedaMovimiento = monedaMovimientoRepository;
            Moneda = monedaRepository;
            Municipio = municipioRepository;
            Objeto = objetoRepository;
            ObjetoTipo = objetoTipoRepository;
            Perfil = perfilRepository;
            Puesto = puestoRepository;
            TipoAsentamiento = tipoAsentamientoRepository;
            UnidadMedida = unidadMedidaRepository;
            UnidadMedidaSAT = unidadMedidaSATRepository;
            Usuario = usuarioRepository;
            UsuarioToken = usuarioTokenRepository;
        }

        public IAccesoRepository Acceso { get; }
        public IAlmacenRepository Almacen { get; }
        public IArticuloRepository Articulo { get; }
        public IAsentamientoRepository Asentamiento { get; }
        public ICategoriaArticuloRepository CategoriaArticulo { get; }
        public ICiudadRepository Ciudad { get; }
        public IContactoRepository Contacto { get; }
        public IEmpleadoRepository Empleado { get; }
        public IEnvioCorreoRepository EnvioCorreo { get; }
        public IEstadoRepository Estado { get; }
        public IFraccionArancelariaRepository FraccionArancelaria { get; }
        public IImpuestoRepository Impuesto { get; }
        public IMonedaRepository Moneda { get; }
        public IMonedaMovimientoRepository MonedaMovimiento { get; }
        public IMunicipioRepository Municipio { get; }
        public IObjetoRepository Objeto { get; }
        public IObjetoTipoRepository ObjetoTipo { get; }
        public IPerfilRepository Perfil { get; }
        public IPuestoRepository Puesto { get; }
        public ITipoAsentamientoRepository TipoAsentamiento { get; }
        public IUnidadMedidaRepository UnidadMedida { get; }
        public IUnidadMedidaSATRepository UnidadMedidaSAT { get; }
        public IUsuarioRepository Usuario { get; }
        public IUsuarioTokenRepository UsuarioToken { get; }
    }
}