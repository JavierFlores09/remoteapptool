// code sample taken from:
// https://code.msdn.microsoft.com/windowsapps/How-to-know-the-process-170ed5f3/sourcecode?fileId=151114&pathId=1558127374
// Modified to suit the needs of this application
using System.Runtime.InteropServices;

namespace LockChecker
{
    internal static partial class LockCheckType
    {
        /// <summary>
        /// Describes an application that is to be registered with the Restart Manager.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct RmProcessInfo
        {
            // Contains an RM_UNIQUE_PROCESS structure that uniquely identifies the 
            // application by its PID and the time the process began. 
            internal RmUniqueProcess Process;
            // If the process is a service, this parameter returns the  
            // long name for the service. 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CchRmMaxAppName + 1)]
            internal string strAppName;
            // If the process is a service, this is the short name for the service. 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CchRmMaxSvcName + 1)]
            internal string strServiceShortName;
            // Contains an RM_APP_TYPE enumeration value. 
            internal RmAppType ApplicationType;
            // Contains a bit mask that describes the current status of the application. 
            internal uint AppStatus;
            // Contains the Terminal Services session ID of the process. 
            internal uint TSSessionId;
            // TRUE if the application can be restarted by the  
            // Restart Manager; otherwise, FALSE. 
            [MarshalAs(UnmanagedType.Bool)]
            internal bool bRestartable;
        }
    }
}