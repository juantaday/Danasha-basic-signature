using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CADsisVenta.Data.Entyties
{
    public class ItemStockViewModel : INotifyPropertyChanged
    {
        private decimal _stock;
        private decimal _costo;

        public int idProducto { get; set; }
        public int idCategoria { get; set; }
        public int idSubCategoria { get; set; }

        public string Nom_Categoria { get; set; }

        public string Nom_SubCategoria { get; set; }

        public string Nom_Comercial { get; set; }

        public int Articulos { get; set; }

        public decimal Stock
        {
            get => _stock;
            set => SetValue(ref _stock, value);
        }

        public decimal Costo
        {
            get => _costo;
            set => SetValue(ref _costo, value);
        }

        public decimal CostoTotal { get; set; }

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        protected void SetValue<R>(ref R backingField, R value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<R>.Default.Equals(backingField, value))
            {
                return;
            }

            backingField = value;
            OnPropertyChanged(propertyName);
        }
        #endregion

    }
}
