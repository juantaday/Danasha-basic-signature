using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSignatureAndSRI.SendMail
{
    public class ItemHeaderData
    {
        public string Document_Num { get; set; }
        public DateTime FechaAutoriza { get; set; }
        public DateTime FechaEmision { get; set; }
        public string ClaveAcceso { get; set; }
        public string numeroAutorizacion { get; set; }
        public string RazonSocial { get; set; }
        public string CompanyName { get; internal set; }
        public string Phone { get; internal set; }
        public string CellPhone { get; internal set; }
    }
}
