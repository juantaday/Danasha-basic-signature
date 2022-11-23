using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSignatureAndSRI.Models
{
    public class myAttachment
    {
        public MemoryStream MemoryStream { get; set; }
        public Stream PDF { get; set; }
        public string Name { get; set; }
    }
}
