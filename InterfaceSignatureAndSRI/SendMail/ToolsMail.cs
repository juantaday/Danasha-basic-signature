using CADsisVenta.Statics;
using CrystalDecisions.Shared;
using Domain.Data.Enums;
using Domain.Models;
using ec.gob.sri.comprobantes.Enum;
using InterfaceSignatureAndSRI.Data;
using InterfaceSignatureAndSRI.Models;
using InterfaceSignatureAndSRI.Reports;
using InterfaceSignatureAndSRI.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSignatureAndSRI.SendMail
{
    public static class ToolsMail
    {

        public static async Task<ResultSend> SendMailDefault(OwnerEnum ownerEnum, List<string> emailsSend,
            string xml, RetencionViewModels retencionModel = null, FacturaViewModels facturaModel = null,
            DateTime? fechaEmision = null)
        {

            ResultSend send = new ResultSend();

            if (emailsSend == null || emailsSend.Count == 0)
            {
                send.Estado = "ERROR";
                send.Message = "No existe destinatarios..";
                return send;
            }

            try
            {
                CADsisVenta.MySetting mySetting = CADsisVenta.Funtions.Funtion.GetMySetting(SettingObject.EcommerceActive.CommerceId);
                if (mySetting == null)
                    throw new Exception("No esta configurado las credenciales..");

                switch (ownerEnum)
                {
                    case OwnerEnum.Customer:
                        SendMailCustomer(emailsSend, xml, mySetting, facturaModel, fechaEmision);
                        break;
                    case OwnerEnum.Supplier:

                        break;
                    case OwnerEnum.Employee:
                        break;
                    case OwnerEnum.Proprietor:
                        break;
                    case OwnerEnum.Busines:
                        break;
                    default:
                        break;
                }

                send.Estado = "ENVIADO";
            }
            catch (Exception ex)
            {
                send.Estado = "ERROR";
                send.Message = ex.Message;

                string[] _files = Funciones.GetPersonalfolder();
                Log log = new Log(_files[(int)EnumStateInvoice.Path]);
                log.Add(ex.Message);
                log.Add(ex.StackTrace);
            }

            return send;

        }

        private async static void SendMailCustomer(List<string> emailsSend, string xml,
          CADsisVenta.MySetting mySetting, FacturaViewModels viewmodel, DateTime? fechaEmision = null)
        {

            if (viewmodel == null)
            {
                viewmodel = new FacturaViewModels();
                viewmodel.SetInformatio(xml, fechaEmision: fechaEmision, mySetting.ImageLogo.ToArray());
            }


            string xmlAutorize = string.Empty;

            if (!viewmodel.Data.StartsWith("<?xml"))
                xmlAutorize = "<?xml version=" + "\"1.0\"" + " encoding=" + "\"UTF-8\"" + "?>" + viewmodel.Data;
            else
                xmlAutorize = viewmodel.Data;


            byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlAutorize);

            var attachments = new List<myAttachment>();

            attachments.Add(new myAttachment
            {
                MemoryStream = new MemoryStream(xmlBytes),
                Name = string.Format("{0}.xml", viewmodel.ClaveAcceso),
            });

            FacturaRideRpt rpt = new FacturaRideRpt();
            rpt.SetDataSource(CRUD_database.GetDataSetWithModel(viewmodel));

            Stream pdfRide = rpt.ExportToStream(ExportFormatType.PortableDocFormat);
            attachments.Add(new myAttachment
            {
                PDF = pdfRide,
                Name = string.Format("{0}.pdf", viewmodel.ClaveAcceso),
            });

            await MailHelper.SendMail(mySetting, emailsSend,
                "Nueva FACTURA eléctronica..",
                Funciones.Body(new ItemHeaderData
                {
                    ClaveAcceso = viewmodel.ClaveAcceso,
                    Document_Num = viewmodel.FacturaNum,
                    FechaAutoriza = viewmodel.fechaAutoriza.Value,
                    FechaEmision = viewmodel.fechaEmision,
                    numeroAutorizacion = viewmodel.numeroAutorizacion,
                    RazonSocial = viewmodel.RazonSocialComprador,
                    CompanyName = mySetting.CompanyName,
                    CellPhone = mySetting.CellPhone,
                    Phone = mySetting.Phone
                }), attachments);
        }

    }

}
