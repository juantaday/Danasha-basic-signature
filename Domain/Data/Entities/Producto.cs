using Domain.Data.Enums;
using Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [StringLength(30)]
        public string Cod_Secondary { get; set; }

        [StringLength(30)]
        public string BarCode { get; set; }

        [StringLength(60)]
        public string Name_Producto { get; set; }

        [Column(TypeName = "Decimal(18,3)")]
        public decimal UnitPrice { get; set; }


        public decimal IvaPercent
        {
            get
            {
                if (PRODUCTO_IMPUESTO == null || PRODUCTO_IMPUESTO.Count == 0)
                    return 0;

                return PRODUCTO_IMPUESTO.Where(x => x.TIPO_IMPUESTO == 2).FirstOrDefault().IMPUESTO_VALOR.PORCENTAJE;
            }
        }
        public decimal ICEPercent
        {
            get
            {
                if (PRODUCTO_IMPUESTO == null || PRODUCTO_IMPUESTO.Where (x=>x.TIPO_IMPUESTO ==3).ToList().Count == 0)
                    return 0;
                  return PRODUCTO_IMPUESTO.Where(x => x.TIPO_IMPUESTO == 3).FirstOrDefault().IMPUESTO_VALOR.PORCENTAJE;
            }
        }

        public decimal IRBPNR_Percent
        {
            get
            {
                if (PRODUCTO_IMPUESTO == null || PRODUCTO_IMPUESTO.Where(x => x.TIPO_IMPUESTO == 5).ToList().Count == 0)
                    return 0;

                return PRODUCTO_IMPUESTO.Where(x => x.TIPO_IMPUESTO == 5).FirstOrDefault().IMPUESTO_VALOR.PORCENTAJE;
            }
        }


        public TypeProductEmun TypeProduct { get; set; } = TypeProductEmun.Bien;

        public virtual ICollection<PRODUCTO_IMPUESTO> PRODUCTO_IMPUESTO { get; set; }

        public virtual ICollection<INFO_ADICIONAL> INFO_ADICIONALS { get; set; }
        public virtual ICollection<ItemSalesViewModel> ItemSalesViewModels { get; set; }

    }
}
