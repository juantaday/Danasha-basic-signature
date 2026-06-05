using Domain.Data.Enums;
using Domain.Models;
using ec.gob.sri.comprobantes.Enum;
using ec.gob.sri.comprobantes.Net;
using ec.gob.sri.comprobantes.Utils;
using InterfaceSignatureAndSRI.Data;
using InterfaceSignatureAndSRI.SendMail;
using InterfaceSignatureAndSRI.SigningXML;
using InterfaceSignatureAndSRI.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace InterfaceSignatureAndSRI.Processes
{
    public class GoldenProsecco
    {
        private static string _xml;
        private static string _estado;
        private static bool _isFinally;

        public async static void ExecuteFullProcess(OwnerEnum ownerEnum, string xmlPlaneText, string claveAcceso,
            DateTime fechaEmision, TokensValidos tokensValidos, int idDataRelation, string ambiente, int voucherID,
            List<string> emailsSend, ItemsVats itemVats, IProgress<string> progress, bool SaveFile = false,
            bool SaveInDataBase = false, string[] files = null)
        {


            _estado = string.Empty;
            _xml = string.Empty;
            _isFinally = false;

            await Task.Factory.StartNew(() =>
            {
                try
                {

                    if (string.IsNullOrEmpty(claveAcceso))
                    {
                        throw new Exception("Algo salio mal. \n No pude obteber la clave de Acceso.", new Exception("User_index"));

                    }

                    // si debo guardar en una carpeta...
                    if (SaveFile)
                    {
                        string pathGenera = string.Format("{0}\\{1}.xml", files[(int)EnumStateInvoice.Generated], claveAcceso);
                        File.WriteAllText(pathGenera, xmlPlaneText);
                        progress.Report("Guardado el archivo xml");
                        _xml = pathGenera;
                        _estado = "GENERADO";
                    }

                    //si piden que guarde en base de datos

                    //Firmado del documento si pudo ya guarda en carpeta firmados..
                    string xmlFirmado = SigningProcess.GeneratedSignigWithPlaneText(xmlPlaneText, tokensValidos, files[(int)EnumStateInvoice.Singned], SaveFile);
                    _xml = xmlFirmado;
                    _estado = "FIRMADO";
                    progress.Report("Firmado xml");
                    //revisamos si hay que guardar en base de tatos
                    if (SaveInDataBase && voucherID > 0)
                    {
                        CRUD_database.UpdateSingned(voucherID, xmlFirmado);
                    }

                    if (!CheckConnectivity.IsInternetAvailable() || string.IsNullOrWhiteSpace(ambiente))
                    {
                        progress.Report("No hay conexión a la red..");
                        return;
                    }

                    /*Enviamo el SRI en Documento firmado
                        * checque si hay conneccional al internet.
                    */
                    progress.Report("Xml enviando al sri");
                    // envio a SRI
                    var result = SendFromSRI(ambiente, xmlFirmado, claveAcceso, progress, SaveFile, files);

                    if (result.Estado.Contains("RECIBIDA")) // Si es recibida
                    {
                        progress.Report("Xml recibido por SRI");

                        if (SaveInDataBase && voucherID > 0)
                        {
                            CRUD_database.UpdateEnter(voucherID);
                        }

                        // si le compribante fue recibido espero 2 segundo para consultar...
                        progress.Report("Consultando aprobación..");
                        Thread.Sleep(1500);
                        // consulto si fuen aprobado
                        var check = CheckedState(ambiente, claveAcceso, progress, SaveFile, files);

                        if (check.Estado.Equals("AUTORIZADO") && emailsSend != null && emailsSend.Count > 0)
                        {
                            ToolsMail.SendMailDefault(ownerEnum, emailsSend, check.XML, fechaEmision: fechaEmision);
                        }

                        if (SaveInDataBase && voucherID > 0)
                        {

                            CRUD_database.UpdateAutorize(voucherID, check.XML, check.Estado, check.fechaAutorizacion, check.Message);
                        }

                    }
                    else
                    {
                        _estado = "DEVUELTA";
                        progress.Report(result.Message);
                        if (SaveInDataBase && voucherID > 0)
                        {
                            CRUD_database.UpdateRejected(voucherID, result.Message);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Log.Error("InterfaceSignatureAndSRI.GoldenProsecco", "Fallo ExecuteFullProcess", ex);
                    progress.Report(ex.Message + "\n" + ex.StackTrace);
                }
                finally
                {
                    _isFinally = true;
                }
            });
        }

        public static ResultSend SendFromSRI(string ambiente, string xmlFirmado, string claveAcceso, IProgress<string> progress, bool SaveFile, string[] files = null)
        {
            if (SaveFile && (files == null || files.Length == 0))
                throw new Exception("Pide  que guarde el archivo, pero no envia la ruta..");

            ResultSend send = new ResultSend();

            using (SRI sri = new SRI(Save: SaveFile, Notifycation: false, files: files))
            {
                //cualquier resultado que ocurra al enviar ya mueve el archivo  si esta activo SaveFile
                progress.Report("Enviado al sri para su aprobación");
                send.Estado = "ENVIANDO";
                send.Message = sri.EnviarComprobante(xmlFirmado, ambiente);
                send.Estado = sri.GetEstado();
                return send;
            }

        }

        public static ResultSend CheckedState(string ambiente, string claveAcceso, IProgress<string> progress, bool SaveFile, string[] files = null)
        {
            if (SaveFile && (files == null || files.Length == 0))
                throw new Exception("Pide  que guarde el archivo, pero no envia la ruta..");

            ResultSend send = new ResultSend();

            using (SRI sri = new SRI(Save: SaveFile, Notifycation: false, files: files))
            {
                send.Message = sri.ConsultarComprobante(claveAcceso, ambiente);
                send.Estado = sri.GetEstado();

                var autorization = sri.GetAutorizacions();
                if (autorization != null && autorization.Count > 0)
                {
                    if (!send.Estado.Equals("AUTORIZADO"))
                        progress.Report("Documento " + send.Estado + "\n\n" + send.Message);
                    else
                        progress.Report("Documento " + send.Estado);

                    send.fechaAutorizacion = sri.GetFechaAutoriza();

                    _xml = autorization[0].CDataComprobante;
                    _estado = sri.GetEstado();
                    send.XML = _xml;

                }
                else
                    progress.Report(send.Message);

                return send;
            }
        }

        public static string Estado { get => _estado; }

        internal static string GetXmlProcess()
        {
            return _xml;
        }



        public static bool IsFinally { get => _isFinally; }

    }

}
