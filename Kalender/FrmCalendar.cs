using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Reflection;

namespace Kalender
{
    public partial class FrmCalendar : Form
    {
        private static readonly string appName = Application.ProductName; // "Kalender";
        private static readonly string assLctn = Assembly.GetExecutingAssembly().Location;  // EXE-Pfad
        private readonly int currentYear = DateTime.Now.Year; // Wert wird nicht geändert
        private Dictionary<DateTime, string> tooltipDays;
        private Dictionary<DateTime, string> tooltipFixDays;
        private Point lastMouseLocation = new Point(-1, -1); // Die letzte Mausposition. Wird zum Verhindern von Flackern benötigt.
        private DateTime lastDateUnderMouse = new DateTime(1756, 1, 27);
        private Setting setting;
        private static readonly string xmlPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), appName, appName + ".xml");
        private readonly Config config = new Config(xmlPath);
        private readonly string defFormLocation = "FormLocation";
        private readonly string defSavedFormat = "SavedFormat";
        private readonly string defLastView = "LastView";
        private readonly string defColorScheme = "ColorScheme";
        private readonly string defAlwaysOnTop = "AlwaysOnTop";
        private readonly string defTrayModus = "TrayModus";
        private readonly string defHotkeyLetter = "HotkeyLetter";
        private readonly string defResetDate = "ResetDate";
        private readonly string defGoogleCalendar = "GoogleCalendar";
        private readonly string defTooltipText = "TooltipText";
        private char varHotkeyLetter = 'K';
        private int varColorScheme = 0;
        private bool varAlwaysOnTop = true;
        private bool varTrayModus = true;
        private bool varResetDate = true;
        private readonly string varFormLocation = "10,10"; // default
        private readonly int varCurrentView = 1;
        private string urlGoogleCalendar = string.Empty;
        private string varTooltipText = "Urlaub";
        private DateTime lastMouseClick = DateTime.Now;
        private readonly bool argumentCmdLine = false;
        private int varDateFormatIndex = 3; // default
        private readonly string[] dateFormat = {
            "d.M.", // 1.1.
            "d.M.yy", // 1.1.11
            "dd.MM.yy", // 01.01.11
            "d.M.yyyy", // 1.11.2011
            "dd.MM.yyyy", // 01.01.2011
            "d. MMMM yyyy", // 1. Januar 2011
            "ddd., d.M.yyyy", // So., 1.1.12
            "dddd, 'den' d. MMMM yyyy", // Sonntag, den 1.Januar 2012
        };

        public FrmCalendar(string[] arguments)
        {
            argumentCmdLine = ((IList<string>)arguments).Contains("showDespiteTrayModus"); // Array.Exists(arguments, element => element.ToLower() == "show");
            InitializeComponent();
            Directory.CreateDirectory(Path.GetDirectoryName(xmlPath)); // If the folder exists already, the line will be ignored.
            foreach (ToolStripMenuItem menuItem in menuMain.Items)
            {
                ((ToolStripDropDownMenu)menuItem.DropDown).ShowImageMargin = false;
                ((ToolStripDropDownMenu)menuItem.DropDown).BackColor = SystemColors.ButtonFace;
            }
            config.Sections.Add(appName); // Konfigurations-Sektionen und Einstellungen definieren
            config.Sections[appName].Settings.Add(defSavedFormat, varDateFormatIndex.ToString());
            config.Sections[appName].Settings.Add(defFormLocation, varFormLocation.ToString());
            config.Sections[appName].Settings.Add(defLastView, varCurrentView.ToString());
            config.Sections[appName].Settings.Add(defColorScheme, varColorScheme.ToString());
            config.Sections[appName].Settings.Add(defAlwaysOnTop, varAlwaysOnTop.ToString());
            config.Sections[appName].Settings.Add(defTrayModus, varTrayModus.ToString());
            config.Sections[appName].Settings.Add(defHotkeyLetter, varHotkeyLetter.ToString());
            config.Sections[appName].Settings.Add(defResetDate, varResetDate.ToString());
            config.Sections[appName].Settings.Add(defGoogleCalendar, urlGoogleCalendar);
            config.Sections[appName].Settings.Add(defTooltipText, varTooltipText);
            config.Load(); // Datei einlesen
            try
            {
                varDateFormatIndex = Convert.ToInt32(GetConfigValue(defSavedFormat));
                varCurrentView = Convert.ToInt32(GetConfigValue(defLastView));
                varColorScheme = Convert.ToInt32(GetConfigValue(defColorScheme));
                varAlwaysOnTop = Convert.ToBoolean(GetConfigValue(defAlwaysOnTop));
                varTrayModus = Convert.ToBoolean(GetConfigValue(defTrayModus));
                varHotkeyLetter = Convert.ToChar(GetConfigValue(defHotkeyLetter)); //   (new Regex(@"^[A-Z]$").IsMatch(hkLetter) ? hkLetter : "A"));
                varResetDate = Convert.ToBoolean(GetConfigValue(defResetDate));
                varFormLocation = (GetConfigValue(defFormLocation));
                urlGoogleCalendar = (GetConfigValue(defGoogleCalendar));
                varTooltipText = (GetConfigValue(defTooltipText));
            }
            catch (Exception ex) when (ex is FormatException || ex is KeyNotFoundException)
            {
                MessageBox.Show("Fehler beim Lesen der Konfiguration (" + appName + ".xml)." + Environment.NewLine + Environment.NewLine + ex.Message, appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (varColorScheme != 0)
            {
                monthCalendar.HandleCreated += new EventHandler((sender, args) => { NativeMethods.SetWindowTheme(monthCalendar.Handle, string.Empty, string.Empty); });
            }
            monthCalendar.SetCalendarDimensions(1, varCurrentView);
            TopMost = varAlwaysOnTop;
            btnSendDate.Text = monthCalendar.SelectionRange.Start.ToString(dateFormat[varDateFormatIndex], DateTimeFormatInfo.CurrentInfo);
        }

        //private void MonthCalendar_HandleCreated(object sender, EventArgs e) { NativeMethods.SetWindowTheme(monthCalendar.Handle, string.Empty, string.Empty); } // disable visual styles for this control 

        private void FrmCalendar_Load(object sender, EventArgs e)
        {
            SetColors();
            if (!string.IsNullOrEmpty(urlGoogleCalendar) && urlGoogleCalendar.Substring(urlGoogleCalendar.Length - 4).Equals(".ics"))
            {
                string localFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Kalender.ics");
                Task.Run(() => Utilities.HttpGetGoogleCalendar(urlGoogleCalendar, localFile)).Wait();
            }
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            string[] coords = varFormLocation.Split(',');
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
                yPos = yPos < 0 ? 0 : yPos + Height > screen.Height ? screen.Height - Height : yPos;
                Location = new Point(xPos, yPos);
            }
            lblHorizontalLine.Location = new Point(lblHorizontalLine.Location.X, lblInfo.Location.Y); // if currentView = 3
            if (varTrayModus && varHotkeyLetter >= 'A' && varHotkeyLetter <= 'Z')
            {
                if (NativeMethods.RegisterHotKey(Handle, NativeMethods.HOTKEY_ID, (uint)(NativeMethods.Modifiers.Control | NativeMethods.Modifiers.Win), (uint)(Keys)varHotkeyLetter) == false)
                {
                    MessageBox.Show("Fehler beim Registrieren des Hotkey!\nDer Hotkey wird wahrscheinlich bereits\nvon einem anderen Programm benutzt.", appName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    varHotkeyLetter = '-';
                }
                else { anzeigenToolStripMenuItem.ShortcutKeyDisplayString = "Strg+Win+" + Char.ToUpper(varHotkeyLetter); }
            }
            monthCalendar.AnnuallyBoldedDates = ClsBoldedDays.GetAnnualDays().Keys.ToArray(); // muss vor MaxDate-Zeile stehen!
            if (varCurrentView == 3) { monthCalendar.MaxDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).AddMonths(1); }
        }   // Trick to show previous month (default ist actual and future month); reset to default 31.12.9998 later

        private void TimerRunOnce_Tick(object sender, EventArgs e)
        {// Schreibt etwas zeitverzögert die Feiertage in die BoldedDates-Eigenschaft (Workaround)
            timerRunOnce.Stop(); //tooltipFixDays = annualFixDays.ToDictionary(x => new DateTime(currentYear, x.Key.Month, x.Key.Day), x => x.Value);
            tooltipFixDays = ClsBoldedDays.GetAnnualDays(currentYear);
            Dictionary<DateTime, string> boldedMobileDays = ClsBoldedDays.GetMobileDays(currentYear, varTooltipText);
            tooltipDays = ClsBoldedDays.MergeBoldedDays(tooltipFixDays, boldedMobileDays);
            monthCalendar.BoldedDates = boldedMobileDays.Keys.ToArray(); // diese Zeile den Fehler, wenn BoldedDates bei DateChanged neu geschrieben werden sollen
            monthCalendar.Focus();
            monthCalendar.MaxDate = new DateTime(9998, 12, 31); // to show previous month (default ist actual and future month); reset to default 31.12.9998 after Load per Timer
        }

        private void FrmCalendar_Shown(object sender, EventArgs e)
        {
            timerRunOnce.Start(); // Workaround: Timer starten, der etwas zeitverzögert die Feiertage ermittelt und in BoldedDates schreibt
            if (varTrayModus && !argumentCmdLine) { Visible = false; notifyIcon.Visible = true; }
            else { Utilities.CenterMouseOverControl(btnSendDate, -45); }
        }

        private void MonthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {// blInfo.BackColor = monthCalendar.SelectionRange.Start != monthCalendar.SelectionRange.End ? SystemColors.ActiveCaption : SystemColors.ControlLightLight;
            btnSendDate.Text = FormatDateRange(monthCalendar.SelectionRange.Start, monthCalendar.SelectionRange.End);
            Utilities.DateDiff ddf = Utilities.CalcDateDiff(monthCalendar.SelectionStart, (monthCalendar.SelectionRange.Start != monthCalendar.SelectionRange.End ? monthCalendar.SelectionEnd.AddDays(1) : monthCalendar.TodayDate));
            lblInfo.Text = "" + (!ddf.years.Equals(0) ? ddf.years.ToString() + ((ddf.years.Equals(1) ? " Jahr" : " Jahre") +
                (ddf.months.Equals(0) && ddf.days.Equals(0) ? "" : ", ")) : "") + (!ddf.months.Equals(0) ? ddf.months.ToString() +
                ((ddf.months.Equals(1) ? " Monat" : " Monate") + (ddf.days.Equals(0) ? "" : ", ")) : "") +
                (!ddf.days.Equals(0) ? ddf.days.ToString() + (ddf.days.Equals(1) ? " Tag" : " Tage") : "") +
                (ddf.years.Equals(0) && ddf.months.Equals(0) && ddf.days > 13 ? " (" + (ddf.days / 7).ToString() + " Wochen" + (ddf.days % 7 > 1 ? ", " + (ddf.days % 7) + " Tage" : (ddf.days % 7 == 1 ? ", " + " 1 Tag" : "")) + ")" : "");
            lblInfo.Text = monthCalendar.SelectionRange.Start != monthCalendar.SelectionRange.End ? (lblInfo.Text.Length > 0 ? "↔ " + lblInfo.Text : "") : lblInfo.Text;
            lastMouseClick = DateTime.Now.Subtract(new TimeSpan(1, 0, 0));
        }

        private void MonthCalendar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Location != lastMouseLocation)
            {
                lastMouseLocation = e.Location;
                try
                {
                    DateTime date = monthCalendar.HitTest(e.X, e.Y).Time;
                    if (date != lastDateUnderMouse)
                    {
                        lastDateUnderMouse = date;
                        if (date.Year == currentYear - 1 && (date.Month == 11 || date.Month == 12)) { date = new DateTime(currentYear, date.Month, date.Day); }
                        else if (date.Year == currentYear + 1 && (date.Month == 1 || date.Month == 2)) { date = new DateTime(currentYear, date.Month, date.Day); }
                        if (tooltipDays != null && tooltipDays.ContainsKey(date.Date))
                        {
                            toolTip.SetToolTip(monthCalendar, tooltipDays[date.Date]); // toolTip.SetToolTip(monthCalendar, item.Value);
                            return;
                        }
                        toolTip.SetToolTip(monthCalendar, null);
                    }
                }
                catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is ArgumentException) { toolTip.SetToolTip(monthCalendar, null); }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D | Keys.Control:
                    {
                        DifferenzRechnerToolStripMenuItem_Click(null, null);
                        return true;
                    }
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
                            DateTime selStart = monthCalendar.SelectionStart;
                            if (selStart.Month != monthCalendar.GetDisplayRange(true).Start.Month)
                            {
                                monthCalendar.MinDate = new DateTime(selStart.Year, selStart.Month, 1);
                                timerF3.Start();  // Trick to show previous month (default ist actual and future month); reset to default 31.12.9998 later
                            }
                            lblHorizontalLine.Location = new Point(lblHorizontalLine.Location.X, 230); // muss vor SetCalendardDimension erfolgen
                            monthCalendar.SetCalendarDimensions(1, 1);
                            Utilities.CenterMouseOverControl(btnSendDate, -45);

                        }
                        else
                        {
                            DateTime selEnd = monthCalendar.SelectionEnd;
                            monthCalendar.MaxDate = new DateTime(selEnd.Year, selEnd.Month, DateTime.DaysInMonth(selEnd.Year, selEnd.Month)).AddMonths(1);
                            timerF3.Start();
                            monthCalendar.SetCalendarDimensions(1, 3);
                            Utilities.CenterMouseOverControl(btnSendDate, -45);
                            lblHorizontalLine.Location = new Point(lblHorizontalLine.Location.X, lblInfo.Location.Y);
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
                        varDateFormatIndex++;
                        varDateFormatIndex = varDateFormatIndex >= dateFormat.Length ? 0 : varDateFormatIndex;
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
                if (varResetDate)
                {
                    if (varCurrentView == 3) { monthCalendar.MaxDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).AddMonths(1); }
                    monthCalendar.SetDate(monthCalendar.TodayDate);
                    monthCalendar.MaxDate = new DateTime(9998, 12, 31); // to show previous month (default ist actual and future month); reset to default 31.12.9998 after Load per Timer
                }
                monthCalendar.Focus();
            }
            else { notifyIcon.Text = FormatDateRange(monthCalendar.TodayDate, monthCalendar.TodayDate); }
        }

        private void DatumÜbertragenToolStripMenuItem_Click(object sender, EventArgs e) { BtnSendDate_Click(null, null); }
        private void BeendenToolStripMenuItem_Click(object sender, EventArgs e) { Close(); }
        private void FormatÄndernToolStripMenuItem_Click(object sender, EventArgs e) { BtnFormatDate_Click(null, null); }
        private void GeheZuToolStripMenuItem_Click(object sender, EventArgs e) { monthCalendar.SetDate(DateTime.Now); }
        private void MonatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime selStart = monthCalendar.SelectionStart;
            if (selStart.Month != monthCalendar.GetDisplayRange(true).Start.Month)
            {
                monthCalendar.MinDate = new DateTime(selStart.Year, selStart.Month, 1);
                timerF3.Start();  // Trick to show previous month (default ist actual and future month); reset to default 31.12.9998 later
            }
            lblHorizontalLine.Location = new Point(lblHorizontalLine.Location.X, 230); // muss vor SetCalendardDimension erfolgen
            monthCalendar.SetCalendarDimensions(1, 1);
            Utilities.CenterMouseOverControl(btnSendDate, -45);
        }
        private void MonateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime selEnd = monthCalendar.SelectionEnd;
            monthCalendar.MaxDate = new DateTime(selEnd.Year, selEnd.Month, DateTime.DaysInMonth(selEnd.Year, selEnd.Month)).AddMonths(1);
            timerF3.Start();
            monthCalendar.SetCalendarDimensions(1, 3);
            Utilities.CenterMouseOverControl(btnSendDate, -45);
            lblHorizontalLine.Location = new Point(lblHorizontalLine.Location.X, lblInfo.Location.Y);
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

        private void FrmCalendar_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (varTrayModus && e.CloseReason == CloseReason.UserClosing && (Control.ModifierKeys & Keys.Shift) != Keys.Shift) // Anwendung kann nur über «Beenden» in cntxtMenuTNA beendet werden!
            {
                e.Cancel = true; // das Schließen des Formulars verhindern
                notifyIcon.Visible = true;
                Hide();
            }
            else
            {
                if (Char.IsLetter(varHotkeyLetter)) { NativeMethods.UnregisterHotKey(Handle, NativeMethods.HOTKEY_ID); }
                SaveConfigValue(defFormLocation, string.Concat(Location.X.ToString(), ", ", Location.Y.ToString()));
                SaveConfigValue(defSavedFormat, varDateFormatIndex.ToString());
                SaveConfigValue(defLastView, monthCalendar.CalendarDimensions.Height.ToString());
                SaveConfigValue(defColorScheme, varColorScheme.ToString());
                SaveConfigValue(defAlwaysOnTop, varAlwaysOnTop.ToString());
                SaveConfigValue(defTrayModus, varTrayModus.ToString());
                SaveConfigValue(defHotkeyLetter, varHotkeyLetter.ToString());
                SaveConfigValue(defResetDate, varResetDate.ToString());
                SaveConfigValue(defGoogleCalendar, urlGoogleCalendar);
                SaveConfigValue(defTooltipText, varTooltipText);
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
            catch (KeyNotFoundException ex) { MessageBox.Show(Name + " (" + setting.Value + ")" + ": " + ex.Message, appName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
            try // hinzugefügt am 11.10.20
            {
                Hide(); // Hiding is equivalent to setting the Visible property to false
                Clipboard.Clear();
                string date = FormatDateRange(monthCalendar.SelectionRange.Start, monthCalendar.SelectionRange.End);
                if (!Utilities.SetClipboardText(date))
                {
                    MessageBox.Show("Es ist ein Fehler aufgetreten.\nVersuchen Sie es noch einmal.", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Show();
                }
                else
                {
                    IntPtr activeWindowHandle = NativeMethods.GetForegroundWindow();  // MessageBox.Show(NativeMethods.GetActiveWindowClass(activeWindowHandle));
                    if ((activeWindowHandle != null && !NativeMethods.GetActiveWindowClass(activeWindowHandle).Equals("Shell_TrayWnd")) || NativeMethods.SetForegroundWindow(NativeMethods.GetLastWinHandle(Handle))) { SendKeys.SendWait("^(v)"); }
                    else { System.Media.SystemSounds.Beep.Play(); }
                    Close();
                }
            }
            catch (Exception ex) when (ex is NullReferenceException) { }
        }

        private void BtnFormatDate_Click(object sender, EventArgs e)
        {
            string[] format = dateFormat.Where((val, idx) => idx != varDateFormatIndex).ToArray();
            formatMenu.Items.Clear();
            for (int i = 0; i < format.Length; i++)
            {
                var menuItem = new ToolStripMenuItem(monthCalendar.SelectionRange.Start.ToString(format[i], DateTimeFormatInfo.CurrentInfo))
                {
                    ShortcutKeys = Keys.F2,
                    ShowShortcutKeys = false,
                    Tag = i
                };
                menuItem.Click += new EventHandler(SelectFormatEvent);
                formatMenu.Items.Add(menuItem);
            }
            formatMenu.Show(this, new Point(btnFormatDate.Location.X - formatMenu.Width / 2, btnFormatDate.Parent.Location.Y - formatMenu.Height / 2));
        }

        private void SelectFormatEvent(object sender, EventArgs e)
        {
            int newFormatIndex = Convert.ToInt16(((ToolStripItem)sender).Tag);
            varDateFormatIndex = newFormatIndex >= varDateFormatIndex ? newFormatIndex + 1 : newFormatIndex; // enthält möglicherweise noch einen Fehler!!!
            btnSendDate.Text = FormatDateRange(monthCalendar.SelectionRange.Start, monthCalendar.SelectionRange.End);
        }

        private string FormatDateRange(DateTime startDate, DateTime endDate)
        {
            string result = endDate.ToString(dateFormat[varDateFormatIndex], DateTimeFormatInfo.CurrentInfo);
            if (startDate.Date != endDate.Date)
            {
                result = String.Format(new Regex("(MMMM|ddd)").IsMatch(dateFormat[varDateFormatIndex]) ? "{0} - {1}" : "{0}-{1}",
                  startDate.Year == endDate.Year && startDate.Month == endDate.Month ?
                    startDate.ToString(Regex.Replace(dateFormat[varDateFormatIndex], @"\s?(y+|M+\.|MMMM)", ""), DateTimeFormatInfo.CurrentInfo) :
                    startDate.Year == endDate.Year ?
                      startDate.ToString(Regex.Replace(dateFormat[varDateFormatIndex], @"\s?y+", ""), DateTimeFormatInfo.CurrentInfo) :
                      startDate.Month == endDate.Month ?
                        startDate.ToString(Regex.Replace(dateFormat[varDateFormatIndex], @"\s?(M+\.|MMMM)", ""), DateTimeFormatInfo.CurrentInfo) :
                        startDate.ToString(dateFormat[varDateFormatIndex], DateTimeFormatInfo.CurrentInfo), result);
            }
            return result;
        }

        protected override void WndProc(ref Message m)
        {// Hotkey functionallity ...
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
            bool varAutoStart = Utilities.IsAutoStartEnabled(appName);
            using (FrmSettings frmSettings = new FrmSettings(varColorScheme, varAlwaysOnTop, varAutoStart, varTrayModus, varHotkeyLetter, varResetDate, urlGoogleCalendar, varTooltipText))
            {
                Char previousHKChar = varHotkeyLetter;
                bool restartRequired = false;
                if (frmSettings.ShowDialog() == DialogResult.OK)
                {
                    int baz = frmSettings.ColorScheme;
                    if (!baz.Equals(varColorScheme))
                    {
                        if (varColorScheme == 0 || baz == 0) { restartRequired = true; }
                        varColorScheme = baz;
                        SetColors();
                    }
                    TopMost = varAlwaysOnTop = frmSettings.AlwaysOnTop;

                    if (frmSettings.AutoStart && !varAutoStart) { Utilities.SetAutoStart(appName, assLctn); }
                    else { if (!frmSettings.AutoStart && varAutoStart) { Utilities.UnSetAutoStart(appName); } }

                    varTrayModus = frmSettings.TrayModus;
                    varHotkeyLetter = frmSettings.HKLetter;
                    varResetDate = frmSettings.ResetDate;
                    string foo = frmSettings.GoogleCal;
                    string bar = frmSettings.TooltipText;
                    if (!foo.Equals(urlGoogleCalendar) || !bar.Equals(varTooltipText))
                    {
                        urlGoogleCalendar = foo;
                        varTooltipText = bar;
                        restartRequired = true;
                    }
                }
                if (restartRequired && MessageBox.Show("Die Änderungen erfordern einen Programm-Neustart.\n\nMöchten Sie das Kalender-Programm jetzt neustarten?", appName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Reload(true);
                }
                if (varTrayModus && varHotkeyLetter >= 'A' && varHotkeyLetter <= 'Z' && varHotkeyLetter != previousHKChar)
                {
                    if (Char.IsLetter(previousHKChar)) { NativeMethods.UnregisterHotKey(Handle, NativeMethods.HOTKEY_ID); }
                    if (NativeMethods.RegisterHotKey(Handle, NativeMethods.HOTKEY_ID, (uint)(NativeMethods.Modifiers.Control | NativeMethods.Modifiers.Win), (uint)(Keys)varHotkeyLetter) == false)
                    {
                        MessageBox.Show("Fehler beim Registrieren des Hotkey!\nDer Hotkey wird wahrscheinlich bereits\nvon einem anderen Programm benutzt.", appName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        anzeigenToolStripMenuItem.ShortcutKeyDisplayString = String.Empty;
                        varHotkeyLetter = '-';
                    }
                    else { anzeigenToolStripMenuItem.ShortcutKeyDisplayString = "Strg+Win+" + varHotkeyLetter; }
                }
                else if (!varTrayModus && Char.IsLetter(previousHKChar))
                {
                    NativeMethods.UnregisterHotKey(Handle, NativeMethods.HOTKEY_ID);
                    anzeigenToolStripMenuItem.ShortcutKeyDisplayString = String.Empty;
                }
            }
        }

        private void Reload(bool showMe)
        {
            Application.Exit(); //Application.Restart(); Environment.Exit(0);
            System.Diagnostics.Process.Start(Application.ExecutablePath, showMe ? "showDespiteTrayModus" : "");
        }

        private void SetColors()
        {// Color backColor = SystemColors.Window; // weiß //Color trailFore = SystemColors.GrayText; // Tage außerhalb des Monats (109, 109, 109)
            Color foreColor, titleBack, titleFore;
            if (varColorScheme == 1) // grün
            {
                foreColor = ColorTranslator.FromHtml("#000000"); // DaysMonthCal 
                titleBack = ColorTranslator.FromHtml("#006E52"); // DarkMonthCal 
                titleFore = ColorTranslator.FromHtml("#FFFFFF"); // HeadMonthCal  darf nicht weiß sein?
            }
            else if (varColorScheme == 2) // blau
            {
                foreColor = ColorTranslator.FromHtml("#000000"); // DaysMonthCal 
                titleBack = ColorTranslator.FromHtml("#0A246A"); // DarkMonthCal 
                titleFore = ColorTranslator.FromHtml("#FFFFFF"); // HeadMonthCal  darf nicht weiß sein?
            }
            else if (varColorScheme == 3) // gelb
            {
                foreColor = ColorTranslator.FromHtml("#000000"); // DaysMonthCal 
                titleBack = ColorTranslator.FromHtml("#DEAE00"); // DarkMonthCal 
                titleFore = ColorTranslator.FromHtml("#FFFFFF"); // HeadMonthCal  darf nicht weiß sein?
            }
            else
            {
                foreColor = SystemColors.WindowText; // schwarz
                titleBack = SystemColors.ActiveCaption; // (153, 180, 209)
                titleFore = SystemColors.ActiveCaptionText; // schwarz
            }
            //int colorBack = BitConverter.ToInt32(new byte[] { backColor.R, backColor.G, backColor.B, 0x00 }, 0);
            //NativeMethods.SendMessage(monthCalendar.Handle, NativeMethods.MCM_SETCOLOR, (IntPtr)NativeMethods.MCSC_BACKGROUND, (IntPtr)colorBack); // NativeMethods.SendMessage(monthCalendar.Handle, 0x100A, (IntPtr)0, (IntPtr)0xE5F0ED);
            monthCalendar.ForeColor = foreColor;
            monthCalendar.TitleBackColor = titleBack;
            monthCalendar.TitleForeColor = titleFore;
            //monthCalendar.BackColor = lblInfo.BackColor = backColor;
        }

        private void AnzeigenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            Activate();
            notifyIcon.Visible = false;
            Utilities.CenterMouseOverControl(btnSendDate, -45);
            monthCalendar.Focus();
        }

        private void NeuStartenToolStripMenuItem_Click(object sender, EventArgs e) { Reload(false); }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) { Application.Exit(); }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Visible = !Visible;
                notifyIcon.Visible = !Visible;
                if (Visible) { Utilities.CenterMouseOverControl(btnSendDate, -45); }
            }
        }

        private void GeheZuHeuteToolStripMenuItem_Click(object sender, EventArgs e) { monthCalendar.SetDate(DateTime.Now); }

        private void FrmCalendar_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            using (FrmHelp frmHelp = new FrmHelp(varAlwaysOnTop)) { frmHelp.ShowDialog(); }
        }

        private void FrmCalendar_HelpRequested(object sender, HelpEventArgs hlpevent)
        {// Das HelpRequested-Ereignis wird ausgelöst, wenn der Benutzer F1 drückt
            hlpevent.Handled = true;
            using (FrmHelp frmHelp = new FrmHelp(varAlwaysOnTop)) { frmHelp.ShowDialog(); }
        }

        private void ContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e) { Cursor.Position = formatMenu.PointToScreen(new Point(formatMenu.Width / 2, formatMenu.Height / 2)); } // formatMenu.Items[1].Select();

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
            if ((DateTime.Now - lastMouseClick).TotalMilliseconds <= SystemInformation.DoubleClickTime) { BtnSendDate_Click(null, null); }
            lastMouseClick = DateTime.Now;
        }

        private void TimerF3_Tick(object sender, EventArgs e)
        {
            timerF3.Stop();
            monthCalendar.MinDate = new DateTime(1753, 01, 01); // to show previous month (default ist actual and future month); reset to default 01.01.1753.
            monthCalendar.MaxDate = new DateTime(9998, 12, 31);
        }

        private void DifferenzRechnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmDiffDates frmDiffDates = new FrmDiffDates(varColorScheme))
            {
                frmDiffDates.ShowDialog();
            }
        }
    }
}
