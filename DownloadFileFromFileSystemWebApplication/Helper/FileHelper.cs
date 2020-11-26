using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DownloadFileFromFileSystemWebApplication.Helper
{
    public class FileHelper
    {
        public static List<string> GetListOfFilePath(string filePath = @"D:\temp")
        {
            return Directory.GetFiles($@"{filePath}\").ToList();
        }

        public static string GetFilePath(string fileName)
        {
            return @"D:\temp" + $@"\{fileName}" + ".txt";
        }

        //public static string GetFilePathFromListWithFileNameParameter(List<string> listOfFilePath, string fileName)
        //{
        //    return listOfFilePath.FirstOrDefault(filePath => Path.GetFileNameWithoutExtension(filePath) == fileName);
        //}
        public static string GetFilePathFromListWithFileNameParameter(string fileName)
        {
            return GetListOfFilePath().FirstOrDefault(filePath => Path.GetFileNameWithoutExtension(filePath) == fileName);
        }
    }
}
