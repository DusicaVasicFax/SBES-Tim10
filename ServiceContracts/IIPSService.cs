using System;
using System.ServiceModel;

namespace ServiceContracts
{
    [ServiceContract]
    public interface IIPSService
    {
        [OperationContract]
        void CriticalLog(Alarm alarm);

        [OperationContract]
        void InformationLog(Alarm alarm);

        [OperationContract]
        void WarningLog(Alarm alarm);
    }
}