// code sample taken from:
// https://code.msdn.microsoft.com/windowsapps/How-to-know-the-process-170ed5f3/sourcecode?fileId=151114&pathId=1558127374
// Modified to suit the needs of this application
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace LockChecker
{
    internal static partial class LockCheckType
    {
        // maximum character count of application friendly name. 
        private const int CchRmMaxAppName = 255;
        // maximum character count of service short name. 
        private const int CchRmMaxSvcName = 63;
        // A system restart is not required. 
        private const int RmRebootReasonNone = 0;

        public static string LockCheck(string filename)
        {
            var handle = default(uint);
            var sessionkey = Guid.NewGuid().ToString();
            var result = new System.Text.StringBuilder();
            var res = Interop.RmStartSession(ref handle, 0, sessionkey);
            if (res != 0)
            {
                throw new Exception("Could not begin restart session.");
            }

            try
            {
                var pnProcInfoNeeded = 0U;
                var pnProcInfo = 100U;
                uint lpdwRebootReasons = RmRebootReasonNone;
                var resources = new string[] { filename };

                // Create an array to store the process results. 
                var processInfo = new RmProcessInfo[(int)(pnProcInfo - 1L + 1)];
                res = Interop.RmRegisterResources(handle, (uint)resources.Length, resources, 0U, null, 0U, null);
                if (res != 0)
                {
                    throw new Exception("Could not register resource.");
                }

                res = Interop.RmGetList(handle, ref pnProcInfoNeeded, ref pnProcInfo, processInfo, ref lpdwRebootReasons);
                if (res == 0)
                {
                    // The function completed successfully. 

                    if (pnProcInfo != 0L)
                    {
                        for (int i = 0, loopTo = (int)(pnProcInfo - 1L); i <= loopTo; i++)
                        {
                            // Console.WriteLine("File Name :" + resources(0))
                            result.Append("File Name :").AppendLine(resources[0]);
                            // Console.WriteLine("Application locking the file :" + processInfo(i).strAppName)
                            result.Append("Application locking the file : ").AppendLine(processInfo[i].strAppName);
                            result.Append("PID of process locking the file: ").AppendLine(processInfo[i].Process.dwProcessId.ToString());
                        }
                    }
                    else
                    {
                        // Console.WriteLine("The specified file '{0}' is not locked by any process", resources(0))
                        result.AppendLine("No locks");
                    }
                }
                else
                {
                    throw new Exception("Could not list processes locking resource.");
                }

                if (res != 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            catch (Exception exception)
            {
                // Console.WriteLine(exception.Message)
                result.AppendLine(exception.Message);
            }
            finally
            {
                Interop.RmEndSession(handle);
            }

            return result.ToString();
        }
    }
}