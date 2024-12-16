using entities.entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core.mappings
{
    public class ObjetoMap : IEntityTypeConfiguration<Objeto>
    {
        public void Configure(EntityTypeBuilder<Objeto> builder)
        {
            builder.ToTable("objetos").HasKey(c => c.objetoId);
        }
    }
}