namespace Kalender
{
    partial class FrmHelp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHelp));
            this.label = new System.Windows.Forms.Label();
            this.lCopyright = new System.Windows.Forms.Label();
            this.labelLine = new System.Windows.Forms.Label();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Dock = System.Windows.Forms.DockStyle.Top;
            this.label.Location = new System.Drawing.Point(0, 0);
            this.label.Name = "label";
            this.label.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label.Size = new System.Drawing.Size(205, 157);
            this.label.TabIndex = 0;
            this.label.Text = resources.GetString("label.Text");
            this.label.Click += new System.EventHandler(this.Label_Click);
            // 
            // lCopyright
            // 
            this.lCopyright.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lCopyright.Location = new System.Drawing.Point(0, 166);
            this.lCopyright.Name = "lCopyright";
            this.lCopyright.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lCopyright.Size = new System.Drawing.Size(205, 25);
            this.lCopyright.TabIndex = 1;
            this.lCopyright.Text = "© 2019, Dr. Wilhelm Happe, Kiel";
            this.lCopyright.Click += new System.EventHandler(this.LCopyright_Click);
            // 
            // labelLine
            // 
            this.labelLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelLine.Location = new System.Drawing.Point(0, 157);
            this.labelLine.Name = "labelLine";
            this.labelLine.Size = new System.Drawing.Size(205, 2);
            this.labelLine.TabIndex = 2;
            this.labelLine.Click += new System.EventHandler(this.LabelLine_Click);
            // 
            // linkLabel
            // 
            this.linkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel.AutoSize = true;
            this.linkLabel.Location = new System.Drawing.Point(12, 133);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(124, 13);
            this.linkLabel.TabIndex = 3;
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "Aufgabenplanung öffnen";
            this.linkLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // FrmHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(205, 191);
            this.Controls.Add(this.linkLabel);
            this.Controls.Add(this.labelLine);
            this.Controls.Add(this.lCopyright);
            this.Controls.Add(this.label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmHelp";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hilfe";
            this.TopMost = true;
            this.Click += new System.EventHandler(this.FrmHelp_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmHelp_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label lCopyright;
        private System.Windows.Forms.Label labelLine;
        private System.Windows.Forms.LinkLabel linkLabel;
    }
}