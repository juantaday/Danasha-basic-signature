using Domain.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Logica
{
    public static  class Estructuras
    {
    }

    public struct currentWarehouse {
        public string Token { get; set; }
        public ushort Environment { get; set; }
        public bool SaveToFile { get; set; }

        public bool SaveToDataBase { get; set; }
        public int TipoAmbiente { get; set; }

    }

    public struct currentCommerce
    {
        public int Id { get; set; }
        public string  NameCompany { get; set; }
        public string Ruc { get; set; }

    }



}
