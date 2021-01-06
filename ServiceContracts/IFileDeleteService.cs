using System.ServiceModel;

namespace ServiceContracts
{
    [ServiceContract]
    public interface IFileDeleteService
    {
        [OperationContract]
        [FaultContract(typeof(FileOperationsException))]
        void DeleteFile(string fileName);
    }
}