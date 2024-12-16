using entities.entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core.mappings
{
    public class AccesoMap : IEntityTypeConfiguration<Acceso>
    {
        public void Configure(EntityTypeBuilder<Acceso> builder)
        {
            builder.ToTable("accesos").HasKey(c => new { c.perfilId, c.objetoId });
        }
    }
}