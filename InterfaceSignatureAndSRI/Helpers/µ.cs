using InterfaceSignatureAndSRI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSignatureAndSRI.Helpers
{
    public  class µ
    {
        public string encrypt(string value) {
            Encriptador enc = new Encriptador();
           return enc.Encriptar(value, Properties.Settings.Default.KeyCode);
        }
        public string decrypt(string value)
        {
            Encriptador enc = new Encriptador();
            return enc.Desencriptar(value, Properties.Settings.Default.KeyCode);
        }

    }
}
