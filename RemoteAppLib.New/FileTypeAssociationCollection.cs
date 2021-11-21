namespace RemoteAppLib.New
{
    public class FileTypeAssociationCollection : System.Collections.CollectionBase
    {
        public void Add(FileTypeAssociation fileTypeAssociation)
        {
            List.Add(fileTypeAssociation);
        }

        public void Remove(FileTypeAssociation fileTypeAssociation)
        {
            List.Remove(fileTypeAssociation);
        }

        public string GetFlatFileTypes()
        {
            var GetFlatFileTypesRet = string.Empty;
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