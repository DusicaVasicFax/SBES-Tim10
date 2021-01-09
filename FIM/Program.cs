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
        public static string pathConfig = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())), "FIMfiles.txt"));
        public static string fimConfig = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())), "FIMConfig.txt"));
        

        private static void Main(string[] args)
        {
            Console.ReadLine();
            if (!CheckIfConfigExists()) return;
            Console.WriteLine("FIM service is started.\n");

            Thread thread = new Thread(() => FimServiceFlow());
            thread.Start();
        }

        public static void FimServiceFlow()
        {
            /// Define the expected service certificate. It is required to establish cmmunication using certificates.
            string srvCertCN = "ips";

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            /// Use CertManager class to obtain the certificate based on the "srvCertCN" representing the expected service identity.
            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:8888/IIPSService"),
                                      new X509CertificateEndpointIdentity(srvCert));
            try
            {
                int number = 0;
                using (FIMClient proxy = new FIMClient(binding, address))
                {
                    while (true)
                    {
                        if (!CheckIfConfigExists())
                        {
                            Console.WriteLine("No files present! Exiting the program...");
                            Console.ReadLine();
                            Environment.Exit(42);
                        }


                        if (!Int32.TryParse(File.ReadAllText(fimConfig), out number))
                        {
                            Console.WriteLine("Config file not present! Exiting the program...");
                            Console.ReadLine();
                            Environment.Exit(42);
                        }
                    


                        Console.WriteLine("Validating files...");
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
                        Thread.Sleep(number);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong while starting FIM service");
                Console.WriteLine("[ERROR] {0}", e.Message);
                Console.WriteLine("[StackTrace] {0}", e.StackTrace);
            }
        }

        private static bool CheckIfConfigExists()
        {
            if (!File.Exists(pathConfig) || !File.Exists(fimConfig))
            {
                Console.WriteLine("Cannot start fim without proper configuration file!");
                Console.ReadLine();
                return false;
            }

            return true;
        }
    }
}