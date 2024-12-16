using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    [Table("municipios")]
    public class Municipio
    {
        [ForeignKey("estadoId")]
        [Display(Name = "Estado")]
        [Column("estadoId")]
        public int estadoId { get; set; }

        [Display(Name = "Activo")]
        [Column("municipioActivo")]
        public bool municipioActivo { get; set; }

        [Display(Name = "Id")]
        [Column("municipioId")]
        public int municipioId { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre")]
        [Column("municipioNombre")]
        public bool municipioNombre { get; set; }

        [ForeignKey("objetoId")]
        [Display(Name = "Objeto")]
        [Column("objetoId")]
        public int objetoId { get; set; }
    }
}