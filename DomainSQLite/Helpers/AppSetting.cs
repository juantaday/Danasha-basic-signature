using DomainSQLite.Setting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainSQLite.Helpers
{
    public class AppSetting
    {
         System.Configuration.Configuration config;

        private static System.Configuration.Configuration configStatic = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


        Dictionary<string, System.Configuration.ConnectionStringSettings> _localStrings = new Dictionary<string, System.Configuration.ConnectionStringSettings>();


        public AppSetting()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }
        public string GetConnectionString(string key)
        {
            var cnn = config.ConnectionStrings.ConnectionStrings[key];
            if (cnn == null)
            {
                throw new Exception(string.Format("The key {0} connection not fout", key));
            }


            return cnn.ConnectionString;
        }

        private static string GetFolderDbDirectory()
        {
            string phat = "";

            string directoryDB = System.Environment.GetFolderPath(
                        System.Environment.SpecialFolder.UserProfile);

            string folder = Properties.Settings.Default.FolderPath;

            if(!string.IsNullOrEmpty ( folder ))
                phat = String.Format("{0}\\{1}", directoryDB, folder);
            else 
                phat = String.Format("{0}\\Danasha Gold", directoryDB);

            if (!Microsoft.VisualBasic.FileIO.FileSystem.DirectoryExists(phat))
            {
                Microsoft.VisualBasic.FileIO.FileSystem.CreateDirectory(phat);
            }

            return phat;
        }

        public static string StrigFileDB
        {
            get
            {
                string file = string.Format("{0}\\AutoBackup.sqlite", GetFolderDbDirectory());
                return string.Format("Data Source={0};foreign keys=true", file);
            }
        }

        public static string StrigFileTextConection
        {
            get
            {
                var path = string.Format("{0}\\Conection.txt", GetFolderDbDirectory());
                if (!Microsoft.VisualBasic.FileIO.FileSystem.FileExists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                    }
                }

                return path;
            }
        }

    }
}
