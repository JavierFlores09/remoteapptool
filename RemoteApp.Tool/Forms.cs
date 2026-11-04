using System;
using System.Windows.Forms;

namespace RemoteApp.Tool
{
    // Substitutes the VB.NET My.Forms namespace. VB recreated a form when the cached instance had
    // been disposed; without that, closing a window that was never shown (Close() disposes a
    // non-modal form) left a dead instance in here and the next ShowDialog threw
    // ObjectDisposedException. Every accessor goes through Reuse() now.
    internal static class Forms
    {
        private static RemoteAppCreateClientConnection _remoteAppCreateClientConnection;
        private static RemoteAppIconPicker _remoteAppIconPicker;
        private static RemoteAppEditWindow _remoteAppEditWindow;
        private static RemoteAppMainWindow _remoteAppMainWindow;
        private static RemoteAppFileTypeAssociation _remoteAppFileTypeAssociation;
        private static RemoteAppHostOptions _remoteAppHostOptions;
        private static RemoteAppAboutWindow _remoteAppAboutWindow;
        private static RDPOptionsWindow _rdpOptionsWindow;

        /// <summary>
        ///     Returns the cached window, or a fresh one when there is none or it was disposed.
        /// </summary>
        private static T Reuse<T>(ref T window, Func<T> create) where T : Form
        {
            if (window == null || window.IsDisposed) window = create();
            return window;
        }

        internal static RDPOptionsWindow RDPOptionsWindow =>
            Reuse(ref _rdpOptionsWindow, () => new RDPOptionsWindow());

        internal static RemoteAppAboutWindow RemoteAppAboutWindow =>
            Reuse(ref _remoteAppAboutWindow, () => new RemoteAppAboutWindow());

        internal static RemoteAppHostOptions RemoteAppHostOptions =>
            Reuse(ref _remoteAppHostOptions, () => new RemoteAppHostOptions());

        internal static RemoteAppFileTypeAssociation RemoteAppFileTypeAssociation =>
            Reuse(ref _remoteAppFileTypeAssociation, () => new RemoteAppFileTypeAssociation());

        internal static RemoteAppMainWindow RemoteAppMainWindow =>
            Reuse(ref _remoteAppMainWindow, () => new RemoteAppMainWindow());

        internal static RemoteAppEditWindow RemoteAppEditWindow =>
            Reuse(ref _remoteAppEditWindow, () => new RemoteAppEditWindow());

        internal static RemoteAppIconPicker RemoteAppIconPicker =>
            Reuse(ref _remoteAppIconPicker, () => new RemoteAppIconPicker());

        internal static RemoteAppCreateClientConnection RemoteAppCreateClientConnection =>
            Reuse(ref _remoteAppCreateClientConnection, () => new RemoteAppCreateClientConnection());
    }
}
