// code sample taken from:
// https://code.msdn.microsoft.com/windowsapps/How-to-know-the-process-170ed5f3/sourcecode?fileId=151114&pathId=1558127374
// Modified to suit the needs of this application
using System.Runtime.InteropServices;

namespace LockChecker
{
    internal static partial class LockCheckType
    {
        /// <summary>
        /// Uniquely identifies a process by its PID and the time the process began.
        /// An array of RM_UNIQUE_PROCESS structures can be passed
        /// to the RmRegisterResources function.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct RmUniqueProcess
        {
            // The product identifier (PID). 
            public int dwProcessId;
            // The creation time of the process. 
            public System.Runtime.InteropServices.ComTypes.FILETIME ProcessStartTime;
        }
    }
}