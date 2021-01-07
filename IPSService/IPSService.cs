using ServiceContracts;
using System;
using System.Security.Principal;
using System.ServiceModel;

namespace IPSService
{
    public class IPSService : IIPSService, IDisposable
    {
        private FileManagerProxy proxy;

        public IPSService()
        {
            NetTcpBinding binding = new NetTcpBinding();

            string address = "net.tcp://localhost:9999/FileManager";

            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

            EndpointAddress endpointAddress = new EndpointAddress(new Uri(address),
                EndpointIdentity.CreateUpnIdentity("filemanager"));

            this.proxy = new FileManagerProxy(binding, endpointAddress);
            if (this.proxy == null || this.proxy.State != CommunicationState.Opened)
            {
                this.proxy.Open();
            }
        }

        public void CriticalLog(Alarm alarm)
        {
            Console.WriteLine("Critical");
            Audit.CriticalLog(alarm);
            this.proxy.DeleteFile(alarm.Filename);
        }

        public void Dispose()
        {
            if (this.proxy != null)
            {
                if (this.proxy.State != CommunicationState.Closed)
                    this.proxy.Close();
                this.proxy = null;
            }
        }

        public void InformationLog(Alarm alarm)
        {
            Console.WriteLine("Information");
            Audit.InformationLog(alarm);
        }

        public void WarningLog(Alarm alarm)
        {
            Console.WriteLine("Warning");
            Audit.WarningLog(alarm);
        }
    }
}