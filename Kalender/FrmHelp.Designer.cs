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
            this.labelCaption = new System.Windows.Forms.Label();
            this.lCopyright = new System.Windows.Forms.Label();
            this.labelLine = new System.Windows.Forms.Label();
            this.linkLabelOS = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // labelCaption
            // 
            this.labelCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelCaption.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCaption.Location = new System.Drawing.Point(0, 0);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.labelCaption.Size = new System.Drawing.Size(205, 55);
            this.labelCaption.TabIndex = 0;
            this.labelCaption.Text = "Kalender";
            this.labelCaption.Click += new System.EventHandler(this.Label_Click);
            // 
            // lCopyright
            // 
            this.lCopyright.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lCopyright.Location = new System.Drawing.Point(0, 71);
            this.lCopyright.Name = "lCopyright";
            this.lCopyright.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lCopyright.Size = new System.Drawing.Size(205, 21);
            this.lCopyright.TabIndex = 1;
            this.lCopyright.Text = "© 2019-2022, Dr. Wilhelm Happe";
            this.lCopyright.Click += new System.EventHandler(this.LCopyright_Click);
            // 
            // labelLine
            // 
            this.labelLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelLine.Location = new System.Drawing.Point(0, 55);
            this.labelLine.Name = "labelLine";
            this.labelLine.Size = new System.Drawing.Size(205, 2);
            this.labelLine.TabIndex = 2;
            this.labelLine.Click += new System.EventHandler(this.LabelLine_Click);
            // 
            // linkLabelOS
            // 
            this.linkLabelOS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelOS.AutoSize = true;
            this.linkLabelOS.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelOS.Location = new System.Drawing.Point(12, 95);
            this.linkLabelOS.Name = "linkLabelOS";
            this.linkLabelOS.Size = new System.Drawing.Size(181, 15);
            this.linkLabelOS.TabIndex = 4;
            this.linkLabelOS.TabStop = true;
            this.linkLabelOS.Text = "www.ophthalmostar.de/freeware";
            this.linkLabelOS.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.linkLabelOS.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelOS_LinkClicked);
            // 
            // FrmHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(205, 122);
            this.Controls.Add(this.linkLabelOS);
            this.Controls.Add(this.labelLine);
            this.Controls.Add(this.lCopyright);
            this.Controls.Add(this.labelCaption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmHelp";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Über...";
            this.TopMost = true;
            this.Click += new System.EventHandler(this.FrmHelp_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmHelp_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.Label lCopyright;
        private System.Windows.Forms.Label labelLine;
        private System.Windows.Forms.LinkLabel linkLabelOS;
    }
}