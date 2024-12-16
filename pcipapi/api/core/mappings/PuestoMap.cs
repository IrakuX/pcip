using entities.entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core.mappings
{
    public class PuestoMap : IEntityTypeConfiguration<Puesto>
    {
        public void Configure(EntityTypeBuilder<Puesto> builder)
        {
            builder.ToTable("puestos").HasKey(c => c.puestoId);
        }
    }
}