using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADsisVenta.Class
{
    public static class GeneratedSplit
    {
        public static ResponseSpliter GenerateSpliter(string myFindText, bool isPreparatedStatement = false)
        {
            string sql=string.Empty;
            try
            {
                ResponseSpliter _responseSpliter = new ResponseSpliter();
                // si no ha dada
                myFindText = Strings.Trim(myFindText);
                if (myFindText.Length == 0 || string.IsNullOrWhiteSpace(myFindText)) 
                    return _responseSpliter;
                // preparamos el texto
                bool isSpace = false;
                sql = "";
                foreach (var stri in myFindText)
                {
                    if (!isSpace)
                    {
                        sql += stri;
                        isSpace = false;
                    }
                    if (string.IsNullOrWhiteSpace(stri.ToString ()))
                        isSpace = true;
                    else
                    {
                        if (isSpace)
                            sql += stri;
                        isSpace = false;
                    }
                }
                myFindText = sql;

                // rebisamos si no es codigo munerico entonces es barra de codigo
                bool isnumric = true;
                foreach (var texto in myFindText)
                {
                    if (Strings.InStr("0123456789", texto.ToString ()) ==0)
                    {
                        isnumric = false;
                        break;
                    }
                }
                if (isnumric)
                {
                    _responseSpliter = new ResponseSpliter()
                    {
                        IsSucces = true,
                        IsNumeric = isnumric,
                        Spliter = myFindText.Split(' ')
                    };
                    goto Salida;
                }

                // para codigo de producto
                bool isText = false;
                isnumric = false;
                foreach (var texto in myFindText)
                {
                    if (string.IsNullOrWhiteSpace(texto.ToString()))
                    {
                        isText = false;
                        isnumric = false;
                        break;
                    }
                    else if (Strings.InStr("0123456789", texto.ToString()) == 0)
                    {
                        if (!isText)
                            isText = true;
                    }
                    else if (!isnumric)
                        isnumric = true;
                }
                // // si es codigo
                if ((isText == true) & (isnumric == true))
                {
                    _responseSpliter = new ResponseSpliter()
                    {
                        IsSucces = true,
                        IsCode = true,
                        Spliter = myFindText.Split(' ')
                    };
                    goto Salida;
                }
                else
                    // si es nombre de producto covierto en una matriz
                    _responseSpliter = new ResponseSpliter()
                    {
                        IsSucces = true,
                        Spliter = myFindText.Split(' ')
                    };
                Salida:
                
                if (isPreparatedStatement){
                }

                if (_responseSpliter.Spliter.Length == 0 )
                    _responseSpliter.Spliter = new string[] {"","","" };
                else if (_responseSpliter.Spliter.Length ==1)
                    _responseSpliter.Spliter = new string[] { _responseSpliter.Spliter[0],"",""};
                else if (_responseSpliter.Spliter.Length == 2)
                    _responseSpliter.Spliter = new string[] 
                    { _responseSpliter.Spliter[0], _responseSpliter.Spliter[1], "" };
           
                return _responseSpliter;
            }
            catch (Exception ex)
            {
                 throw new Exception  (ex.Message + "\n"+ ex.StackTrace);
            }

        }
    }
}
