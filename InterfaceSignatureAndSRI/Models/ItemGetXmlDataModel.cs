using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSignatureAndSRI.Models
{
    public class ItemGetXmlDataModel
    {
        public int VoucherId { get; set; }
        public int IDRelationData { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaAutorizacion { get; set; }
        public string XmlString { get; set; }
        public byte[] LogoImage { get; set; }

    }
}
