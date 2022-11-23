using System.Data;

namespace CADsisVenta.Class
{
    public class ResponseTerminal
    {
        public  int IDterminal;
        private DataTable _data;
        private bool _success;
        public DataTable DataDb
        {
            get
            {
                IDterminal = System.Convert.ToInt32(_data.Rows[0]["idCajaStado"]);
                return _data;
            }
            set
            {
                _data = value;
            }
        }
        public bool Success
        {
            get
            {
                return _success;
            }
            set
            {
                _success = value;
            }
        }
    }
}
