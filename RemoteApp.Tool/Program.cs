using System;
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
            Application.Run(new RemoteAppMainWindow());

            // Persist settings on the normal way out as well (VB used to do this for us).
            UserSettings.Instance.Save();
        }
    }
}
