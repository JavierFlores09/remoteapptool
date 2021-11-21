namespace RemoteAppLib.New
{
    public class RemoteAppCollection : System.Collections.CollectionBase
    {
        public void Add(RemoteApp RemoteApp)
        {
            List.Add(RemoteApp);
        }

        public void Remove(RemoteApp RemoteApp)
        {
            List.Remove(RemoteApp);
        }
    }
}