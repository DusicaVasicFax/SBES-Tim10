using System.Reflection;
using System.Resources;
using Resources;

namespace IPSService
{
    public enum AuditEventTypes
    {
        Critical = 0,
        Information = 1,
        Warning = 2
    }

    public class AuditEvents
    {
        private static ResourceManager resourceManager = null;
        private static object resourceLock = new object();

        private static ResourceManager ResourceMgr
        {
            get
            {
                lock (resourceLock)
                {
                    if (resourceManager == null)
                    {
                        Assembly localisationAssembly = Assembly.Load("Resources");
                        resourceManager = new ResourceManager
                            (typeof(CustomLoggerResource).ToString(),
                            localisationAssembly);
                    }
                    return resourceManager;
                }
            }
        }

        public static string Warning
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.Warning.ToString());
            }
        }

        public static string Critical
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.Critical.ToString());
            }
        }

        public static string Information
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.Information.ToString());
            }
        }
    }
}