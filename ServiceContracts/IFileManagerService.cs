using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    [ServiceContract]
    public interface IFileManagerService
    {
        [OperationContract]
        [FaultContract(typeof(FileOperationsException))]
        void AddFile(string fileName, string text);

        [OperationContract]
        [FaultContract(typeof(FileOperationsException))]
        void EditFile(string fileName, string text);

        [OperationContract]
        [FaultContract(typeof(FileOperationsException))]
        void DeleteFile(string fileName);
    }
}