using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    [ServiceContract]
    public interface IFileAddAndModifyService
    {
        [OperationContract]
        [FaultContract(typeof(FileOperationsException))]
        void AddFile(string fileName, byte[] signature, string text);

        [OperationContract]
        [FaultContract(typeof(FileOperationsException))]
        void EditFile(string fileName, byte[] signature, string text);
    }
}