using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;

namespace RemoteApp.Tool.New
{
    internal static class AdminRelauncher
    {
        public static void RelaunchIfNotAdmin()
        {
            if (RunningAsAdmin()) return;

            Console.WriteLine("Running as admin required!");
            var proc = new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = Assembly.GetEntryAssembly().CodeBase,
                Verb = "runas"
            };
            try
            {
                Process.Start(proc);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"This program must be run as an administrator!{Environment.NewLine}{ex}");
                Environment.Exit(0);
            }
        }

        private static bool RunningAsAdmin()
        {
            var id = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(id);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
