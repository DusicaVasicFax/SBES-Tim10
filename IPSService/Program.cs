using Manager;
using ServiceContracts;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;

namespace IPSService
{
    internal partial class Program
    {
        private static void Main(string[] args)
        {
            Console.ReadLine();
            string ipsCert = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);//autetntifikacija putem sertifikata
            //Console.log(ipsCert);

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;//autetntifikacija putem sertifikata

            string address = "net.tcp://localhost:8888/IIPSService";            

            ServiceHost host = new ServiceHost(typeof(IPSService));
            host.AddServiceEndpoint(typeof(IIPSService), binding, address);
            ///Custom validation mode enables creation of a custom validator - CustomCertificateValidator
            host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;//autetntifikacija putem sertifikata
            host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();//autetntifikacija putem sertifikata

            ///If CA doesn't have a CRL associated, WCF blocks every client because it cannot be validated
            host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            ///Set appropriate service's certificate on the host. Use CertManager class to obtain the certificate based on the "srvCertCN"
            host.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, ipsCert);//autetntifikacija putem sertifikata


            try
            {
                host.Open();
                Console.WriteLine("IPS service is started.\nPress <enter> to stop ...");
                IPSService iPSService = new IPSService();
                Console.ReadLine();
            }
            catch (Exception e)
            {
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