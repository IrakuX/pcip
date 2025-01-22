using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    [Table("ciudades")]
    public class Ciudad
    {
        [Display(Name = "Activo")]
        [Column("ciudadActivo")]
        public bool ciudadActivo { get; set; } = false;

        [Key]
        [Display(Name = "Id")]
        [Column("ciudadId")]
        public int ciudadId { get; set; } = -1;

        [StringLength(100)]
        [Display(Name = "Nombre")]
        [Column("ciudadNombre")]
        public string ciudadNombre { get; set; } = string.Empty;

        [ForeignKey("municipioId")]
        [Display(Name = "Municipio")]
        [Column("municipioId")]
        public int? municipioId { get; set; } = null;

        [ForeignKey("objetoId")]
        [Display(Name = "Objeto")]
        [Column("objetoId")]
        public int objetoId { get; set; } = 7;
    }
}