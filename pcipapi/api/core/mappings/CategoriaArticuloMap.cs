using entities.entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core.mappings
{
    public class CategoriaArticuloMap : IEntityTypeConfiguration<CategoriaArticulo>
    {
        public void Configure(EntityTypeBuilder<CategoriaArticulo> builder)
        {
            builder.ToTable("categoriasArticulo").HasKey(c => c.categoriaArticuloId);
        }
    }
}