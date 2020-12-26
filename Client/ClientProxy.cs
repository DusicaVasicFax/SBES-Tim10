using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
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
            factory = this.CreateChannel();
        }

        public void AddFile(string fileName, string text)
        {
            try
            {
                factory.AddFile(fileName, text);
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

        public void EditFile(string fileName, string text)
        {
            try
            {
                factory.EditFile(fileName, text);
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