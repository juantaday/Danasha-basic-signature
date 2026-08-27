using Domain.Data;
using Domain.Data.Entities;
using Domain.Data.Enums;
using Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Funtions
{
    public static class Funtion
    {

        private static string keyHas = "de12↓}Ä7`U7♦1↓asdr34523";
        public static List<Product> GetListProductsAll()
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                return db.Products.Include(x => x.PRODUCTO_IMPUESTO)
                    .Include(x => x.INFO_ADICIONALS)
                    .Take(500).ToList();
            }
        }


        public static List<Cliente> GetListClientAll()
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                return db.Clientes.Include(x => x.Personas).Take(500).ToList();
            }
        }

        public static List<TypeIdentification> GetListTypeIdentificationAll()
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                return db.TypeIdentifications.Take(10).ToList();
            }
        }

        public static List<TypeDocument> GetListTypeDocumentsAll()
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                return db.TypeDocuments.Take(10).ToList();
            }
        }

        public static List<IMPUESTO_VALOR> GetListIVA_All()
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                var typeVat = new string[] { "I", "B" };
                return db.IMPUESTO_VALOR.Where(t => typeVat.Contains(t.TIPO_IMPUESTO)).ToList();
            }
        }

        public static List<IMPUESTO_VALOR> GetListIVA()
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                return db.IMPUESTO_VALOR.Where(t => t.CODIGO_IMPUESTO == 2 && t.TIPO_IMPUESTO == "I").ToList();
            }
        }

        public static List<IMPUESTO> GetListIMPUESTO()
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                return db.IMPUESTOS.ToList();
            }
        }

        public static List<IMPUESTO_VALOR> GetListICE_All()
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                return db.IMPUESTO_VALOR.Where(X => X.CODIGO_IMPUESTO == 3).ToList();
            }
        }

        public static List<IMPUESTO_VALOR> GetListIRBPNR_All()
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                return db.IMPUESTO_VALOR.Where(X => X.CODIGO_IMPUESTO == 5).ToList();
            }
        }

        public static async Task<Tuple<bool, Product>> SaveProductAsync(Product currentProduct)
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                db.Products.Add(currentProduct);
                await db.SaveChangesAsync();
                return Tuple.Create(true, currentProduct);
            }

        }

        public static async Task<Tuple<bool, Product>> UpdateProductAsync(Product currentProduct)
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        db.Entry(currentProduct).State = EntityState.Modified;

                        await db.SaveChangesAsync();

                        foreach (PRODUCTO_IMPUESTO item in currentProduct.PRODUCTO_IMPUESTO)
                        {
                            if (item.Id > 0 && item.TIPO_IMPUESTO != 2 && item.CODIGO_IMPUESTO == "0")
                                db.Entry(item).State = EntityState.Deleted;
                            else
                                db.Entry(item).State = item.Id == 0 ?
                                         EntityState.Added :
                                         EntityState.Modified;
                        }

                        await db.SaveChangesAsync();

                        if (currentProduct.INFO_ADICIONALS != null)
                        {
                            foreach (INFO_ADICIONAL item in currentProduct.INFO_ADICIONALS)
                            {
                                if (item.Id > 0 && string.IsNullOrWhiteSpace(item.ValueAtribute))
                                    db.Entry(item).State = EntityState.Deleted;
                                else
                                    db.Entry(item).State = item.Id == 0 ?
                                             EntityState.Added :
                                             EntityState.Modified;
                            }

                            await db.SaveChangesAsync();
                        }




                        transaction.Commit();

                        return Tuple.Create(true, currentProduct);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new Exception(ex.Message, ex.InnerException);
                    }

                }

            }
        }

        public static SignatureOption GetSignatureOptionByCommerceId(int myCommerceId)
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                return db.SignatureOptions.Where(x => x.MyCommerceId == myCommerceId).FirstOrDefault();
            }
        }

        public static MyCommerce GetMyCommerceFirst()
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                return db.MyCommerce.Include(x => x.SignatureOptions).FirstOrDefault();
            }
        }

        public static async Task<Tuple<bool, MyCommerce>> SaveAndUpdateECommerceAsync(MyCommerce currentCommerce)
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        db.Entry(currentCommerce).State = currentCommerce.CommerceId == 0 ?
                               EntityState.Added :
                               EntityState.Modified;


                        await db.SaveChangesAsync();

                        if (currentCommerce.SignatureOptions != null)
                        {
                            foreach (SignatureOption item in currentCommerce.SignatureOptions)
                            {
                                db.Entry(item).State = item.Id == 0 ?
                                    EntityState.Added :
                                    EntityState.Modified;
                            }

                            await db.SaveChangesAsync();
                        }

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

        public async static Task<bool> SaveListTypeDocumentAsync(List<TypeDocument> listTypeDocuments)
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                foreach (var item in listTypeDocuments)
                {
                    db.Entry(item).State = EntityState.Modified;
                }
                await db.SaveChangesAsync();
                return true;
            }
        }

        public static List<Product> GetListProductsByParameter(string search)
        {
            var response = PFunciones.GenerateSpliter(search);
            if (!response.IsSucces)
                throw new Exception("No pude convertir a parametros");


            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                if (response.IsNumeric)
                    return db.Products.Include(i => i.INFO_ADICIONALS).Include(x => x.PRODUCTO_IMPUESTO)
                        .ThenInclude(z => z.IMPUESTO_VALOR)
                        .Where(x => x.BarCode.Trim() == response.Spliter[0]).ToList();
                else if (response.IsCode)
                    return db.Products.Include(i => i.INFO_ADICIONALS).Include(x => x.PRODUCTO_IMPUESTO)
                        .ThenInclude(z => z.IMPUESTO_VALOR)
                        .Where(x => x.Cod_Secondary.Trim() == response.Spliter[0]).ToList();
                else
                {
                    if (response.Spliter[2].Length > 0)
                    {
                        var qry = db.Products.Include(i => i.INFO_ADICIONALS).Include(x => x.PRODUCTO_IMPUESTO)
                        .ThenInclude(z => z.IMPUESTO_VALOR)
                        .Where(x => x.Name_Producto.Contains(response.Spliter[0]));

                        var qry1 = qry.Where(x => x.Name_Producto.Contains(response.Spliter[1]));
                        return qry.Where(x => x.Name_Producto.Contains(response.Spliter[2])).ToList(); ;
                    }
                    else if (response.Spliter[1].Length > 0)
                    {
                        var qry = db.Products.Include(i => i.INFO_ADICIONALS).Include(x => x.PRODUCTO_IMPUESTO)
                        .ThenInclude(z => z.IMPUESTO_VALOR)
                        .Where(x => x.Name_Producto.Contains(response.Spliter[0]));

                        return qry.Where(x => x.Name_Producto.Contains(response.Spliter[1])).ToList();
                    }
                    else
                    {
                        return db.Products.Include(i => i.INFO_ADICIONALS).Include(x => x.PRODUCTO_IMPUESTO)
                            .ThenInclude(x => x.IMPUESTO_VALOR)
                            .Where(x => x.Name_Producto.Contains(response.Spliter[0])).ToList();
                    }

                }

            }

        }

        public static List<Product> GetListProductsWithList(List<Product> products)
        {

            if (products == null || products.Count == 0)
                return null;


            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {

                return db.Products.Include(i => i.INFO_ADICIONALS).Include(x => x.PRODUCTO_IMPUESTO)
                        .ThenInclude(z => z.IMPUESTO_VALOR).Join(
                            products,
                            p => p.Id,
                            s => s.Id,
                            (prod, sel) => new { prod, sel }).Select(x => x.prod).ToList();


            }
        }


     
        public static FORMAS_PAGO GetDefaultFormasPago()
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                return db.FORMAS_PAGOS.Where(x => x.CODIGO_FORMA_PAGO == "01").FirstOrDefault();
            }
        }

        public static Cliente GetDefaultCustomer()
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                return db.Clientes.Include(x => x.Personas).Where(x => x.Personas.Ruc_Ci == "9999999999999").FirstOrDefault();
            }
        }

        public static byte[] GetLogoPDFByte()
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                var comerce = db.MyCommerce.FirstOrDefault();
                if (comerce == null)
                    return null;

                return comerce.LogoPDF;

            }
        }
        public static byte[] GetLogoTicketByte()
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                var comerce = db.MyCommerce.FirstOrDefault();
                if (comerce == null)
                    return null;

                return comerce.LogoTicket;

            }
        }


        public static MySetting GetMySetting(int myCommerceId)
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                return db.MySettings.Where(x => x.MyCommerceId == myCommerceId).FirstOrDefault();

            }
        }

        public async static Task<Tuple<bool, int>> SaveAndUpdateMySettingAync(MySetting currentMySetting)
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {
                db.Entry(currentMySetting).State = currentMySetting.MySettingID == 0 ?
                      EntityState.Added :
                      EntityState.Modified;


                await db.SaveChangesAsync();

                return Tuple.Create(true, currentMySetting.MySettingID);
            }
        }

       
        public async static Task<bool> UpdatePriceProduc(int productId, decimal newPrice)
        {
            using (var db = new DomainDataContext(new DbContextOptions<DataContext>()))
            {

                var product = db.Products.Where(x => x.Id == productId).FirstOrDefault();
                if (product == null)
                    throw new KeyNotFoundException(nameof(Product) + "ID: " + productId.ToString());

                product.UnitPrice = newPrice;

                await db.SaveChangesAsync();
                return true;

            }
        }


    }

}
