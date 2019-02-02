using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Kalender
{
    public partial class FrmSettings : Form
    {
        public FrmSettings(bool topMost, bool trayModus, char hkLetter)
        {
            InitializeComponent();
            TopMost = ckbTopMost.Checked = topMost;
            ckbTrayModus.Checked = trayModus;
            cmbHotkeyLetter.Text = hkLetter.ToString();
        }

        private void CkbTrayModus_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbTrayModus.Checked)
            {
                lblHotkey.Enabled = true;
                cmbHotkeyLetter.Enabled = true;
                cmbHotkeyLetter.Focus(); //cmbxHotkey.SelectedItem = "A";
            }
            else
            {
                lblHotkey.Enabled = false;
                cmbHotkeyLetter.Enabled = false;
            }
        }

        private void CmbHotkeyLetter_KeyPress(object sender, KeyPressEventArgs e)
        {// "MaxLength" ist in Eigenschaften auf "1" gesetzt worden. 
            e.Handled = !Char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (Char.IsLetter(e.KeyChar)) { e.KeyChar = Char.ToUpper(e.KeyChar); } // nur Großbuchstaben
        }

        public bool AlwaysOnTop { get { return ckbTopMost.Checked; } }
        public bool TrayModus { get { return ckbTrayModus.Checked; } }
        public char HKLetter { get { return Convert.ToChar(new Regex(@"^[A-Z]$").IsMatch(cmbHotkeyLetter.Text) ? cmbHotkeyLetter.Text : "-"); } }

        private void FrmSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4) { DialogResult = DialogResult.Cancel; }
        }

    }
}
