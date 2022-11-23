namespace CADsisVenta.Data.Emuns
{
    public sealed class EnumSatateModule
    {
        public enum stateOperation
        {
            Insert = 0,
            Update = 1,
            Delete = 2,
            View = 3
        }
        public enum stateLoad
        {
            Dialogo = 0,
            List = 1,
            View = 2,
            Select = 3,
        }
        public enum stateClient
        {
            Admin = 0,
            User = 1,
            Cliente = 2
        }
        public enum stateReturn
        {
            _nothing = 0,
            _response = 1
        }
        public enum viewLoadReport
        {
            Select_ = 0,
            All = 1,
            Latest = 2
        }

        public static stateOperation StateOpenShow;
    }
}
