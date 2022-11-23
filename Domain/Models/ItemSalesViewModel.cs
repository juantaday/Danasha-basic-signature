using Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Windows.Media.TextFormatting;

namespace Domain.Models
{
    public class ItemSalesViewModel
    {
        /// <summary>
        /// id de producto  
        /// </summary>
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName ="decimal(18,3)")]
        public decimal Quatity { get; set; }

        public decimal SubTotal { get 
            {
                if (Product == null)
                    return 0;

                return  Math.Round( Product.UnitPrice * Quatity,2);
            } 
        }

        public decimal BaseIva00
        {
            get
            {
                if (Product == null || Product.PRODUCTO_IMPUESTO.Count  == 0)
                    return 0;

                if (Product.PRODUCTO_IMPUESTO.Where(x => x.CODIGO_IMPUESTO == "0").Any())
                    return this.SubTotal;
                else
                    return 0;
            }
        }

        public decimal BaseIva12
        {
            get
            {
                if (Product == null || Product.PRODUCTO_IMPUESTO.Count == 0)
                    return 0;

                if (Product.PRODUCTO_IMPUESTO.Where(x => x.CODIGO_IMPUESTO == "2").Any())
                    return this.SubTotal;
                else
                    return 0;
            }
        }

        public decimal Iva { 
            get
            {
                if (this.Product == null)
                    return 0;

                return ( Product.IvaPercent  * this.SubTotal) /100;
            } 
        }

        public decimal ICE
        {
            get
            {
                if (this.Product == null)
                    return 0;

                return (Product.ICEPercent * this.SubTotal) / 100;
            }
        }

        public decimal IRBPNR
        {
            get
            {
                if (this.Product == null)
                    return 0;

                return (Product.IRBPNR_Percent * this.SubTotal) / 100;
            }
        }
        public decimal TotalItem { get {
                return this.SubTotal + this.Iva + this.ICE + this.IRBPNR;    
            }
        }
        public Product Product { get; set; }
    }
}
