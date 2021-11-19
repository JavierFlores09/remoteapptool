using System;
using System.Windows.Forms;

namespace RemoteApp.Tool.New
{
    public partial class RemoteAppFileTypeAssociation
    {
        public RemoteAppFileTypeAssociation()
        {
            InitializeComponent();
            _FTAListView.Name = "FTAListView";
            _CreateButton.Name = "CreateButton";
            _DeleteButton.Name = "DeleteButton";
            _EditButton.Name = "EditButton";
            _CloseButton.Name = "CloseButton";
            _SetAssociationButton.Name = "SetAssociationButton";
        }

        private RemoteAppLib.RemoteApp RemoteApp = new RemoteAppLib.RemoteApp();

        public RemoteAppLib.RemoteApp EditFileTypes(RemoteAppLib.RemoteApp SelectedRemoteApp)
        {
            RemoteApp = SelectedRemoteApp;
            Text = "File type associations for " + RemoteApp.Name;
            HelpSystem.SetupTips(this);
            LoadFTAs();
            ShowDialog();
            //Dispose();
            return RemoteApp;
        }

        private void LoadFTAs()
        {
            FTAListView.Items.Clear();
            if (RemoteApp.FileTypeAssociations != null)
            {
                foreach (RemoteAppLib.FileTypeAssociation fta in RemoteApp.FileTypeAssociations)
                {
                    var FTitem = new ListViewItem(fta.Extension);
                    string ftaStatus;
                    if (LocalFtaModule.DoesFTAExist(fta.Extension))
                    {
                        // FTA exists - created by another app
                        ftaStatus = "Existing";
                        if (LocalFtaModule.IsFTAMine(fta.Extension))
                        {
                            // FTA exists - created by RemoteApp Tool
                            ftaStatus = "Yes";
                        }
                    }
                    else
                    {
                        // FTA does not exist
                        ftaStatus = "No";
                    }

                    FTitem.SubItems.Add(fta.IconPath);
                    FTitem.SubItems.Add(fta.IconIndex);
                    FTitem.SubItems.Add(ftaStatus);
                    FTAListView.Items.Add(FTitem);
                }
            }

            CheckSelection();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            if (RemoteApp.FileTypeAssociations is null)
            {
                var FTACol = new RemoteAppLib.FileTypeAssociationCollection();
                RemoteApp.FileTypeAssociations = FTACol;
            }

            _ = new RemoteAppLib.FileTypeAssociation();
            RemoteAppLib.FileTypeAssociation FTA = Forms.RemoteAppIconPicker.ManageFTA(RemoteApp.IconPath, 0);
            bool SameFTA = false;
            if (FTA.Extension != null)
            {
                foreach (RemoteAppLib.FileTypeAssociation RAFTA in RemoteApp.FileTypeAssociations)
                {
                    if ((RAFTA.Extension ?? "") == (FTA.Extension ?? ""))
                        SameFTA = true;
                }

                if (!SameFTA)
                {
                    RemoteApp.FileTypeAssociations.Add(FTA);
                }
                else
                {
                    MessageBox.Show("There is already an association for filetype: " + FTA.Extension, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }

            LoadFTAs();
        }

        private void FTAListView_DoubleClick(object sender, EventArgs e)
        {
            EditFTA();
        }

        private void FTAListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckSelection();
        }

        private void CheckSelection()
        {
            if (FTAListView.SelectedItems.Count > 0)
            {
                EditButton.Enabled = true;
                DeleteButton.Enabled = true;
                SetAssociationButton.Enabled = true;
            }
            else
            {
                EditButton.Enabled = false;
                DeleteButton.Enabled = false;
                SetAssociationButton.Enabled = false;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            string FileType = FTAListView.SelectedItems[0].Text;
            if (MessageBox.Show("Are you sure you want to remove filetype:" + Environment.NewLine + "." + FileType + " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                _ = FTAListView.SelectedItems[0].SubItems[0].Text;
                _ = FTAListView.SelectedItems[0].SubItems[1].Text;
                var FTAtoRemove = new RemoteAppLib.FileTypeAssociation();
                foreach (RemoteAppLib.FileTypeAssociation FTA in RemoteApp.FileTypeAssociations)
                {
                    if ((FTA.Extension ?? "") == (FileType ?? ""))
                        FTAtoRemove = FTA;
                }

                RemoteApp.FileTypeAssociations.Remove(FTAtoRemove);
            }

            LoadFTAs();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            EditFTA();
        }

        private void EditFTA()
        {
            _ = new RemoteAppLib.FileTypeAssociation();
            RemoteAppLib.FileTypeAssociation FTA = Forms.RemoteAppIconPicker.ManageFTA(FTAListView.SelectedItems[0].SubItems[1].Text, (int)Math.Round(double.Parse(FTAListView.SelectedItems[0].SubItems[2].Text)), FTAListView.SelectedItems[0].Text, true);
            if (FTA.Extension != null)
            {
                _ = FTAListView.SelectedItems[0].SubItems[0].Text;
                _ = FTAListView.SelectedItems[0].SubItems[1].Text;
                string FileType = FTAListView.SelectedItems[0].Text;
                var FTAtoRemove = new RemoteAppLib.FileTypeAssociation();
                foreach (RemoteAppLib.FileTypeAssociation FTA2 in RemoteApp.FileTypeAssociations)
                {
                    if ((FTA2.Extension ?? "") == (FileType ?? ""))
                        FTAtoRemove = FTA2;
                }

                RemoteApp.FileTypeAssociations.Remove(FTAtoRemove);
                RemoteApp.FileTypeAssociations.Add(FTA);
                LoadFTAs();
            }
        }

        private void SetAssociationButton_Click(object sender, EventArgs e) // NEED TO SORT OUT EVERYTHING IN THIS SUB!!!
        {
            var fta = new RemoteAppLib.FileTypeAssociation
            {
                Extension = FTAListView.SelectedItems[0].SubItems[0].Text,
                IconPath = FTAListView.SelectedItems[0].SubItems[1].Text,
                IconIndex = FTAListView.SelectedItems[0].SubItems[2].Text
            };
            if (LocalFtaModule.DoesFTAExist(fta.Extension))
            {
                DialogResult MsgBoxResult;
                // An existing association was found
                if (!LocalFtaModule.IsFTAMine(fta.Extension))
                {
                    // FTA found and belongs to another application (replace?)
                    MsgBoxResult = MessageBox.Show("An association already exists on the local computer for filetype. Would you like to replace it?" + Environment.NewLine + Environment.NewLine + "Warning: This association was created by another application. Replacing it can cause problems. If the existing association is working, there is no need to replace it.", "File Type Association", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                    if (MsgBoxResult == DialogResult.Yes)
                        LocalFtaModule.CreateFTA(fta, RemoteApp.Path, RemoteApp.Name, true);
                }
                else
                {
                    // FTA found and belongs to RemoteApp Tool (remove)
                    MsgBoxResult = MessageBox.Show("Are you sure you want to remove this file type association on the local computer?", "File Type Association", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                    if (MsgBoxResult == DialogResult.Yes)
                        LocalFtaModule.DeleteFTA(fta.Extension);
                }
            }
            // FTA not found (create)
            else if (LocalFtaModule.CreateFTA(fta, RemoteApp.Path, RemoteApp.Name))
            {
                MessageBox.Show("File type association created for " + fta.Extension + ".", "File Type Association", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("File type association not created. There was an error.", "File Type Association", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LoadFTAs();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}