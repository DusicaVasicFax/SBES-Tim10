using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace FileManagerService
{
    public class FileManager : IFileManagerService
    {
        public static string path =Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())),"..\\Files\\"));

        //TODO add dictionary
        public void AddFile(string fileName, string text)
        {
            File.WriteAllText(path + fileName, text);
            Console.WriteLine(path);
        }

        public void DeleteFile(string fileName)
        {
            File.Delete(path + fileName);
        }

        public void EditFile(string fileName, string text)
        {
            File.WriteAllText(path + fileName, text);
        }
    }
}
