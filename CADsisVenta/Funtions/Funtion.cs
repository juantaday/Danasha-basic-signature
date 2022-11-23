using CADsisVenta.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CADsisVenta.Statics;
using System.Net.NetworkInformation;

namespace CADsisVenta.Funtions
{
    public static class Funtion
    {
        public static List<IMPUESTO> GetListIMPUESTO()
        {

            using (var db = new DataContext())
            {
                return db.IMPUESTO.ToList();
            }
        }

        public static List<IMPUESTO_VALOR> GetListIVA()
        {
            using (var db = new DataContext())
            {

                return db.IMPUESTO_VALOR.Where(t => t.CODIGO_IMPUESTO == 2 && t.TIPO_IMPUESTO == "I").ToList();
            }
        }

        public static byte[] GetLogoPDFByte()
        {
            using (var db = new DataContext())
            {

                var commerce = db.myCommerce.FirstOrDefault();
                if (commerce != null)
                    return commerce.LogoPDF.ToArray ();

                return null;
            }
        }

        public static byte[] GetLogoTicketByte()
        {
            using (var db = new DataContext())
            {

                var commerce = db.myCommerce.FirstOrDefault();
                if (commerce != null)
                    commerce.LogoTicket.ToArray();

                return null;
            }
        }

        public static myCommerce GetMyCommerceFirst()
        {
            using (var db = new CADsisVenta.Data.DataContext())
            {
                var commer = db.myCommerce.FirstOrDefault();

                foreach (var item in commer.SignatureOptions)
                {
                    var toke = item.TOKEN;
                }

                return commer;

            }
        }

        public static CADsisVenta.MySetting GetMySetting(int commerceId)
        {
            using (var db = new CADsisVenta.Data.DataContext())
            {
                return db.MySetting.Where(x => x.MyCommerceId == commerceId).FirstOrDefault();
            }
        }

        public async static Task<Tuple<bool, myCommerce>> SaveAndUpdateECommerceAsync(myCommerce currentCommerce)
        {
            await Task.Delay(4);

            using (var db = new CADsisVenta.Data.DataContext())
            {
                db.Connection.Open();
                using (var transaction = db.Connection.BeginTransaction())
                {
                    try
                    {
                        db.Transaction= transaction;

                        string[] notMapped = new string[] { "CommerceId" };

                        var commerce= db.myCommerce.FirstOrDefault();
                        if (commerce == null)
                        {
                            commerce = new myCommerce();
                     
                            PropertyCopier<myCommerce, myCommerce>.Copy(currentCommerce, commerce, notMapped);
                            db.myCommerce.InsertOnSubmit(commerce);
                        }
                        else {
                            var dff = currentCommerce.SignatureOptions.FirstOrDefault();    
                            PropertyCopier<myCommerce, myCommerce>.Copy(currentCommerce, commerce, notMapped);
                        }
                       db.SubmitChanges();



                        var singOption = db.SignatureOptions.FirstOrDefault();
                        if (singOption == null)
                        {
                            singOption = new SignatureOptions();
                            PropertyCopier<SignatureOptions, SignatureOptions>.Copy(
                                currentCommerce.SignatureOptions.FirstOrDefault (), singOption, null);
                            db.SignatureOptions.InsertOnSubmit(singOption);
                        }
                        else
                        {
                            PropertyCopier<SignatureOptions, SignatureOptions>.Copy(
                           currentCommerce.SignatureOptions.FirstOrDefault(), singOption, null);
                        }
                        db.SubmitChanges();


                        transaction.Commit();

                        return Tuple.Create(true, currentCommerce);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new Exception(ex.Message, ex.InnerException);
                    }

                }

            }
        }

        public async   static Task<Tuple<bool, int>> SaveAndUpdateMySettingAync(MySetting currentMySetting)
        {
            await Task.Delay(4);

            using (var db = new CADsisVenta.Data.DataContext())
            {

                var setting = db.MySetting.Where(x => x.MyCommerceId == currentMySetting.MyCommerceId).FirstOrDefault();
                if (setting == null)
                {
                    setting = new CADsisVenta.MySetting();
                    PropertyCopier<CADsisVenta.MySetting, CADsisVenta.MySetting>.Copy(
                        currentMySetting, setting, null);

                    db.MySetting.InsertOnSubmit(setting);
                }
                else {
                    PropertyCopier<CADsisVenta.MySetting, CADsisVenta.MySetting>.Copy(
                         currentMySetting, setting, null);
                }
          

                db.SubmitChanges ();

                return Tuple.Create(true, setting.MySettingID);
            }

        }

    }
}
