using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Kalender
{
    public partial class FrmSettings : Form
    {
        public int ColorScheme { get { return rbutton3.Checked ? 3 : rbutton2.Checked ? 2 : rbutton1.Checked ? 1 : 0; } }
        public bool AlwaysOnTop { get { return ckbTopMost.Checked; } }
        public bool AutoStart { get { return ckbAutoStart.Checked; } }
        public bool TrayModus { get { return ckbTrayModus.Checked; } }
        public char HKLetter { get { return Convert.ToChar(new Regex(@"^[A-Z]$").IsMatch(cmbHotkeyLetter.Text) ? cmbHotkeyLetter.Text : "-"); } }
        public bool ResetDate { get { return ckbResetDate.Checked; } }
        public string GoogleCal { get { return tbGoogleCal.Text; } }
        public string TooltipText { get { return tbTooltipText.Text; } }

        public FrmSettings(int colorScheme, bool topMost, bool autoStart, bool trayModus, char hkLetter, bool resetDate, string calGoogle, string textTooltip)
        {
            InitializeComponent();
            rbutton0.Checked = colorScheme == 0;
            rbutton1.Checked = colorScheme == 1;
            rbutton2.Checked = colorScheme == 2;
            rbutton3.Checked = colorScheme == 3;
            TopMost = ckbTopMost.Checked = topMost;
            ckbTrayModus.Checked = trayModus;
            ckbAutoStart.Checked = autoStart;
            cmbHotkeyLetter.Text = hkLetter.ToString();
            ckbResetDate.Checked = resetDate;
            tbGoogleCal.Text = calGoogle;
            tbTooltipText.Text = textTooltip;
            lblHotkey.Enabled = ckbResetDate.Enabled = cmbHotkeyLetter.Enabled = trayModus;
        }

        private void CkbTrayModus_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbTrayModus.Checked)
            {
                lblHotkey.Enabled = ckbResetDate.Enabled = cmbHotkeyLetter.Enabled = true;
                cmbHotkeyLetter.Focus(); //cmbxHotkey.SelectedItem = "A";
            }
            else { lblHotkey.Enabled = ckbResetDate.Enabled = cmbHotkeyLetter.Enabled = false; }
        }

        private void CmbHotkeyLetter_KeyPress(object sender, KeyPressEventArgs e)
        {// "MaxLength" ist in Eigenschaften auf "1" gesetzt worden. 
            e.Handled = !Char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (Char.IsLetter(e.KeyChar)) { e.KeyChar = Char.ToUpper(e.KeyChar); } // nur Großbuchstaben
        }

        private void FrmSettings_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.F4) { DialogResult = DialogResult.Cancel; } }

        private void TbGoogleCal_TextChanged(object sender, EventArgs e)
        {
            if (tbGoogleCal.TextLength.Equals(0)) { tbTooltipText.Enabled = false; }
            else { tbTooltipText.Enabled = true; }
        }

    }
}
