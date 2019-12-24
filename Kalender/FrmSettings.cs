using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Kalender
{
    public partial class FrmSettings : Form
    {
        public FrmSettings(int colorScheme, bool topMost, bool trayModus, char hkLetter, string calGoogle, string textTooltip)
        {
            InitializeComponent();
            rbutton0.Checked = colorScheme == 0 ? true : false;
            rbutton1.Checked = colorScheme == 1 ? true : false;
            rbutton2.Checked = colorScheme == 2 ? true : false;
            rbutton3.Checked = colorScheme == 3 ? true : false;
            TopMost = ckbTopMost.Checked = topMost;
            ckbTrayModus.Checked = trayModus;
            cmbHotkeyLetter.Text = hkLetter.ToString();
            tbGoogleCal.Text = calGoogle;
            tbTooltipText.Text = textTooltip;
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

        public int ColorScheme { get { return rbutton3.Checked ? 3 : rbutton2.Checked ? 2 : rbutton1.Checked ? 1 : 0; } }
        public bool AlwaysOnTop { get { return ckbTopMost.Checked; } }
        public bool TrayModus { get { return ckbTrayModus.Checked; } }
        public char HKLetter { get { return Convert.ToChar(new Regex(@"^[A-Z]$").IsMatch(cmbHotkeyLetter.Text) ? cmbHotkeyLetter.Text : "-"); } }
        public string GoogleCal { get { return tbGoogleCal.Text; } }
        public string TooltipText { get { return tbTooltipText.Text; } }

        private void FrmSettings_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.F4) { DialogResult = DialogResult.Cancel; } }

        private void TbGoogleCal_TextChanged(object sender, EventArgs e)
        {
            if (tbGoogleCal.TextLength.Equals(0)) { tbTooltipText.Enabled = false; }
            else { tbTooltipText.Enabled = true; }
        }

    }
}
