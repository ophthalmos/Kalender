using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kalender
{
    public partial class FrmDiffDates : Form
    {
        public FrmDiffDates(int colorScheme)
        {
            InitializeComponent();
            textBox.BackColor = colorScheme.Equals(1) ? ColorTranslator.FromHtml("#E5F0ED") : colorScheme.Equals(2) ? ColorTranslator.FromHtml("#E9EFFF") : colorScheme.Equals(3) ? ColorTranslator.FromHtml("#FFFFEC") : Color.White;
        }

        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            int days = (dateTimePicker2.Value.Date - dateTimePicker1.Value.Date).Days;
            Utilities.DateDiff ddf = Utilities.CalcDateDiff(dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
            if (Math.Abs(days) <= 31)
            {
                textBox.Text = Math.Abs(days).Equals(1) ? days.ToString() + " Tag" : days.ToString() + " Tage";
            }
            else
            {
                textBox.Text = days.ToString() + " Tage (" +
                    (!ddf.years.Equals(0) ? ddf.years.ToString() + ((ddf.years.Equals(1) ? " Jahr" : " Jahre") +
                    (ddf.months.Equals(0) && ddf.days.Equals(0) ? "" : ", ")) : "") + (!ddf.months.Equals(0) ? ddf.months.ToString() +
                    ((ddf.months.Equals(1) ? " Monat" : " Monate") + (ddf.days.Equals(0) ? "" : ", ")) : "") +
                    (!ddf.days.Equals(0) ? ddf.days.ToString() + (ddf.days.Equals(1) ? " Tag)" : " Tage)") : ")");
            }
        }

        //protected override bool ProcessDialogKey(Keys keyData)
        //{
        //    if ((Form.ModifierKeys == Keys.None && keyData == Keys.Escape) || (Form.ModifierKeys == Keys.None && keyData == Keys.Enter))
        //    {
        //        Close();
        //        return true;
        //    }
        //    return base.ProcessDialogKey(keyData);
        //}

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Checked == false) return; // event fires twice!
            if (rb.Name.Equals(radioButton2.Name))
            {
                dateTimePicker2.Visible = false;
                numericUpDown.Visible = true;
                lblResullt.Text = "Errechnetes Datum:";
            }
            else
            {
                numericUpDown.Visible = false;
                dateTimePicker2.Visible = true;
                lblResullt.Text = "Unterschied in Tagen:";
            }
            textBox.Clear();
        }

        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            { 
            textBox.Text = dateTimePicker1.Value.AddDays((double)numericUpDown.Value).ToLongDateString();
            }
            catch (ArgumentOutOfRangeException ex) { MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information); }

        }

        private void NumericUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NumericUpDown_ValueChanged(null, null);
                e.Handled = e.SuppressKeyPress = true;
            }
        }
    }
}
