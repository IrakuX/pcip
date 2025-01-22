using entities.entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core.mappings
{
    internal class EstadoMap : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable<Estado>("estados").HasKey(c => c.estadoId);
        }
    }
}