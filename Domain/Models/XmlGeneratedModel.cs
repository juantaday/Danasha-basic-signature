using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public  class XmlGeneratedModel
    {
        public int IdFactura { get; set; }
        public DateTime FechaEmision { get; set; }
        public  string  ClaveAcceso { get; set; }
        public string  Ambiente { get; set; }
        public string  CodDocumento { get; set; }
        public List<string > EmailsSend { get; set; }
        public string  Xml_Plano { get; set; }
        public ItemsVats ItemsVats { get; set; }
    }
}
