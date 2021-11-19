using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace RemoteApp.Tool.New
{
    internal static class HelpSystem
    {
        public static void SetupTips(Form TheForm)
        {
            var toolTip1 = new ToolTip()
            {
                AutoPopDelay = 10000,
                InitialDelay = 500,
                ReshowDelay = 500
            };
            string HelpString;
            foreach (Control Control in TheForm.Controls)
            {
                foreach (Control SubControl in Control.Controls)
                {
                    foreach (Control SubSubControl in SubControl.Controls)
                    {
                        foreach (Control SubSubSubControl in (IEnumerable)SubSubControl.Controls)
                        {
                            HelpString = GetTipString(Control.Parent.Name, SubSubSubControl.Name);
                            if (!string.IsNullOrEmpty(HelpString))
                                toolTip1.SetToolTip(SubSubSubControl, HelpString);
                        }

                        HelpString = GetTipString(Control.Parent.Name, SubSubControl.Name);
                        if (!string.IsNullOrEmpty(HelpString))
                            toolTip1.SetToolTip(SubSubControl, HelpString);
                    }

                    HelpString = GetTipString(Control.Parent.Name, SubControl.Name);
                    if (!string.IsNullOrEmpty(HelpString))
                        toolTip1.SetToolTip(SubControl, HelpString);
                }

                HelpString = GetTipString(Control.Parent.Name, Control.Name);
                if (!string.IsNullOrEmpty(HelpString))
                    toolTip1.SetToolTip(Control, HelpString);
            }
        }

        private static string GetTipString(string FormName, string ControlName)
        {
            var TipString = string.Empty;
            var TipText = GetTipFile();
            foreach (string TipLine in TipText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var TipLineArray = TipLine.Split('|');
                if ((TipLineArray[0] ?? string.Empty) == (FormName ?? string.Empty) && (TipLineArray[1] ?? string.Empty) == (ControlName ?? string.Empty))
                    TipString = TipLineArray[2];
            }

            return TipString;
        }

        private static string GetTipFile()
        {
            return File.Exists("tips.txt")
                ? File.ReadAllText("tips.txt")
                : GetBuiltInTips();
        }

        private static string GetBuiltInTips()
        {
            var tips = new StringBuilder();
            tips.AppendLine("RemoteAppMainWindow|CreateButton|Add a new RemoteApp.");
            tips.AppendLine("RemoteAppMainWindow|DeleteButton|Remove selected RemoteApp.");
            tips.AppendLine("RemoteAppMainWindow|EditButton|Edit properties of selected RemoteApp.");
            tips.AppendLine("RemoteAppMainWindow|CreateClientConnection|Creates a RDP file or MSI installer for the selected RemoteApp.");
            tips.AppendLine("RemoteAppEditWindow|SaveButton|Save changes and close.");
            tips.AppendLine("RemoteAppEditWindow|FTAButton|Set file type associations for this RemoteApp.");
            tips.AppendLine("RemoteAppEditWindow|CancelEditButton|Discard changes and close.");
            tips.AppendLine("RemoteAppEditWindow|BrowseIconPath|Select an icon for the RemoteApp.");
            tips.AppendLine("RemoteAppEditWindow|IconPathCopyButton|Copy the value from the \"Path\" field into the \"Icon path\" field. ");
            tips.AppendLine("RemoteAppEditWindow|BrowsePath|Browse for application.");
            tips.AppendLine("RemoteAppEditWindow|IconResetButton|Reset the icon to the default for this application.");

            // IconResetButton
            tips.Append("RemoteAppCreateClientConnection|RDPRadioButton|Create an RDP file.");
            tips.Append("RemoteAppCreateClientConnection|MSIRadioButton|Create an MSI file.");
            tips.Append("RemoteAppCreateClientConnection|EditAfterSave|Edit the RDP connection file.");
            tips.Append("RemoteAppCreateClientConnection|CreateRAWebIcon|Generate an icon for the application and any file type associations.\r\nUse this with RAWeb.");
            tips.Append("RemoteAppCreateClientConnection|FTAButton|Set file type associations for this RemoteApp.\r\nChanges here will only affect this client connection. They will not be saved.");
            tips.Append("RemoteAppCreateClientConnection|DisabledFTACheckBox|Do not include file type associations in this client connection.");
            tips.Append("RemoteAppCreateClientConnection|SaveButton|Save window settings for next time.");
            tips.Append("RemoteAppCreateClientConnection|ResetButton|Reset window settings to defaults.");
            tips.Append("RemoteAppCreateClientConnection|CancelEditButton|Close and return to the main window.");
            tips.Append("RemoteAppCreateClientConnection|CreateButton|Create the client connection and choose where to save it.");
            tips.Append("RemoteAppCreateClientConnection|UseRDGatewayCheckBox|Use a Remote Desktop Gateway to connect to the host.");
            tips.Append("RemoteAppCreateClientConnection|AttemptDirectCheckBox|Try a direct connection first, if that fails then use the Remote Desktop Gateway.");
            tips.Append("RemoteAppCreateClientConnection|ShortcutTagCheckBox|Append some text to the end of each shortcut title.");
            tips.Append("RemoteAppCreateClientConnection|PerMachineRadioButton|Should the shortcuts install for all users (per-machine), or only for the logged in user (per user)?");
            tips.Append("RemoteAppCreateClientConnection|PerUserRadioButton|Should the shortcuts install for all users (per-machine), or only for the logged in user (per user)?");
            tips.Append("RemoteAppCreateClientConnection|CheckBoxSignRDPEnabled|Digitally sign the RDP file.");
            tips.Append("RemoteAppCreateClientConnection|CheckBoxCreateSignedAndUnsigned|Produce both a signed and unsigned copy of the RDP file.");
            tips.Append("RemoteAppFileTypeAssociation|CreateButton|Create a new File Type Association.");
            tips.Append("RemoteAppFileTypeAssociation|DeleteButton|Delete selected File Type Association.");
            tips.Append("RemoteAppFileTypeAssociation|EditButton|Change icon of selected File Type Association.");
            tips.Append("RemoteAppFileTypeAssociation|SetAssociationButton|Create or remove the selected File Type Association on the current system.");
            tips.Append("RemoteAppFileTypeAssociation|CloseButton|Save changes and close.");
            tips.Append("RemoteAppIconPicker|BrowseButton|Browse for a file that contains icons.");
            tips.Append("RemoteAppIconPicker|CancelEditButton|Discard changes and close.");
            tips.Append("RemoteAppIconPicker|OKButton|Choose the selected icon.");

            return tips.ToString();
        }
    }
}