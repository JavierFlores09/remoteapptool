using System;
using System.Windows.Forms;
using RemoteAppLib.New;
using Microsoft.Win32;

namespace RemoteApp.Tool.New
{
    internal static class LocalFtaModule
    {
        public static void RemoveUnusedFtAs()
        {
            var KeyNames = Registry.ClassesRoot.GetSubKeyNames();
            var RemoveCount = 0;
            foreach (var KeyName in KeyNames)
            {
                var ftAkey = Registry.ClassesRoot.OpenSubKey(KeyName);
                var RAvalue = (string)ftAkey.GetValue("RemoteApp", "");
                if (!string.IsNullOrEmpty(RAvalue))
                {
                    foreach (var KeyName2 in KeyNames)
                    {
                        try
                        {
                            var FTkey = Registry.ClassesRoot.OpenSubKey(KeyName2);
                            var FTvalue = (string)FTkey.GetValue("", "");
                            if ((FTvalue ?? "") == (KeyName ?? ""))
                            {
                                var sra = new SystemRemoteApps();
                                var AppExists = false;
                                foreach (RemoteAppLib.New.RemoteApp app in sra.GetAll())
                                {
                                    if ((app.Name ?? "") == (GetAppForFta(KeyName2) ?? ""))
                                        AppExists = true;
                                }

                                if (!AppExists)
                                {
                                    DeleteFta(KeyName2);
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

        public static string CreateFtaCollection(FileTypeAssociationCollection ftaCol, string exeFile, string remoteAppName, bool overwrite = false)
        {
            var createFtaCollectionRet = string.Empty;
            foreach (FileTypeAssociation fta in ftaCol)
            {
                if (DoesFtaExist(fta.Extension) && !CreateFta(fta, exeFile, remoteAppName, overwrite))
                {
                    createFtaCollectionRet += "|" + fta.Extension;
                }
            }

            return createFtaCollectionRet.Trim('|');
        }

        public static bool CreateFta(FileTypeAssociation fta, string exeFile, string remoteAppName, bool overwrite = false)
        {
            if (DoesFtaExist(fta.Extension) && !overwrite)
            {
                return false;
            }
            else
            {
                DeleteFta(fta.Extension);
                var FileTypeReg = Registry.ClassesRoot.CreateSubKey("." + fta.Extension);
                FileTypeReg.SetValue("", fta.Extension + "_file");
                var FileTypeKey = Registry.ClassesRoot.CreateSubKey(fta.Extension + "_file");
                FileTypeKey.SetValue("RemoteApp", remoteAppName);
                var FileTypeKeyShell = FileTypeKey.CreateSubKey("shell");
                var FileTypeKeyShellOpen = FileTypeKeyShell.CreateSubKey("open");
                var FileTypeKeyShellOpenCommand = FileTypeKeyShellOpen.CreateSubKey("command");
                FileTypeKeyShellOpenCommand.SetValue("", "\"" + exeFile + "\" \"%1\"");
                var FileTypeKeyDefIcon = FileTypeKey.CreateSubKey("DefaultIcon");
                FileTypeKeyDefIcon.SetValue("", "\"" + fta.IconPath + "\"," + fta.IconIndex);
                return true;
            }
        }

        public static string GetAppForFta(string fileExtension)
        {
            fileExtension = fileExtension.TrimStart('.');
            if (DoesFtaExist(fileExtension) && IsFtaMine(fileExtension))
            {
                try
                {
                    var hkcRext = Registry.ClassesRoot.OpenSubKey("." + fileExtension);
                    var hkcRfta = hkcRext.GetValue("", "");
                    if (string.IsNullOrEmpty(hkcRfta as string))
                    {
                        return string.Empty;
                    }
                    var hkcRftaKey = Registry.ClassesRoot.OpenSubKey((string)hkcRfta);
                    return (string)hkcRftaKey.GetValue("RemoteApp", "");
                }
                catch (Exception)
                {
                }
            }

            return string.Empty;
        }

        public static void DeleteFta(string fileExtension)
        {
            fileExtension = fileExtension.TrimStart('.');
            if (Registry.ClassesRoot.OpenSubKey("." + fileExtension) != null)
                Registry.ClassesRoot.DeleteSubKeyTree("." + fileExtension);
            if (Registry.ClassesRoot.OpenSubKey(fileExtension + "_file") != null)
                Registry.ClassesRoot.DeleteSubKeyTree(fileExtension + "_file");
        }

        public static bool DoesFtaExist(string fileExtension)
        {
            fileExtension = fileExtension.TrimStart('.');
            var ftAexists = false;
            if (Registry.ClassesRoot.OpenSubKey("." + fileExtension) != null)
            {
                ftAexists = true;
            }

            return ftAexists;
        }

        public static bool IsFtaMine(string fileExtension)
        {
            fileExtension = fileExtension.TrimStart('.');
            try
            {
                var hkcRext = Registry.ClassesRoot.OpenSubKey("." + fileExtension);
                var hkcRfta = hkcRext.GetValue("", "");
                if (string.IsNullOrEmpty(hkcRfta as string))
                {
                    return false;
                }
                var hkcRftaKey = Registry.ClassesRoot.OpenSubKey((string)hkcRfta);

                return !string.IsNullOrEmpty(hkcRftaKey.GetValue("RemoteApp", "") as string);
            }
            catch (Exception)
            {
            }

            return false;
        }
    }
}