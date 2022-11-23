using DomainSQLite.Crypto;
using DomainSQLite.Helpers;
using DomainSQLite.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DomainSQLite.Funtions
{
    public class FunctionSQLite
    {

        private static string keyHas = "fdg36125☺}♫825╩5-5645644○87m4:█J";

        public async static Task<Conection> GetDefaultConectionInLine()
        {
            await  Task.Delay (5);

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
                            Encryptor encryptor = new Encryptor(keyHas);

                            item.Id = 1;
                            item.IpConection = encryptor.Desencriptar(lineas[0]);
                            item.NameDatabase = encryptor.Desencriptar(lineas[1]);
                            item.UserId = encryptor.Desencriptar(lineas[2]);
                            item.Password = encryptor.Desencriptar(lineas[3]);
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
            return await Task.Run(() => {
                try
                {
                    Encryptor encryptor = new Encryptor(keyHas);

                    using (StreamWriter sw = File.CreateText(AppSetting.StrigFileTextConection))
                    {
                        sw.WriteLine(encryptor.Encriptar(itemConect.IpConection));
                        sw.WriteLine(encryptor.Encriptar(itemConect.NameDatabase));
                        sw.WriteLine(encryptor.Encriptar(itemConect.UserId));
                        sw.WriteLine(encryptor.Encriptar(itemConect.Password));
                        sw.WriteLine(encryptor.Encriptar(itemConect.FilePath));
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
