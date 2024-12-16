using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    [Table("ciudades")]
    public class Ciudad
    {
        public Ciudad()
        {
            this.ciudadId = -1;
            this.ciudadNombre = string.Empty;
            this.ciudadActivo = false;
            this.objetoId = 7;
        }

        [Display(Name = "Activo")]
        [Column("ciudadActivo")]
        public bool ciudadActivo { get; set; }

        [Key]
        [Display(Name = "Id")]
        [Column("ciudadId")]
        public int ciudadId { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre")]
        [Column("ciudadNombre")]
        public string ciudadNombre { get; set; }

        [ForeignKey("municipioId")]
        [Display(Name = "Municipio")]
        [Column("municipioId")]
        public int? municipioId { get; set; }

        [ForeignKey("objetoId")]
        [Display(Name = "Objeto")]
        [Column("objetoId")]
        public int objetoId { get; set; }
    }
}