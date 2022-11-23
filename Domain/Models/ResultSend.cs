using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public  class ResultSend
    {
        public string Estado { get; set; }
        public string  Message { get; set; }
        public DateTime fechaAutorizacion { get; set; }
        public string XML { get; set; }
    }
}
