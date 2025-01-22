using entities.entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace core.mappings
{
    public class AsentamientoMap : IEntityTypeConfiguration<Asentamiento>
    {
        public void Configure(EntityTypeBuilder<Asentamiento> builder)
        {
            builder
                .ToTable<Asentamiento>("asentamientos")
                .HasKey(c => c.asentamientoId);
        }
    }
}