using System;
using Domain.Data.Enums;

namespace Domain.Helpers
{
    public static class IdentificationTypeResolver
    {

        /// <summary>
        /// Evalúa el número de documento y devuelve directamente el código SRI ("04", "05", "07", etc.).
        /// </summary>
        /// <param name="identificationNumber">Número de RUC, cédula, consumidor final, etc.</param>
        /// <returns>Código SRI en formato de texto.</returns>
        public static string ResolveCode(string identificationNumber)
        {
            return Resolve(identificationNumber).getCode();
        }

        /// <summary>
        /// Evalúa el número de documento y resuelve el tipo de identificación (SRI Ecuador).
        /// </summary>
        /// <param name="identificationNumber">Número de RUC, cédula, consumidor final, etc.</param>
        /// <returns>Objeto TypeIdentificationEmun mapeado.</returns>
        public static TypeIdentificationEmun Resolve(string identificationNumber)
        {
            if (string.IsNullOrWhiteSpace(identificationNumber))
            {
                return TypeIdentificationEmun.VENTA_A_CONSUMIDOR_FINAL;
            }

            string cleanNumber = identificationNumber.Trim();

            // 1. Consumidor Final ("9999999999999")
            if (cleanNumber == "9999999999999")
            {
                return TypeIdentificationEmun.VENTA_A_CONSUMIDOR_FINAL;
            }

            // 2. Cédula ecuatoriana (10 dígitos)
            if (cleanNumber.Length == 10 && EsNumero(cleanNumber))
            {
                return TypeIdentificationEmun.CEDULA;
            }

            // 3. RUC ecuatoriano (13 dígitos)
            if (cleanNumber.Length == 13 && EsNumero(cleanNumber))
            {
                return TypeIdentificationEmun.RUC;
            }

            // 4. Si contiene letras o longitud distinta, asume Pasaporte/Exterior
            return TypeIdentificationEmun.PASAPORTE;
        }

        private static bool EsNumero(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsDigit(c)) return false;
            }
            return true;
        }
    }
}