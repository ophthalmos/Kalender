namespace Kalender
{
    partial class FrmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettings));
            this.ckbTrayModus = new System.Windows.Forms.CheckBox();
            this.cmbHotkeyLetter = new System.Windows.Forms.ComboBox();
            this.lblHotkey = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblTooltipText = new System.Windows.Forms.Label();
            this.tbTooltipText = new System.Windows.Forms.TextBox();
            this.lblGoogleCal = new System.Windows.Forms.Label();
            this.tbGoogleCal = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckbTopMost = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbutton0 = new System.Windows.Forms.RadioButton();
            this.rbutton1 = new System.Windows.Forms.RadioButton();
            this.rbutton2 = new System.Windows.Forms.RadioButton();
            this.rbutton3 = new System.Windows.Forms.RadioButton();
            this.panel.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // ckbTrayModus
            // 
            this.ckbTrayModus.AutoSize = true;
            this.ckbTrayModus.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ckbTrayModus.Location = new System.Drawing.Point(6, 19);
            this.ckbTrayModus.Name = "ckbTrayModus";
            this.ckbTrayModus.Size = new System.Drawing.Size(202, 17);
            this.ckbTrayModus.TabIndex = 0;
            this.ckbTrayModus.Text = "In das Tray minimieren statt schließen";
            this.ckbTrayModus.UseVisualStyleBackColor = false;
            this.ckbTrayModus.CheckedChanged += new System.EventHandler(this.CkbTrayModus_CheckedChanged);
            // 
            // cmbHotkeyLetter
            // 
            this.cmbHotkeyLetter.DropDownWidth = 35;
            this.cmbHotkeyLetter.Enabled = false;
            this.cmbHotkeyLetter.FormattingEnabled = true;
            this.cmbHotkeyLetter.Items.AddRange(new object[] {
            "-",
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z"});
            this.cmbHotkeyLetter.Location = new System.Drawing.Point(154, 42);
            this.cmbHotkeyLetter.MaxLength = 1;
            this.cmbHotkeyLetter.Name = "cmbHotkeyLetter";
            this.cmbHotkeyLetter.Size = new System.Drawing.Size(49, 21);
            this.cmbHotkeyLetter.TabIndex = 1;
            this.cmbHotkeyLetter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbHotkeyLetter_KeyPress);
            // 
            // lblHotkey
            // 
            this.lblHotkey.AutoSize = true;
            this.lblHotkey.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblHotkey.Enabled = false;
            this.lblHotkey.Location = new System.Drawing.Point(6, 45);
            this.lblHotkey.Name = "lblHotkey";
            this.lblHotkey.Size = new System.Drawing.Size(142, 13);
            this.lblHotkey.TabIndex = 2;
            this.lblHotkey.Text = "Globaler Hotkey: Strg+Win+ ";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(12, 314);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(124, 314);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(102, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel.Controls.Add(this.groupBox4);
            this.panel.Controls.Add(this.groupBox3);
            this.panel.Controls.Add(this.groupBox1);
            this.panel.Controls.Add(this.groupBox2);
            this.panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(238, 308);
            this.panel.TabIndex = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblTooltipText);
            this.groupBox3.Controls.Add(this.tbTooltipText);
            this.groupBox3.Controls.Add(this.lblGoogleCal);
            this.groupBox3.Controls.Add(this.tbGoogleCal);
            this.groupBox3.Location = new System.Drawing.Point(12, 193);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(214, 105);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Externer Kalender";
            // 
            // lblTooltipText
            // 
            this.lblTooltipText.AutoSize = true;
            this.lblTooltipText.Location = new System.Drawing.Point(6, 79);
            this.lblTooltipText.Name = "lblTooltipText";
            this.lblTooltipText.Size = new System.Drawing.Size(66, 13);
            this.lblTooltipText.TabIndex = 3;
            this.lblTooltipText.Text = "Tooltip-Text:";
            // 
            // tbTooltipText
            // 
            this.tbTooltipText.Location = new System.Drawing.Point(78, 76);
            this.tbTooltipText.Name = "tbTooltipText";
            this.tbTooltipText.Size = new System.Drawing.Size(126, 20);
            this.tbTooltipText.TabIndex = 2;
            this.tbTooltipText.Text = "Urlaub";
            // 
            // lblGoogleCal
            // 
            this.lblGoogleCal.AutoSize = true;
            this.lblGoogleCal.Location = new System.Drawing.Point(6, 16);
            this.lblGoogleCal.Name = "lblGoogleCal";
            this.lblGoogleCal.Size = new System.Drawing.Size(206, 26);
            this.lblGoogleCal.TabIndex = 1;
            this.lblGoogleCal.Text = "URL des Google-Kalenders für zusätzliche\r\nBoldedDays (Privatadresse, iCal-Format)" +
    ":";
            // 
            // tbGoogleCal
            // 
            this.tbGoogleCal.Location = new System.Drawing.Point(10, 49);
            this.tbGoogleCal.Name = "tbGoogleCal";
            this.tbGoogleCal.Size = new System.Drawing.Size(194, 20);
            this.tbGoogleCal.TabIndex = 0;
            this.tbGoogleCal.TextChanged += new System.EventHandler(this.TbGoogleCal_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckbTopMost);
            this.groupBox1.Location = new System.Drawing.Point(12, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(214, 44);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AlwaysOnTop";
            // 
            // ckbTopMost
            // 
            this.ckbTopMost.AutoSize = true;
            this.ckbTopMost.Checked = true;
            this.ckbTopMost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbTopMost.Location = new System.Drawing.Point(7, 19);
            this.ckbTopMost.Name = "ckbTopMost";
            this.ckbTopMost.Size = new System.Drawing.Size(204, 17);
            this.ckbTopMost.TabIndex = 3;
            this.ckbTopMost.Text = "Kalender immer im Vordergrund halten";
            this.ckbTopMost.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ckbTrayModus);
            this.groupBox2.Controls.Add(this.lblHotkey);
            this.groupBox2.Controls.Add(this.cmbHotkeyLetter);
            this.groupBox2.Location = new System.Drawing.Point(12, 115);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(214, 72);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Standby Mode";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbutton3);
            this.groupBox4.Controls.Add(this.rbutton2);
            this.groupBox4.Controls.Add(this.rbutton1);
            this.groupBox4.Controls.Add(this.rbutton0);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(214, 47);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Farbschema";
            // 
            // rbutton0
            // 
            this.rbutton0.AutoSize = true;
            this.rbutton0.Checked = true;
            this.rbutton0.Location = new System.Drawing.Point(6, 20);
            this.rbutton0.Name = "rbutton0";
            this.rbutton0.Size = new System.Drawing.Size(48, 17);
            this.rbutton0.TabIndex = 0;
            this.rbutton0.TabStop = true;
            this.rbutton0.Text = "norm";
            this.rbutton0.UseVisualStyleBackColor = true;
            // 
            // rbutton1
            // 
            this.rbutton1.AutoSize = true;
            this.rbutton1.Location = new System.Drawing.Point(60, 20);
            this.rbutton1.Name = "rbutton1";
            this.rbutton1.Size = new System.Drawing.Size(46, 17);
            this.rbutton1.TabIndex = 1;
            this.rbutton1.Text = "grün";
            this.rbutton1.UseVisualStyleBackColor = true;
            // 
            // rbutton2
            // 
            this.rbutton2.AutoSize = true;
            this.rbutton2.Location = new System.Drawing.Point(112, 20);
            this.rbutton2.Name = "rbutton2";
            this.rbutton2.Size = new System.Drawing.Size(45, 17);
            this.rbutton2.TabIndex = 2;
            this.rbutton2.Text = "blau";
            this.rbutton2.UseVisualStyleBackColor = true;
            // 
            // rbutton3
            // 
            this.rbutton3.AutoSize = true;
            this.rbutton3.Location = new System.Drawing.Point(163, 20);
            this.rbutton3.Name = "rbutton3";
            this.rbutton3.Size = new System.Drawing.Size(45, 17);
            this.rbutton3.TabIndex = 3;
            this.rbutton3.Text = "gelb";
            this.rbutton3.UseVisualStyleBackColor = true;
            // 
            // FrmSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(238, 344);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Einstellungen";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSettings_KeyDown);
            this.panel.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox ckbTrayModus;
        private System.Windows.Forms.ComboBox cmbHotkeyLetter;
        private System.Windows.Forms.Label lblHotkey;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.CheckBox ckbTopMost;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblGoogleCal;
        private System.Windows.Forms.TextBox tbGoogleCal;
        private System.Windows.Forms.Label lblTooltipText;
        private System.Windows.Forms.TextBox tbTooltipText;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbutton3;
        private System.Windows.Forms.RadioButton rbutton2;
        private System.Windows.Forms.RadioButton rbutton1;
        private System.Windows.Forms.RadioButton rbutton0;
    }
}