using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace LockChecker
{
    internal static class Interop
    {
        /// <summary>
        /// Registers resources to a Restart Manager session. The Restart Manager uses
        /// the list of resources registered with the session to determine which
        /// applications and services must be shut down and restarted. Resources can be
        /// identified by filenames, service short names, or RM_UNIQUE_PROCESS structures
        /// that describe running applications.
        /// </summary>
        /// <param name="pSessionHandle">
        /// A handle to an existing Restart Manager session.
        /// </param>
        /// <param name="nFiles">The number of files being registered</param>
        /// <param name="rgsFilenames">
        /// An array of null-terminated strings of full filename paths.
        /// </param>
        /// <param name="nApplications">The number of processes being registered</param>
        /// <param name="rgApplications">An array of RM_UNIQUE_PROCESS structures</param>
        /// <param name="nServices">The number of services to be registered</param>
        /// <param name="rgsServiceNames">
        /// An array of null-terminated strings of service short names.
        /// </param>
        /// <returns>The function can return one of the system error codes that
        /// are defined in Winerror.h
        /// </returns>
        [DllImport("rstrtmgr.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int RmRegisterResources(uint pSessionHandle, uint nFiles, string[] rgsFilenames, uint nApplications, [In] LockCheckType.RmUniqueProcess[] rgApplications, uint nServices, string[] rgsServiceNames);

        /// <summary>
        /// Starts a new Restart Manager session. A maximum of 64 Restart Manager
        /// sessions per user session can be open on the system at the same time.
        /// When this function starts a session, it returns a session handle and
        /// session key that can be used in subsequent calls to the Restart Manager API.
        /// </summary>
        /// <param name="pSessionHandle">
        /// A pointer to the handle of a Restart Manager session.
        /// </param>
        /// <param name="dwSessionFlags">Reserved. This parameter should be 0.</param>
        /// <param name="strSessionKey">
        /// A null-terminated string that contains the session key to the new session.
        /// </param>
        /// <returns></returns>
        [DllImport("rstrtmgr.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int RmStartSession(ref uint pSessionHandle, int dwSessionFlags, string strSessionKey);

        /// <summary>
        /// Ends the Restart Manager session. This function should be called by the
        /// primary installer that has previously started the session by calling the
        /// RmStartSession function. The RmEndSession function can be called by a
        /// secondary installer that is joined to the session once no more resources
        /// need to be registered by the secondary installer.
        /// </summary>
        /// <param name="pSessionHandle">
        /// A handle to an existing Restart Manager session.
        /// </param>
        /// <returns>
        /// The function can return one of the system error codes
        /// that are defined in Winerror.h.
        /// </returns>
        [DllImport("rstrtmgr.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int RmEndSession(uint pSessionHandle);

        /// <summary>
        /// Gets a list of all applications and services that are currently using
        /// resources that have been registered with the Restart Manager session.
        /// </summary>
        /// <param name="dwSessionHandle">
        /// A handle to an existing Restart Manager session.
        /// </param>
        /// <param name="pnProcInfoNeeded">A pointer to an array size necessary to
        /// receive RM_PROCESS_INFO structures required to return information for
        /// all affected applications and services.
        /// </param>
        /// <param name="pnProcInfo">
        /// A pointer to the total number of RM_PROCESS_INFO structures in an array
        /// and number of structures filled.
        /// </param>
        /// <param name="rgAffectedApps">
        /// An array of RM_PROCESS_INFO structures that list the applications and
        /// services using resources that have been registered with the session.
        /// </param>
        /// <param name="lpdwRebootReasons">
        /// Pointer to location that receives a value of the RM_REBOOT_REASON
        /// enumeration that describes the reason a system restart is needed.
        /// </param>
        /// <returns></returns>
        [DllImport("rstrtmgr.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int RmGetList(uint dwSessionHandle, ref uint pnProcInfoNeeded, ref uint pnProcInfo, [In] LockCheckType.RmProcessInfo[] rgAffectedApps, ref uint lpdwRebootReasons);
    }
}
