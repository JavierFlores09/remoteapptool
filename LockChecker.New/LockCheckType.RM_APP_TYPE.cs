// code sample taken from:
// https://code.msdn.microsoft.com/windowsapps/How-to-know-the-process-170ed5f3/sourcecode?fileId=151114&pathId=1558127374
// Modified to suit the needs of this application

namespace LockChecker.New
{
    internal static partial class LockCheckType
    {
        /// <summary>
        /// Specifies the type of application that is described by
        /// the RM_PROCESS_INFO structure.
        /// </summary>
        internal enum RM_APP_TYPE
        {
            // The application cannot be classified as any other type. 
            RmUnknownApp = 0,
            // A Windows application run as a stand-alone process that 
            // displays a top-level window. 
            RmMainWindow = 1,
            // A Windows application that does not run as a stand-alone 
            // process and does not display a top-level window. 
            RmOtherWindow = 2,
            // The application is a Windows service. 
            RmService = 3,
            // The application is Windows Explorer. 
            RmExplorer = 4,
            // The application is a stand-alone console application. 
            RmConsole = 5,
            // A system restart is required to complete the installation because 
            // a process cannot be shut down. 
            RmCritical = 1000
        }
    }
}