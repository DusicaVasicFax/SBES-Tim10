using Manager;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ClientProxy : ChannelFactory<IFileManagerService>, IFileManagerService, IDisposable
    {
        private IFileManagerService factory;

        public ClientProxy(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            //string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            //this.Credentials.ClientCertificate.Certificate =
            //    CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);

            factory = this.CreateChannel();
        }

        public void AddFile(string fileName, byte[] signature, string text)
        {
            try
            {
                factory.AddFile(fileName, signature, text);
            }
            catch (FaultException<FileOperationsException> e)
            {
                Console.WriteLine($"Error while trying to add file: {fileName}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteFile(string fileName)
        {
            try
            {
                factory.DeleteFile(fileName);
            }
            catch (FaultException<FileOperationsException> e)
            {
                Console.WriteLine($"Error while trying to delete file: {fileName}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void EditFile(string fileName, byte[] signature, string text)
        {
            try
            {
                factory.EditFile(fileName, signature, text);
            }
            catch (FaultException<FileOperationsException> e)
            {
                Console.WriteLine($"Error while trying to edit file: {fileName}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Dispose()
        {
            if (factory != null)
            {
                factory = null;
            }

            this.Close();
        }
    }
}