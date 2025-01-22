using entities.entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using System.Data;

namespace entities.interfaces
{
    public interface IApplicationDbContext : IDisposable
    {
        public DbSet<Acceso> Accesos { get; set; }
        public DbSet<Almacen> Almacenes { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Asentamiento> Asentamientos { get; set; }
        public DbSet<CategoriaArticulo> CategoriasArticulo { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
        public IDbConnection Connection { get; }
        public DbSet<Contacto> Contactos { get; set; }
        DatabaseFacade Database { get; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<FraccionArancelaria> FraccionArancelarias { get; set; }
        public DbSet<Impuesto> Impuestos { get; set; }
        public DbSet<MonedaMovimiento> MonedaMovimientos { get; set; }
        public DbSet<Moneda> Monedas { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Objeto> Objetos { get; set; }
        public DbSet<ObjetoTipo> ObjetoTipos { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }
        public DbSet<Puesto> Puestos { get; set; }
        public DbSet<TipoAsentamiento> TipoAsentamientos { get; set; }
        public DbSet<UnidadMedida> UnidadesMedida { get; set; }
        public DbSet<UnidadMedidaSAT> UnidadesMedidaSAT { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioToken> UsuarioTokens { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}