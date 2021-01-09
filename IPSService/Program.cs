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
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;//autetntifikacija putem sertifikata

            string address = "net.tcp://localhost:8888/IIPSService";

            ServiceHost host = new ServiceHost(typeof(IPSService));
            host.AddServiceEndpoint(typeof(IIPSService), binding, address);

            host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;//autetntifikacija putem sertifikata
            host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();//autetntifikacija putem sertifikata

            host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            host.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, ipsCert);//autetntifikacija putem sertifikata
            IPSService iPSService = null;

            try
            {
                host.Open();
                Console.WriteLine("IPS service is started.\nPress <enter> to stop ...");
                iPSService = new IPSService();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong while starting IPS service");
                Console.WriteLine("[ERROR] {0}", e.Message);
                Console.WriteLine("[StackTrace] {0}", e.StackTrace);
            }
            finally
            {
                iPSService.Dispose();
                host.Close();
            }
        }
    }
}