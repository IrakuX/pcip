using entities.entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core.mappings
{
    public class ImpuestoMap : IEntityTypeConfiguration<Impuesto>
    {
        public void Configure(EntityTypeBuilder<Impuesto> builder)
        {
            builder.ToTable<Impuesto>("impuestos").HasKey(c => c.impuestoId);
        }
    }
}