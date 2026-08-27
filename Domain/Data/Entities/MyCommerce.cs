using Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("myCommerce", Schema = "cmc")]
public class MyCommerce
{
    [Key]
    [Column("CommerceId")]
    public int CommerceId { get; set; }

    [Required]
    [StringLength(80)]
    [Column("RazonSocial", TypeName = "varchar(80)")]
    public string RazonSocial { get; set; } = string.Empty;

    [Required]
    [StringLength(13)]
    [Column("Ruc", TypeName = "varchar(13)")]
    public string Ruc { get; set; } = string.Empty;

    [StringLength(100)]
    [Column("lema", TypeName = "varchar(100)")]
    public string Lema { get; set; }

    [StringLength(50)]
    [Column("Phone", TypeName = "varchar(50)")]
    public string Phone { get; set; }

    [Column("DateStar", TypeName = "date")]
    public DateTime DateStar { get; set; }

    [StringLength(150)]
    [Column("Domicilio", TypeName = "varchar(150)")]
    public string Domicilio { get; set; }

    [StringLength(100)]
    [Column("Representante", TypeName = "varchar(100)")]
    public string Representante { get; set; }

    [StringLength(255)]
    [Column("note", TypeName = "varchar(255)")]
    public string Note { get; set; }

    [Column("dateRegister", TypeName = "datetime")]
    public DateTime DateRegister { get; set; }

    [Column("IsCancelInSalesNotStock")]
    public bool IsCancelInSalesNotStock { get; set; }

    [StringLength(30)]
    [Column("Company", TypeName = "varchar(30)")]
    public string Company { get; set; }

    [Required]
    [StringLength(50)]
    [Column("NameComercial", TypeName = "varchar(50)")]
    public string NameComercial { get; set; } = string.Empty;

    [StringLength(35)]
    [Column("AgenteRetencion", TypeName = "varchar(35)")]
    public string AgenteRetencion { get; set; }

    [Column("IdTypeRegimen")]
    public byte IdTypeRegimen { get; set; }

    [StringLength(13)]
    [Column("SpecialTaxNumber", TypeName = "varchar(13)")]
    public string SpecialTaxNumber { get; set; }

    [Column("KeepAccounting")]
    public bool KeepAccounting { get; set; }

    [Required]
    [StringLength(15)]
    [Column("TypoMonedaDecrip", TypeName = "varchar(15)")]
    public string TypoMonedaDecrip { get; set; } = string.Empty;

    [StringLength(25)]
    [Column("CellPhone", TypeName = "varchar(25)")]
    public string CellPhone { get; set; }

    [Column("LogoPDF", TypeName = "varbinary(max)")]
    public byte[] LogoPDF { get; set; }

    [Column("LogoTicket", TypeName = "varbinary(max)")]
    public byte[] LogoTicket { get; set; }

    [StringLength(35)]
    [Column("RegimenMicroempresas", TypeName = "varchar(35)")]
    public string RegimenMicroempresas { get; set; }

    [StringLength(45)]
    [Column("ContribuyenteRimpe", TypeName = "varchar(45)")]
    public string ContribuyenteRimpe { get; set; }

    public virtual MySetting MySetting { get; set; }

    public virtual ICollection<SignatureOption> SignatureOptions { get; set; }
}