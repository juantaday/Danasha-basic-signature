using System;

namespace Domain.Data.Enums
{
    public class TypeIdentificationEmun
    {
        #region Atributes

        private static readonly TypeIdentificationEmun[] VALUES;
        private string code;
        private string name;
        private int index;

        #endregion
        #region Constructor

        static TypeIdentificationEmun()
        {
            RUC = new TypeIdentificationEmun("RUC", 1, "04");
            CEDULA = new TypeIdentificationEmun("CEDULA", 2, "05");
            PASAPORTE = new TypeIdentificationEmun("PASAPORTE", 3, "06");
            VENTA_A_CONSUMIDOR_FINAL = new TypeIdentificationEmun("VENTA A CONSUMIDOR FINAL", 4, "07");
            IDENTIFICACION_DEL_EXTERIOR = new TypeIdentificationEmun("IDENTIFICACION DEL EXTERIOR", 5, "08");
            PLACA = new TypeIdentificationEmun("PLACA", 6, "09");
            VALUES = (new TypeIdentificationEmun[]
            {
                RUC,
                CEDULA,
                PASAPORTE,
                VENTA_A_CONSUMIDOR_FINAL,
                IDENTIFICACION_DEL_EXTERIOR,
                PLACA
            });
        }
        #endregion
        #region Properties


        public static readonly TypeIdentificationEmun RUC;
        public static readonly TypeIdentificationEmun CEDULA;
        public static readonly TypeIdentificationEmun PASAPORTE;
        public static readonly TypeIdentificationEmun VENTA_A_CONSUMIDOR_FINAL;
        public static readonly TypeIdentificationEmun IDENTIFICACION_DEL_EXTERIOR;
        public static readonly TypeIdentificationEmun PLACA;

        public static TypeIdentificationEmun[] values()
        {
            return VALUES;
        }

        #endregion
        #region Methodos

        private TypeIdentificationEmun(string _name, int _index, string code)
        {
            this.code = code;
            this.name = _name;
            this.index = _index;
        }
        /// <summary>
        /// Devuelve el codigo del documento segun SRI
        /// </summary>
        /// <returns></returns>
        public String getCode()
        {
            return code;
        }
        /// <summary>
        /// Devuelve el nombre del documento Factura, nota de venta.....
        /// </summary>
        /// <returns></returns>
        public String getName()
        {
            return name;
        }
        /// <summary>
        /// Devuelve el index del Emun ..
        /// </summary>
        /// <returns></returns>
        public int getIndex()
        {
            return this.index;
        }

        #endregion

    }
}
