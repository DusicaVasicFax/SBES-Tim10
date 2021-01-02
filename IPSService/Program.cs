using System;
using System.Security.Principal;
using System.ServiceModel;

namespace IPSService
{
    internal partial class Program
    {
        private static void Main(string[] args)
        {
            //NetTcpBinding binding = new NetTcpBinding();
            //string address = "net.tcp://localhost:9999/SecurityService";

            //binding.Security.Mode = SecurityMode.Transport;
            //binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            //binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

            //Console.WriteLine("Korisnik koji je pokrenuo klijenta je : " + WindowsIdentity.GetCurrent().Name);

            //EndpointAddress endpointAddress = new EndpointAddress(new Uri(address),
            //    EndpointIdentity.CreateUpnIdentity("wcfServer"));

            //using (FileManagerProxy proxy = new FileManagerProxy(binding, endpointAddress))
            //{
            //}
            Audit.CriticalLog(DateTime.Now, "some path", "some filename");

            Console.ReadLine();
        }
    }
}