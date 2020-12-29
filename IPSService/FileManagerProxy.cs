using ServiceContracts;
using System;
using System.ServiceModel;

namespace IPSService
{
    internal partial class Program
    {
        public class FileManagerProxy : ChannelFactory<IFileManagerService>, IFileManagerService, IDisposable
        {
            private IFileManagerService factory;

            public FileManagerProxy(NetTcpBinding binding, string address) : base(binding, address)
            {
                factory = this.CreateChannel();
            }

            public FileManagerProxy(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
            {
                factory = this.CreateChannel();
            }

            public void AddFile(string fileName, byte[] signature, string text)
            {
                throw new NotImplementedException();
            }

            public void DeleteFile(string fileName)
            {
                try
                {
                    factory.DeleteFile(fileName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error deleting file");
                }
            }

            public void EditFile(string fileName, byte[] signature, string text)
            {
                throw new NotImplementedException();
            }
        }
    }
}