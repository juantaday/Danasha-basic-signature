using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSignatureAndSRI.Helpers
{
    using System.IO;
    using System.Windows.Media.Imaging;

    public static class ImageHelper
    {
        public static byte[] ImageToByteArray(System.Drawing.Image imagen)
        {
            MemoryStream ms = new MemoryStream();
            imagen.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        public static byte[] ImageToByte(object images)
        {
            try
            {
                BitmapImage imageSource = (BitmapImage)images;

                using (MemoryStream ms = (MemoryStream)imageSource.StreamSource)
                {
                    return ms.ToArray();
                }

            }
            catch (System.Exception)
            {

                return null; ;
            }

        }

    }
}
