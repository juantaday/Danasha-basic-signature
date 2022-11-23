using System;

namespace CADsisVenta.Helpers
{
    public class GetInicio : IDisposable
    {
        string Usuario;
        string Contrasena;
        string _password;
        public GetInicio()
        {
        }
        public GetInicio(string Usuario, string Contrasena)
        {
            GUsuario = Usuario;
            gContrasena = Contrasena;
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }
        public dynamic GUsuario
        {
            get
            {
                return Usuario;
            }
            set
            {
                Usuario = System.Convert.ToString(value);
            }
        }
        public dynamic gContrasena
        {
            get
            {
                return CADsisVenta.Funtions.SecuritySpamp.SHA1(Contrasena);
            }
            set
            {
                Contrasena = System.Convert.ToString(value);
                _password = System.Convert.ToString(value);
            }
        }

        #region IDisposable Support
        private bool disposedValue; // Para detectar llamadas redundantes

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

            }
            disposedValue = true;
        }

        ~GetInicio()
        {
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
            Dispose(false);
            //base.Finalize();
        }

        // Visual Basic agrega este código para implementar correctamente el patrón descartable.
        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
