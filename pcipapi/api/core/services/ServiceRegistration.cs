using core.repositories;

using entities.interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace core.services
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IAccesoRepository, AccesoRepository>();
            services.AddTransient<IAlmacenRepository, AlmacenRepository>();
            services.AddTransient<IArticuloRepository, ArticuloRepository>();
            services.AddTransient<IAsentamientoRepository, AsentamientoRepository>();
            services.AddTransient<ICategoriaArticuloRepository, CategoriaArticuloRepository>();
            services.AddTransient<ICiudadRepository, CiudadRepository>();
            services.AddTransient<IContactoRepository, ContactoRepository>();
            services.AddTransient<IEmpleadoRepository, EmpleadoRepository>();
            services.AddTransient<IEnvioCorreoRepository, EnvioCorreoRepository>();
            services.AddTransient<IEstadoRepository, EstadoRepository>();
            services.AddTransient<IFraccionArancelariaRepository, FraccionArancelariaRepository>();
            services.AddTransient<IImpuestoRepository, ImpuestoRepository>();
            services.AddTransient<IMonedaRepository, MonedaRepository>();
            services.AddTransient<IMonedaMovimientoRepository, MonedaMovimientoRepository>();
            services.AddTransient<IMunicipioRepository, MunicipioRepository>();
            services.AddTransient<IObjetoRepository, ObjetoRepository>();
            services.AddTransient<IObjetoTipoRepository, ObjetoTipoRepository>();
            services.AddTransient<IPerfilRepository, PerfilRepository>();
            services.AddTransient<IPuestoRepository, PuestoRepository>();
            services.AddTransient<ITipoAsentamientoRepository, TipoAsentamientoRepository>();
            services.AddTransient<IUnidadMedidaRepository, UnidadMedidaRepository>();
            services.AddTransient<IUnidadMedidaSATRepository, UnidadMedidaSATRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IUsuarioTokenRepository, UsuarioTokenRepository>();
            services.AddTransient<IWorkRepository, WorkRepository>();
        }
    }
}