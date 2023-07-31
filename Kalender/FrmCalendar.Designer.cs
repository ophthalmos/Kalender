namespace Kalender
{
    partial class FrmCalendar
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCalendar));
            this.formatMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.datumÜbertragenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bearbeitenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatÄndernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geheZuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ansichtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.differenzRechnerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.einstellungenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnFormatDate = new System.Windows.Forms.Button();
            this.btnSendDate = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.timerRunOnce = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.anzeigenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblInfo = new System.Windows.Forms.Label();
            this.cmsInfoLabel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.geheZuHeuteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.lblHorizontalLine = new System.Windows.Forms.Label();
            this.timerF3 = new System.Windows.Forms.Timer(this.components);
            this.menuMain.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.trayMenu.SuspendLayout();
            this.cmsInfoLabel.SuspendLayout();
            this.SuspendLayout();
            // 
            // formatMenu
            // 
            this.formatMenu.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.formatMenu.Name = "formatMenu";
            this.formatMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.formatMenu.ShowImageMargin = false;
            this.formatMenu.Size = new System.Drawing.Size(36, 4);
            this.formatMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenu_Opening);
            // 
            // menuMain
            // 
            this.menuMain.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.bearbeitenToolStripMenuItem,
            this.ansichtToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuMain.Size = new System.Drawing.Size(201, 24);
            this.menuMain.TabIndex = 3;
            this.menuMain.TabStop = true;
            this.menuMain.MenuActivate += new System.EventHandler(this.MenuStrip_MenuActivate);
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.datumÜbertragenToolStripMenuItem,
            this.beendenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "&Datei";
            // 
            // datumÜbertragenToolStripMenuItem
            // 
            this.datumÜbertragenToolStripMenuItem.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.datumÜbertragenToolStripMenuItem.Name = "datumÜbertragenToolStripMenuItem";
            this.datumÜbertragenToolStripMenuItem.ShortcutKeyDisplayString = "Enter";
            this.datumÜbertragenToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.datumÜbertragenToolStripMenuItem.Text = "&Datum übertragen";
            this.datumÜbertragenToolStripMenuItem.Click += new System.EventHandler(this.DatumÜbertragenToolStripMenuItem_Click);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.ShortcutKeyDisplayString = "Esc";
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.beendenToolStripMenuItem.Text = "&Schließen";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.BeendenToolStripMenuItem_Click);
            // 
            // bearbeitenToolStripMenuItem
            // 
            this.bearbeitenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formatÄndernToolStripMenuItem,
            this.geheZuToolStripMenuItem});
            this.bearbeitenToolStripMenuItem.Name = "bearbeitenToolStripMenuItem";
            this.bearbeitenToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.bearbeitenToolStripMenuItem.Text = "&Bearbeiten";
            // 
            // formatÄndernToolStripMenuItem
            // 
            this.formatÄndernToolStripMenuItem.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.formatÄndernToolStripMenuItem.Name = "formatÄndernToolStripMenuItem";
            this.formatÄndernToolStripMenuItem.ShortcutKeyDisplayString = "F2/Leertaste";
            this.formatÄndernToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.formatÄndernToolStripMenuItem.Text = "&Format ändern";
            this.formatÄndernToolStripMenuItem.Click += new System.EventHandler(this.FormatÄndernToolStripMenuItem_Click);
            // 
            // geheZuToolStripMenuItem
            // 
            this.geheZuToolStripMenuItem.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.geheZuToolStripMenuItem.Name = "geheZuToolStripMenuItem";
            this.geheZuToolStripMenuItem.ShortcutKeyDisplayString = "F5";
            this.geheZuToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.geheZuToolStripMenuItem.Text = "&Gehe zu Heute";
            this.geheZuToolStripMenuItem.Click += new System.EventHandler(this.GeheZuToolStripMenuItem_Click);
            // 
            // ansichtToolStripMenuItem
            // 
            this.ansichtToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.monatToolStripMenuItem,
            this.monateToolStripMenuItem,
            this.toolStripSeparator1,
            this.differenzRechnerToolStripMenuItem,
            this.toolStripSeparator3,
            this.einstellungenToolStripMenuItem});
            this.ansichtToolStripMenuItem.Name = "ansichtToolStripMenuItem";
            this.ansichtToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.ansichtToolStripMenuItem.Text = "&Optionen";
            // 
            // monatToolStripMenuItem
            // 
            this.monatToolStripMenuItem.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.monatToolStripMenuItem.Name = "monatToolStripMenuItem";
            this.monatToolStripMenuItem.ShortcutKeyDisplayString = "F3";
            this.monatToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.monatToolStripMenuItem.Text = "1 &Monat";
            this.monatToolStripMenuItem.Click += new System.EventHandler(this.MonatToolStripMenuItem_Click);
            // 
            // monateToolStripMenuItem
            // 
            this.monateToolStripMenuItem.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.monateToolStripMenuItem.Name = "monateToolStripMenuItem";
            this.monateToolStripMenuItem.ShortcutKeyDisplayString = "F3";
            this.monateToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.monateToolStripMenuItem.Text = "3 &Monate";
            this.monateToolStripMenuItem.Click += new System.EventHandler(this.MonateToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(205, 6);
            // 
            // differenzRechnerToolStripMenuItem
            // 
            this.differenzRechnerToolStripMenuItem.Name = "differenzRechnerToolStripMenuItem";
            this.differenzRechnerToolStripMenuItem.ShortcutKeyDisplayString = "Strg+D";
            this.differenzRechnerToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.differenzRechnerToolStripMenuItem.Text = "Datumsrechner...";
            this.differenzRechnerToolStripMenuItem.Click += new System.EventHandler(this.DifferenzRechnerToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(205, 6);
            // 
            // einstellungenToolStripMenuItem
            // 
            this.einstellungenToolStripMenuItem.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem";
            this.einstellungenToolStripMenuItem.ShortcutKeyDisplayString = "F4";
            this.einstellungenToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.einstellungenToolStripMenuItem.Text = "Einstellungen...";
            this.einstellungenToolStripMenuItem.Click += new System.EventHandler(this.EinstellungenToolStripMenuItem_Click);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 86.13861F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.86139F));
            this.tableLayoutPanel.Controls.Add(this.btnFormatDate, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.btnSendDate, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 242);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(201, 33);
            this.tableLayoutPanel.TabIndex = 6;
            // 
            // btnFormatDate
            // 
            this.btnFormatDate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFormatDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFormatDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFormatDate.Location = new System.Drawing.Point(173, 3);
            this.btnFormatDate.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnFormatDate.Name = "btnFormatDate";
            this.btnFormatDate.Size = new System.Drawing.Size(28, 27);
            this.btnFormatDate.TabIndex = 1;
            this.btnFormatDate.Text = "#";
            this.btnFormatDate.UseVisualStyleBackColor = true;
            this.btnFormatDate.Click += new System.EventHandler(this.BtnFormatDate_Click);
            // 
            // btnSendDate
            // 
            this.btnSendDate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSendDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSendDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendDate.Location = new System.Drawing.Point(0, 3);
            this.btnSendDate.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnSendDate.Name = "btnSendDate";
            this.btnSendDate.Size = new System.Drawing.Size(173, 27);
            this.btnSendDate.TabIndex = 2;
            this.btnSendDate.Text = "1.1.2000";
            this.btnSendDate.UseVisualStyleBackColor = true;
            this.btnSendDate.TextChanged += new System.EventHandler(this.BtnSendDate_TextChanged);
            this.btnSendDate.Click += new System.EventHandler(this.BtnSendDate_Click);
            // 
            // timerRunOnce
            // 
            this.timerRunOnce.Enabled = true;
            this.timerRunOnce.Tick += new System.EventHandler(this.TimerRunOnce_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Kalender";
            this.notifyIcon.ContextMenuStrip = this.trayMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Kalender";
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseClick);
            // 
            // trayMenu
            // 
            this.trayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.anzeigenToolStripMenuItem,
            this.toolStripSeparator2,
            this.reloadToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.trayMenu.Name = "trayMenu";
            this.trayMenu.Size = new System.Drawing.Size(163, 76);
            // 
            // anzeigenToolStripMenuItem
            // 
            this.anzeigenToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("anzeigenToolStripMenuItem.Image")));
            this.anzeigenToolStripMenuItem.Name = "anzeigenToolStripMenuItem";
            this.anzeigenToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.anzeigenToolStripMenuItem.Text = "Anzeigen";
            this.anzeigenToolStripMenuItem.Click += new System.EventHandler(this.AnzeigenToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(159, 6);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Image = global::Kalender.Properties.Resources._16x16Change;
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.reloadToolStripMenuItem.Text = "Neu starten";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.NeuStartenToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::Kalender.Properties.Resources._16x16Close;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeyDisplayString = "Alt+F4";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.exitToolStripMenuItem.Text = "Beenden";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblInfo.ContextMenuStrip = this.cmsInfoLabel;
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(0, 220);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(201, 22);
            this.lblInfo.TabIndex = 7;
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmsInfoLabel
            // 
            this.cmsInfoLabel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.geheZuHeuteToolStripMenuItem});
            this.cmsInfoLabel.Name = "cmsInfoLabel";
            this.cmsInfoLabel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsInfoLabel.Size = new System.Drawing.Size(152, 26);
            // 
            // geheZuHeuteToolStripMenuItem
            // 
            this.geheZuHeuteToolStripMenuItem.Name = "geheZuHeuteToolStripMenuItem";
            this.geheZuHeuteToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.geheZuHeuteToolStripMenuItem.Text = "&Gehe zu Heute";
            this.geheZuHeuteToolStripMenuItem.Click += new System.EventHandler(this.GeheZuHeuteToolStripMenuItem_Click);
            // 
            // monthCalendar
            // 
            this.monthCalendar.Dock = System.Windows.Forms.DockStyle.Top;
            this.monthCalendar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monthCalendar.Location = new System.Drawing.Point(0, 24);
            this.monthCalendar.MaxSelectionCount = 128;
            this.monthCalendar.MinimumSize = new System.Drawing.Size(200, 208);
            this.monthCalendar.Name = "monthCalendar";
            this.monthCalendar.ShowWeekNumbers = true;
            this.monthCalendar.TabIndex = 0;
            this.monthCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.MonthCalendar_DateChanged);
            this.monthCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.MonthCalendar_DateSelected);
            this.monthCalendar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MonthCalendar_MouseMove);
            // 
            // lblHorizontalLine
            // 
            this.lblHorizontalLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHorizontalLine.Location = new System.Drawing.Point(9, 220);
            this.lblHorizontalLine.Name = "lblHorizontalLine";
            this.lblHorizontalLine.Size = new System.Drawing.Size(182, 1);
            this.lblHorizontalLine.TabIndex = 8;
            // 
            // timerF3
            // 
            this.timerF3.Tick += new System.EventHandler(this.TimerF3_Tick);
            // 
            // FrmCalendar
            // 
            this.AcceptButton = this.btnSendDate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(201, 275);
            this.Controls.Add(this.lblHorizontalLine);
            this.Controls.Add(this.monthCalendar);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.menuMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(217, 314);
            this.Name = "FrmCalendar";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Kalender";
            this.TopMost = true;
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.FrmCalendar_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCalendar_FormClosing);
            this.Load += new System.EventHandler(this.FrmCalendar_Load);
            this.Shown += new System.EventHandler(this.FrmCalendar_Shown);
            this.VisibleChanged += new System.EventHandler(this.FrmCalendar_VisibleChanged);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.FrmCalendar_HelpRequested);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            this.trayMenu.ResumeLayout(false);
            this.cmsInfoLabel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip formatMenu;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem datumÜbertragenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bearbeitenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatÄndernToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem geheZuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ansichtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monateToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button btnSendDate;
        private System.Windows.Forms.Button btnFormatDate;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Timer timerRunOnce;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip trayMenu;
        private System.Windows.Forms.ToolStripMenuItem anzeigenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem einstellungenToolStripMenuItem;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.ContextMenuStrip cmsInfoLabel;
        private System.Windows.Forms.ToolStripMenuItem geheZuHeuteToolStripMenuItem;
        private System.Windows.Forms.MonthCalendar monthCalendar;
        private System.Windows.Forms.Label lblHorizontalLine;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Timer timerF3;
        private System.Windows.Forms.ToolStripMenuItem differenzRechnerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

