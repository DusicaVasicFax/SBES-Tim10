using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace FileManagerService
{
    public class FileManager : IFileManagerService
    {
        public static string path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())), "..\\Files\\"));
        public static string pathConfig = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())), "..\\FIM\\FIMConfig.txt"));

        [PrincipalPermission(SecurityAction.Demand, Role = "Managment")]
        public void AddFile(string fileName, byte[] signature, string text)
        {
            if (!File.Exists(GetFilePath(fileName)))
            {
                File.WriteAllText(GetFilePath(fileName), text + '\n' + Convert.ToBase64String(signature));
                File.AppendAllText(pathConfig, $"\r\n{fileName}");
            }
            else
            {
                throw new FaultException<FileOperationsException>(new FileOperationsException("Something went wrong while adding the file"));
            }
            Console.WriteLine(path);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public void DeleteFile(string fileName)
        {
            IIdentity identity = Thread.CurrentPrincipal.Identity;

            Console.WriteLine("Tip autentifikacije : " + identity.AuthenticationType);

            WindowsIdentity windowsIdentity = identity as WindowsIdentity;

            Console.WriteLine("Ime klijenta koji je pozvao metodu : " + windowsIdentity.Name);
            Console.WriteLine("Jedinstveni identifikator : " + windowsIdentity.User);


            if (File.Exists(GetFilePath(fileName)))
            {
                File.Delete(GetFilePath(fileName));
                List<string> filesWithDeleted = File.ReadAllLines(pathConfig).Where(x => !x.Equals(fileName)).ToList();
                File.WriteAllLines(pathConfig, filesWithDeleted);
            }
            else
                throw new FaultException<FileOperationsException>(new FileOperationsException("Cannot delete a file that does not exist"));
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Managment")]
        public void EditFile(string fileName, byte[] signature, string text)
        {

            IIdentity identity = Thread.CurrentPrincipal.Identity;

            Console.WriteLine("Tip autentifikacije : " + identity.AuthenticationType);

            WindowsIdentity windowsIdentity = identity as WindowsIdentity;

            Console.WriteLine("Ime klijenta koji je pozvao metodu : " + windowsIdentity.Name);
            Console.WriteLine("Jedinstveni identifikator : " + windowsIdentity.User);

            Console.WriteLine("Grupe korisnika:");
            

            if (File.Exists(GetFilePath(fileName)))
            {
                File.WriteAllText(GetFilePath(fileName), text + '\n' + Convert.ToBase64String(signature));
            }
            else
            {
                throw new FaultException<FileOperationsException>(new FileOperationsException("Cannot edit a file that does not exist"));
            }
        }

        private string GetFilePath(string fileName)
        {
            return $"{path}{fileName}";
        }
    }
}