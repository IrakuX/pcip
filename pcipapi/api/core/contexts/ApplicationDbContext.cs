using core.mappings;

using entities.entities;
using entities.interfaces;

using Microsoft.EntityFrameworkCore;

using System.Data;

namespace core.contexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly DbContextOptions _options;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            _options = options;
        }

        public virtual DbSet<Acceso> Accesos { get; set; }
        public virtual DbSet<Articulo> Articulos { get; set; }
        public virtual DbSet<CategoriaArticulo> CategoriasArticulo { get; set; }
        public IDbConnection Connection => Database.GetDbConnection();
        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<Objeto> Objetos { get; set; }
        public virtual DbSet<ObjetoTipo> ObjetoTipos { get; set; }
        public virtual DbSet<Perfil> Perfiles { get; set; }
        public virtual DbSet<Puesto> Puestos { get; set; }
        public virtual DbSet<UnidadMedida> UnidadesMedida { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<UsuarioToken> UsuarioTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AccesoMap());
            modelBuilder.ApplyConfiguration(new ArticuloMap());
            modelBuilder.ApplyConfiguration(new CategoriaArticuloMap());
            modelBuilder.ApplyConfiguration(new EmpleadoMap());
            modelBuilder.ApplyConfiguration(new ObjetoMap());
            modelBuilder.ApplyConfiguration(new ObjetoTipoMap());
            modelBuilder.ApplyConfiguration(new PerfilMap());
            modelBuilder.ApplyConfiguration(new PuestoMap());
            modelBuilder.ApplyConfiguration(new UnidadMedidaMap());
            modelBuilder.ApplyConfiguration(new UnidadMedidaSATMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new UsuarioTokenMap());
        }
    }
}