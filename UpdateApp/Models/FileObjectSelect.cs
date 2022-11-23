using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateApp.Models
{
    public class FileObjectSelect
    {
        public string NameFile { get; set; }
        public string PathFile { get; set; }
        public string Extencion { get; set; }
        public bool ExcuteSuccess { get; set; }

        public bool IsPrepared { get; set; }

        public string ErrorMesague { get; set; }

        public long Size { get; set; }

        public string SizeInfo
        {
            get
            {
                if (this.Size > 0)
                    return string.Format("{0} KB", Size / 1024);
                else
                    return "0 KB";

            }
        }

    }

}
