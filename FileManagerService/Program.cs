using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerService
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/FileManager";

            // da se uspostavi veza izmedju klijenta i servera
            ServiceHost host = new ServiceHost(typeof(FileManager));
            host.AddServiceEndpoint(typeof(IFileManagerService), binding, address);

           // host.Authorization.ServiceAuthorizationManager = new AuthorizationManager();

            host.Open();
            Console.WriteLine("WCFService is opened. Press <enter> to finish...");
            Console.ReadLine();

            host.Close();

        }
    }
}
