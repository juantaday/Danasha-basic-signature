using System;
using System.IO;

namespace DomainSQLite.Setting
{
    public class AppSetting
    {
        // fore real
        //public static string klibraryKey = "DC307C3AD386C44F9CCD1EC2F1F9ADC23B3636C0ABBA9319729C238AD713F9BA8282EB491506";

        //for demo
        public static string klibraryKey = "89979BD86E62BCE69B80A72A993E9973A46C3A48CA948CD447D6D37D7F43778146AB41727FF4";

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
        private static string GetFolderDbDirectory()
        {

            string phat = "";

            string directoryDB = System.Environment.GetFolderPath(
                        System.Environment.SpecialFolder.UserProfile);

            string nameFolder = Properties.Settings.Default.FolderPath;
            if (!string.IsNullOrEmpty(nameFolder))
                phat = String.Format("{0}\\{1}", directoryDB, nameFolder);
            else
                phat = String.Format("{0}\\Danasha Gold", directoryDB);


            if (!Microsoft.VisualBasic.FileIO.FileSystem.DirectoryExists(phat))
            {
                Microsoft.VisualBasic.FileIO.FileSystem.CreateDirectory(phat);
            }
            return phat;
        }

    }
}
