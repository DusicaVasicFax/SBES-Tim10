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
                Console.WriteLine("Connected to Services");
                byte[] signature;

                while (true)
                {
                    Console.WriteLine("*****************Menu*****************");
                    Console.WriteLine("1. Doda fajl\n2. Obrisi fajl\n3. Azuriraj fajl\n4.Exit");
                    Console.Write("Choose option: ");
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Unesite ime fajla u kom zelite da unesete tekst");
                            string filename = Console.ReadLine();
                            Console.WriteLine("Unesite tekst koji zelite da upisete u fajl koji ste uneli");
                            string text = Console.ReadLine();
                            signature = DigitalSignature.Create(text, HashAlgorithms.SHA1, clientCERT);
                            proxy.AddFile(filename, signature, text);
                            break;

                        case 2:
                            Console.WriteLine("Unesite ime fajla koje zelite da obrisite");
                            filename = Console.ReadLine();
                            proxy.DeleteFile(filename);
                            break;

                        case 3:
                            Console.WriteLine("Unesite ime fajla u kom zelite da promenite tekst");
                            filename = Console.ReadLine();
                            Console.WriteLine("Unesite tekst koji zelite da izmenite");
                            text = Console.ReadLine();
                            signature = DigitalSignature.Create(text, HashAlgorithms.SHA1, clientCERT);
                            proxy.EditFile(filename, signature, text);
                            break;

                        case 4:
                            Console.WriteLine("CAO");
                            Console.ReadLine();
                            return;

                        default:
                            Console.WriteLine("Niste uneli ispravnu opciju.");
                            break;
                    }
                }
            }
        }
    }
}