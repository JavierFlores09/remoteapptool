using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RemoteApp.Tool
{
    public partial class RDPOptionsWindow
    {
        // Working copy of the catalogue for the current editing session.
        private RdpOption[] _changedOptions;

        // Index of the option currently shown in the detail pane.
        private int _selectedRow;

        public RDPOptionsWindow()
        {
            // VB called this implicitly for DesignerGenerated forms; C# has to do it explicitly.
            InitializeComponent();
        }

        /// <summary>
        ///     Opens the window seeded with the options currently applied to the client connection and
        ///     returns the options that differ from the .rdp defaults, as
        ///     [internal name, rdp key, type, value] rows.
        /// </summary>
        public string[,] EditAdditionalOptions(string[,] additionalOptions)
        {
            CopyOptions();
            LoadAdditionalOptions(additionalOptions);
            UpdateChangedOptions();

            _OptionsListBox.Items.Clear();
            foreach (var option in OptionCatalogue)
            {
                _OptionsListBox.Items.Add(option.Key);
            }

            _OptionsListBox.SelectedIndex = _selectedRow;
            LoadSelected();

            ShowDialog();

            // The form instance lives in Forms (see Forms.cs), so it must not be disposed here:
            // it is reused the next time the button is pressed.
            return ExportSavedOptions();
        }

        /// <summary>
        ///     Resets the working copy to the catalogue defaults, i.e. "write nothing".
        ///     An option only counts as selected when its value differs from its documented default,
        ///     which is what the upstream window did.
        /// </summary>
        private void CopyOptions()
        {
            _changedOptions = new RdpOption[OptionCatalogue.Length];
            for (var row = 0; row < OptionCatalogue.Length; row++)
            {
                _changedOptions[row] = OptionCatalogue[row].Clone();
                _changedOptions[row].Value = OptionCatalogue[row].DefaultValue;
            }
        }

        /// <summary>
        ///     Applies a [name, key, type, value] table on top of the working copy.
        /// </summary>
        private void LoadAdditionalOptions(string[,] additionalOptions)
        {
            if (additionalOptions == null || additionalOptions.GetLength(1) < 4)
            {
                return;
            }

            for (var row = 0; row < additionalOptions.GetLength(0); row++)
            {
                var key = additionalOptions[row, 1];
                var value = additionalOptions[row, 3];
                foreach (var option in _changedOptions)
                {
                    if (option.Key == key)
                    {
                        option.Value = value;
                    }
                }
            }
        }

        private void OptionsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_OptionsListBox.SelectedItems.Count != 1) return;
            _selectedRow = _OptionsListBox.SelectedIndex;
            LoadSelected();
        }

        private void LoadSelected()
        {
            if (_changedOptions.Length == 0) return;
            if (_selectedRow < 0 || _selectedRow >= _changedOptions.Length) return;

            var option = OptionCatalogue[_selectedRow];

            var descriptionText = "# " + option.Key + Environment.NewLine + Environment.NewLine +
                                  option.Description.Replace("\\n", Environment.NewLine);

            if (option.DefaultValue != string.Empty)
            {
                descriptionText += Environment.NewLine + Environment.NewLine + "Default: " + option.DefaultValue;
            }

            DescriptionTextBox.Text = descriptionText;
            _ValueTextBox.Text = _changedOptions[_selectedRow].Value;
        }

        private void ValueTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_changedOptions.Length == 0) return;
            if (_changedOptions[_selectedRow].Value == _ValueTextBox.Text) return;
            _changedOptions[_selectedRow].Value = _ValueTextBox.Text;
            UpdateChangedOptions();
        }

        private void UpdateChangedOptions()
        {
            _ChangedOptionsListView.Items.Clear();
            for (var row = 0; row < OptionCatalogue.Length; row++)
            {
                if (OptionCatalogue[row].DefaultValue == _changedOptions[row].Value) continue;
                var newItem = _ChangedOptionsListView.Items.Add(_changedOptions[row].Key);
                newItem.SubItems.Add(_changedOptions[row].Type);
                newItem.SubItems.Add(_changedOptions[row].Value);
            }
        }

        /// <summary>
        ///     Builds the [name, key, type, value] table of the options that were changed.
        /// </summary>
        private string[,] ExportSavedOptions()
        {
            var selectedRows = new List<int>();
            for (var row = 0; row < OptionCatalogue.Length; row++)
            {
                if (OptionCatalogue[row].DefaultValue != _changedOptions[row].Value)
                {
                    selectedRows.Add(row);
                }
            }

            var result = new string[selectedRows.Count, 4];
            for (var i = 0; i < selectedRows.Count; i++)
            {
                var row = selectedRows[i];
                result[i, 0] = _changedOptions[row].Name;
                result[i, 1] = _changedOptions[row].Key;
                result[i, 2] = _changedOptions[row].Type;
                result[i, 3] = _changedOptions[row].Value;
            }

            return result;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     Clears every option so nothing is written to the .rdp file.
        /// </summary>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            CopyOptions();
            UpdateChangedOptions();
            LoadSelected();
        }

        /// <summary>
        ///     Applies the recommended defaults, used for a new client connection.
        /// </summary>
        private void DefaultsButton_Click(object sender, EventArgs e)
        {
            CopyOptions();
            LoadAdditionalOptions(RecommendedDefaults);
            UpdateChangedOptions();
            LoadSelected();
        }

        private void ChangedOptionsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScrollToSelectedOption();
            _ChangedOptionsListView.Focus();
        }

        private void ScrollToSelectedOption()
        {
            if (_ChangedOptionsListView.SelectedItems.Count > 0)
            {
                _OptionsListBox.SelectedItem = _ChangedOptionsListView.SelectedItems[0].Text;
            }
        }

        private void ResetValueButton_Click(object sender, EventArgs e)
        {
            if (_changedOptions.Length == 0) return;
            if (_ValueTextBox.Text == OptionCatalogue[_selectedRow].DefaultValue) return;
            _changedOptions[_selectedRow].Value = OptionCatalogue[_selectedRow].DefaultValue;
            LoadSelected();
            UpdateChangedOptions();
        }
    }
}
