using ServiceContracts;
using System;

namespace IPSService
{
    public class IPSService : IIPSService
    {
        public void Alarm(DateTime detectionTime, string path, string fileName)
        {
            //TODO read log and get a list of fileNames
            //TODO count the number of times this fileName has been repeadet

            /*if (FileName.count == 0)
             *      log.critical
             * else if (fileName.count == 1)
             *      log.Warning
             * else {
             *       log.Critical
             *       contant fileManagerService to delete the file
             * }
             *
             *
             *
             */
        }
    }
}