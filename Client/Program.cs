using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        private static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/FileManager";

            using (ClientProxy proxy = new ClientProxy(binding, new EndpointAddress(new Uri(address))))
            {
                proxy.AddFile("file1.txt", "First file");
                proxy.EditFile("file2.txt", "First file again");
                //proxy.DeleteFile("file1.txt");
            }

            Console.ReadLine();
        }
    }
}