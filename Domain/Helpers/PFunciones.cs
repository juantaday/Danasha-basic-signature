using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Helpers
{
    public class PFunciones
    {

        public static ResponseSpliter GenerateSpliter(string myFindText, bool isPreparatedStatement = false)
        {
            try
            {
                ResponseSpliter _responseSpliter = new ResponseSpliter();
                // si no ha dada
                myFindText = myFindText.Trim();
                if (myFindText.Length == 0 | string.IsNullOrWhiteSpace(myFindText))
                    _responseSpliter = new ResponseSpliter();
                // preparamos el texto
                bool isSpace = false;
                string sql = "";
                foreach (var stri in myFindText)
                {
                    if (!isSpace)
                    {
                        sql += stri;
                        isSpace = false;
                    }
                    if (string.IsNullOrWhiteSpace(stri.ToString()))
                        isSpace = true;
                    else
                    {
                        if (isSpace)
                            sql += stri;
                        isSpace = false;
                    }
                }
                myFindText = sql;

                // rebisamos si no es codigo munerico entonces es barra de codigo 0123456789
                bool isnumric = true;
                foreach (var texto in myFindText)
                {
                    if (("0123456789").IndexOf(texto) == -1)
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
                    else if (("0123456789").IndexOf(texto) == -1)
                    {
                        if (!isText)
                            isText = true;
                    }
                    else if (!isnumric)
                        isnumric = true;
                }
                // // si es codigo
                if ((isText) & (isnumric))
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
                {
                    _responseSpliter = new ResponseSpliter()
                    {
                        IsSucces = true,
                        Spliter = myFindText.Split(' ')
                    };
                }
            // si es nombre de producto covierto en una matriz

            Salida:
                ;
                if (isPreparatedStatement)
                {
                    if (preparatedStatement(_responseSpliter))
                        return _responseSpliter;
                }

                string[] data = new string[3];

                data[0] = string.Empty;
                data[1] = string.Empty;
                data[2] = string.Empty;
                switch (_responseSpliter.Spliter.Count())
                {

                    case 1:
                        {
                            data[0] = _responseSpliter.Spliter[0];
                            _responseSpliter.Spliter = data;
                            break;
                        }

                    case 2:
                        {
                            data[0] = _responseSpliter.Spliter[0];
                            data[1] = _responseSpliter.Spliter[1];
                            _responseSpliter.Spliter = data;
                            break;
                        }

                    default:
                        {
                            data[0] = _responseSpliter.Spliter[0];
                            data[1] = _responseSpliter.Spliter[1];
                            data[2] = _responseSpliter.Spliter[2];
                            break;
                        }
                }



                return _responseSpliter;
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(
                         ex.Message, new Exception { Source = "InSpliter" });

            }
        }
        public static bool preparatedStatement(ResponseSpliter _Spliter)
        {
            try
            {
                string param1 = string.Empty;
                string param2 = string.Empty;
                string param3 = string.Empty;

                if (_Spliter.IsNumeric)
                {
                    param1 = _Spliter.Spliter[0];
                }

                if (_Spliter.IsCode)
                {
                    param1 = _Spliter.Spliter[0];
                }

                if (!_Spliter.IsNumeric & !_Spliter.IsCode)
                {
                    switch (_Spliter.Spliter.Count())
                    {
                        case 1:
                            {
                                param1 = _Spliter.Spliter[0];
                                break;
                            }

                        case 2:
                            {
                                param1 = _Spliter.Spliter[0]; param2 = _Spliter.Spliter[1];
                                break;
                            }

                        case 3:
                            {
                                param1 = _Spliter.Spliter[0]; param2 = _Spliter.Spliter[1]; param3 = _Spliter.Spliter[2];
                                break;
                            }
                    }
                }
                return true;

            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(
                           ex.Message, new Exception { Source = "InSpliter" });
            }
        }
    }
}
