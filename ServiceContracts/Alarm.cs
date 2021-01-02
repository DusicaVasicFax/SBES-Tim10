using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public enum AuditEventTypes
    {
        Critical = 0,
        Information = 1,
        Warning = 2
    }

    public class Alarm
    {
        public DateTime TimeStamp { get; set; }
        public string Path { get; set; }

        public string Filename { get; set; }

        public AuditEventTypes Risk { get; set; }

        public Alarm(DateTime timeStamp, string path, AuditEventTypes risk, string filename)
        {
            TimeStamp = timeStamp;
            Path = path;
            Risk = risk;
            Filename = filename;
        }
    }
}