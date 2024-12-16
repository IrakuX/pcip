using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    [Table("monedas_mov")]
    public class MonedaMov
    {
        public MonedaMov()
        {
            this.monedaMovimientoCambio = 0;
            this.monedaNombre = string.Empty;
            this.monedaMovimientoFecha = DateTime.Now;
        }

        [ForeignKey("monedaId")]
        [Display(Name = "ID de la Moneda")]
        [Column("monedaId")]
        public int monedaId { get; set; }

        [Display(Name = "Cambio del Movimiento")]
        [Column("monedaMovimientoCambio")]
        public decimal monedaMovimientoCambio { get; set; }

        [Display(Name = "Fecha del Movimiento")]
        [Column("monedaMovimientoFecha")]
        public DateTime monedaMovimientoFecha { get; set; }

        [Key]
        [Display(Name = "ID del Movimiento")]
        [Column("monedaMovimientoId")]
        public int monedaMovimientoId { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre de la Moneda")]
        [Column("monedaNombre")]
        public string monedaNombre { get; set; }
    }
}