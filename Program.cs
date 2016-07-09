using System;
using System.Collections.Generic;
using System.Windows.Forms;
using com.nmtinstaller.csi.Forms;
using com.nmtinstaller.csi.Utilities;
using System.IO;
using System.Reflection;
using com.nmtinstaller.csi.Properties;
using System.Threading;

namespace TransmissionApplicationUploader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main()
        {
            switch (Settings.Default.UILanguage)
            {
                case "English":
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                    break;
                case "Русский":
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru");
                    break;
                case "Nederlands":
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("nl");
                    break;
                case "Türkçe":
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("tr");
                    break;
                case "Français":
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr");
                    break;
            }

            //delete old files
            foreach (string file in Directory.GetFiles(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "*.old", SearchOption.AllDirectories))
            {
                try
                {
                    File.Delete(file);
                }
                catch { }
            }

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmInstaller());

            return 0;
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.GetInstance().AddLogLine(LogLevel.Error, "An uncaught thread exception was detected, skipping", (Exception)e.ExceptionObject);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Logger.GetInstance().AddLogLine(LogLevel.Error, "An uncaught thread exception was detected, skipping", e.Exception);
        }
    }
}
