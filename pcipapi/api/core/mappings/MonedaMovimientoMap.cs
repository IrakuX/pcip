using entities.entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core.mappings
{
    public class MonedaMovimientoMap : IEntityTypeConfiguration<MonedaMovimiento>
    {
        public void Configure(EntityTypeBuilder<MonedaMovimiento> builder)
        {
            builder.ToTable<MonedaMovimiento>("monedasMovimientos").HasKey(c => c.monedaMovimientoId);
        }
    }
}