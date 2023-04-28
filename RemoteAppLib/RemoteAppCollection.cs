namespace RemoteAppLib
{
    public class RemoteAppCollection : System.Collections.CollectionBase
    {
        public void Add(RemoteApp remoteApp)
        {
            List.Add(remoteApp);
        }

        public void Remove(RemoteApp remoteApp)
        {
            List.Remove(remoteApp);
        }
    }
}