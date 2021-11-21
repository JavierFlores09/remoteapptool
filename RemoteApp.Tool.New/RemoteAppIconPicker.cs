using System;
using System.IO;
using System.Windows.Forms;

namespace RemoteApp.Tool.New
{
    public partial class RemoteAppIconPicker
    {
        public RemoteAppIconPicker()
        {
            InitializeComponent();
            _IconList.Name = "IconList";
            _BrowseButton.Name = "BrowseButton";
            _IconIndexTextBox.Name = "IconIndexTextBox";
            _FileTypeTextBox.Name = "FileTypeTextBox";
        }

        private bool _noValidate = false;

        public RemoteAppLib.New.IconSelection PickIcon(string defaultPath, int defaultIndex = 0)
        {
            FileTypeLabel.Visible = false;
            FileTypeTextBox.Visible = false;
            IconIndexLabel.Visible = false;
            IconIndexTextBox.Visible = false;
            Text = "Select Icon";
            IconPathTextBox.Text = defaultPath;
            IconIndexTextBox.Text = defaultIndex.ToString();
            LoadIcons();
            var IconPick = new RemoteAppLib.New.IconSelection();
            if (ShowDialog() == DialogResult.OK)
            {
                IconPick.IconPath = IconPathTextBox.Text;
                IconPick.IconIndex = IconIndexTextBox.Text;
            }

            return IconPick;
            //Dispose();
        }

        public RemoteAppLib.New.FileTypeAssociation ManageFta(string defaultPath, int defaultIndex = 0, string fileType = ".xyz", bool isEdit = false)
        {
            var fta = new RemoteAppLib.New.FileTypeAssociation();
            FileTypeLabel.Visible = true;
            FileTypeTextBox.Visible = true;
            IconIndexLabel.Visible = true;
            IconIndexTextBox.Visible = true;
            IconIndexTextBox.ReadOnly = true;
            FileTypeTextBox.ReadOnly = isEdit;

            Text = "File Type";
            FileTypeTextBox.Text = fileType;
            IconPathTextBox.Text = defaultPath;
            IconIndexTextBox.Text = defaultIndex.ToString();
            LoadIcons();
            if (ShowDialog() == DialogResult.OK)
            {
                fta.Extension = FileTypeTextBox.Text;
                fta.IconPath = IconPathTextBox.Text;
                fta.IconIndex = IconIndexTextBox.Text;
            }

            //Dispose();
            return fta;
        }

        private bool LoadIcons()
        {
            IconList.Clear();
            SmallIcons.Images.Clear();
            var IconError = false;
            IconIndexLabel.Visible = true;
            IconIndexTextBox.Visible = true;
            if (File.Exists(IconPathTextBox.Text))
            {
                var mIcon = new System.Drawing.IconLib.MultiIcon();
                var IconIndex = 0;
                try
                {
                    mIcon.Load(IconPathTextBox.Text);
                    IconIndexTextBox.ReadOnly = true;
                }
                catch (Exception)
                {
                    IconError = true;
                    _noValidate = true;
                    OKButton.Enabled = true;
                    MessageBox.Show("There was an error loading icons from:" + Environment.NewLine + IconPathTextBox.Text + Environment.NewLine + "Icons may still be usable, but you can not select one using the icon picker.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    IconIndexTextBox.Text = "0";
                    IconIndexTextBox.ReadOnly = false;
                }

                if (!IconError)
                {
                    _noValidate = false;
                    foreach (var sIcon in mIcon)
                    {
                        var IconKey = IconIndex;
                        var IconItem = new ListViewItem(IconKey.ToString());
                        var fiImage = sIcon[0];
                        foreach (var iImage in sIcon)
                        {
                            if (iImage.Icon.Width == 32)
                            {
                                switch (iImage.ColorsInPalette)
                                {
                                    case 0:
                                        {
                                            fiImage = iImage;
                                            break;
                                        }

                                    default:
                                        {
                                            if (iImage.ColorsInPalette > fiImage.ColorsInPalette)
                                                fiImage = iImage;
                                            break;
                                        }
                                }

                                if (iImage.ColorsInPalette == 0)
                                    fiImage = iImage;
                            }
                        }

                        var TheBitmap = fiImage.Icon.ToBitmap();
                        SmallIcons.Images.Add(IconKey.ToString(), TheBitmap);
                        IconItem.ImageKey = IconKey.ToString();
                        IconList.Items.Add(IconItem);
                        IconIndex++;
                    }

                    if (!_noValidate)
                        OKButton.Enabled = false;
                }
            }

            return IconError;
        }

        private void IconList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckSelection();
        }

        private void CheckSelection()
        {
            if (IconList.SelectedItems.Count > 0)
            {
                // something is selected
                if (!string.IsNullOrEmpty(FileTypeTextBox.Text))
                    OKButton.Enabled = true;
                IconIndexTextBox.Text = IconList.SelectedItems[0].Text;
            }
            else
            {
                // nothing is selected
                if (!_noValidate)
                    OKButton.Enabled = false;
                IconIndexTextBox.Text = "0";
            }
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(IconPathTextBox.Text))
                FileBrowserIcon.InitialDirectory = System.IO.Path.GetDirectoryName(IconPathTextBox.Text);
            if (FileBrowserIcon.ShowDialog() == DialogResult.OK)
            {
                IconPathTextBox.Text = FileBrowserIcon.FileName;
                LoadIcons();
            }
        }

        private void FileTypeTextBox_TextChanged(object sender, EventArgs e)
        {
            RemoteAppFunction.ValidateFileType(FileTypeTextBox);
            if (string.IsNullOrEmpty(FileTypeTextBox.Text))
            {
                OKButton.Enabled = false;
            }
            else
            {
                if (IconList.SelectedItems.Count == 1)
                {
                    OKButton.Enabled = true;
                }
            }
        }

        private void IconIndexTextBox_TextChanged(object sender, EventArgs e)
        {
            RemoteAppFunction.ValidateInteger(IconIndexTextBox);
        }
    }
}