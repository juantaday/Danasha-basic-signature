using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSignatureAndSRI.Utils
{
    public class ResponseSpliter
    {
        private bool _isNumeric;
        private bool _isCode;
        private string[] _Spliter;
        private bool _isSucces;

        public bool IsNumeric
        {
            get
            {
                return _isNumeric;
            }
            set
            {
                _isNumeric = value;
            }
        }

        public string[] Spliter
        {
            get
            {
                return _Spliter;
            }
            set
            {
                _Spliter = value;
            }
        }

        public bool IsSucces
        {
            get
            {
                return _isSucces;
            }
            set
            {
                _isSucces = value;
            }
        }

        public bool IsCode
        {
            get
            {
                return _isCode;
            }
            set
            {
                _isCode = value;
            }
        }
    }
}
