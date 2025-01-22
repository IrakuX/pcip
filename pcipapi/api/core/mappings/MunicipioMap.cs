using entities.entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace core.mappings
{
    public class MunicipioMap : IEntityTypeConfiguration<Municipio>
    {
        public void Configure(EntityTypeBuilder<Municipio> builder)
        {
            builder.ToTable<Municipio>("municipios").HasKey(c => c.municipioId);
        }
    }
}