using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    [Table("monedas")]
    public class Moneda
    {
        public Moneda()
        {
            this.monedaId = -1;
            this.monedaCodigo = string.Empty;
            this.monedaNombre = string.Empty;
            this.monedaActivo = false;
            this.objetoId = 17;
        }

        [Display(Name = "Activo")]
        [Column("monedaActivo")]
        public bool monedaActivo { get; set; }

        [StringLength(5)]
        [Display(Name = "Código")]
        [Column("monedaCodigo")]
        public string monedaCodigo { get; set; }

        [Key]
        [Display(Name = "Id")]
        [Column("monedaId")]
        public int monedaId { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre")]
        [Column("monedaNombre")]
        public string monedaNombre { get; set; }

        [ForeignKey("objetoId")]
        [Display(Name = "Objeto")]
        [Column("objetoId")]
        public int objetoId { get; set; }
    }
}