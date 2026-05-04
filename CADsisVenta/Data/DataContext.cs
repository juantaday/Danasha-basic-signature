namespace CADsisVenta.Data
{
    public class DataContext : CADsisVenta.DataClassesDBDataContext
    {
        public DataContext() : base(DomainSQLite.Setting.Configuration.ConectionString)
        {
        }

    }
}
