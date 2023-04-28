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
        }
    }
}
