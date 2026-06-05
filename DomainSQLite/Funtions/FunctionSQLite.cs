using DomainSQLite.Crypto;
using DomainSQLite.Helpers;
using DomainSQLite.Models;
using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DomainSQLite.Funtions
{
    public class FunctionSQLite
    {

       
        public async static Task<Conection> GetDefaultConectionInLine()
        {
            await Task.Delay(5);

            Conection item = new Conection { Id = -1 };
            try
            {
                string fichero = AppSetting.StrigFileTextConection;
                string contenido = String.Empty;

                if (File.Exists(fichero))
                {
                    contenido = File.ReadAllText(fichero);
                    if (!string.IsNullOrEmpty(contenido))
                    {
                        string[] lineas = contenido.Split(Convert.ToChar("\n"));
                        if (lineas.Length >= 4)
                        {
                            item.Id = 1;
                            item.IpConection = DomainSQLite.Crypto.Encriptador.DesencriptarValor(lineas[0]);
                            item.NameDatabase = DomainSQLite.Crypto.Encriptador.DesencriptarValor(lineas[1]);
                            item.UserId = DomainSQLite.Crypto.Encriptador.DesencriptarValor(lineas[2]);
                            item.Password = DomainSQLite.Crypto.Encriptador.DesencriptarValor(lineas[3]);
                        }
                    }

                }

                return item;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message + "\n" + ex.StackTrace);
                return item;
            }

        }


        public async static Task<bool> SaveItemContectionAsync(Conection itemConect)
        {
            return await Task.Run(() =>
            {
                try
                {
     
                    using (StreamWriter sw = File.CreateText(AppSetting.StrigFileTextConection))
                    {
                        sw.WriteLine(DomainSQLite.Crypto.Encriptador.EncriptarValor(itemConect.IpConection));
                        sw.WriteLine(DomainSQLite.Crypto.Encriptador.EncriptarValor(itemConect.NameDatabase));
                        sw.WriteLine(DomainSQLite.Crypto.Encriptador.EncriptarValor(itemConect.UserId));
                        sw.WriteLine(DomainSQLite.Crypto.Encriptador.EncriptarValor(itemConect.Password));
                        sw.WriteLine(DomainSQLite.Crypto.Encriptador.EncriptarValor(itemConect.FilePath));
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    string sql = "";
                    if (ex.InnerException != null && ex.InnerException.InnerException != null)
                    {
                        sql = ex.InnerException.InnerException.Message;
                    }
                    else if (ex.InnerException != null)
                    {
                        sql = ex.InnerException.Message;
                    }
                    else
                    {
                        sql = ex.Message;
                    }
                    Interaction.MsgBox(sql + "\n" + ex.StackTrace);
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return false;
                }

            });
        }

    }
}
