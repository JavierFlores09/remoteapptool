using System;
using System.Windows.Forms;
using RemoteAppLib;

namespace RemoteApp.Tool.New
{
    internal static class LocalFtaModule
    {
        public static void RemoveUnusedFTAs()
        {
            var KeyNames = Microsoft.Win32.Registry.ClassesRoot.GetSubKeyNames();
            int RemoveCount = 0;
            foreach (var KeyName in KeyNames)
            {
                var FTAkey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(KeyName);
                string RAvalue = (string)FTAkey.GetValue("RemoteApp", "");
                if (!string.IsNullOrEmpty(RAvalue))
                {
                    foreach (var KeyName2 in KeyNames)
                    {
                        try
                        {
                            var FTkey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(KeyName2);
                            string FTvalue = (string)FTkey.GetValue("", "");
                            if ((FTvalue ?? "") == (KeyName ?? ""))
                            {
                                var sra = new SystemRemoteApps();
                                bool AppExists = false;
                                foreach (RemoteAppLib.RemoteApp app in sra.GetAll())
                                {
                                    if ((app.Name ?? "") == (GetAppForFTA(KeyName2) ?? ""))
                                        AppExists = true;
                                }

                                if (!AppExists)
                                {
                                    DeleteFTA(KeyName2);
                                    RemoveCount++;
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }

            MessageBox.Show("Unused file type associations removed: " + RemoveCount, "File Type Association", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static string CreateFTACollection(FileTypeAssociationCollection ftaCol, string exeFile, string RemoteAppName, bool overwrite = false)
        {
            var CreateFTACollectionRet = string.Empty;
            foreach (FileTypeAssociation fta in ftaCol)
            {
                if (DoesFTAExist(fta.Extension) && !CreateFTA(fta, exeFile, RemoteAppName, overwrite))
                {
                    CreateFTACollectionRet += "|" + fta.Extension;
                }
            }

            return CreateFTACollectionRet.Trim('|');
        }

        public static bool CreateFTA(FileTypeAssociation fta, string exeFile, string RemoteAppName, bool overwrite = false)
        {
            if (DoesFTAExist(fta.Extension) && !overwrite)
            {
                return false;
            }
            else
            {
                DeleteFTA(fta.Extension);
                var FileTypeReg = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey("." + fta.Extension);
                FileTypeReg.SetValue("", fta.Extension + "_file");
                var FileTypeKey = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(fta.Extension + "_file");
                FileTypeKey.SetValue("RemoteApp", RemoteAppName);
                var FileTypeKeyShell = FileTypeKey.CreateSubKey("shell");
                var FileTypeKeyShellOpen = FileTypeKeyShell.CreateSubKey("open");
                var FileTypeKeyShellOpenCommand = FileTypeKeyShellOpen.CreateSubKey("command");
                FileTypeKeyShellOpenCommand.SetValue("", "\"" + exeFile + "\" \"%1\"");
                var FileTypeKeyDefIcon = FileTypeKey.CreateSubKey("DefaultIcon");
                FileTypeKeyDefIcon.SetValue("", "\"" + fta.IconPath + "\"," + fta.IconIndex);
                return true;
            }
        }

        public static string GetAppForFTA(string fileExtension)
        {
            fileExtension = fileExtension.TrimStart('.');
            if (DoesFTAExist(fileExtension) && IsFTAMine(fileExtension))
            {
                try
                {
                    var HKCRext = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("." + fileExtension);
                    var HKCRfta = HKCRext.GetValue("", "");
                    if (string.IsNullOrEmpty(HKCRfta as string))
                    {
                        return string.Empty;
                    }
                    var HKCRftaKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey((string)HKCRfta);
                    return (string)HKCRftaKey.GetValue("RemoteApp", "");
                }
                catch (Exception)
                {
                }
            }

            return string.Empty;
        }

        public static void DeleteFTA(string fileExtension)
        {
            fileExtension = fileExtension.TrimStart('.');
            if (Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("." + fileExtension) != null)
                Microsoft.Win32.Registry.ClassesRoot.DeleteSubKeyTree("." + fileExtension);
            if (Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(fileExtension + "_file") != null)
                Microsoft.Win32.Registry.ClassesRoot.DeleteSubKeyTree(fileExtension + "_file");
        }

        public static bool DoesFTAExist(string fileExtension)
        {
            fileExtension = fileExtension.TrimStart('.');
            var FTAexists = false;
            if (Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("." + fileExtension) != null)
            {
                FTAexists = true;
            }

            return FTAexists;
        }

        public static bool IsFTAMine(string fileExtension)
        {
            fileExtension = fileExtension.TrimStart('.');
            try
            {
                var HKCRext = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("." + fileExtension);
                var HKCRfta = HKCRext.GetValue("", "");
                if (string.IsNullOrEmpty(HKCRfta as string))
                {
                    return false;
                }
                var HKCRftaKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey((string)HKCRfta);

                return !string.IsNullOrEmpty(HKCRftaKey.GetValue("RemoteApp", "") as string);
            }
            catch (Exception)
            {
            }

            return false;
        }
    }
}