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
        public virtual DbSet<Almacen> Almacenes { get; set; }
        public virtual DbSet<Articulo> Articulos { get; set; }
        public virtual DbSet<Asentamiento> Asentamientos { get; set; }
        public virtual DbSet<CategoriaArticulo> CategoriasArticulo { get; set; }
        public virtual DbSet<Ciudad> Ciudades { get; set; }
        public virtual DbSet<Contacto> Contactos { get; set; }
        public IDbConnection Connection => Database.GetDbConnection();
        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<FraccionArancelaria> FraccionArancelarias { get; set; }
        public virtual DbSet<Impuesto> Impuestos { get; set; }
        public virtual DbSet<Moneda> Monedas { get; set; }
        public virtual DbSet<MonedaMovimiento> MonedaMovimientos { get; set; }
        public virtual DbSet<Municipio> Municipios { get; set; }
        public virtual DbSet<Objeto> Objetos { get; set; }
        public virtual DbSet<ObjetoTipo> ObjetoTipos { get; set; }
        public virtual DbSet<Perfil> Perfiles { get; set; }
        public virtual DbSet<Puesto> Puestos { get; set; }
        public virtual DbSet<TipoAsentamiento> TipoAsentamientos { get; set; }
        public virtual DbSet<UnidadMedida> UnidadesMedida { get; set; }
        public virtual DbSet<UnidadMedidaSAT> UnidadesMedidaSAT { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<UsuarioToken> UsuarioTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AccesoMap());
            modelBuilder.ApplyConfiguration(new AlmacenMap());
            modelBuilder.ApplyConfiguration(new ArticuloMap());
            modelBuilder.ApplyConfiguration(new AsentamientoMap());
            modelBuilder.ApplyConfiguration(new CategoriaArticuloMap());
            modelBuilder.ApplyConfiguration(new CiudadMap());
            modelBuilder.ApplyConfiguration(new ContactoMap());
            modelBuilder.ApplyConfiguration(new EmpleadoMap());
            modelBuilder.ApplyConfiguration(new EstadoMap());
            modelBuilder.ApplyConfiguration(new FraccionArancelariaMap());
            modelBuilder.ApplyConfiguration(new ImpuestoMap());
            modelBuilder.ApplyConfiguration(new MonedaMap());
            modelBuilder.ApplyConfiguration(new MonedaMovimientoMap());
            modelBuilder.ApplyConfiguration(new MunicipioMap());
            modelBuilder.ApplyConfiguration(new ObjetoTipoMap());
            modelBuilder.ApplyConfiguration(new ObjetoMap());
            modelBuilder.ApplyConfiguration(new PerfilMap());
            modelBuilder.ApplyConfiguration(new PuestoMap());
            modelBuilder.ApplyConfiguration(new TipoAsentamientoMap());
            modelBuilder.ApplyConfiguration(new UnidadMedidaMap());
            modelBuilder.ApplyConfiguration(new UnidadMedidaSATMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new UsuarioTokenMap());
        }
    }
}