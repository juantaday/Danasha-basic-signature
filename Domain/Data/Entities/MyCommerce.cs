using Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Data.Enums
{
    [Table("MyCommerce", Schema ="cmc")]
    public class MyCommerce
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(70)]
        public string RazonSocial { get; set; }

        [Required]
        [StringLength(15)]
        public string Ruc { get; set; }

        [StringLength(255)]
        public string Motto { get; set; }

        [StringLength(25)]
        public string Phone { get; set; }


        [Column(TypeName="date")]
        public DateTime? DateStar { get; set; }
        
        [Required]
        [StringLength(250)]
        public string BusinessAddress { get; set; }

        [Required]
        [StringLength(250)]
        public string EstablishmentAddress { get; set; }

        [StringLength(255)]
        public string Note { get; set; }
        /// <summary>
        /// Nombre de compania  o alias
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime DateRegister { get; set; }


        [Required]
        [StringLength(15)]
        public string Company { get; set; }
        /// <summary>
        /// Nombre Comercial
        /// </summary>
        [Required]
        [StringLength(100)]
        public string NameComercial { get; set; }

        /// <summary>
        /// Contribuyente especial numero
        /// </summary>
        [StringLength(13,ErrorMessage ="The fiel [0] is maximun {1} charecter and  minimun {2} character",MinimumLength =3)]
        public string  SpecialTaxNumber { get; set; }
        /// <summary>
        /// Obligado llevar contabilidad
        /// </summary>
        public bool KeepAccounting { get; set; } = false;

        [StringLength(3)]
        [Required]
        public string CodEstablec { get; set; }

        [StringLength(3)]
        [Required]
        public string CodPntoEmision { get; set; }


        public byte[] LogoPDF { get; set; }

        public byte[] LogoTicket { get; set; }

        [StringLength(35)]
        public string RegimenMicroempresas { get; set; }
        /// <summary>
        /// Agente de Retencion Nro Resolucion:
        /// </summary>
        [StringLength(35)]
        public string AgenteRetencion { get; set; }

        [StringLength(27)]
        public string ContribuyenteRimpe { get; set; }

        public TypeECommerceEnum? IdTypeRegimen { get; set; } = TypeECommerceEnum.RIMPE_Taxpayer;

        public  virtual  MySetting MySetting { get; set; }

        public virtual ICollection<SignatureOption> SignatureOptions { get; set; }

    }

}
