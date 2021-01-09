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
    public class ClientProxy : ChannelFactory<IFileAddAndModifyService>, IFileAddAndModifyService, IDisposable
    {
        private IFileAddAndModifyService factory;

        public ClientProxy(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public void AddFile(string fileName, byte[] signature, string text)
        {
            try
            {
                factory.AddFile(fileName, signature, text);
                Console.WriteLine("Successfully added file");
            }
            catch (FaultException<FileOperationsException> e)
            {
                Console.WriteLine($"{e.Detail.Message}");
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
                Console.WriteLine("Successfully edited file");
            }
            catch (FaultException<FileOperationsException> e)
            {
                Console.WriteLine($"{e.Detail.Message}");
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