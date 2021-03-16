using System;
using System.IO;
using System.Threading.Tasks;

namespace EasierLog
{
    internal static class FileHelper 
    {
        public static bool CreateDirectoryIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            return Directory.Exists(directory);
        }

        public static Task DeletePastFilesAccorddingSettings()
        {
            return Task.Run(() =>
            {
                if (Settings.DaysToKeepLogFiles > 0)
                {
                    foreach (var file in Directory.GetFiles(Settings.DirectoryToStoreLog))
                    {
                        var fileName = file.Substring(file.LastIndexOf('\\') + 1, 8);
                        var year = Convert.ToInt16(fileName.Substring(0, 4));
                        var month = Convert.ToInt16(fileName.Substring(4, 2));
                        var day = Convert.ToInt16(fileName.Substring(6, 2));
                        var fileDate = new DateTime(year, month, day);

                        if (DateTime.Now.Subtract(fileDate).Days > Settings.DaysToKeepLogFiles)
                            File.Delete(file);
                    }
                }
            });
        }
    }
}
