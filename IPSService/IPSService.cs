using ServiceContracts;
using System;

namespace IPSService
{
    public class IPSService : IIPSService
    {
        public void CriticalLog(Alarm alarm)
        {
            Audit.CriticalLog(alarm);
            Console.WriteLine("Critical");
            //TODO delete the file
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