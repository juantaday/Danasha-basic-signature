namespace CADsisVenta.Class
{
    public class Response
    {
        private string _messague;
        private bool _success;
        public string Messague
        {
            get
            {
                return _messague;
            }
            set
            {
                _messague = value;
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
