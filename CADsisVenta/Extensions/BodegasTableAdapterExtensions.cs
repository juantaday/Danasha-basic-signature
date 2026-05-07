using System.Data;
using System.Data.SqlClient;
using static CADsisVenta.Helpers.FInicio;

namespace CADsisVenta.DataSetComprasTableAdapters
{
    public partial class BodegasTableAdapter
    {
        public DataTable GetBodegasByHostName(int idTerminal)
        {
            var dt = new DataTable();
            using (var cmd = new SqlCommand(
                "SELECT b.* FROM Bodegas b \r\nINNER JOIN  stm.Terminal t ON t.idBodega = b.idBodega\r\nWHERE t.idTerminal = @idTerminal",
                Connection))
            {
                cmd.Parameters.AddWithValue("@idTerminal", idTerminal);
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GetAllBodegas()
        {
            var dt = new DataTable();
            using (var cmd = new SqlCommand(
                "SELECT idBodega, nom_bodega, EsSucursalRemota, CiudadSucursal FROM Bodegas ORDER BY nom_bodega",
                Connection))
            {
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
    }
}
