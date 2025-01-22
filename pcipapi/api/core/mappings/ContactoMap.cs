using entities.entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace core.mappings
{
    internal class ContactoMap : IEntityTypeConfiguration<Contacto>
    {
        public void Configure(EntityTypeBuilder<Contacto> builder)
        {
            builder.ToTable<Contacto>("contactos").HasKey(c => c.contactoId);
        }
    }
}