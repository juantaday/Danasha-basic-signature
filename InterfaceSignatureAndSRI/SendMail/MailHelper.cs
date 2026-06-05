using ec.gob.sri.comprobantes.Enum;
using InterfaceSignatureAndSRI.Models;
using InterfaceSignatureAndSRI.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace InterfaceSignatureAndSRI.SendMail
{
    public class MailHelper
    {
        public static async Task SendMail(CADsisVenta.MySetting seting,
            List<string> mails, string subject, string body,
            List<myAttachment> attachments
            )
        {
            Encriptador enc = new Encriptador();
            var message = new MailMessage();
            try
            {

                foreach (var to in mails)
                {
                    message.To.Add(new MailAddress(to));
                }

                message.From = new MailAddress(seting.Email, seting.CompanyName.ToUpper().Trim());
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                foreach (myAttachment item in attachments)
                {
                    if (item.PDF == null)
                    {
                        message.Attachments.Add(new Attachment(item.MemoryStream, item.Name));
                    }
                    else
                    {
                        message.Attachments.Add(new Attachment(item.PDF, item.Name));
                    }
                }

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = seting.Email,
                        Password = enc.Desencriptar(seting.Password, Properties.Settings.Default.KeyCode),
                    };

                    smtp.Credentials = credential;
                    smtp.Host = seting.SMTP;
                    smtp.Port = int.Parse(seting.Port);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                }

            }
            catch (Exception ex)
            {
               Log.Error("MailHelper.SendMail", "Error al enviar el correo", ex);   
            }

        }
    }
}
