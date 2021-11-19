namespace RemoteApp.Tool.New
{
    internal static class Forms
    {
        private static RemoteAppCreateClientConnection remoteAppCreateClientConnection;
        private static RemoteAppIconPicker remoteAppIconPicker;
        private static RemoteAppEditWindow remoteAppEditWindow;
        private static RemoteAppMainWindow remoteAppMainWindow;
        private static RemoteAppFileTypeAssociation remoteAppFileTypeAssociation;
        private static RemoteAppHostOptions remoteAppHostOptions;
        private static RemoteAppAboutWindow remoteAppAboutWindow;

        internal static RemoteAppAboutWindow RemoteAppAboutWindow =>
            remoteAppAboutWindow ?? (remoteAppAboutWindow = new RemoteAppAboutWindow());

        internal static RemoteAppHostOptions RemoteAppHostOptions =>
            remoteAppHostOptions ?? (remoteAppHostOptions = new RemoteAppHostOptions());
        internal static RemoteAppFileTypeAssociation RemoteAppFileTypeAssociation =>
            remoteAppFileTypeAssociation ?? (remoteAppFileTypeAssociation = new RemoteAppFileTypeAssociation());
        internal static RemoteAppMainWindow RemoteAppMainWindow =>
            remoteAppMainWindow ?? (remoteAppMainWindow = new RemoteAppMainWindow());
        internal static RemoteAppEditWindow RemoteAppEditWindow =>
            remoteAppEditWindow ?? (remoteAppEditWindow = new RemoteAppEditWindow());
        internal static RemoteAppIconPicker RemoteAppIconPicker =>
            remoteAppIconPicker ?? (remoteAppIconPicker = new RemoteAppIconPicker());
        internal static RemoteAppCreateClientConnection RemoteAppCreateClientConnection =>
            remoteAppCreateClientConnection ?? (remoteAppCreateClientConnection = new RemoteAppCreateClientConnection());
    }
}