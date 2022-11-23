using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public  class ItemDocumentsModel
    {
        public int Id { get; set; }
        public int FumFactur { get; set; }
        public string  ClaveAcceso { get; set; }
        public DateTime  DateModify { get; set; }
        public bool  IsChecked { get; set; }
        public string  State { get; set; }

        public string ErrorMessage { get; set; }

    }
}
