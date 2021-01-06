using Manager;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FIM
{
    internal class Program
    {
        public static string pathConfig = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())), "FIMConfig.txt"));

        private static void Main(string[] args)
        {
            Console.ReadLine();
            if (!CheckIfConfigExists()) return;
            Console.WriteLine("FIM service is started.\n");

            int number = -1;
            do
            {
                Console.WriteLine("Enter the number N used for the thread sleep function");
                if (!int.TryParse(Console.ReadLine(), out number))
                {
                    number = -1;
                }
            }
            while (number < 0);

            Thread thread = new Thread(() => FimServiceFlow(number));
            thread.Start();
        }

        public static void FimServiceFlow(int n)
        {
            /// Define the expected service certificate. It is required to establish cmmunication using certificates.
            string srvCertCN = "ips";

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            /// Use CertManager class to obtain the certificate based on the "srvCertCN" representing the expected service identity.
            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:8888/IIPSService"),
                                      new X509CertificateEndpointIdentity(srvCert));

            using (FIMClient proxy = new FIMClient(binding, address))
            {
                while (true)
                {
                    if (!CheckIfConfigExists())
                    {
                        Console.WriteLine("Config file not present! Exiting the program...");
                        Console.ReadLine();
                        Environment.Exit(42);
                    }
                    Console.WriteLine("Validation files...");
                    List<string> filenames = File.ReadAllText(pathConfig).Split('\n').Select(x => x.Replace("\r", string.Empty)).Where(x => !String.IsNullOrWhiteSpace(x)).ToList();

                    FIMService service = new FIMService();

                    filenames.ForEach(currentFilename =>
                    {
                        Alarm alarm = service.Check(currentFilename);
                        if (alarm != null)
                        {
                            switch (alarm.Risk)
                            {
                                case AuditEventTypes.Critical:
                                    proxy.CriticalLog(alarm);
                                    break;

                                case AuditEventTypes.Information:
                                    proxy.InformationLog(alarm);
                                    break;

                                case AuditEventTypes.Warning:
                                    proxy.WarningLog(alarm);
                                    break;
                            }
                        }
                    });
                    Thread.Sleep(n);
                }
            }
        }

        private static bool CheckIfConfigExists()
        {
            if (!File.Exists(pathConfig))
            {
                Console.WriteLine("Cannot start fim without proper configuration file!");
                Console.ReadLine();
                return false;
            }
            return true;
        }
    }
}