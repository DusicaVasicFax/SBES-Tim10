using ServiceContracts;
using System;

namespace IPSService
{
    public class IPSService : IIPSService
    {
        public void CriticalLog(Alarm alarm)
        {
            Audit.CriticalLog(alarm);
            //TODO delete the file
        }

        public void InformationLog(Alarm alarm)
        {
            Audit.InformationLog(alarm);
        }

        public void WarningLog(Alarm alarm)
        {
            Audit.WarningLog(alarm);
        }
    }
}