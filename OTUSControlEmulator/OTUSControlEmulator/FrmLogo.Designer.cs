namespace OTUSControlEmulator
{
    partial class FrmLogo
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
            this.components = new System.ComponentModel.Container();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.timerLogo = new System.Windows.Forms.Timer(this.components);
            this.timerOpacity = new System.Windows.Forms.Timer(this.components);
            this.labVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pbLogo
            // 
            this.pbLogo.Image = global::OTUSControlEmulator.Properties.Resources.load;
            this.pbLogo.Location = new System.Drawing.Point(126, 104);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(100, 50);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            this.pbLogo.UseWaitCursor = true;
            // 
            // timerLogo
            // 
            this.timerLogo.Enabled = true;
            this.timerLogo.Interval = 500;
            this.timerLogo.Tick += new System.EventHandler(this.timerLogo_Tick);
            // 
            // timerOpacity
            // 
            this.timerOpacity.Enabled = true;
            this.timerOpacity.Interval = 15;
            this.timerOpacity.Tick += new System.EventHandler(this.timerOpacity_Tick);
            // 
            // labVersion
            // 
            this.labVersion.AutoSize = true;
            this.labVersion.Location = new System.Drawing.Point(232, 129);
            this.labVersion.Name = "labVersion";
            this.labVersion.Size = new System.Drawing.Size(45, 13);
            this.labVersion.TabIndex = 1;
            this.labVersion.Text = "Version:";
            // 
            // FrmLogo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImage = global::OTUSControlEmulator.Properties.Resources.Logo5Peleng;
            this.ClientSize = new System.Drawing.Size(350, 151);
            this.Controls.Add(this.labVersion);
            this.Controls.Add(this.pbLogo);
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLogo";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmLogo";
            this.Load += new System.EventHandler(this.FrmLogo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Timer timerLogo;
        private System.Windows.Forms.Timer timerOpacity;
        private System.Windows.Forms.Label labVersion;
    }
}