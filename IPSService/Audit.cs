using System;
using System.Diagnostics;

namespace IPSService
{
    public class Audit : IDisposable
    {
        private static EventLog customLog = null;
        private const string SourceName = "SBES-PROJ";
        private const string LogName = "SBES-PROJ-LOG";

        static Audit()
        {
            try
            {
                if (!EventLog.SourceExists(SourceName))
                    EventLog.CreateEventSource(SourceName, LogName);
                customLog = new EventLog(LogName, Environment.MachineName, SourceName);
            }
            catch (Exception e)
            {
                customLog = null;
                Console.WriteLine($"Error while trying to create log handle. Error = {e.Message}");
            }
        }

        public static void CriticalLog(DateTime time, string path, string fileName)
        {
            if (customLog != null)
            {
                string critical = AuditEvents.Critical;
                string message = String.Format(critical, time.ToString(), fileName, path);
                customLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.Critical));
            }
        }

        public static void InformationLog(DateTime time, string path, string fileName)
        {
            if (customLog != null)
            {
                string information = AuditEvents.Information;
                string message = String.Format(information, time.ToString(), fileName, path);
                customLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.Information));
            }
        }

        public static void WarningLog(DateTime time, string path, string fileName)
        {
            if (customLog != null)
            {
                string warning = AuditEvents.Warning;
                string message = String.Format(warning, time.ToString(), fileName, path);
                customLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.Warning));
            }
        }

        public void Dispose()
        {
            if (customLog != null)
            {
                customLog.Dispose();
                customLog = null;
            }
        }
    }
}