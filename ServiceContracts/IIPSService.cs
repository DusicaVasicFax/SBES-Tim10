using System;
using System.ServiceModel;

namespace ServiceContracts
{
    [ServiceContract]
    public interface IIPSService
    {
        [OperationContract]
        void Alarm(DateTime detectionTime, string path, string fileName);
    }
}