using entities.entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core.mappings
{
    public class UnidadMedidaSATMap : IEntityTypeConfiguration<UnidadMedidaSAT>
    {
        public void Configure(EntityTypeBuilder<UnidadMedidaSAT> builder)
        {
            builder.ToTable("UnidadMedidaSAT").HasKey(c => c.unidadId);
        }
    }
}