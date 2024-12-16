using entities.entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core.mappings
{
    public class ObjetoTipoMap : IEntityTypeConfiguration<ObjetoTipo>
    {
        public void Configure(EntityTypeBuilder<ObjetoTipo> builder)
        {
            builder.ToTable("objetoTipos").HasKey(c => c.objetoTipoId);
        }
    }
}