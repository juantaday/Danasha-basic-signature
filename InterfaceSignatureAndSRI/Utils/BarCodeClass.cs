using iTextSharp.text.pdf;
using System;
using System.Drawing;

namespace InterfaceSignatureAndSRI.Utils
{
    public static class BarCodeClass
    {
        public static Image codigo128(string _code, bool verTexto = false, float Heigt = 0)
        {
            BarcodeCodabar barcode = new BarcodeCodabar();
            barcode.StartStopText = true;
            if (Heigt != 0)
                barcode.BarHeight = Heigt;
            barcode.Code = _code;
            try
            {
                System.Drawing.Bitmap bm = new System.Drawing.Bitmap(barcode.CreateDrawingImage(Color.Black, Color.White));
                if (!verTexto)
                    return bm;

                string ncode = _code.Substring(1, _code.Length - 2);
                Font pintarTexto = new Font("Arial", 10);
                SolidBrush brocha = new SolidBrush(Color.Black);

                SizeF stringZise = new SizeF();
                Image bmT;
                bmT = new Bitmap(bm.Width, bm.Height + 20);
                Graphics g = Graphics.FromImage(bmT);
                stringZise = g.MeasureString(ncode, pintarTexto);




                g.FillRectangle(new SolidBrush(Color.White), 0, 0, bm.Width, bm.Height + 20);


                float centrox = stringZise.Width / 2;
                float y = bm.Height + 2;
                float x = (bm.Width / 2) - (centrox);

                StringFormat drawFormat = new StringFormat();
                drawFormat.FormatFlags = StringFormatFlags.NoWrap;

                g.DrawImage(bm, 0, 0);
                g.DrawString(ncode, pintarTexto, brocha, x, y, drawFormat);

                return bmT;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al pintar en codigo" + ex.Message);
            }
        }
    }
}
