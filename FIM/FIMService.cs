using Manager;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FIM
{
    public class FIMService : IFIMService
    {
        public static string path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())), "..\\Files\\"));

        public void Check()
        {
            X509Certificate2 certificate = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople,
               StoreLocation.LocalMachine, "client_sign");

            var sign = File.ReadLines(path+"file1.txt");
            

            byte[] signature = Convert.FromBase64String(sign.Last());

            if (DigitalSignature.Verify("Ovde treba fajl ili deo fajla da se salje", HashAlgorithms.SHA1, signature, certificate))//TODO kako doci do potpisa
            {
                Console.WriteLine("Valid signature");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Invalid signature");
                Console.ReadLine();
            }
           
        }
    }
}
