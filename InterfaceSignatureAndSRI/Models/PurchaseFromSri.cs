using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSignatureAndSRI.Models
{
    public class PurchaseFromSri
    {
        private string _id;
        public PurchaseFromSri()
        {
            _id = Guid.NewGuid().ToString();
        }
        public string  ID { get { return _id; } }
        public int IdPresent { get; set; }
        public int idProducto { get; set; }
        public string CodPrincipal { get; set; }
        public string CodSecundario { get; set; }
        public string  MedidaSRI { get; set; }
        public string DescripSRI { get; set; }
        public string  DescripDB { get; set; }
        public string MedidaDB { get; set; }
        public double Cantidad { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        /// <summary>
        /// codigo  1 segun SRI
        /// </summary>
        public decimal RENTA { get; set; }
        /// <summary>
        /// codigo  2 segun SRI
        /// </summary>
        public decimal IVA { get; set; }
        /// <summary>
        /// codigo  3 segun SRI
        /// </summary>
        public decimal ICE { get; set; }
        /// <summary>
        ///  codigo  5 segun SRI
        /// </summary>
        public decimal IRBPNR { get; set; }

        
        public double SubTotal { get; set; }
        public double  Total { get
            {
                return Math.Round((double)this.IVA +  (double)this.ICE  + (double)this.IRBPNR + (double )this.RENTA + this.SubTotal,2);
             }
        }

        public short ErroIva { get; set; }

    }
}
