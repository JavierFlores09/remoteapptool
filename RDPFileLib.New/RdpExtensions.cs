namespace RDPFileLib.New
{
    internal static class RdpExtensions
    {
        internal static string GetTypeNameForRdp(this string propertyName)
        {
            return propertyName.GetType().ToString().Replace("System.", "").ToLower().Substring(0, 1);
        }

        internal static string GetTypeNameForRdp(this int propertyName)
        {
            return propertyName.GetType().ToString().Replace("System.", "").ToLower().Substring(0, 1);
        }
    }
}
