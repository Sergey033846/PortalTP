using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace mcperscab
{
    public class MC_PersCab_FileIO
    {
        // формирование и создание пути документа заявки, формирование полного имени файла
        public static void MC_PersCab_FileIO_CreateZayavkaDocPath(Guid id_zayavka, string fileNameSrc, DateTime dateCreate, out string zayavkaDocPathOut, out string docFileNameOut, out string docFileFullNameOut)
        {
            string zayavkaDocPath = String.Format("{0}\\{1:D2}\\{2:D2}\\{3}", dateCreate.Year, dateCreate.Month, dateCreate.Day, id_zayavka);

            string docFileName = fileNameSrc.Replace(',', '_'); // удаляем нежелательные символы в имени файла

            string docDirPath = String.Concat(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "App_Data\\UploadTemp");

            string docFileFullName = String.Concat(docDirPath, "\\", zayavkaDocPath, "\\" + docFileName);

            DirectoryInfo d1 = new DirectoryInfo(String.Concat(docDirPath, "\\", zayavkaDocPath));
            if (!d1.Exists) d1.Create();
            if (File.Exists(docFileFullName))
            {
                zayavkaDocPath = String.Concat(zayavkaDocPath, "\\", Guid.NewGuid().ToString());
                DirectoryInfo d2 = new DirectoryInfo(String.Concat(docDirPath, "\\", zayavkaDocPath));
                if (!d2.Exists) d2.Create();
                docFileFullName = String.Concat(docDirPath, "\\", zayavkaDocPath, "\\", docFileName);
            }

            zayavkaDocPathOut = zayavkaDocPath;
            docFileNameOut = docFileName;
            docFileFullNameOut = docFileFullName;
        }

        // удаление файла
        public static int MC_PersCab_FileIO_DeleteFile(string fileNameSrc, string pathSrc)
        {
            string docDirPath = String.Concat(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "App_Data\\UploadTemp\\", pathSrc, "\\");
            string docFileFullName = String.Concat(docDirPath, fileNameSrc);

            FileInfo fileInfo = new FileInfo(docFileFullName);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();

                // если в папке больше нет файлов и других каталогов, удаляем папку
                if (Directory.GetFiles(docDirPath).Length == 0 && Directory.GetDirectories(docDirPath).Length == 0) Directory.Delete(docDirPath);

                return 0;
            }
            else return -1;
        }
    }
}
