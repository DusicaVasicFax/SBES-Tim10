using ServiceContracts;
using System;
using System.Security.Principal;
using System.ServiceModel;

namespace IPSService
{
    public class IPSService : IIPSService
    {
        public void CriticalLog(Alarm alarm)
        {
            Audit.CriticalLog(alarm);
            Console.WriteLine("Critical");
            NetTcpBinding binding = new NetTcpBinding();

            string address = "net.tcp://localhost:9999/FileManager";

            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

            Console.WriteLine("Korisnik koji je pokrenuo klijenta je : " + WindowsIdentity.GetCurrent().Name);

            EndpointAddress endpointAddress = new EndpointAddress(new Uri(address),
                EndpointIdentity.CreateUpnIdentity("filemanager"));

            FileManagerProxy proxy = new FileManagerProxy(binding, endpointAddress);
            if (proxy == null || proxy.State != CommunicationState.Opened)
            {
                proxy.Open();
            }
            proxy.DeleteFile(alarm.Filename);
            proxy.Close();
        }

        public void InformationLog(Alarm alarm)
        {
            Audit.InformationLog(alarm);
            Console.WriteLine("Information");
        }

        public void WarningLog(Alarm alarm)
        {
            Audit.WarningLog(alarm);
            Console.WriteLine("Warning");
        }
    }
}