using NLog;

namespace WorkData.Util
{
    public class LoggerHelper
    {
        public static volatile ILogger BusinessLog = LogManager.GetLogger("businessLog");
        public static volatile ILogger SystemLog = LogManager.GetLogger("systemLog");

    }
}
