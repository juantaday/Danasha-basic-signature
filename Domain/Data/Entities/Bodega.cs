using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Data.Entities
{
    [Table("Bodegas")]
    public class Bodega
    {
        [Key]
        [Column("idBodega")]
        public int IdBodega { get; set; }

        [Column("Nom_Bodega")]
        [StringLength(100)]
        public string NomBodega { get; set; }

        [Column("Des_Bodega")]
        [StringLength(200)]
        public string DescripcionBodega { get; set; }

        [Column("Direc_Bodega")]
        [StringLength(500)]
        public string DireccionBodega { get; set; }

        [Column("Telef1_Bodega")]
        [StringLength(25)]
        public string Telefono1Bodega { get; set; }

        [Column("Telef2_Bodega")]
        [StringLength(25)]
        public string Telefono2Bodega { get; set; }

        [Column("Telef3_Bodega")]
        [StringLength(25)]
        public string Telefono3Bodega { get; set; }

        [Column("Resp1_idEmpleado")]
        public int? Resp1IdEmpleado { get; set; }

        [Column("Resp2_idEmpleado")]
        public int? Resp2IdEmpleado { get; set; }

        [Column("Resp3_idEmpleado")]
        public int? Resp3IdEmpleado { get; set; }

        [Column("Fecha_Apertura")]
        public DateTime FechaApertura { get; set; }

        [Column("Fecha_reg")]
        public DateTime? FechaRegistro { get; set; }

        [Column("TypoBodega")]
        public int? TypoBodega { get; set; }

        [Column("CodEstablec")]
        [StringLength(10)]
        public string CodEstablec { get; set; }

        [Column("EsSucursalRemota")]
        public bool? EsSucursalRemota { get; set; }

        [Column("CiudadSucursal")]
        [StringLength(100)]
        public string CiudadSucursal { get; set; }

        [Column("TailscaleIp")]
        [StringLength(100)]
        public string TailscaleIp { get; set; }

        [Column("TailscaleUsuario")]
        [StringLength(100)]
        public string TailscaleUsuario { get; set; }

        [Column("TailscalePassword")]
        [StringLength(255)]
        public string TailscalePassword { get; set; }

        [Column("TailscaleDatabase")]
        [StringLength(100)]
        public string TailscaleDatabase { get; set; }
    }
}
