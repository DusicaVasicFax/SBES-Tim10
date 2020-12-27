using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Security.Permissions;

namespace FileManagerService
{
    public class FileManager : IFileManagerService
    {
        public static string path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())), "..\\Files\\"));

        [PrincipalPermission(SecurityAction.Demand, Role = "Managment")]
        public void AddFile(string fileName, string text)
        {
            if (!File.Exists(GetFilePath(fileName)))
            {
                File.WriteAllText(GetFilePath(fileName), text);
            }
            else
            {
                throw new FaultException<FileOperationsException>(new FileOperationsException("Something went wrong while adding the file"));
            }
            Console.WriteLine(path);
        }

        public void DeleteFile(string fileName)
        {
            if (File.Exists(GetFilePath(fileName)))
                File.Delete(GetFilePath(fileName));
            else
                throw new FaultException<FileOperationsException>(new FileOperationsException("Cannot delete a file that does not exist"));
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Managment")]
        public void EditFile(string fileName, string text)
        {
            if (File.Exists(GetFilePath(fileName)))
            {
                //TODO edit file without creating a new one
            }
            else
            {
                throw new FaultException<FileOperationsException>(new FileOperationsException("Cannot edit a file that does not exist"));
            }
            File.WriteAllText(GetFilePath(fileName), text);
        }

        private string GetFilePath(string fileName)
        {
            return $"{path}{fileName}";
        }
    }
}