using entities.entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core.mappings
{
    public class CiudadMap : IEntityTypeConfiguration<Ciudad>
    {
        public void Configure(EntityTypeBuilder<Ciudad> builder)
        {
            builder
                .ToTable<Ciudad>("ciudades")
                .HasKey(c => c.ciudadId);
        }
    }
}