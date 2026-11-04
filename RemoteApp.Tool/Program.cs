using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace RemoteApp.Tool
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            AdminRelauncher.RelaunchIfNotAdmin();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Without this a fault inside a form's Load handler dumps a raw .NET exception
            // ("Valor no establecido...") and leaves the window half built. Report it instead.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, args) => ReportError(args.Exception);
            AppDomain.CurrentDomain.UnhandledException += (sender, args) => ReportError(args.ExceptionObject as Exception);

            Application.Run(new RemoteAppMainWindow());

            // Persist settings on the normal way out as well (VB used to do this for us).
            UserSettings.Instance.Save();
        }

        /// <summary>
        ///     Writes the exception to %APPDATA%\RemoteApp Tool\error.log and shows a short message.
        /// </summary>
        private static void ReportError(Exception exception)
        {
            if (exception == null) return;

            var logPath = string.Empty;
            try
            {
                var directory = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RemoteApp Tool");
                Directory.CreateDirectory(directory);
                logPath = Path.Combine(directory, "error.log");
                var entry = new StringBuilder()
                    .AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {exception}")
                    .AppendLine();
                File.AppendAllText(logPath, entry.ToString(), Encoding.UTF8);
            }
            catch (Exception)
            {
                // Reporting must never throw.
            }

            var detail = string.IsNullOrEmpty(logPath)
                ? exception.Message
                : exception.Message + Environment.NewLine + Environment.NewLine + "Details were written to:" +
                  Environment.NewLine + logPath;

            MessageBox.Show(detail, "RemoteApp Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
