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
        void AddFile(string fileName,string text);

        [OperationContract]
        void EditFile(string fileName, string text);

        [OperationContract]
        void DeleteFile(string fileName);
    }
}
