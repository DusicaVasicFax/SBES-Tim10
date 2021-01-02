using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    //TODO maybe remove this since it's not hosted on the server
    [ServiceContract]
    public interface IFIMService
    {
        [OperationContract]
        Alarm Check(string filename);
    }
}