namespace CADsisVenta.Helpers
{
    public sealed class FInicio
    {


        public static string SHA1(string strToHash)
        {
            using (var sha1Obj = new System.Security.Cryptography.SHA1CryptoServiceProvider())
            {
                byte[] bytesToHash = System.Text.Encoding.ASCII.GetBytes(strToHash);
                bytesToHash = (byte[])(sha1Obj.ComputeHash(bytesToHash));
                string strResult = "";
                foreach (byte b in bytesToHash)
                {
                    strResult += b.ToString("x2");
                }
                return strResult;
            }

        }


        public struct Usuario
        {
            public string DataSource;
            public int IdUsuario;
            public string codUser;
            public string codRecupera;
            public string Nombre;
            public string Apellido;
        }

        public struct Terminal
        {
            public int idTerminal;
            public int idBodega;
            public string codTerminal;
            public string Dominio;
            public string nombreBodega;
            public int idCajaStado;
            public string CodPntoEmision;
        }
        public struct Cliente
        {
            public int id;
            public string Nombres;
            public string Ruc;
            public string Direcc;
            public string Telf;
            public int itemsTotal;
            public double Total;
            public double OtroValor;
        }
        public struct _dominio
        {
            public string _HotName;
            public string _ip;
            public bool isWep;
        }
        public static Usuario UsuarioActivo;
        public static Terminal TerminalActivo;
        public static Cliente ClienteActivo;
        public static _dominio Dominio;
   
    }
}
