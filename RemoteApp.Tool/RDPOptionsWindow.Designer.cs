using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace RemoteApp.Tool
{
    public partial class RDPOptionsWindow : Form
    {
        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is object)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(RDPOptionsWindow));
            _OptionsListBox = new ListBox();
            _OptionsListBox.SelectedIndexChanged += new EventHandler(OptionsListBox_SelectedIndexChanged);
            DescriptionTextBox = new TextBox();
            Label1 = new Label();
            _ValueTextBox = new TextBox();
            _ValueTextBox.TextChanged += new EventHandler(ValueTextBox_TextChanged);
            _ChangedOptionsListView = new ListView();
            ColumnHeader1 = new ColumnHeader();
            ColumnHeader2 = new ColumnHeader();
            ColumnHeader3 = new ColumnHeader();
            _SaveButton = new Button();
            _SaveButton.Click += new EventHandler(SaveButton_Click);
            SmallerIcons = new ImageList(components);
            _ResetButton = new Button();
            _ResetButton.Click += new EventHandler(ResetButton_Click);
            Label2 = new Label();
            _DefaultsButton = new Button();
            _DefaultsButton.Click += new EventHandler(DefaultsButton_Click);
            _ResetValueButton = new Button();
            _ResetValueButton.Click += new EventHandler(ResetValueButton_Click);
            SuspendLayout();
            //
            // OptionsListBox
            //
            _OptionsListBox.FormattingEnabled = true;
            _OptionsListBox.ItemHeight = 15;
            _OptionsListBox.Location = new Point(11, 12);
            _OptionsListBox.Margin = new Padding(2);
            _OptionsListBox.Name = "_OptionsListBox";
            _OptionsListBox.Size = new Size(213, 169);
            _OptionsListBox.TabIndex = 0;
            //
            // DescriptionTextBox
            //
            DescriptionTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            DescriptionTextBox.Location = new Point(228, 12);
            DescriptionTextBox.Margin = new Padding(2);
            DescriptionTextBox.Multiline = true;
            DescriptionTextBox.Name = "DescriptionTextBox";
            DescriptionTextBox.ScrollBars = ScrollBars.Vertical;
            DescriptionTextBox.Size = new Size(347, 169);
            DescriptionTextBox.TabIndex = 1;
            //
            // Label1
            //
            Label1.AutoSize = true;
            Label1.Location = new Point(11, 188);
            Label1.Margin = new Padding(2, 0, 2, 0);
            Label1.Name = "Label1";
            Label1.Size = new Size(57, 15);
            Label1.TabIndex = 2;
            Label1.Text = "Set value:";
            //
            // ValueTextBox
            //
            _ValueTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _ValueTextBox.Location = new Point(72, 185);
            _ValueTextBox.Margin = new Padding(2);
            _ValueTextBox.Name = "_ValueTextBox";
            _ValueTextBox.Size = new Size(476, 23);
            _ValueTextBox.TabIndex = 3;
            //
            // ChangedOptionsListView
            //
            _ChangedOptionsListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _ChangedOptionsListView.Columns.AddRange(new ColumnHeader[] { ColumnHeader1, ColumnHeader2, ColumnHeader3 });
            _ChangedOptionsListView.FullRowSelect = true;
            _ChangedOptionsListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            _ChangedOptionsListView.HideSelection = false;
            _ChangedOptionsListView.Location = new Point(11, 238);
            _ChangedOptionsListView.Margin = new Padding(2);
            _ChangedOptionsListView.MultiSelect = false;
            _ChangedOptionsListView.Name = "_ChangedOptionsListView";
            _ChangedOptionsListView.Size = new Size(564, 117);
            _ChangedOptionsListView.Sorting = SortOrder.Ascending;
            _ChangedOptionsListView.TabIndex = 6;
            _ChangedOptionsListView.UseCompatibleStateImageBehavior = false;
            _ChangedOptionsListView.View = View.Details;
            _ChangedOptionsListView.SelectedIndexChanged += new EventHandler(ChangedOptionsListView_SelectedIndexChanged);
            //
            // ColumnHeader1
            //
            ColumnHeader1.Text = "Option";
            ColumnHeader1.Width = 200;
            //
            // ColumnHeader2
            //
            ColumnHeader2.Text = "Type";
            //
            // ColumnHeader3
            //
            ColumnHeader3.Text = "Value";
            ColumnHeader3.Width = 282;
            //
            // SaveButton
            //
            _SaveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _SaveButton.BackColor = Color.WhiteSmoke;
            _SaveButton.ImageList = SmallerIcons;
            _SaveButton.Location = new Point(511, 366);
            _SaveButton.Margin = new Padding(2);
            _SaveButton.Name = "_SaveButton";
            _SaveButton.Size = new Size(64, 29);
            _SaveButton.TabIndex = 9;
            _SaveButton.Text = "Close";
            _SaveButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _SaveButton.UseVisualStyleBackColor = false;
            //
            // SmallerIcons
            //
            SmallerIcons.ImageStream = (ImageListStreamer)resources.GetObject("SmallerIcons.ImageStream");
            SmallerIcons.TransparentColor = Color.Transparent;
            SmallerIcons.Images.SetKeyName(0, "save-as_16x16.png");
            SmallerIcons.Images.SetKeyName(1, "msi small.ico");
            SmallerIcons.Images.SetKeyName(2, "doc_file_document_manager_paper_phone.ico");
            SmallerIcons.Images.SetKeyName(3, "16.ico");
            SmallerIcons.Images.SetKeyName(4, "cross.ico");
            SmallerIcons.Images.SetKeyName(5, "pictures (1).ico");
            SmallerIcons.Images.SetKeyName(6, "Remote Desktop Connection.ico");
            //
            // ResetButton
            //
            _ResetButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _ResetButton.BackColor = Color.WhiteSmoke;
            _ResetButton.ImageIndex = 4;
            _ResetButton.ImageList = SmallerIcons;
            _ResetButton.Location = new Point(417, 366);
            _ResetButton.Margin = new Padding(2);
            _ResetButton.Name = "_ResetButton";
            _ResetButton.Size = new Size(90, 29);
            _ResetButton.TabIndex = 8;
            _ResetButton.Text = "Clear all";
            _ResetButton.TextAlign = ContentAlignment.MiddleRight;
            _ResetButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _ResetButton.UseVisualStyleBackColor = false;
            //
            // Label2
            //
            Label2.AutoSize = true;
            Label2.Location = new Point(12, 221);
            Label2.Name = "Label2";
            Label2.Size = new Size(97, 15);
            Label2.TabIndex = 5;
            Label2.Text = "Selected options:";
            //
            // DefaultsButton
            //
            _DefaultsButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _DefaultsButton.BackColor = Color.WhiteSmoke;
            _DefaultsButton.ImageIndex = 3;
            _DefaultsButton.ImageList = SmallerIcons;
            _DefaultsButton.Location = new Point(324, 366);
            _DefaultsButton.Margin = new Padding(2);
            _DefaultsButton.Name = "_DefaultsButton";
            _DefaultsButton.Size = new Size(89, 29);
            _DefaultsButton.TabIndex = 7;
            _DefaultsButton.Text = "Defaults";
            _DefaultsButton.TextAlign = ContentAlignment.MiddleRight;
            _DefaultsButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _DefaultsButton.UseVisualStyleBackColor = false;
            //
            // ResetValueButton
            //
            _ResetValueButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _ResetValueButton.BackColor = Color.WhiteSmoke;
            _ResetValueButton.ImageIndex = 4;
            _ResetValueButton.ImageList = SmallerIcons;
            _ResetValueButton.Location = new Point(552, 184);
            _ResetValueButton.Margin = new Padding(2);
            _ResetValueButton.Name = "_ResetValueButton";
            _ResetValueButton.Size = new Size(25, 25);
            _ResetValueButton.TabIndex = 4;
            _ResetValueButton.TextAlign = ContentAlignment.MiddleRight;
            _ResetValueButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _ResetValueButton.UseVisualStyleBackColor = false;
            //
            // RDPOptionsWindow
            //
            AcceptButton = _SaveButton;
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(586, 406);
            Controls.Add(_ResetValueButton);
            Controls.Add(Label2);
            Controls.Add(_DefaultsButton);
            Controls.Add(_ResetButton);
            Controls.Add(_SaveButton);
            Controls.Add(_ChangedOptionsListView);
            Controls.Add(_ValueTextBox);
            Controls.Add(Label1);
            Controls.Add(DescriptionTextBox);
            Controls.Add(_OptionsListBox);
            Font = new Font("Segoe UI", 9.0f);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RDPOptionsWindow";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Additional RDP Options";
            ResumeLayout(false);
            PerformLayout();
        }

        internal ListBox _OptionsListBox;
        internal TextBox DescriptionTextBox;
        internal Label Label1;
        internal TextBox _ValueTextBox;
        internal ListView _ChangedOptionsListView;
        internal ColumnHeader ColumnHeader1;
        internal ColumnHeader ColumnHeader2;
        internal ColumnHeader ColumnHeader3;
        internal Button _SaveButton;
        internal ImageList SmallerIcons;
        internal Button _ResetButton;
        internal Label Label2;
        internal Button _DefaultsButton;
        internal Button _ResetValueButton;
    }
}
