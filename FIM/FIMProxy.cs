using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FIM
{
    public class FIMProxy : ChannelFactory<IFIMService>, IFIMService, IDisposable
    {
        private IFIMService factory;

        public FIMProxy(NetTcpBinding binding, string address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }
        public void Check()
        {
            throw new NotImplementedException();
        }
    }
}
