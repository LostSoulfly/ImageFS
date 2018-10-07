using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageFS.Utilities
{
    public static class Logger
    {

        public static string logFile = "log.txt";
        public static bool loggingEnabled = true;

        public enum LOG_LEVEL
        {
            NONE = 0,
            ERR,
            INF,
            DBG,
            VRB
        }

        public static void Log(string text = "", LOG_LEVEL logLevel = LOG_LEVEL.INF)
        {
            string logName = Enum.GetName(typeof(LOG_LEVEL), logLevel);
            Console.WriteLine($"[{logName}] {text}");
        }

        public static void WriteToLog(string file, string text)
        {
            try
            {
                File.WriteAllText(file, text + Environment.NewLine);
            }
            catch (Exception ex) { Logger.Log("Unable to write log: " + ex.Message, LOG_LEVEL.ERR); }
         }
    }
}
