using entities.entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core.mappings
{
    public class TipoAsentamientoMap : IEntityTypeConfiguration<TipoAsentamiento>
    {
        public void Configure(EntityTypeBuilder<TipoAsentamiento> builder)
        {
            builder.ToTable("tiposAsentamientos").HasKey(c => c.tipoAsentamientoId);
        }
    }
}