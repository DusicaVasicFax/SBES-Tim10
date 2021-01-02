using Manager;
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
    public class Program
    {
        private static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/FileManager";

            //string clientCert = WindowsIdentity.GetCurrent().Name;

            //sertifikat za potpisivanje client_sign
            string signCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name.ToLower()) + "_sign";

            Console.ReadLine();

            using (ClientProxy proxy = new ClientProxy(binding, new EndpointAddress(new Uri(address))))
            {
                X509Certificate2 clientCERT = CertManager.GetCertificateFromStorage(StoreName.My,
                StoreLocation.LocalMachine, signCertCN);

                byte[] signature = DigitalSignature.Create("Ovde treba fajl ili deo fajla da se salje", HashAlgorithms.SHA1, clientCERT);
                //proxy.AddFile("file5.txt", signature, "First file");
                ////  proxy.EditFile("file1.txt", signature,"First file again");
                //proxy.DeleteFile("file5.txt");
            }

            Console.ReadLine();
        }
    }
}