using ec.gob.sri.comprobantes.Enum;
using InterfaceSignatureAndSRI.SendMail;
using Microsoft.VisualBasic;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace InterfaceSignatureAndSRI.Utils
{
    public static class Funciones
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

                // rebisamos si no es codigo munerico entonces es barra de codigo
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
                ;
                if (isPreparatedStatement)
                {
                }
                return _responseSpliter;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical, "Error");
                return new ResponseSpliter();
            }
        }

        public static string Body(ItemHeaderData details)
        {
            return string.Format(@"
                                     <p>Estimad@:
                                     <br/>
                                      <b><strong>{5}</b>
                                      </p>
                                         <p><br/></p>
                                         <p>Adjuntamos su comprobante eléctronico...</p>
                                         <table border=1 cellspacing=0 cellpadding=1 bordercolor='red'>
                                          <tr>
                                            <th scope='col'>-DETALLE-</th>
                                            <th scope='col'>  -VALOR-  </th>
                                          </tr>
                                          <tr>
                                            <td>Nro. Documento:</td>
                                            <td><strong>{0}</td>
                                          </tr>
                                          <tr>
                                            <td>Fecha de emisión:</td>
                                            <td><strong>{1}</td>
                                          </tr>
                                          <tr>
                                             <td>Clave de acceso:</td>
                                             <td><strong>{2}</td>
                                           </tr>
                                          <tr>
                                             <td>Número de Autorización SRI:</td>
                                             <td><strong>{3}</td>
                                           </tr>
                                          <tr>
                                             <td>Fecha de autorización SRI:</td>
                                             <td><strong>{4}</td>
                                           </tr>
                                         </table>
                                     <br/>
                                        <p>Atentamete:
                                        <br/>
                                        <b>{6}</b>
                                        <br/>
                                        <small>Teléfono: {7}</small>
                                        <br/>
                                        <small>Celular: {8}</small>
                                      </p>
                                    <footer>
                                        <hr/>
                                        <p align='center'>
                                            <small>Powered by CREDIT MANAGER SYSTEM Un producto de JMTSystemSofware</small>
                                            <br/>                                             
                                            <small>https://juantadaymalan3.wixsite.com/website</small>
                                            <br/>                                             
                                            <small>juantadaymalan3@gmail.com - 0981464575 -</small>
                                        </p>
                                    </footer>",
                                    details.Document_Num,
                                    details.FechaEmision,
                                    details.ClaveAcceso,
                                    details.numeroAutorizacion,
                                    details.FechaAutoriza,
                                    details.RazonSocial,
                                    details.CompanyName,
                                    details.Phone,
                                    details.CellPhone);
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static string[] GetPersonalfolder()
        {
            string directoryDB;

            directoryDB = System.Environment.GetFolderPath(
                 System.Environment.SpecialFolder.UserProfile);


            var folderName = Properties.Settings.Default.DefaultOutFolder.ToString();

            if (!string.IsNullOrEmpty(folderName))
                directoryDB = System.IO.Path.Combine(directoryDB, folderName);

            directoryDB = System.IO.Path.Combine(directoryDB, "Documentos");



            if (!Microsoft.VisualBasic.FileIO.FileSystem.DirectoryExists(directoryDB))
            {
                Microsoft.VisualBasic.FileIO.FileSystem.CreateDirectory(directoryDB);
            }
            //generados
            if (!Microsoft.VisualBasic.FileIO.FileSystem.DirectoryExists(String.Format("{0}\\Generados", directoryDB)))
            {
                Microsoft.VisualBasic.FileIO.FileSystem.CreateDirectory(String.Format("{0}\\Generados", directoryDB));
            }

            string[] _files = new string[7];
            _files[(int)EnumStateInvoice.Generated] = String.Format("{0}\\Generados", directoryDB);
            _files[(int)EnumStateInvoice.Singned] = String.Format("{0}\\Firmados", directoryDB);
            _files[(int)EnumStateInvoice.Send] = String.Format("{0}\\Enviados", directoryDB);
            _files[(int)EnumStateInvoice.Returned] = String.Format("{0}\\Devueltos", directoryDB);
            _files[(int)EnumStateInvoice.NotAuthorized] = String.Format("{0}\\No Autorizados", directoryDB);
            _files[(int)EnumStateInvoice.Authorized] = String.Format("{0}\\Autorizados", directoryDB);
            _files[(int)EnumStateInvoice.Path] = String.Format(directoryDB);
            return _files;
        }

        public static void CreatedDeafaultFoldes()
        {
            string directoryDB;

            directoryDB = System.Environment.GetFolderPath(
                 System.Environment.SpecialFolder.UserProfile);


            var folderName = Properties.Settings.Default.DefaultOutFolder.ToString();

            if (!string.IsNullOrEmpty(folderName))
                directoryDB = System.IO.Path.Combine(directoryDB, folderName);

            directoryDB = System.IO.Path.Combine(directoryDB, "Documentos");

            //not fount root directoty created
            if (!Microsoft.VisualBasic.FileIO.FileSystem.DirectoryExists(directoryDB))
            {
                Microsoft.VisualBasic.FileIO.FileSystem.CreateDirectory(directoryDB);
            }

            //Generados
            if (!Microsoft.VisualBasic.FileIO.FileSystem.DirectoryExists(String.Format("{0}\\Generados", directoryDB)))
            {
                Microsoft.VisualBasic.FileIO.FileSystem.CreateDirectory(String.Format("{0}\\Generados", directoryDB));
            }

            //Firmados
            if (!Microsoft.VisualBasic.FileIO.FileSystem.DirectoryExists(String.Format("{0}\\Firmados", directoryDB)))
            {
                Microsoft.VisualBasic.FileIO.FileSystem.CreateDirectory(String.Format("{0}\\Firmados", directoryDB));
            }

            //Enviados
            if (!Microsoft.VisualBasic.FileIO.FileSystem.DirectoryExists(String.Format("{0}\\Enviados", directoryDB)))
            {
                Microsoft.VisualBasic.FileIO.FileSystem.CreateDirectory(String.Format("{0}\\Enviados", directoryDB));
            }

            //Devueltos
            if (!Microsoft.VisualBasic.FileIO.FileSystem.DirectoryExists(String.Format("{0}\\Devueltos", directoryDB)))
            {
                Microsoft.VisualBasic.FileIO.FileSystem.CreateDirectory(String.Format("{0}\\Devueltos", directoryDB));
            }

            //No Autorizados
            if (!Microsoft.VisualBasic.FileIO.FileSystem.DirectoryExists(String.Format("{0}\\No Autorizados", directoryDB)))
            {
                Microsoft.VisualBasic.FileIO.FileSystem.CreateDirectory(String.Format("{0}\\No Autorizados", directoryDB));
            }


            //Autorizados
            if (!Microsoft.VisualBasic.FileIO.FileSystem.DirectoryExists(String.Format("{0}\\Autorizados", directoryDB)))
            {
                Microsoft.VisualBasic.FileIO.FileSystem.CreateDirectory(String.Format("{0}\\Autorizados", directoryDB));
            }

        }
    }
}
