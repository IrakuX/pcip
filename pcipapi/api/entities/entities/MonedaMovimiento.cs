using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    [Table("monedasMovimientos")]
    public class MonedaMovimiento
    {
        [ForeignKey("monedaId")]
        [Display(Name = "Id de la moneda")]
        [Column("monedaId")]
        public int? monedaId { get; set; } = null;

        [Display(Name = "Cambio del movimiento")]
        [Column("monedaMovimientoCambio")]
        public decimal monedaMovimientoCambio { get; set; } = 0M;

        [Display(Name = "Fecha del movimiento")]
        [Column("monedaMovimientoFecha")]
        public DateTime monedaMovimientoFecha { get; set; } = DateTime.Now;

        [Key]
        [Display(Name = "Id del movimiento")]
        [Column("monedaMovimientoId")]
        public int monedaMovimientoId { get; set; } = -1;
    }
}