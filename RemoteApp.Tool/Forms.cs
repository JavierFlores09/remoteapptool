namespace RemoteApp.Tool
{
    // TODO: This is a workaround that substitutes My folder in VB.NET. These should not be singletons.
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

        internal static RDPOptionsWindow RDPOptionsWindow =>
            _rdpOptionsWindow ?? (_rdpOptionsWindow = new RDPOptionsWindow());

        internal static RemoteAppAboutWindow RemoteAppAboutWindow =>
            _remoteAppAboutWindow ?? (_remoteAppAboutWindow = new RemoteAppAboutWindow());

        internal static RemoteAppHostOptions RemoteAppHostOptions =>
            _remoteAppHostOptions ?? (_remoteAppHostOptions = new RemoteAppHostOptions());
        internal static RemoteAppFileTypeAssociation RemoteAppFileTypeAssociation =>
            _remoteAppFileTypeAssociation ?? (_remoteAppFileTypeAssociation = new RemoteAppFileTypeAssociation());
        internal static RemoteAppMainWindow RemoteAppMainWindow =>
            _remoteAppMainWindow ?? (_remoteAppMainWindow = new RemoteAppMainWindow());
        internal static RemoteAppEditWindow RemoteAppEditWindow =>
            _remoteAppEditWindow ?? (_remoteAppEditWindow = new RemoteAppEditWindow());
        internal static RemoteAppIconPicker RemoteAppIconPicker =>
            _remoteAppIconPicker ?? (_remoteAppIconPicker = new RemoteAppIconPicker());
        internal static RemoteAppCreateClientConnection RemoteAppCreateClientConnection =>
            _remoteAppCreateClientConnection ?? (_remoteAppCreateClientConnection = new RemoteAppCreateClientConnection());
    }
}