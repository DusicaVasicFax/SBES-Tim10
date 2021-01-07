using Manager;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FIM
{
    public class FIMClient : ChannelFactory<IIPSService>, IIPSService, IDisposable
    {
        private IIPSService factory;

        public FIMClient(NetTcpBinding binding, EndpointAddress address)
            : base(binding, address)
        {
            string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);

            factory = this.CreateChannel();
        }

        public void CriticalLog(Alarm alarm)
        {
            try
            {
                factory.CriticalLog(alarm);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong with the critical log {e.Message}");
            }
        }

        public void InformationLog(Alarm alarm)
        {
            try
            {
                factory.InformationLog(alarm);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong with the information log {e.Message}");
            }
        }

        public void WarningLog(Alarm alarm)
        {
            try
            {
                factory.WarningLog(alarm);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong with the warning log {e.Message}");
            }
        }

        public void Dispose()
        {
            if (factory != null)
            {
                factory = null;
            }

            this.Close();
        }
    }
}