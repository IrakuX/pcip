using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    [Table("estados")]
    public class Estado
    {
        public Estado()
        {
            this.estadoId = -1;
            this.estadoNombre = string.Empty;
            this.estadoActivo = false;
            this.objetoId = 7;
        }

        [Display(Name = "Activo")]
        [Column("estadoActivo")]
        public bool estadoActivo { get; set; }

        [Key]
        [Display(Name = "Id")]
        [Column("estadoId")]
        public int estadoId { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre")]
        [Column("estadoNombre")]
        public string estadoNombre { get; set; }

        [ForeignKey("objetoId")]
        [Display(Name = "Objeto")]
        [Column("objetoId")]
        public int objetoId { get; set; }
    }
}