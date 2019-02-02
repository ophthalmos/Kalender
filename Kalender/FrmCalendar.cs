using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Kalender
{
    public partial class FrmCalendar : Form
    {
        private static string appName = Application.ProductName; // "Kalender";
        private ClsSpecialDays holidays;
        private Point lastMouseLocation = new Point(-1, -1); // Die letzte Mausposition. Wird zum Verhindern von Flackern benötigt.
        private Setting setting;
        private Config config = new Config(Path.Combine(Application.StartupPath, appName + ".xml"));
        private string appLocation = "FormLocation";
        private string savedFormat = "SavedFormat";
        private string lastView = "LastView";
        private string topMost = "AlwaysOnTop";
        private string trayModus = "TrayModus";
        private string hkLetter = "HotkeyLetter";
        private char letterHKChar = 'K';
        private bool isTopMost = true;
        private bool isTrayModus = true;
        private string formLocation = "10,10"; // default
        private int currentView = 1;
        private DateTime lastMouseClick = DateTime.Now;

        private int dateFormatIndex = 3; // default
        private String[] dateFormat = {
            "d.M.", // 1.1.
            "d.M.yy", // 1.1.11
            "dd.MM.yy", // 01.01.11
            "d.M.yyyy", // 1.11.2011
            "dd.MM.yyyy", // 01.01.2011
            "d. MMMM yyyy", // 1. Januar 2011
            "ddd., d.M.yyyy", // So., 1.1.12
            "dddd, 'den' d. MMMM yyyy", //Sonntag, den 1.Januar 2012
        };

        public FrmCalendar()
        {
            InitializeComponent();
            foreach (ToolStripMenuItem menuItem in menuStrip.Items)
            {
                ((ToolStripDropDownMenu)menuItem.DropDown).ShowImageMargin = false;
                ((ToolStripDropDownMenu)menuItem.DropDown).BackColor = SystemColors.ButtonFace;
            }
            config.Sections.Add(appName); // Konfigurations-Sektionen und Einstellungen definieren
            config.Sections[appName].Settings.Add(savedFormat, dateFormatIndex.ToString());
            config.Sections[appName].Settings.Add(appLocation, formLocation.ToString());
            config.Sections[appName].Settings.Add(lastView, currentView.ToString());
            config.Sections[appName].Settings.Add(topMost, isTopMost.ToString());
            config.Sections[appName].Settings.Add(trayModus, isTrayModus.ToString());
            config.Sections[appName].Settings.Add(hkLetter, letterHKChar.ToString());
            config.Load(); // Datei einlesen
            try
            {
                dateFormatIndex = Convert.ToInt32(GetConfigValue(savedFormat));
                currentView = Convert.ToInt32(GetConfigValue(lastView));
                isTopMost = Convert.ToBoolean(GetConfigValue(topMost));
                isTrayModus = Convert.ToBoolean(GetConfigValue(trayModus));
                letterHKChar = Convert.ToChar(GetConfigValue(hkLetter)); //   (new Regex(@"^[A-Z]$").IsMatch(hkLetter) ? hkLetter : "A"));
                formLocation = (GetConfigValue(appLocation));
            }
            catch (Exception ex) // when (ex is FormatException || ex is KeyNotFoundException)
            {
                MessageBox.Show("Fehler beim Lesen der Konfiguration (" + appName + ".xml)." + Environment.NewLine + Environment.NewLine + ex.Message, appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateFormatIndex = 3;
                currentView = 1;
                isTopMost = true;
                isTrayModus = true;
                letterHKChar = 'K';
                formLocation = "10,10";
            }
            monthCalendar.SetCalendarDimensions(1, currentView);
            TopMost = isTopMost;
            btnSendDate.Text = monthCalendar.SelectionRange.Start.ToString(dateFormat[dateFormatIndex], DateTimeFormatInfo.CurrentInfo);
            GetHolidays(monthCalendar.SelectionStart.Year);
        }

        private void DatumÜbertragenToolStripMenuItem_Click(object sender, EventArgs e) { BtnSendDate_Click(null, null); }
        private void BeendenToolStripMenuItem_Click(object sender, EventArgs e) { Close(); }
        private void FormatÄndernToolStripMenuItem_Click(object sender, EventArgs e) { BtnFormatDate_Click(null, null); }
        private void GeheZuToolStripMenuItem_Click(object sender, EventArgs e) { monthCalendar.SetDate(DateTime.Now); }
        private void MonatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            monthCalendar.SetCalendarDimensions(1, 1);
            Utilities.CenterMouseOverControl(btnSendDate, -45);
        }
        private void MonateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            monthCalendar.SetCalendarDimensions(1, 3);
            Utilities.CenterMouseOverControl(btnSendDate, -45);
        }
        private void MenuStrip_MenuActivate(object sender, EventArgs e)
        {
            if (monthCalendar.CalendarDimensions.Height == 3)
            {
                monatToolStripMenuItem.Visible = true;
                monateToolStripMenuItem.Visible = false;
            }
            else
            {
                monatToolStripMenuItem.Visible = false;
                monateToolStripMenuItem.Visible = true;
            }
            if (monthCalendar.SelectionRange.Start == monthCalendar.TodayDate)
            { geheZuToolStripMenuItem.Enabled = false; }
            else { geheZuToolStripMenuItem.Enabled = true; }
        }

        private void MonthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            GetHolidays(e.Start.Year);
            btnSendDate.Text = FormatDateRange(monthCalendar.SelectionRange.Start, monthCalendar.SelectionRange.End);
            Utilities.DateDiff ddf = Utilities.CalcDateDiff(monthCalendar.SelectionStart, (monthCalendar.SelectionRange.Start != monthCalendar.SelectionRange.End ? monthCalendar.SelectionEnd.AddDays(1) : monthCalendar.TodayDate));
            lblInfo.ForeColor = monthCalendar.SelectionRange.Start != monthCalendar.SelectionRange.End ? Color.DarkRed : SystemColors.ControlText;
            lblInfo.Text = "" + (!ddf.years.Equals(0) ? ddf.years.ToString() + ((ddf.years.Equals(1) ? " Jahr" : " Jahre") +
                (ddf.months.Equals(0) && ddf.days.Equals(0) ? "" : ", ")) : "") + (!ddf.months.Equals(0) ? ddf.months.ToString() +
                ((ddf.months.Equals(1) ? " Monat" : " Monate") + (ddf.days.Equals(0) ? "" : ", ")) : "") +
                (!ddf.days.Equals(0) ? ddf.days.ToString() + (ddf.days.Equals(1) ? " Tag" : " Tage") : "");
            lastMouseClick = DateTime.Now.Subtract(new TimeSpan(1, 0, 0));
        }

        private void FrmCalendar_Load(object sender, EventArgs e)
        {
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            string[] coords = formLocation.Split(',');
            Point point = new Point(int.Parse(coords[0]), int.Parse(coords[1]));
            if (point.IsEmpty)
            {// CenterOnSreen wenn keine gespeicherten Werte vorhanden sind
                Location = new Point((screen.Height / 2) - (Height / 2), (screen.Width / 2) - (Width / 2));
            }
            else
            {// Form komplett innerhalb der WorkingArea angezeigt werden
                int xPos = point.X;
                int yPos = point.Y;
                xPos = xPos < 0 ? 0 : xPos + Width > screen.Width ? screen.Width - Width : xPos;
                yPos = yPos < 0 ? 0 : yPos + Height > screen.Height ? yPos = screen.Height - Height : yPos;
                Location = new Point(xPos, yPos);
            }
            if (isTrayModus && letterHKChar >= 'A' && letterHKChar <= 'Z')
            {
                if (NativeMethods.RegisterHotKey(Handle, NativeMethods.HOTKEY_ID, (uint)(NativeMethods.Modifiers.Control | NativeMethods.Modifiers.Win), (uint)(Keys)letterHKChar) == false)
                {
                    MessageBox.Show("Fehler beim Registrieren des Hotkey!\nDer Hotkey wird wahrscheinlich bereits\nvon einem anderen Programm benutzt.", appName);
                    letterHKChar = '-';
                }
                else
                {
                    anzeigenToolStripMenuItem.ShortcutKeyDisplayString = "Strg+Win+" + Char.ToUpper(letterHKChar);
                }
            }
        }

        private void FrmCalendar_Shown(object sender, EventArgs e)
        {
            if (isTrayModus) { Visible = false; notifyIcon.Visible = true; }
            else { Utilities.CenterMouseOverControl(btnSendDate, -45); }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F4 | Keys.Alt:
                    {
                        Application.Exit();
                        return true;
                    }
                case Keys.Escape:
                    {
                        Close();
                        return true;
                    }
                case Keys.F2:
                    {
                        BtnFormatDate_Click(null, null);
                        return true;
                    }
                case Keys.F3:
                    {
                        if (monthCalendar.CalendarDimensions.Height == 3)
                        {
                            monthCalendar.SetCalendarDimensions(1, 1);
                            Utilities.CenterMouseOverControl(btnSendDate, -45);
                        }
                        else
                        {
                            monthCalendar.SetCalendarDimensions(1, 3);
                            Utilities.CenterMouseOverControl(btnSendDate, -45);
                        }
                        return true;
                    }
                case Keys.F4:
                    {
                        EinstellungenToolStripMenuItem_Click(null, null);
                        return true;
                    }
                case Keys.F5:
                    {
                        monthCalendar.SetDate(DateTime.Now);
                        return true;
                    }
                case Keys.Space:
                    {
                        dateFormatIndex++;
                        dateFormatIndex = dateFormatIndex >= dateFormat.Length ? 0 : dateFormatIndex;
                        btnSendDate.Text = FormatDateRange(monthCalendar.SelectionRange.Start, monthCalendar.SelectionRange.End);
                        return true;
                    }
            }// switch (keyData)
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FrmCalendar_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                Activate();
                monthCalendar.SetDate(DateTime.Now);
                monthCalendar.Focus();
            }
            else { notifyIcon.Text = FormatDateRange(monthCalendar.TodayDate, monthCalendar.TodayDate); }
        }

        private void FrmCalendar_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isTrayModus && e.CloseReason == CloseReason.UserClosing) // Anwendung kann nur über «Beenden» in cntxtMenuTNA beendet werden!
            {
                e.Cancel = true; // das Schließen des Formulars verhindern
                notifyIcon.Visible = true;
                Hide();
            }
            else
            {
                if (Char.IsLetter(letterHKChar)) { NativeMethods.UnregisterHotKey(Handle, NativeMethods.HOTKEY_ID); }
                SaveConfigValue(appLocation, string.Concat(Location.X.ToString(), ", ", Location.Y.ToString()));
                SaveConfigValue(savedFormat, dateFormatIndex.ToString());
                SaveConfigValue(lastView, monthCalendar.CalendarDimensions.Height.ToString());
                SaveConfigValue(topMost, isTopMost.ToString());
                SaveConfigValue(trayModus, isTrayModus.ToString());
                SaveConfigValue(hkLetter, letterHKChar.ToString());
            }
        }

        private void SaveConfigValue(string Name, string String)
        {
            try
            {
                setting = config.Sections[appName].Settings[Name];
                setting.Value = String; // Den Wert ändern
                config.Save(); // Datei speichern
            }
            catch (KeyNotFoundException ex) { MessageBox.Show(Name + " (" + setting.Value + ")" + ": " + ex.Message, "Error"); }
        }

        private string GetConfigValue(string Name)
        {
            setting = config.Sections[appName].Settings[Name];
            if (setting.NotInFile)
            {
                setting.Value = setting.DefaultValue;
                config.Save(); // Datei speichern
            }
            return setting.Value;
        }

        private void BtnSendDate_Click(object sender, EventArgs e)
        {
            Hide();
            Clipboard.Clear();
            string date = FormatDateRange(monthCalendar.SelectionRange.Start, monthCalendar.SelectionRange.End);
            if (!Utilities.SetClipboardText(date))
            {
                MessageBox.Show("Es ist ein Fehler aufgetreten.\nVersuchen Sie es noch einmal.", "Error");
                Show();
            }
            else
            {
                SendKeys.SendWait("^(v)");
                SendKeys.Flush();
                Close();
            }
        }

        private void BtnFormatDate_Click(object sender, EventArgs e)
        {
            string[] format = dateFormat.Where((val, idx) => idx != dateFormatIndex).ToArray();
            contextMenu.Items.Clear();
            for (int i = 0; i < format.Length; i++)
            {
                var menuItem = new ToolStripMenuItem(monthCalendar.SelectionRange.Start.ToString(format[i], DateTimeFormatInfo.CurrentInfo))
                {
                    ShortcutKeys = Keys.F2,
                    ShowShortcutKeys = false,
                    Tag = i
                };
                menuItem.Click += new EventHandler(SelectFormatEvent);
                contextMenu.Items.Add(menuItem);
            }
            contextMenu.Show(this, new Point(btnFormatDate.Location.X - contextMenu.Width / 2, btnFormatDate.Parent.Location.Y - contextMenu.Height / 2));
        }

        private void SelectFormatEvent(object sender, EventArgs e)
        {
            int newFormatIndex = Convert.ToInt16(((ToolStripItem)sender).Tag);
            dateFormatIndex = newFormatIndex >= dateFormatIndex ? newFormatIndex + 1 : newFormatIndex; // enthält möglicherweise noch einen Fehler!!!
            btnSendDate.Text = FormatDateRange(monthCalendar.SelectionRange.Start, monthCalendar.SelectionRange.End);
        }

        private string FormatDateRange(DateTime startDate, DateTime endDate)
        {
            string result = endDate.ToString(dateFormat[dateFormatIndex], DateTimeFormatInfo.CurrentInfo);
            if (startDate.Date != endDate.Date)
            {
                result = String.Format(new Regex("(MMMM|ddd)").IsMatch(dateFormat[dateFormatIndex]) ? "{0} - {1}" : "{0}-{1}",
                  startDate.Year == endDate.Year && startDate.Month == endDate.Month ?
                    startDate.ToString(Regex.Replace(dateFormat[dateFormatIndex], @"\s?(y+|M+\.|MMMM)", ""), DateTimeFormatInfo.CurrentInfo) :
                    startDate.Year == endDate.Year ?
                      startDate.ToString(Regex.Replace(dateFormat[dateFormatIndex], @"\s?y+", ""), DateTimeFormatInfo.CurrentInfo) :
                      startDate.Month == endDate.Month ?
                        startDate.ToString(Regex.Replace(dateFormat[dateFormatIndex], @"\s?(M+\.|MMMM)", ""), DateTimeFormatInfo.CurrentInfo) :
                        startDate.ToString(dateFormat[dateFormatIndex], DateTimeFormatInfo.CurrentInfo), result);
            }
            return result;
        }

        private void GetHolidays(int year)
        {
            if (holidays == null || holidays.Year != year)
            {//Die speziellen Tage für das angezeigte Jahr ermitteln
                ClsSpecialDays specialDays = GermanSpecialDays.GetGermanSpecialDays(year);
                holidays = new ClsSpecialDays(year);
                foreach (var specialDay in specialDays.Values)
                {
                    if (specialDay.IsHoliday) { holidays.Add(specialDay.Key, specialDay); }
                }
                timer.Start(); // Timer starten, der etwas zeitverzögert die Feiertage ermittelt und in BoldedDates schreibt
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {// Schreibt etwas zeitverzögert die Feiertage in die BoldedDates-Eigenschaft
            timer.Stop();
            if (monthCalendar.IsHandleCreated)
            {
                DateTime[] specialDates = new DateTime[holidays.Count];
                int index = -1;
                foreach (var specialDay in holidays.Values)
                {
                    index++;
                    specialDates[index] = specialDay.Date;
                }
                monthCalendar.BoldedDates = specialDates;
            }
        }

        private void MonthCalendar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Location != lastMouseLocation)
            {
                lastMouseLocation = e.Location;
                if (holidays != null)
                {
                    DateTime date = monthCalendar.HitTest(e.X, e.Y).Time;
                    foreach (GermanSpecialDay holiday in holidays.Values)
                    {
                        if (holiday.Date.Date == date.Date)
                        {
                            toolTip.SetToolTip(monthCalendar, holiday.Name);
                            return;
                        }
                    }
                    toolTip.SetToolTip(monthCalendar, null);
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case NativeMethods.WM_HOTKEY:
                    if (Visible && !ContainsFocus)
                    {
                        BringToFront();
                        foreach (Form form in Application.OpenForms) { form.Activate(); }
                    }
                    else
                    {
                        notifyIcon.Visible = Visible = !Visible;
                        notifyIcon.Visible = !Visible;
                        if (Visible) { Utilities.CenterMouseOverControl(btnSendDate, -45); }
                    }
                    break;
            }
            base.WndProc(ref m); // Die geerbte Fenster-Methode aufrufen
        }

        private void EinstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmSettings frmSettings = new FrmSettings(isTopMost, isTrayModus, letterHKChar))
            {
                Char previousHKChar = letterHKChar;
                if (frmSettings.ShowDialog() == DialogResult.OK)
                {
                    TopMost = isTopMost = frmSettings.AlwaysOnTop;
                    isTrayModus = frmSettings.TrayModus;
                    letterHKChar = frmSettings.HKLetter;
                }
                if (isTrayModus && letterHKChar >= 'A' && letterHKChar <= 'Z' && letterHKChar != previousHKChar)
                {
                    if (Char.IsLetter(previousHKChar)) { NativeMethods.UnregisterHotKey(Handle, NativeMethods.HOTKEY_ID); }
                    if (NativeMethods.RegisterHotKey(Handle, NativeMethods.HOTKEY_ID, (uint)(NativeMethods.Modifiers.Control | NativeMethods.Modifiers.Win), (uint)(Keys)letterHKChar) == false)
                    {
                        MessageBox.Show("Fehler beim Registrieren des Hotkey!\nDer Hotkey wird wahrscheinlich bereits\nvon einem anderen Programm benutzt.", appName);
                        anzeigenToolStripMenuItem.ShortcutKeyDisplayString = String.Empty;
                        letterHKChar = '-';
                    }
                    else
                    {
                        anzeigenToolStripMenuItem.ShortcutKeyDisplayString = "Strg+Win+" + letterHKChar;
                    }
                }
                else if (!isTrayModus && Char.IsLetter(previousHKChar))
                {
                    NativeMethods.UnregisterHotKey(Handle, NativeMethods.HOTKEY_ID);
                    anzeigenToolStripMenuItem.ShortcutKeyDisplayString = String.Empty;
                }
            }
        }

        private void AnzeigenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            Activate();
            notifyIcon.Visible = false;
            Utilities.CenterMouseOverControl(btnSendDate, -45);
        }

        private void BeendenToolStripMenuItem1_Click(object sender, EventArgs e) { Application.Exit(); }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Visible = !Visible;
                notifyIcon.Visible = !Visible;
                if (Visible) { Utilities.CenterMouseOverControl(btnSendDate, -45); }
            }
        }

        private void FrmCalendar_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            using (FrmHelp frmHelp = new FrmHelp(isTopMost)) { frmHelp.ShowDialog(); }
        }

        private void FrmCalendar_HelpRequested(object sender, HelpEventArgs hlpevent)
        {// Das HelpRequested-Ereignis wird ausgelöst, wenn der Benutzer F1 drückt
            hlpevent.Handled = true;
            using (FrmHelp frmHelp = new FrmHelp(isTopMost)) { frmHelp.ShowDialog(); }
        }

        private void ContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e) { Cursor.Position = contextMenu.PointToScreen(new Point(contextMenu.Width / 2, contextMenu.Height / 2)); } // contextMenu.Items[1].Select();

        private void BtnSendDate_TextChanged(object sender, EventArgs e)
        {// Überprüfen, ob der Text in der Breite auf den Button passt
            bool flag = false;
            string text = btnSendDate.Text;
            while (btnSendDate.Width - 10 < TextRenderer.MeasureText(btnSendDate.Text, new Font(btnSendDate.Font.FontFamily, btnSendDate.Font.Size, btnSendDate.Font.Style)).Width)
            {
                btnSendDate.Text = Regex.Replace(btnSendDate.Text, @"\s[^\s]+$", "...");
                flag = true;
            }
            if (flag) { { toolTip.SetToolTip(btnSendDate, text); } }
            else { toolTip.SetToolTip(btnSendDate, null); }

        }

        private void MonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {// The MonthCalendar control sets the StandardClick bit flag to false, so the MonthCalendar will not raise the DoubleClick event.
            if ((DateTime.Now - lastMouseClick).TotalMilliseconds <= SystemInformation.DoubleClickTime)
            {
                BtnSendDate_Click(null, null);
            }
            lastMouseClick = DateTime.Now;
        }
    }
}
