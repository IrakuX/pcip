using entities.entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core.mappings
{
    public class MonedaMap : IEntityTypeConfiguration<Moneda>
    {
        public void Configure(EntityTypeBuilder<Moneda> builder)
        {
            builder.ToTable<Moneda>("monedas").HasKey(c => c.monedaId);
        }
    }
}