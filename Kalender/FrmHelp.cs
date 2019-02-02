using System;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

namespace Kalender
{
    public partial class FrmHelp : Form
    {
        public FrmHelp(bool topMost)
        {
            InitializeComponent();
            TopMost = topMost;
            Text = "Version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void FrmHelp_Click(object sender, EventArgs e) { Close(); }

        private void FrmHelp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter || e.KeyCode == Keys.F1) { Close(); }
        }

        private void Label_Click(object sender, EventArgs e) { Close(); }

        private void LCopyright_Click(object sender, EventArgs e) { Close(); }

        private void LabelLine_Click(object sender, EventArgs e) { Close(); }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try { Process.Start("taskschd.msc"); }
            catch (Exception ex) { MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

    }
}
