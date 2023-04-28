using System;
using System.Windows.Forms;

namespace RemoteApp.Tool
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

        private RemoteAppLib.RemoteApp _remoteApp = new RemoteAppLib.RemoteApp();

        public RemoteAppLib.RemoteApp EditFileTypes(RemoteAppLib.RemoteApp selectedRemoteApp)
        {
            _remoteApp = selectedRemoteApp;
            Text = "File type associations for " + _remoteApp.Name;
            HelpSystem.SetupTips(this);
            LoadFtAs();
            ShowDialog();
            //Dispose();
            return _remoteApp;
        }

        private void LoadFtAs()
        {
            FTAListView.Items.Clear();
            if (_remoteApp.FileTypeAssociations != null)
            {
                foreach (RemoteAppLib.FileTypeAssociation fta in _remoteApp.FileTypeAssociations)
                {
                    var FTitem = new ListViewItem(fta.Extension);
                    string ftaStatus;
                    if (LocalFtaModule.DoesFtaExist(fta.Extension))
                    {
                        // FTA exists - created by another app
                        ftaStatus = "Existing";
                        if (LocalFtaModule.IsFtaMine(fta.Extension))
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
            if (_remoteApp.FileTypeAssociations is null)
            {
                var ftaCol = new RemoteAppLib.FileTypeAssociationCollection();
                _remoteApp.FileTypeAssociations = ftaCol;
            }

            _ = new RemoteAppLib.FileTypeAssociation();
            var fta = Forms.RemoteAppIconPicker.ManageFta(_remoteApp.IconPath, 0);
            var sameFta = false;
            if (fta.Extension != null)
            {
                foreach (RemoteAppLib.FileTypeAssociation rafta in _remoteApp.FileTypeAssociations)
                {
                    if ((rafta.Extension ?? "") == (fta.Extension ?? ""))
                        sameFta = true;
                }

                if (!sameFta)
                {
                    _remoteApp.FileTypeAssociations.Add(fta);
                }
                else
                {
                    MessageBox.Show("There is already an association for filetype: " + fta.Extension, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }

            LoadFtAs();
        }

        private void FTAListView_DoubleClick(object sender, EventArgs e)
        {
            EditFta();
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
            var FileType = FTAListView.SelectedItems[0].Text;
            if (MessageBox.Show("Are you sure you want to remove filetype:" + Environment.NewLine + "." + FileType + " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                _ = FTAListView.SelectedItems[0].SubItems[0].Text;
                _ = FTAListView.SelectedItems[0].SubItems[1].Text;
                var ftAtoRemove = new RemoteAppLib.FileTypeAssociation();
                foreach (RemoteAppLib.FileTypeAssociation fta in _remoteApp.FileTypeAssociations)
                {
                    if ((fta.Extension ?? "") == (FileType ?? ""))
                        ftAtoRemove = fta;
                }

                _remoteApp.FileTypeAssociations.Remove(ftAtoRemove);
            }

            LoadFtAs();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            EditFta();
        }

        private void EditFta()
        {
            _ = new RemoteAppLib.FileTypeAssociation();
            var fta = Forms.RemoteAppIconPicker.ManageFta(FTAListView.SelectedItems[0].SubItems[1].Text, (int)Math.Round(double.Parse(FTAListView.SelectedItems[0].SubItems[2].Text)), FTAListView.SelectedItems[0].Text, true);
            if (fta.Extension != null)
            {
                _ = FTAListView.SelectedItems[0].SubItems[0].Text;
                _ = FTAListView.SelectedItems[0].SubItems[1].Text;
                var FileType = FTAListView.SelectedItems[0].Text;
                var ftAtoRemove = new RemoteAppLib.FileTypeAssociation();
                foreach (RemoteAppLib.FileTypeAssociation fta2 in _remoteApp.FileTypeAssociations)
                {
                    if ((fta2.Extension ?? "") == (FileType ?? ""))
                        ftAtoRemove = fta2;
                }

                _remoteApp.FileTypeAssociations.Remove(ftAtoRemove);
                _remoteApp.FileTypeAssociations.Add(fta);
                LoadFtAs();
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
            if (LocalFtaModule.DoesFtaExist(fta.Extension))
            {
                DialogResult MsgBoxResult;
                // An existing association was found
                if (!LocalFtaModule.IsFtaMine(fta.Extension))
                {
                    // FTA found and belongs to another application (replace?)
                    MsgBoxResult = MessageBox.Show("An association already exists on the local computer for filetype. Would you like to replace it?" + Environment.NewLine + Environment.NewLine + "Warning: This association was created by another application. Replacing it can cause problems. If the existing association is working, there is no need to replace it.", "File Type Association", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                    if (MsgBoxResult == DialogResult.Yes)
                        LocalFtaModule.CreateFta(fta, _remoteApp.Path, _remoteApp.Name, true);
                }
                else
                {
                    // FTA found and belongs to RemoteApp Tool (remove)
                    MsgBoxResult = MessageBox.Show("Are you sure you want to remove this file type association on the local computer?", "File Type Association", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                    if (MsgBoxResult == DialogResult.Yes)
                        LocalFtaModule.DeleteFta(fta.Extension);
                }
            }
            // FTA not found (create)
            else if (LocalFtaModule.CreateFta(fta, _remoteApp.Path, _remoteApp.Name))
            {
                MessageBox.Show("File type association created for " + fta.Extension + ".", "File Type Association", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("File type association not created. There was an error.", "File Type Association", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LoadFtAs();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}