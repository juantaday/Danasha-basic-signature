using CADsisVenta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSignatureAndSRI.Data
{
       public class DataContextReflex : DataContextLinqDataContext
    {
        private static DataContextLinqDataContext dbReflexion = new DataContextLinqDataContext(DomainSQLite.Setting.Configuration.ConectionString);
        private static string _NameDatabase = Properties.Settings.Default.NameDataBse;
        public DataContextReflex() : base(DomainSQLite.Setting.Configuration.ConectionString)
        {
            string stringConnection = dbReflexion.Connection.ConnectionString;
            string nameDataBase = dbReflexion.Connection.Database;
            stringConnection = stringConnection.Replace(nameDataBase, _NameDatabase);
            base.Connection.ConnectionString = stringConnection;
        }

    }

}
