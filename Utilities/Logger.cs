using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace com.nmtinstaller.csi.Utilities
{
    public enum LogLevel : byte { Critical = 0, Error, Warning, Verbose }

    public class Logger
    {
        private static Logger _instance = new Logger();
        public static Logger GetInstance()
        {
            return _instance;
        }
        private StreamWriter logWriter = null;
        

        public void AddLogLine(LogLevel level, string message)
        {
            AddLogLine(level, message, null);
        }

        public void AddLogLine(LogLevel level, string message, Exception exception)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(DateTime.Now.ToString()+";"+level.ToString() + ";" + message);

            Exception current = exception;
            while (current != null)
            {
                strb.Append(";" + current.Message + ";" + current.StackTrace.Replace(Environment.NewLine," - "));
                current = current.InnerException;
            }

            logWriter.WriteLine(strb.ToString());
            logWriter.Flush();
        }

        private Logger()
        {
            logWriter = File.AppendText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "ErrorLog.csv");
        }
    }
}
