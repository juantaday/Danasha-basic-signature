using Domain.Data.Entities;
using Domain.Data.Enums;
using Domain.Models;
using ec.gob.sri.comprobantes.Enum;
using InterfaceSignatureAndSRI.Data;
using InterfaceSignatureAndSRI.GeneratedXML;
using InterfaceSignatureAndSRI.Models;
using InterfaceSignatureAndSRI.Utils;
using InterfaceSignatureAndSRI.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace InterfaceSignatureAndSRI.Processes
{
    public class SignSendInvoice : IDisposable
    {
        #region Atributes
        private bool dispose;

        private CancellationTokenSource _cancelTokenSource;

        public SignSendInvoice(CancellationTokenSource cancelTokenSource)
        {
            _cancelTokenSource = cancelTokenSource;
        }
        #endregion


        public Action<CancellationToken> ActionToExecute;


        /// <summary>
        /// Se encarga del proceso completo de generar,firmar,enviar, decargar autoriza por SRI.
        /// Mediante el Id de facturacion.
        /// </summary>
        /// <param name="IdFactura">Codigo Interno de factura</param>
        /// <param name="files">Rutas donde se guardará los xml</param>
        public async void ExecuteWidhtIdProcess(TokensValidos tokensValidos,
            string ambiente, IProgress<string> progress, int IdFactura, int _commerceId,
            bool SaveFile = true, bool SaveInDataBase = false, string[] files = null)
        {
            try
            {
                progress.Report("Generando rutas de archivos");

                if (files == null || files.Count() == 0)
                    files = Funciones.GetPersonalfolder();

                int voucherID = 0;
                DateTime _fechaEmision;
                string xmlPlaneText = string.Empty;
                string claveAcceso;
                string codDoc;
                ItemsVats itemVat;

                List<string> emailsSend;

                progress.Report("Directorios: " + files[(int)EnumStateInvoice.Path]);

                progress.Report("Generando xml");

                using (FacturaXMLWithId factura = new FacturaXMLWithId(ambiente, IdFactura, _commerceId))
                {
                    xmlPlaneText = await NewMethod(factura);
                    _fechaEmision = factura.GetFechaEmision();
                    emailsSend = factura.GetEmailSend();
                    itemVat = factura.GetVats();
                    claveAcceso = factura.GetClaveAcceso();
                    codDoc = factura.GetCodDoc();
                }

                if (itemVat == null)
                    itemVat = new ItemsVats();

                progress.Report("Documento generado =>Clave de acceso: " + claveAcceso);

                if (SaveInDataBase)
                {

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlPlaneText);
                    voucherID = CRUD_database.InsertGenerated(claveAcceso, doc, IdFactura, codDoc, _fechaEmision);

                    progress.Report("Documento generado y guardado en base de datos");
                }

                // envio al auto procesoo.........
                GoldenProsecco.ExecuteFullProcess(OwnerEnum.Customer, xmlPlaneText, claveAcceso, _fechaEmision, tokensValidos,
                   IdFactura, ambiente, voucherID, emailsSend, itemVat, progress, SaveFile, SaveInDataBase, files);

            }

            catch (Exception ex)
            {
                Log.Error("InterfaceSignatureAndSRI.SignSendInvoice", "Fallo ExecuteWidhtIdProcess", ex);   
                progress.Report(ex.Message + "\n"+  ex.StackTrace);
   
            }

        }

        /// <summary>
        /// genera xml dependiendo que ambiente lo envien 
        /// </summary>
        /// <param name="ambiente">-1 para generar como nota  de venta (1 o 2) genera para enviar al sri PRUEBA o PRODUCCION </param>
        /// <param name="progress"></param>
        /// <param name="_fechaEmision"></param>
        /// <param name="_customer"></param>
        /// <param name="formaPago"></param>
        /// <param name="listItemSales"></param>
        /// <param name="SaveFile"></param>
        /// <param name="SaveInDataBase"></param>
        /// <param name="files"></param>
        public async void ExecuteWidhtListTicket(  string ambiente, IProgress<string> progress, DateTime _fechaEmision, Cliente _customer,
            FORMAS_PAGO formaPago, List<ItemSalesViewModel> listItemSales,
            bool SaveFile = true, bool SaveInDataBase = false, string[] files = null)
        {
            try
            {
                if (files == null || files.Count() == 0)
                    files = Funciones.GetPersonalfolder();

                int IdFactura = 0;
                string xmlPlaneText = string.Empty;
                string numDocument;
                string codDoc;
                string claveAcceso;
                string nameDoc;
                ItemsVats itemVat;

                List<string> emailsSend;

                using (FacturaXMLWithList factura = new FacturaXMLWithList(ambiente, _fechaEmision, _customer,
                    formaPago, listItemSales))
                {
                    xmlPlaneText = await NewMethod(factura);
                    _fechaEmision = factura.GetFechaEmision();
                    emailsSend = factura.GetEmailSend();
                    itemVat = factura.GetVats();
                    numDocument = factura.GetNumDocument();
                    codDoc = factura.GetCodDoc();
                    IdFactura = factura.GetIdFactura();
                    claveAcceso =factura.GetClaveAcceso();
                }

                if (ambiente.Equals("-1"))
                    nameDoc = numDocument;
                else
                    nameDoc = claveAcceso;

                if (itemVat == null)
                    itemVat = new ItemsVats();

                progress.Report("Documento generado =>Clave de acceso: " + numDocument);

                // si debo guardar en una carpeta...
                if (SaveFile)
                {
                    string pathGenera = string.Format("{0}\\{1}.xml", files[(int)EnumStateInvoice.Generated], nameDoc);
                    File.WriteAllText(pathGenera, xmlPlaneText);
                    progress.Report("Guardado el archivo xml");
                }

            }
            catch (Exception ex)
            {
               Log.Error("InterfaceSignatureAndSRI.SignSendInvoice", "Fallo ExecuteWidhtListTicket", ex);
                progress.Report(ex.Message + "\n" + ex.StackTrace); 
            }

        }


        /// <summary>
        /// Se encarga del proceso completo de generar,firmar,enviar, decargar autoriza por SRI.
        /// </summary>
        /// <param name="IdFactura">Codigo Interno de factura</param>
        /// <param name="files">Rutas donde se guardará los xml</param>
        public async void ExecuteWidhtListProcess( TokensValidos tokensValidos,
            string ambiente, IProgress<string> progress,DateTime _fechaEmision, Cliente _customer,
            FORMAS_PAGO formaPago , List<ItemSalesViewModel > listItemSales,
            bool SaveFile = true, bool SaveInDataBase = false, string[] files = null)
        {
            try
            {
                if (files == null || files.Count() == 0)
                    files = Funciones.GetPersonalfolder();

                int IdFactura=0;
                int voucherID = 0;

            

                string xmlPlaneText = string.Empty;
                string claveAcceso;
                string codDoc;
                ItemsVats itemVat;

                List<string> emailsSend;

                using (FacturaXMLWithList factura = new FacturaXMLWithList(ambiente,_fechaEmision, _customer ,
                    formaPago, listItemSales))
                {
                    xmlPlaneText = await NewMethod(factura);
                    _fechaEmision = factura.GetFechaEmision();
                    emailsSend = factura.GetEmailSend();
                    itemVat = factura.GetVats();
                    claveAcceso = factura.GetClaveAcceso();
                    codDoc = factura.GetCodDoc();
                    IdFactura = factura.GetIdFactura();
                }

                if (itemVat == null)
                    itemVat = new ItemsVats();

                progress.Report("Documento generado =>Clave de acceso: " + claveAcceso);

                if (SaveInDataBase)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlPlaneText);

                    voucherID = CRUD_database.InsertGenerated(claveAcceso, doc, IdFactura, codDoc, _fechaEmision);

                    progress.Report("Documento generado y guardado en base de datos");
                }

                // envio al auto procesoo.........
                GoldenProsecco.ExecuteFullProcess(OwnerEnum.Customer, xmlPlaneText, claveAcceso, _fechaEmision, tokensValidos,
                   IdFactura, ambiente, 0, emailsSend, itemVat, progress, SaveFile, SaveInDataBase, files);

                while (!GoldenProsecco.IsFinally )
                {
                    Thread.Sleep(500);
                }

                string xml =GoldenProsecco.GetXmlProcess();

                FacturaViewModels viewModel = new FacturaViewModels();
                viewModel.SetInformatio(xml,_fechaEmision, null);

                using (var pdfView = new ViewPdfForm( Data.Enums.ViewTypePDFEnum.TICKET, viewModel)) {
                    pdfView.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    pdfView.ShowDialog();
                }

            }

            catch (Exception ex)
            {
                Log.Error("InterfaceSignatureAndSRI.SignSendInvoice", "Fallo ExecuteWidhtListTicket", ex);
                progress.Report(ex.Message + "\n" + ex.StackTrace);
            }

        }

        /// <summary>
        /// Devuelve los datos de sql server en formato texto plano (string)
        /// </summary>
        /// <param name="IdFactura">key</param>
        /// <param name="generaFac"></param>
        /// <returns></returns>
        private async Task<string> NewMethod(FacturaXMLWithId generaFac)
        {
            return await generaFac.GetXmlFactura();
        }
        /// <summary>
        /// Devuelve los datos de sql server en formato texto plano (string)
        /// </summary>
        /// <param name="IdFactura">key</param>
        /// <param name="generaFac"></param>
        /// <returns></returns>
        private async Task<string> NewMethod(FacturaXMLWithList generaFac)
        {
            return await generaFac.GetXmlFactura();
        }


        public void star()
        {
            Task.Factory.StartNew(() =>
               ActionToExecute(_cancelTokenSource.Token)).ContinueWith((t) => taskCompleted());
        }
        private void taskCompleted()
        {
            releaseCancellationTokenSource();
        }
        private void releaseCancellationTokenSource()
        {
            if (_cancelTokenSource != null)
            {
                _cancelTokenSource.Dispose();
                _cancelTokenSource = null;
            }
            this.Dispose();
        }

        [System.Diagnostics.DebuggerNonUserCode()]
        public void Dispose()
        {
            if (dispose) return;
            dispose = true;
        }

    }
}
