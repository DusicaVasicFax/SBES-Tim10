using Manager;
using Microsoft.Win32;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void Check(string filename)
        {
            X509Certificate2 certificate = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople,
               StoreLocation.LocalMachine, "client_sign");

            var sign = File.ReadLines(path + filename);

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
                DetermineAuditType(filename);
            }
        }

        private void DetermineAuditType(string filename)
        {
            switch (ReadEventLog(filename))
            {
                case 0:
                case 1: // INFORMATION
                    break;

                case 2: //  WARNING
                    break;

                default:
                    //CRITICAL
                    break;
            }
        }

        public int ReadEventLog(string filename)
        {
            EventLog log = new EventLog();
            log.Log = "SBES-PROJ-LOG";
            int cnt = 0;

            foreach (EventLogEntry entry in log.Entries)
            {
                int pFrom = entry.Message.IndexOf("[");
                int pTo = entry.Message.LastIndexOf("]");
                string msg = entry.Message.Substring(pFrom + 1, pTo - pFrom - 1);

                if (msg.Contains(filename))
                {
                    cnt++;
                }
            }
            return cnt;

            //log.Entries.Cast<EventLogEntry>().Where(x => x.Message.Contains(filename)).Count();
        }
    }
}