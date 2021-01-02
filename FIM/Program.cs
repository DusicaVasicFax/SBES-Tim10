using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FIM
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Thread thread = new Thread(FimService);
            thread.Start();
            //TODO enter the number N that will put the thread to sleep
        }

        private static void FimService()
        {
            while (true)
            {
                Console.WriteLine("Sleep now");

                FIMService fimservice = new FIMService();
                fimservice.Check();
                Thread.Sleep(2000);
            }
        }
    }
}