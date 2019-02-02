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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckbTopMost = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.cmbHotkeyLetter.Location = new System.Drawing.Point(168, 42);
            this.cmbHotkeyLetter.MaxLength = 1;
            this.cmbHotkeyLetter.Name = "cmbHotkeyLetter";
            this.cmbHotkeyLetter.Size = new System.Drawing.Size(35, 21);
            this.cmbHotkeyLetter.TabIndex = 1;
            this.cmbHotkeyLetter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbHotkeyLetter_KeyPress);
            // 
            // lblHotkey
            // 
            this.lblHotkey.AutoSize = true;
            this.lblHotkey.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblHotkey.Enabled = false;
            this.lblHotkey.Location = new System.Drawing.Point(3, 45);
            this.lblHotkey.Name = "lblHotkey";
            this.lblHotkey.Size = new System.Drawing.Size(165, 13);
            this.lblHotkey.TabIndex = 2;
            this.lblHotkey.Text = "Systemweiter Hotkey: Strg+Win+ ";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(13, 152);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(130, 152);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel.Controls.Add(this.groupBox1);
            this.panel.Controls.Add(this.groupBox2);
            this.panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(238, 146);
            this.panel.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckbTopMost);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
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
            this.ckbTopMost.Location = new System.Drawing.Point(6, 19);
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
            this.groupBox2.Location = new System.Drawing.Point(13, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(213, 72);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Standby Mode";
            // 
            // FrmSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(238, 181);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
    }
}