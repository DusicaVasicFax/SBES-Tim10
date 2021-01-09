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

        public Alarm Check(string filename)
        {
            X509Certificate2 certificate = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople,
               StoreLocation.LocalMachine, "client_sign");

            var sign = File.ReadLines(path + filename);
           
            try
            {
                byte[] signature = Convert.FromBase64String(sign.Last());
                if (!DigitalSignature.Verify(sign.First(), HashAlgorithms.SHA1, signature, certificate))
                {
                    Console.WriteLine("Invalid signature");
                    return DetermineAuditType(filename);
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid signature" + e.Message);
                return DetermineAuditType(filename);
            }
        }

        private Alarm DetermineAuditType(string filename)
        {
            switch (ReadEventLog(filename))
            {
                case -1:
                    return null;

                case 0:
                    return new Alarm(DateTime.Now, path, AuditEventTypes.Information, filename);

                case 1:
                    return new Alarm(DateTime.Now, path, AuditEventTypes.Warning, filename);

                default:
                    return new Alarm(DateTime.Now, path, AuditEventTypes.Critical, filename);
            }
        }

        public int ReadEventLog(string filename)
        {
            try
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
            }
            catch (Exception e)
            {
                Console.WriteLine($"Reading event log failed {e.Message}");
                return -1;
            }
        }
    }
}