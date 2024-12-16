using entities.entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core.mappings
{
    public class UsuarioTokenMap : IEntityTypeConfiguration<UsuarioToken>
    {
        public void Configure(EntityTypeBuilder<UsuarioToken> builder)
        {
            builder.ToTable("usuarioToken").HasKey(c => new { c.usuarioId, c.usuarioJwtId });
        }
    }
}