using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FIM
{
    internal class Program
    {
        public static string pathConfig = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())), "FIMConfig.txt"));

        private static void Main(string[] args)
        {
            if (!CheckIfConfigExists()) return;
            Thread thread = new Thread(FimService);
            //thread.IsBackground = true;
            thread.Start();
            //TODO enter the number N that will put the thread to sleep
        }

        private static void FimService()
        {
            while (true)
            {
                if (!CheckIfConfigExists()) Environment.Exit(42);

                Console.WriteLine("Sleep now");
                List<string> fileText = File.ReadAllText(pathConfig).Split('\n').Select(x => x.Replace("\r", string.Empty)).Where(x => !String.IsNullOrWhiteSpace(x)).ToList();

                FIMService fimservice = new FIMService();
                //TODO uncomment later
                //fimservice.Check("path");
                Thread.Sleep(2000);
            }
        }

        private static bool CheckIfConfigExists()
        {
            if (!File.Exists(pathConfig))
            {
                Console.WriteLine("Cannot start fim without proper configuration file!");
                Console.ReadLine();
                return false;
            }
            return true;
        }
    }
}