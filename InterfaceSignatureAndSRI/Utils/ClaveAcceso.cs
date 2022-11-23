using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSignatureAndSRI.Utils
{

    public static class ClaveAcceso
    {

        private static int GenerarDigitoModulo11(string clave)
        {
            int baseMultiplicador = 7;
            System.Diagnostics.Debug.WriteLine("CADENA-->" + clave);
            int[] aux = new int[clave.Length];
            int multiplicador = 2;
            int total = 0;
            int verificador = 0;

            for (int i = aux.Length - 1; i >= 0; i--)
            {

                aux[i] = int.Parse("" + clave[i]);

                aux[i] *= multiplicador;

                multiplicador++;

                if (multiplicador > baseMultiplicador)
                {
                    multiplicador = 2;
                }
                total += aux[i];
            }
            if ((total == 0) || (total == 1))
            {
                verificador = 0;
            }
            else
            {
                verificador = 11 - total % 11 == 11 ? 0 : 11 - total % 11;
            }

            if (verificador == 10)
            {
                verificador = 1;
            }

            return verificador;
        }

        /// <summary>
        /// Genera clave de acceso basado en los datos emitidos
        /// </summary>
        /// <param name="fechaEmision">Fecha emitido en documento</param>
        /// <returns></returns>
        /// <param name="tipoComprobante">tipo de comprobante Factura 01</param>
        /// <returns></returns>
        /// <param name="ruc">Ruc del emisor de 13 dijitos</param>
        /// <returns></returns>
        ///  <param name="ambiente">Tipo ambiente 1 pruebas 2 produccion</param>
        /// <returns></returns>
        /// <param name="serie">Codigo establecimiento y Pnto de emision juntas</param>
        /// <returns></returns>
        ///  <param name="numeroComprobante">Numero de comprobante</param>
        /// <returns></returns>
        /// <param name="codigoNumerico">Numero aleatorio de 8 dijitos</param>
        /// <returns></returns>
        /// <param name="tipoEmision">Para el método de autorización offline, solo existe el tipo de emisión normal.(1)</param>
        /// <returns></returns>
        public static String generarClaveAcceso(DateTime fechaEmision,
            string tipoComprobante, string ruc, string ambiente,
            string serie, string numeroComprobante, String codigoNumerico,
            string tipoEmision)
        {
            int verificador = 0;
            if (ruc != null && ruc.Length < 13)
            {
                ruc = String.Format("%013d", new Object[] { ruc });
            }

            string fecha = fechaEmision.ToString("ddMMyyyy");
            StringBuilder clave = new StringBuilder(fecha);
            clave.Append(tipoComprobante);
            clave.Append(ruc);
            clave.Append(ambiente);
            clave.Append(serie);
            clave.Append(numeroComprobante);
            clave.Append(codigoNumerico);
            clave.Append(tipoEmision);
            verificador = GenerarDigitoModulo11(clave.ToString());

            clave.Append(java.lang.Integer.valueOf(verificador));
            String claveGenerada = clave.ToString();
            if (clave.ToString().Length != 49)
            {
                System.Diagnostics.Debug.WriteLine(claveGenerada);
                claveGenerada = null;
            }
            return claveGenerada;
        }


        public static String GeneraClaveContingencia(String fechaEmision, String tipoComprobante,
            String clavesContigencia, String tipoEmision)

        {
            int verificador = 0;
            String claveGenerada = "";
            //        SimpleDateFormat dateFormat = new SimpleDateFormat("ddMMyyyy");
            //        String fecha = dateFormat.format(fechaEmision);
            StringBuilder clave = new StringBuilder(fechaEmision);
            clave.Append(tipoComprobante);
            clave.Append(clavesContigencia);
            clave.Append(tipoEmision);
            verificador = GenerarDigitoModulo11(clave.ToString());
            if (verificador != 10)
            {
                clave.Append(java.lang.Integer.valueOf(verificador));
                claveGenerada = clave.ToString();
            }
            if (clave.ToString().Length != 49)
            {
                claveGenerada = null;
            }
            return claveGenerada;
        }

    }

}
