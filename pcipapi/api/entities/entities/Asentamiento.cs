using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    [Table("asentamientos")]
    public class Asentamiento
    {
        public Asentamiento()
        {
            this.asentamientoNombre = string.Empty;
            this.asentamientoCodigoPostal = string.Empty;
            this.asentamientoActivo = false;
            this.objetoId = 17;
        }

        [Display(Name = "Activo")]
        [Column("asentamientoActivo")]
        public bool asentamientoActivo { get; set; }

        [StringLength(10)]
        [Display(Name = "Código Postal")]
        [Column("asentamientoCodigoPostal")]
        public string asentamientoCodigoPostal { get; set; }

        [Key]
        [Display(Name = "ID del Asentamiento")]
        [Column("asentamientoId")]
        public int asentamientoId { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre del Asentamiento")]
        [Column("asentamientoNombre")]
        public string asentamientoNombre { get; set; }

        [ForeignKey("ciudadId")]
        [Display(Name = "ID de la Ciudad")]
        [Column("ciudadId")]
        public int ciudadId { get; set; }

        [ForeignKey("estadoId")]
        [Display(Name = "ID del Estado")]
        [Column("estadoId")]
        public int estadoId { get; set; }

        [ForeignKey("municipioId")]
        [Display(Name = "ID del Municipio")]
        [Column("municipioId")]
        public int municipioId { get; set; }

        [ForeignKey("objetoId")]
        [Display(Name = "Objeto")]
        [Column("objetoId")]
        public int objetoId { get; set; }

        [ForeignKey("tipoAsentamientoId")]
        [Display(Name = "ID del Tipo de Asentamiento")]
        [Column("tipoAsentamientoId")]
        public int tipoAsentamientoId { get; set; }
    }
}