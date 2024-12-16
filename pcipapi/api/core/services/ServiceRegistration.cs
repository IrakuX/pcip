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
            services.AddTransient<IArticuloRepository, ArticuloRepository>();
            services.AddTransient<ICategoriaArticuloRepository, CategoriaArticuloRepository>();
            services.AddTransient<IEmpleadoRepository, EmpleadoRepository>();
            services.AddTransient<IEnvioCorreoRepository, EnvioCorreoRepository>();
            services.AddTransient<IObjetoRepository, ObjetoRepository>();
            services.AddTransient<IObjetoTipoRepository, ObjetoTipoRepository>();
            services.AddTransient<IPerfilRepository, PerfilRepository>();
            services.AddTransient<IPuestoRepository, PuestoRepository>();
            services.AddTransient<IUnidadMedidaRepository, UnidadMedidaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IUsuarioTokenRepository, UsuarioTokenRepository>();
            services.AddTransient<IWorkRepository, WorkRepository>();
        }
    }
}