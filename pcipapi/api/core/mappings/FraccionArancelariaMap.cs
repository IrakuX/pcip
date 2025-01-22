using entities.entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core.mappings
{
    public class FraccionArancelariaMap : IEntityTypeConfiguration<FraccionArancelaria>
    {
        public void Configure(EntityTypeBuilder<FraccionArancelaria> builder)
        {
            builder.ToTable("fraccionesArancelarias").HasKey(c => c.fraccionArancelariaId);
        }
    }
}