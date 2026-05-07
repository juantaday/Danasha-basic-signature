using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace Domain.Helpers
{
    public static class BitmapToImage
    {

        public static byte[] ImageToByte(BitmapImage imageSource)
        {
            try
            {


                MemoryStream stream = (MemoryStream)imageSource.StreamSource;
                byte[] byteToStream;
                byteToStream = (byte[])(stream.ToArray());
                return byteToStream;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace + "\n" + ex.StackTrace, ex.InnerException);
            }
        }




    }

}
