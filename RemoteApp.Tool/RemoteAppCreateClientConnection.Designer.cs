using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace RemoteApp.Tool
{    public partial class RemoteAppCreateClientConnection : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteAppCreateClientConnection));
            _EditAfterSave = new CheckBox();
            _EditAfterSave.CheckedChanged += new EventHandler(EditAfterSave_CheckedChanged);
            SmallerIcons = new ImageList(components);
            _CreateButton = new Button();
            _CreateButton.Click += new EventHandler(CreateButton_Click);
            FileSaveRDP = new SaveFileDialog();
            CancelEditButton = new Button();
            Label13 = new Label();
            Label14 = new Label();
            Label12 = new Label();
            _ServerPort = new TextBox();
            _ServerPort.TextChanged += new EventHandler(ServerPort_TextChanged);
            _AltServerAddress = new TextBox();
            _AltServerAddress.TextChanged += new EventHandler(AltServerAddress_TextChanged);
            _ServerAddress = new TextBox();
            _ServerAddress.TextChanged += new EventHandler(ServerAddress_TextChanged);
            AttemptDirectCheckBox = new CheckBox();
            _UseRDGatewayCheckBox = new CheckBox();
            _UseRDGatewayCheckBox.CheckedChanged += new EventHandler(UseRDGatewayCheckBox_CheckedChanged);
            RDGWLabel = new Label();
            GatewayAddress = new TextBox();
            MSIRadioButton = new RadioButton();
            _RDPRadioButton = new RadioButton();
            _RDPRadioButton.CheckedChanged += new EventHandler(RDPRadioButton_CheckedChanged);
            CreateRAWebIcon = new CheckBox();
            _FTAButton = new Button();
            _FTAButton.Click += new EventHandler(FTAButton_Click);
            FileBrowserIcon = new OpenFileDialog();
            FileSaveMSI = new SaveFileDialog();
            ShortcutDesktopCheckBox = new CheckBox();
            Panel1 = new Panel();
            PerMachineRadioButton = new RadioButton();
            PerUserRadioButton = new RadioButton();
            TopLevelRadioButton = new RadioButton();
            SubfolderRadioButton = new RadioButton();
            _ShortcutStartCheckBox = new CheckBox();
            _ShortcutStartCheckBox.CheckedChanged += new EventHandler(ShortcutStartCheckBox_CheckedChanged);
            Label3 = new Label();
            Label2 = new Label();
            _ShortcutTagCheckBox = new CheckBox();
            _ShortcutTagCheckBox.CheckedChanged += new EventHandler(ShortcutTagCheckBox_CheckedChanged);
            ShortcutTagTextBox = new TextBox();
            Label4 = new Label();
            Label1 = new Label();
            _ResetButton = new Button();
            _ResetButton.Click += new EventHandler(ResetButton_Click);
            _SaveButton = new Button();
            _SaveButton.Click += new EventHandler(SaveButton_Click);
            FTACountLabel = new Label();
            _DisabledFTACheckBox = new CheckBox();
            _DisabledFTACheckBox.CheckedChanged += new EventHandler(DisabledFTACheckBox_CheckedChanged);
            _CheckBoxCreateSignedAndUnsigned = new CheckBox();
            _CheckBoxCreateSignedAndUnsigned.CheckedChanged += new EventHandler(CheckBoxCreateSignedAndUnsigned_CheckedChanged);
            CertificateComboBox = new ComboBox();
            CertificateLabel = new Label();
            _CheckBoxSignRDPEnabled = new CheckBox();
            _CheckBoxSignRDPEnabled.CheckedChanged += new EventHandler(CheckBoxSignRDPEnabled_CheckedChanged);
            TabControl = new TabControl();
            HostTabPage = new TabPage();
            OptionsTabPage = new TabPage();
            _RDPOptionsButton = new Button();
            _RDPOptionsButton.Click += new EventHandler(RDPOptionsButton_Click);
            GatewayTabPage = new TabPage();
            FileTypesTabPage = new TabPage();
            MSIOptionsTabPage = new TabPage();
            SigningTabPage = new TabPage();
            RdpsignErrorLabel = new Label();
            Panel1.SuspendLayout();
            TabControl.SuspendLayout();
            HostTabPage.SuspendLayout();
            OptionsTabPage.SuspendLayout();
            GatewayTabPage.SuspendLayout();
            FileTypesTabPage.SuspendLayout();
            MSIOptionsTabPage.SuspendLayout();
            SigningTabPage.SuspendLayout();
            SuspendLayout();
            // 
            // EditAfterSave
            // 
            _EditAfterSave.BackColor = Color.Transparent;
            _EditAfterSave.ImageIndex = 2;
            _EditAfterSave.ImageList = SmallerIcons;
            _EditAfterSave.Location = new Point(313, 13);
            _EditAfterSave.Name = "_EditAfterSave";
            _EditAfterSave.Size = new Size(165, 30);
            _EditAfterSave.TabIndex = 3;
            _EditAfterSave.Text = "Manually edit RDP file";
            _EditAfterSave.TextImageRelation = TextImageRelation.TextBeforeImage;
            _EditAfterSave.UseVisualStyleBackColor = false;
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
            // CreateButton
            // 
            _CreateButton.BackColor = Color.WhiteSmoke;
            _CreateButton.DialogResult = DialogResult.OK;
            _CreateButton.Font = new Font("Segoe UI", 9.0f);
            _CreateButton.ImageIndex = 6;
            _CreateButton.ImageList = SmallerIcons;
            _CreateButton.Location = new Point(424, 158);
            _CreateButton.Name = "_CreateButton";
            _CreateButton.Size = new Size(80, 29);
            _CreateButton.TabIndex = 9;
            _CreateButton.Text = "Create...";
            _CreateButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _CreateButton.UseVisualStyleBackColor = false;
            // 
            // FileSaveRDP
            // 
            FileSaveRDP.DefaultExt = "rdp";
            FileSaveRDP.Filter = "RDP files|*.rdp";
            // 
            // CancelEditButton
            // 
            CancelEditButton.BackColor = Color.WhiteSmoke;
            CancelEditButton.DialogResult = DialogResult.Cancel;
            CancelEditButton.Font = new Font("Segoe UI", 9.0f);
            CancelEditButton.ImageIndex = 4;
            CancelEditButton.ImageList = SmallerIcons;
            CancelEditButton.Location = new Point(351, 158);
            CancelEditButton.Name = "CancelEditButton";
            CancelEditButton.Size = new Size(67, 29);
            CancelEditButton.TabIndex = 8;
            CancelEditButton.Text = "Cancel";
            CancelEditButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            CancelEditButton.UseVisualStyleBackColor = false;
            // 
            // Label13
            // 
            Label13.AutoSize = true;
            Label13.Location = new Point(9, 53);
            Label13.Name = "Label13";
            Label13.Size = new Size(101, 15);
            Label13.TabIndex = 4;
            Label13.Text = "Alternative server:";
            // 
            // Label14
            // 
            Label14.AutoSize = true;
            Label14.Location = new Point(376, 24);
            Label14.Name = "Label14";
            Label14.Size = new Size(32, 15);
            Label14.TabIndex = 2;
            Label14.Text = "Port:";
            // 
            // Label12
            // 
            Label12.AutoSize = true;
            Label12.Location = new Point(9, 24);
            Label12.Name = "Label12";
            Label12.Size = new Size(85, 15);
            Label12.TabIndex = 0;
            Label12.Text = "Server address:";
            // 
            // ServerPort
            // 
            _ServerPort.Location = new Point(414, 21);
            _ServerPort.Name = "_ServerPort";
            _ServerPort.Size = new Size(53, 23);
            _ServerPort.TabIndex = 3;
            _ServerPort.Text = "3389";
            // 
            // AltServerAddress
            // 
            _AltServerAddress.Location = new Point(120, 50);
            _AltServerAddress.Name = "_AltServerAddress";
            _AltServerAddress.Size = new Size(347, 23);
            _AltServerAddress.TabIndex = 5;
            // 
            // ServerAddress
            // 
            _ServerAddress.Location = new Point(120, 21);
            _ServerAddress.Name = "_ServerAddress";
            _ServerAddress.Size = new Size(250, 23);
            _ServerAddress.TabIndex = 1;
            // 
            // AttemptDirectCheckBox
            // 
            AttemptDirectCheckBox.AutoSize = true;
            AttemptDirectCheckBox.Enabled = false;
            AttemptDirectCheckBox.Location = new Point(12, 70);
            AttemptDirectCheckBox.Name = "AttemptDirectCheckBox";
            AttemptDirectCheckBox.Size = new Size(326, 19);
            AttemptDirectCheckBox.TabIndex = 3;
            AttemptDirectCheckBox.Text = "Only use RD Gateway if direct connection is unsuccessful";
            AttemptDirectCheckBox.UseVisualStyleBackColor = true;
            // 
            // UseRDGatewayCheckBox
            // 
            _UseRDGatewayCheckBox.AutoSize = true;
            _UseRDGatewayCheckBox.Location = new Point(12, 16);
            _UseRDGatewayCheckBox.Name = "_UseRDGatewayCheckBox";
            _UseRDGatewayCheckBox.Size = new Size(111, 19);
            _UseRDGatewayCheckBox.TabIndex = 0;
            _UseRDGatewayCheckBox.Text = "Use RD Gateway";
            _UseRDGatewayCheckBox.UseVisualStyleBackColor = true;
            // 
            // RDGWLabel
            // 
            RDGWLabel.AutoSize = true;
            RDGWLabel.Enabled = false;
            RDGWLabel.Location = new Point(9, 44);
            RDGWLabel.Name = "RDGWLabel";
            RDGWLabel.Size = new Size(116, 15);
            RDGWLabel.TabIndex = 1;
            RDGWLabel.Text = "RD Gateway address:";
            // 
            // GatewayAddress
            // 
            GatewayAddress.Enabled = false;
            GatewayAddress.Location = new Point(138, 41);
            GatewayAddress.Name = "GatewayAddress";
            GatewayAddress.Size = new Size(328, 23);
            GatewayAddress.TabIndex = 2;
            // 
            // MSIRadioButton
            // 
            MSIRadioButton.AutoSize = true;
            MSIRadioButton.Location = new Point(12, 43);
            MSIRadioButton.Name = "MSIRadioButton";
            MSIRadioButton.Size = new Size(89, 19);
            MSIRadioButton.TabIndex = 2;
            MSIRadioButton.TabStop = true;
            MSIRadioButton.Text = "MSI installer";
            MSIRadioButton.UseVisualStyleBackColor = true;
            // 
            // RDPRadioButton
            // 
            _RDPRadioButton.AutoSize = true;
            _RDPRadioButton.Checked = true;
            _RDPRadioButton.Location = new Point(12, 18);
            _RDPRadioButton.Name = "_RDPRadioButton";
            _RDPRadioButton.Size = new Size(66, 19);
            _RDPRadioButton.TabIndex = 1;
            _RDPRadioButton.TabStop = true;
            _RDPRadioButton.Text = "RDP file";
            _RDPRadioButton.UseVisualStyleBackColor = true;
            // 
            // CreateRAWebIcon
            // 
            CreateRAWebIcon.BackColor = Color.Transparent;
            CreateRAWebIcon.ImageIndex = 5;
            CreateRAWebIcon.ImageList = SmallerIcons;
            CreateRAWebIcon.Location = new Point(313, 38);
            CreateRAWebIcon.Name = "CreateRAWebIcon";
            CreateRAWebIcon.Size = new Size(126, 30);
            CreateRAWebIcon.TabIndex = 4;
            CreateRAWebIcon.Text = "Create icon files";
            CreateRAWebIcon.TextImageRelation = TextImageRelation.TextBeforeImage;
            CreateRAWebIcon.UseVisualStyleBackColor = false;
            // 
            // FTAButton
            // 
            _FTAButton.BackColor = Color.WhiteSmoke;
            _FTAButton.ImageIndex = 2;
            _FTAButton.ImageList = SmallerIcons;
            _FTAButton.Location = new Point(298, 15);
            _FTAButton.Name = "_FTAButton";
            _FTAButton.Size = new Size(172, 29);
            _FTAButton.TabIndex = 2;
            _FTAButton.Text = "File type associations...";
            _FTAButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _FTAButton.UseVisualStyleBackColor = false;
            // 
            // FileBrowserIcon
            // 
            FileBrowserIcon.Filter = "Icons|*.exe;*.dll;*.ico|All files|*.*";
            FileBrowserIcon.Title = "Browse...";
            // 
            // FileSaveMSI
            // 
            FileSaveMSI.DefaultExt = "msi";
            FileSaveMSI.Filter = "MSI files|*.msi";
            // 
            // ShortcutDesktopCheckBox
            // 
            ShortcutDesktopCheckBox.AutoSize = true;
            ShortcutDesktopCheckBox.Checked = true;
            ShortcutDesktopCheckBox.CheckState = CheckState.Checked;
            ShortcutDesktopCheckBox.Location = new Point(118, 15);
            ShortcutDesktopCheckBox.Name = "ShortcutDesktopCheckBox";
            ShortcutDesktopCheckBox.Size = new Size(69, 19);
            ShortcutDesktopCheckBox.TabIndex = 1;
            ShortcutDesktopCheckBox.Text = "Desktop";
            ShortcutDesktopCheckBox.UseVisualStyleBackColor = true;
            // 
            // Panel1
            // 
            Panel1.BackColor = Color.Transparent;
            Panel1.Controls.Add(PerMachineRadioButton);
            Panel1.Controls.Add(PerUserRadioButton);
            Panel1.Location = new Point(91, 73);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(200, 26);
            Panel1.TabIndex = 9;
            // 
            // PerMachineRadioButton
            // 
            PerMachineRadioButton.AutoSize = true;
            PerMachineRadioButton.Checked = true;
            PerMachineRadioButton.Location = new Point(3, 3);
            PerMachineRadioButton.Name = "PerMachineRadioButton";
            PerMachineRadioButton.Size = new Size(93, 19);
            PerMachineRadioButton.TabIndex = 0;
            PerMachineRadioButton.TabStop = true;
            PerMachineRadioButton.Text = "Per-machine";
            PerMachineRadioButton.UseVisualStyleBackColor = true;
            // 
            // PerUserRadioButton
            // 
            PerUserRadioButton.AutoSize = true;
            PerUserRadioButton.Location = new Point(100, 3);
            PerUserRadioButton.Name = "PerUserRadioButton";
            PerUserRadioButton.Size = new Size(69, 19);
            PerUserRadioButton.TabIndex = 1;
            PerUserRadioButton.TabStop = true;
            PerUserRadioButton.Text = "Per-user";
            PerUserRadioButton.UseVisualStyleBackColor = true;
            // 
            // TopLevelRadioButton
            // 
            TopLevelRadioButton.AutoSize = true;
            TopLevelRadioButton.Location = new Point(368, 14);
            TopLevelRadioButton.Name = "TopLevelRadioButton";
            TopLevelRadioButton.Size = new Size(71, 19);
            TopLevelRadioButton.TabIndex = 4;
            TopLevelRadioButton.Text = "Top level";
            TopLevelRadioButton.UseVisualStyleBackColor = true;
            // 
            // SubfolderRadioButton
            // 
            SubfolderRadioButton.AutoSize = true;
            SubfolderRadioButton.Checked = true;
            SubfolderRadioButton.Location = new Point(286, 14);
            SubfolderRadioButton.Name = "SubfolderRadioButton";
            SubfolderRadioButton.Size = new Size(76, 19);
            SubfolderRadioButton.TabIndex = 3;
            SubfolderRadioButton.TabStop = true;
            SubfolderRadioButton.Text = "Subfolder";
            SubfolderRadioButton.UseVisualStyleBackColor = true;
            // 
            // ShortcutStartCheckBox
            // 
            _ShortcutStartCheckBox.AutoSize = true;
            _ShortcutStartCheckBox.Checked = true;
            _ShortcutStartCheckBox.CheckState = CheckState.Checked;
            _ShortcutStartCheckBox.Location = new Point(193, 15);
            _ShortcutStartCheckBox.Name = "_ShortcutStartCheckBox";
            _ShortcutStartCheckBox.Size = new Size(87, 19);
            _ShortcutStartCheckBox.TabIndex = 2;
            _ShortcutStartCheckBox.Text = "Start Menu:";
            _ShortcutStartCheckBox.UseVisualStyleBackColor = true;
            // 
            // Label3
            // 
            Label3.AutoSize = true;
            Label3.Location = new Point(291, 47);
            Label3.Name = "Label3";
            Label3.Size = new Size(11, 15);
            Label3.TabIndex = 7;
            Label3.Text = ")";
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Location = new Point(112, 46);
            Label2.Name = "Label2";
            Label2.Size = new Size(11, 15);
            Label2.TabIndex = 4;
            Label2.Text = "(";
            // 
            // ShortcutTagCheckBox
            // 
            _ShortcutTagCheckBox.AutoSize = true;
            _ShortcutTagCheckBox.Checked = true;
            _ShortcutTagCheckBox.CheckState = CheckState.Checked;
            _ShortcutTagCheckBox.Location = new Point(15, 45);
            _ShortcutTagCheckBox.Name = "_ShortcutTagCheckBox";
            _ShortcutTagCheckBox.Size = new Size(94, 19);
            _ShortcutTagCheckBox.TabIndex = 5;
            _ShortcutTagCheckBox.Text = "Shortcut tag:";
            _ShortcutTagCheckBox.UseVisualStyleBackColor = true;
            // 
            // ShortcutTagTextBox
            // 
            ShortcutTagTextBox.Location = new Point(129, 43);
            ShortcutTagTextBox.Name = "ShortcutTagTextBox";
            ShortcutTagTextBox.Size = new Size(156, 23);
            ShortcutTagTextBox.TabIndex = 6;
            ShortcutTagTextBox.Text = "remote";
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.Location = new Point(9, 78);
            Label4.Name = "Label4";
            Label4.Size = new Size(75, 15);
            Label4.TabIndex = 8;
            Label4.Text = "Install scope:";
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Location = new Point(9, 16);
            Label1.Name = "Label1";
            Label1.Size = new Size(103, 15);
            Label1.TabIndex = 0;
            Label1.Text = "Place shortcuts in:";
            // 
            // ResetButton
            // 
            _ResetButton.BackColor = Color.WhiteSmoke;
            _ResetButton.Font = new Font("Segoe UI", 9.0f);
            _ResetButton.ImageIndex = 3;
            _ResetButton.ImageList = SmallerIcons;
            _ResetButton.Location = new Point(126, 158);
            _ResetButton.Name = "_ResetButton";
            _ResetButton.Size = new Size(121, 29);
            _ResetButton.TabIndex = 7;
            _ResetButton.Text = "Reset to default";
            _ResetButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _ResetButton.UseVisualStyleBackColor = false;
            // 
            // SaveButton
            // 
            _SaveButton.BackColor = Color.WhiteSmoke;
            _SaveButton.Font = new Font("Segoe UI", 9.0f);
            _SaveButton.ImageIndex = 0;
            _SaveButton.ImageList = SmallerIcons;
            _SaveButton.Location = new Point(12, 158);
            _SaveButton.Name = "_SaveButton";
            _SaveButton.Size = new Size(108, 29);
            _SaveButton.TabIndex = 6;
            _SaveButton.Text = "Save settings";
            _SaveButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _SaveButton.UseVisualStyleBackColor = false;
            // 
            // FTACountLabel
            // 
            FTACountLabel.AutoSize = true;
            FTACountLabel.Location = new Point(163, 22);
            FTACountLabel.Name = "FTACountLabel";
            FTACountLabel.Size = new Size(52, 15);
            FTACountLabel.TabIndex = 1;
            FTACountLabel.Text = "Count: 0";
            // 
            // DisabledFTACheckBox
            // 
            _DisabledFTACheckBox.AutoSize = true;
            _DisabledFTACheckBox.Location = new Point(12, 21);
            _DisabledFTACheckBox.Name = "_DisabledFTACheckBox";
            _DisabledFTACheckBox.Size = new Size(71, 19);
            _DisabledFTACheckBox.TabIndex = 0;
            _DisabledFTACheckBox.Text = "Disabled";
            _DisabledFTACheckBox.UseVisualStyleBackColor = true;
            // 
            // CheckBoxCreateSignedAndUnsigned
            // 
            _CheckBoxCreateSignedAndUnsigned.AutoSize = true;
            _CheckBoxCreateSignedAndUnsigned.Location = new Point(12, 42);
            _CheckBoxCreateSignedAndUnsigned.Name = "_CheckBoxCreateSignedAndUnsigned";
            _CheckBoxCreateSignedAndUnsigned.Size = new Size(175, 19);
            _CheckBoxCreateSignedAndUnsigned.TabIndex = 1;
            _CheckBoxCreateSignedAndUnsigned.Text = "Create Signed and Unsigned";
            _CheckBoxCreateSignedAndUnsigned.UseVisualStyleBackColor = true;
            // 
            // CertificateComboBox
            // 
            CertificateComboBox.BackColor = Color.WhiteSmoke;
            CertificateComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            CertificateComboBox.FormattingEnabled = true;
            CertificateComboBox.Location = new Point(280, 15);
            CertificateComboBox.Name = "CertificateComboBox";
            CertificateComboBox.Size = new Size(191, 23);
            CertificateComboBox.TabIndex = 3;
            // 
            // CertificateLabel
            // 
            CertificateLabel.AutoSize = true;
            CertificateLabel.Location = new Point(210, 18);
            CertificateLabel.Name = "CertificateLabel";
            CertificateLabel.Size = new Size(64, 15);
            CertificateLabel.TabIndex = 2;
            CertificateLabel.Text = "Certificate:";
            // 
            // CheckBoxSignRDPEnabled
            // 
            _CheckBoxSignRDPEnabled.AutoSize = true;
            _CheckBoxSignRDPEnabled.Location = new Point(12, 17);
            _CheckBoxSignRDPEnabled.Name = "_CheckBoxSignRDPEnabled";
            _CheckBoxSignRDPEnabled.Size = new Size(93, 19);
            _CheckBoxSignRDPEnabled.TabIndex = 0;
            _CheckBoxSignRDPEnabled.Text = "Sign RDP file";
            _CheckBoxSignRDPEnabled.UseVisualStyleBackColor = true;
            // 
            // TabControl
            // 
            TabControl.Controls.Add(HostTabPage);
            TabControl.Controls.Add(OptionsTabPage);
            TabControl.Controls.Add(GatewayTabPage);
            TabControl.Controls.Add(FileTypesTabPage);
            TabControl.Controls.Add(MSIOptionsTabPage);
            TabControl.Controls.Add(SigningTabPage);
            TabControl.Location = new Point(12, 12);
            TabControl.Name = "TabControl";
            TabControl.SelectedIndex = 0;
            TabControl.Size = new Size(492, 137);
            TabControl.TabIndex = 10;
            // 
            // HostTabPage
            // 
            HostTabPage.Controls.Add(Label13);
            HostTabPage.Controls.Add(_ServerAddress);
            HostTabPage.Controls.Add(Label14);
            HostTabPage.Controls.Add(_AltServerAddress);
            HostTabPage.Controls.Add(Label12);
            HostTabPage.Controls.Add(_ServerPort);
            HostTabPage.Location = new Point(4, 24);
            HostTabPage.Name = "HostTabPage";
            HostTabPage.Padding = new Padding(3);
            HostTabPage.Size = new Size(484, 109);
            HostTabPage.TabIndex = 0;
            HostTabPage.Text = "Host";
            HostTabPage.UseVisualStyleBackColor = true;
            // 
            // OptionsTabPage
            // 
            OptionsTabPage.Controls.Add(MSIRadioButton);
            OptionsTabPage.Controls.Add(_RDPRadioButton);
            OptionsTabPage.Controls.Add(_EditAfterSave);
            OptionsTabPage.Controls.Add(CreateRAWebIcon);
            OptionsTabPage.Location = new Point(4, 24);
            OptionsTabPage.Name = "OptionsTabPage";
            OptionsTabPage.Padding = new Padding(3);
            OptionsTabPage.Size = new Size(484, 109);
            OptionsTabPage.TabIndex = 1;
            OptionsTabPage.Text = "Options";
            OptionsTabPage.UseVisualStyleBackColor = true;
            // 
            // RDPOptionsButton
            // 
            _RDPOptionsButton.BackColor = Color.WhiteSmoke;
            _RDPOptionsButton.ImageIndex = 6;
            _RDPOptionsButton.ImageList = SmallerIcons;
            _RDPOptionsButton.Location = new Point(12, 69);
            _RDPOptionsButton.Name = "_RDPOptionsButton";
            _RDPOptionsButton.Size = new Size(118, 29);
            _RDPOptionsButton.TabIndex = 5;
            _RDPOptionsButton.Text = "RDP options...";
            _RDPOptionsButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _RDPOptionsButton.UseVisualStyleBackColor = false;
            OptionsTabPage.Controls.Add(_RDPOptionsButton);
            // 
            // GatewayTabPage
            // 
            GatewayTabPage.Controls.Add(AttemptDirectCheckBox);
            GatewayTabPage.Controls.Add(_UseRDGatewayCheckBox);
            GatewayTabPage.Controls.Add(GatewayAddress);
            GatewayTabPage.Controls.Add(RDGWLabel);
            GatewayTabPage.Location = new Point(4, 24);
            GatewayTabPage.Name = "GatewayTabPage";
            GatewayTabPage.Padding = new Padding(3);
            GatewayTabPage.Size = new Size(484, 109);
            GatewayTabPage.TabIndex = 2;
            GatewayTabPage.Text = "Gateway";
            GatewayTabPage.UseVisualStyleBackColor = true;
            // 
            // FileTypesTabPage
            // 
            FileTypesTabPage.Controls.Add(FTACountLabel);
            FileTypesTabPage.Controls.Add(_FTAButton);
            FileTypesTabPage.Controls.Add(_DisabledFTACheckBox);
            FileTypesTabPage.Location = new Point(4, 24);
            FileTypesTabPage.Name = "FileTypesTabPage";
            FileTypesTabPage.Padding = new Padding(3);
            FileTypesTabPage.Size = new Size(484, 109);
            FileTypesTabPage.TabIndex = 3;
            FileTypesTabPage.Text = "File types";
            FileTypesTabPage.UseVisualStyleBackColor = true;
            // 
            // MSIOptionsTabPage
            // 
            MSIOptionsTabPage.Controls.Add(Panel1);
            MSIOptionsTabPage.Controls.Add(Label1);
            MSIOptionsTabPage.Controls.Add(TopLevelRadioButton);
            MSIOptionsTabPage.Controls.Add(Label4);
            MSIOptionsTabPage.Controls.Add(SubfolderRadioButton);
            MSIOptionsTabPage.Controls.Add(ShortcutTagTextBox);
            MSIOptionsTabPage.Controls.Add(_ShortcutStartCheckBox);
            MSIOptionsTabPage.Controls.Add(ShortcutDesktopCheckBox);
            MSIOptionsTabPage.Controls.Add(Label3);
            MSIOptionsTabPage.Controls.Add(_ShortcutTagCheckBox);
            MSIOptionsTabPage.Controls.Add(Label2);
            MSIOptionsTabPage.Location = new Point(4, 24);
            MSIOptionsTabPage.Name = "MSIOptionsTabPage";
            MSIOptionsTabPage.Padding = new Padding(3);
            MSIOptionsTabPage.Size = new Size(484, 109);
            MSIOptionsTabPage.TabIndex = 4;
            MSIOptionsTabPage.Text = "MSI options";
            MSIOptionsTabPage.UseVisualStyleBackColor = true;
            // 
            // SigningTabPage
            // 
            SigningTabPage.Controls.Add(RdpsignErrorLabel);
            SigningTabPage.Controls.Add(_CheckBoxCreateSignedAndUnsigned);
            SigningTabPage.Controls.Add(_CheckBoxSignRDPEnabled);
            SigningTabPage.Controls.Add(CertificateComboBox);
            SigningTabPage.Controls.Add(CertificateLabel);
            SigningTabPage.Location = new Point(4, 24);
            SigningTabPage.Name = "SigningTabPage";
            SigningTabPage.Padding = new Padding(3);
            SigningTabPage.Size = new Size(484, 109);
            SigningTabPage.TabIndex = 5;
            SigningTabPage.Text = "Signing";
            SigningTabPage.UseVisualStyleBackColor = true;
            // 
            // RdpsignErrorLabel
            // 
            RdpsignErrorLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            RdpsignErrorLabel.Location = new Point(6, 64);
            RdpsignErrorLabel.Name = "RdpsignErrorLabel";
            RdpsignErrorLabel.Size = new Size(472, 21);
            RdpsignErrorLabel.TabIndex = 4;
            RdpsignErrorLabel.Text = "RdpsignErrorLabel";
            RdpsignErrorLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // RemoteAppCreateClientConnection
            // 
            AcceptButton = _CreateButton;
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = CancelEditButton;
            ClientSize = new Size(513, 198);
            Controls.Add(TabControl);
            Controls.Add(_CreateButton);
            Controls.Add(_SaveButton);
            Controls.Add(_ResetButton);
            Controls.Add(CancelEditButton);
            Font = new Font("Segoe UI", 9.0f);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RemoteAppCreateClientConnection";
            StartPosition = FormStartPosition.CenterParent;
            Text = "RemoteAppCreateClientConnection";
            Panel1.ResumeLayout(false);
            Panel1.PerformLayout();
            TabControl.ResumeLayout(false);
            HostTabPage.ResumeLayout(false);
            HostTabPage.PerformLayout();
            OptionsTabPage.ResumeLayout(false);
            OptionsTabPage.PerformLayout();
            GatewayTabPage.ResumeLayout(false);
            GatewayTabPage.PerformLayout();
            FileTypesTabPage.ResumeLayout(false);
            FileTypesTabPage.PerformLayout();
            MSIOptionsTabPage.ResumeLayout(false);
            MSIOptionsTabPage.PerformLayout();
            SigningTabPage.ResumeLayout(false);
            SigningTabPage.PerformLayout();
            ResumeLayout(false);
        }

        private CheckBox _EditAfterSave;

        internal CheckBox EditAfterSave
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _EditAfterSave;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_EditAfterSave != null)
                {
                    _EditAfterSave.CheckedChanged -= EditAfterSave_CheckedChanged;
                }

                _EditAfterSave = value;
                if (_EditAfterSave != null)
                {
                    _EditAfterSave.CheckedChanged += EditAfterSave_CheckedChanged;
                }
            }
        }

        internal ImageList SmallerIcons;
        private Button _CreateButton;

        internal Button CreateButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _CreateButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CreateButton != null)
                {
                    _CreateButton.Click -= CreateButton_Click;
                }

                _CreateButton = value;
                if (_CreateButton != null)
                {
                    _CreateButton.Click += CreateButton_Click;
                }
            }
        }

        internal SaveFileDialog FileSaveRDP;
        internal Button CancelEditButton;
        internal Label Label13;
        internal Label Label14;
        internal Label Label12;
        private TextBox _ServerPort;

        internal TextBox ServerPort
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ServerPort;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ServerPort != null)
                {
                    _ServerPort.TextChanged -= ServerPort_TextChanged;
                }

                _ServerPort = value;
                if (_ServerPort != null)
                {
                    _ServerPort.TextChanged += ServerPort_TextChanged;
                }
            }
        }

        private TextBox _AltServerAddress;

        internal TextBox AltServerAddress
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _AltServerAddress;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_AltServerAddress != null)
                {
                    _AltServerAddress.TextChanged -= AltServerAddress_TextChanged;
                }

                _AltServerAddress = value;
                if (_AltServerAddress != null)
                {
                    _AltServerAddress.TextChanged += AltServerAddress_TextChanged;
                }
            }
        }

        private TextBox _ServerAddress;

        internal TextBox ServerAddress
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ServerAddress;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ServerAddress != null)
                {
                    _ServerAddress.TextChanged -= ServerAddress_TextChanged;
                }

                _ServerAddress = value;
                if (_ServerAddress != null)
                {
                    _ServerAddress.TextChanged += ServerAddress_TextChanged;
                }
            }
        }

        private CheckBox _UseRDGatewayCheckBox;

        internal CheckBox UseRDGatewayCheckBox
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _UseRDGatewayCheckBox;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_UseRDGatewayCheckBox != null)
                {
                    _UseRDGatewayCheckBox.CheckedChanged -= UseRDGatewayCheckBox_CheckedChanged;
                }

                _UseRDGatewayCheckBox = value;
                if (_UseRDGatewayCheckBox != null)
                {
                    _UseRDGatewayCheckBox.CheckedChanged += UseRDGatewayCheckBox_CheckedChanged;
                }
            }
        }

        internal Label RDGWLabel;
        internal TextBox GatewayAddress;
        internal RadioButton MSIRadioButton;
        private RadioButton _RDPRadioButton;

        internal RadioButton RDPRadioButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _RDPRadioButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_RDPRadioButton != null)
                {
                    _RDPRadioButton.CheckedChanged -= RDPRadioButton_CheckedChanged;
                }

                _RDPRadioButton = value;
                if (_RDPRadioButton != null)
                {
                    _RDPRadioButton.CheckedChanged += RDPRadioButton_CheckedChanged;
                }
            }
        }

        private Button _FTAButton;

        internal Button FTAButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _FTAButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_FTAButton != null)
                {
                    _FTAButton.Click -= FTAButton_Click;
                }

                _FTAButton = value;
                if (_FTAButton != null)
                {
                    _FTAButton.Click += FTAButton_Click;
                }
            }
        }

        internal OpenFileDialog FileBrowserIcon;
        internal SaveFileDialog FileSaveMSI;
        internal CheckBox AttemptDirectCheckBox;
        internal CheckBox ShortcutDesktopCheckBox;
        private CheckBox _ShortcutStartCheckBox;

        internal CheckBox ShortcutStartCheckBox
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ShortcutStartCheckBox;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ShortcutStartCheckBox != null)
                {
                    _ShortcutStartCheckBox.CheckedChanged -= ShortcutStartCheckBox_CheckedChanged;
                }

                _ShortcutStartCheckBox = value;
                if (_ShortcutStartCheckBox != null)
                {
                    _ShortcutStartCheckBox.CheckedChanged += ShortcutStartCheckBox_CheckedChanged;
                }
            }
        }

        private CheckBox _ShortcutTagCheckBox;

        internal CheckBox ShortcutTagCheckBox
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ShortcutTagCheckBox;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ShortcutTagCheckBox != null)
                {
                    _ShortcutTagCheckBox.CheckedChanged -= ShortcutTagCheckBox_CheckedChanged;
                }

                _ShortcutTagCheckBox = value;
                if (_ShortcutTagCheckBox != null)
                {
                    _ShortcutTagCheckBox.CheckedChanged += ShortcutTagCheckBox_CheckedChanged;
                }
            }
        }

        internal TextBox ShortcutTagTextBox;
        internal Label Label1;
        internal Label Label3;
        internal Label Label2;
        internal RadioButton TopLevelRadioButton;
        internal RadioButton SubfolderRadioButton;
        internal Button _RDPOptionsButton;
        private Button _ResetButton;

        internal Button ResetButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ResetButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ResetButton != null)
                {
                    _ResetButton.Click -= ResetButton_Click;
                }

                _ResetButton = value;
                if (_ResetButton != null)
                {
                    _ResetButton.Click += ResetButton_Click;
                }
            }
        }

        private Button _SaveButton;

        internal Button SaveButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _SaveButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_SaveButton != null)
                {
                    _SaveButton.Click -= SaveButton_Click;
                }

                _SaveButton = value;
                if (_SaveButton != null)
                {
                    _SaveButton.Click += SaveButton_Click;
                }
            }
        }

        internal CheckBox CreateRAWebIcon;
        internal Panel Panel1;
        internal RadioButton PerMachineRadioButton;
        internal RadioButton PerUserRadioButton;
        internal Label Label4;
        private CheckBox _DisabledFTACheckBox;

        internal CheckBox DisabledFTACheckBox
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _DisabledFTACheckBox;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_DisabledFTACheckBox != null)
                {
                    _DisabledFTACheckBox.CheckedChanged -= DisabledFTACheckBox_CheckedChanged;
                }

                _DisabledFTACheckBox = value;
                if (_DisabledFTACheckBox != null)
                {
                    _DisabledFTACheckBox.CheckedChanged += DisabledFTACheckBox_CheckedChanged;
                }
            }
        }

        internal Label FTACountLabel;
        private CheckBox _CheckBoxSignRDPEnabled;

        internal CheckBox CheckBoxSignRDPEnabled
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _CheckBoxSignRDPEnabled;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CheckBoxSignRDPEnabled != null)
                {
                    _CheckBoxSignRDPEnabled.CheckedChanged -= CheckBoxSignRDPEnabled_CheckedChanged;
                }

                _CheckBoxSignRDPEnabled = value;
                if (_CheckBoxSignRDPEnabled != null)
                {
                    _CheckBoxSignRDPEnabled.CheckedChanged += CheckBoxSignRDPEnabled_CheckedChanged;
                }
            }
        }

        internal ComboBox CertificateComboBox;
        internal Label CertificateLabel;
        private CheckBox _CheckBoxCreateSignedAndUnsigned;

        internal CheckBox CheckBoxCreateSignedAndUnsigned
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _CheckBoxCreateSignedAndUnsigned;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CheckBoxCreateSignedAndUnsigned != null)
                {
                    _CheckBoxCreateSignedAndUnsigned.CheckedChanged -= CheckBoxCreateSignedAndUnsigned_CheckedChanged;
                }

                _CheckBoxCreateSignedAndUnsigned = value;
                if (_CheckBoxCreateSignedAndUnsigned != null)
                {
                    _CheckBoxCreateSignedAndUnsigned.CheckedChanged += CheckBoxCreateSignedAndUnsigned_CheckedChanged;
                }
            }
        }

        internal TabControl TabControl;
        internal TabPage HostTabPage;
        internal TabPage OptionsTabPage;
        internal TabPage GatewayTabPage;
        internal TabPage FileTypesTabPage;
        internal TabPage MSIOptionsTabPage;
        internal TabPage SigningTabPage;
        internal Label RdpsignErrorLabel;
    }
}