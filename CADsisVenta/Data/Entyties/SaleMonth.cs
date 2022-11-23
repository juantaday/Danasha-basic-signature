using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADsisVenta.Data.Entyties
{
    public class SaleMonth
    {
        private DateTime? dateArgument;
        private string _descDate;


        public int Years { get; set; }
        public int Months { get; set; }
        public DateTime DeteArgument
        {
            get
            {
                if (dateArgument.HasValue)
                {
                    return dateArgument.Value;
                }
                else
                {
                    dateArgument = new DateTime(this.Years, this.Months, 1);
                    return dateArgument.Value;
                }
            }
        }


        public string DescripDate
        {
            get
            {
                if (dateArgument.HasValue)
                {
                    return dateArgument.Value.ToString("MM") + "-" + dateArgument.Value.Year.ToString();
                }
                else
                {
                    dateArgument = new DateTime(this.Years, this.Months, 1);
                    return dateArgument.Value.ToString("MM") + "-" + dateArgument.Value.Year.ToString();
                }
            }
        }

        public int TipoDoc { get; set; }

        public decimal Base0 { get; set; }

        public decimal BaseIva { get; set; }
        public decimal Iva { get; set; }

        public decimal Total { get; set; }

    }

}
