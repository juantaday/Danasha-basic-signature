using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Helpers
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

        public int LengthIdex
        {
            get
            {
                int index = 0;
                if (!string.IsNullOrEmpty(this.Spliter[0]))
                {
                    index = 1;
                }
                if (!string.IsNullOrEmpty(this.Spliter[1]))
                {
                    index = 2;
                }
                if (!string.IsNullOrEmpty(this.Spliter[2]))
                {
                    index = 3;
                }
                return index;
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
