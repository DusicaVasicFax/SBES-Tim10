using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/FileManager";

            ServiceHost host = new ServiceHost(typeof(FileManager));
            host.AddServiceEndpoint(typeof(IFileAddAndModifyService), binding, address);
            host.AddServiceEndpoint(typeof(IFileDeleteService), binding, address);

            try
            {
                host.Open();
                Console.WriteLine("FileManagerService is opened. Press <enter> to finish...");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong while starting FileManager service");
                Console.WriteLine("[ERROR] {0}", e.Message);
                Console.WriteLine("[StackTrace] {0}", e.StackTrace);
            }
            finally
            {
                host.Close();
            }
        }
    }
}