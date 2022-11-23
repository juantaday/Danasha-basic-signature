using Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Data.Entities
{
    [Table("IMPUESTO_VALOR", Schema = "sri")]
    public class IMPUESTO_VALOR
    {
        [StringLength (255)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string CODIGO { get; set; }

        public int CODIGO_IMPUESTO { get; set; }

        [Column (TypeName ="decimal(18,2)")]
        public decimal PORCENTAJE { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal? PORCENTAJE_RETENCION { get; set; }

        [StringLength (1)]
        public string TIPO_IMPUESTO { get; set; }

        [StringLength(600)]
        public string DESCRIPCION { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FECHA_INICIO  { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FECHA_FIN { get; set; }

        public int? CODIGO_ADM { get; set; }

        [StringLength(1)]
        public string MARCA_PORCENTAJE_LIBRE { get; set; }

        [Required]
        [StringLength(1)]
        public string CALCURA_CON_CANTIDAD { get; set; }

        public  virtual  IMPUESTO IMPUESTO { get; set; }

        public virtual ICollection<PRODUCTO_IMPUESTO> PRODUCTO_IMPUESTOS { get; set; }
    }
}
