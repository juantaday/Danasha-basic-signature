using CADsisVenta.DataSetClientesTableAdapters;
using DomainSQLite.Setting;

namespace CADsisVenta
{
    public class ClsClientes
    {
        private static ClienteNameTableAdapter Clientes_TableAdapter = new ClienteNameTableAdapter();
        private static ClientesTableAdapter Admin_TableAdapter = new ClientesTableAdapter();

        public static bool isAurotizeCredit(int idCliente)
        { 
            Clientes_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return (bool)Clientes_TableAdapter.ScalarIsAutorizeCredit(idCliente);
        }
        /// <summary>
        /// Return el id del cliente..
        /// </summary>
        /// <param name="idPerson"></param>
        /// <returns></returns>
        public static int isClinteBypersonAdmin(int idPerson)
        {

            int? isClient = (int?)Admin_TableAdapter.ScalarReturnIdClienteByIdPersona(idPerson);

            if (isClient.HasValue )
                return isClient.Value;
            else
            {
                Admin_TableAdapter.InsertCliente(idPerson);
                return (int)Admin_TableAdapter.ScalarReturnIdClienteByIdPersona(idPerson);
            }
        }
    }
}
