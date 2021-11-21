using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RemoteApp.Tool.New
{
    public class IconExtractor
    {
        [DllImport("shell32.dll", EntryPoint = "ExtractIconExA")]
        private static extern int ExtractIconEx(string lpszFile, int nIconIndex, ref int phiconLarge, ref int phiconSmall, int nIcons);

        [DllImport("shell32.dll", EntryPoint = "ExtractIconA")]
        private static extern int ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);

        [DllImport("user32")]
        private static extern int DrawIconEx(int hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyWidth, int istepIfAniCur, int hbrFlickerFreeDraw, int diFlags);

        [DllImport("user32")]
        private static extern int DestroyIcon(int hIcon);

        private int[] _mHIcons;

        ~IconExtractor()
        {
            if (_mHIcons?.Length > 0)
            {
                for (int iconIndex = 0, loopTo = _mHIcons.Length - 1; iconIndex <= loopTo; iconIndex++)
                    DestroyIcon(_mHIcons[iconIndex]);
            }
        }

        public List<Icon> ExtractIcons(string filePath, IntPtr hInst)
        {
            var listIcons = new List<Icon>();
            try
            {
                int argphiconLarge = 0;
                int argphiconSmall = 0;
                int numIcons = ExtractIconEx(filePath, -1, ref argphiconLarge, ref argphiconSmall, 0);
                if (numIcons == 0)
                {
                    throw new Exception("No icons found in " + filePath);
                }

                numIcons--;
                for (int currentIcon = 0, loopTo = numIcons; currentIcon <= loopTo; currentIcon++)
                {
                    _mHIcons[currentIcon] = ExtractIcon(hInst, filePath, currentIcon);
                    var handleIcon = new IntPtr(_mHIcons[currentIcon]);
                    if (!handleIcon.Equals(IntPtr.Zero))
                    {
                        listIcons.Add(Icon.FromHandle(handleIcon));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return listIcons;
        }
    }

    internal static class IconModule
    {
        public static void TestIconLib()
        {
            _ = new System.Drawing.IconLib.MultiIcon();
        }

        public static Icon IconFromFilePath(string filePath)
        {
            Icon result = null;
            try
            {
                result = Icon.ExtractAssociatedIcon(filePath);
            }
            catch
            {
            }

            return result;
        }

        public static bool ExtractToIco(string iconSourcePath, int iconSourceIndex, string icoDestPath)
        {
            bool success = false;
            bool IconLoadError = false;
            if (File.Exists(iconSourcePath))
            {
                var mIcon = new System.Drawing.IconLib.MultiIcon();
                try
                {
                    mIcon.Load(iconSourcePath);
                }
                catch (Exception)
                {
                    IconLoadError = true;
                }

                if (!IconLoadError)
                {
                    System.Drawing.IconLib.SingleIcon sIcon = iconSourceIndex == 0 ? mIcon.FirstOrDefault() : mIcon[iconSourceIndex];
                    try
                    {
                        sIcon.Save(icoDestPath);
                        success = true;
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            return success;
        }

        [DllImport("shell32.dll", EntryPoint = "ExtractIconExA")]
        private static extern int ExtractIcon(string lpszFile, int nIconIndex, ref int phiconLarge, ref int phiconSmall, int nIcons);

        public static Icon ReturnIcon(string path, int index, bool small = false)
        {
            var bigIcon = default(int);
            var smallIcon = default(int);
            ExtractIcon(path, index, ref bigIcon, ref smallIcon, 1);
            if (bigIcon == 0)
            {
                ExtractIcon(path, 0, ref bigIcon, ref smallIcon, 1);
            }

            if (bigIcon != 0)
            {
                if (!small)
                {
                    return Icon.FromHandle((IntPtr)bigIcon);
                }
                else
                {
                    return Icon.FromHandle((IntPtr)smallIcon);
                }
            }
            else
            {
                return ReturnIcon(RemoteAppFunction.GetSysDir() + @"\mstsc.exe", 0);
            }
        }
    }
}