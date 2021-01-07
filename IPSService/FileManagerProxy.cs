using ServiceContracts;
using System;
using System.ServiceModel;

namespace IPSService
{
    public class FileManagerProxy : ChannelFactory<IFileDeleteService>, IFileDeleteService, IDisposable
    {
        private IFileDeleteService factory;

        public FileManagerProxy(NetTcpBinding binding, string address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public FileManagerProxy(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public void DeleteFile(string fileName)
        {
            try
            {
                factory.DeleteFile(fileName);
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
    }
}