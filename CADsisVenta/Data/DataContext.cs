using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADsisVenta.Data
{
    public class DataContext : CADsisVenta.DataClassesDBDataContext
    {
        public DataContext() : base(DomainSQLite.Setting.Configuration.ConectionString)
        {
        }

    }
}
