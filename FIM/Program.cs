using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIM
{
    class Program
    {
        static void Main(string[] args)
        {
            FIMService fimservice = new FIMService();
            fimservice.Check();

        }
    }
}
