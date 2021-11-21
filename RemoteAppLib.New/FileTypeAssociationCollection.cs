namespace RemoteAppLib.New
{
    public class FileTypeAssociationCollection : System.Collections.CollectionBase
    {
        public void Add(FileTypeAssociation FileTypeAssociation)
        {
            List.Add(FileTypeAssociation);
        }

        public void Remove(FileTypeAssociation FileTypeAssociation)
        {
            List.Remove(FileTypeAssociation);
        }

        public string GetFlatFileTypes()
        {
            string GetFlatFileTypesRet = string.Empty;
            if (List.Count > 0)
            {
                foreach (FileTypeAssociation listItem in List)
                {
                    GetFlatFileTypesRet = string.Join(",.", listItem.Extension);
                }

                GetFlatFileTypesRet = GetFlatFileTypesRet.Substring(1);
            }

            return GetFlatFileTypesRet;
        }
    }
}