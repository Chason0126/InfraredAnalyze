namespace InfraredAnalyze
{
    partial class FrmTemperParamConfig
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.pbxScreen = new System.Windows.Forms.PictureBox();
            this.grpIsTempValueOnImage = new System.Windows.Forms.GroupBox();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1149, 35);
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseDown);
            this.pnlHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseMove);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::InfraredAnalyze.Properties.Resources.关闭;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(1114, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(35, 35);
            this.btnClose.TabIndex = 0;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // pbxScreen
            // 
            this.pbxScreen.BackColor = System.Drawing.Color.Black;
            this.pbxScreen.Location = new System.Drawing.Point(12, 41);
            this.pbxScreen.Name = "pbxScreen";
            this.pbxScreen.Size = new System.Drawing.Size(640, 480);
            this.pbxScreen.TabIndex = 1;
            this.pbxScreen.TabStop = false;
            // 
            // grpIsTempValueOnImage
            // 
            this.grpIsTempValueOnImage.Location = new System.Drawing.Point(694, 57);
            this.grpIsTempValueOnImage.Name = "grpIsTempValueOnImage";
            this.grpIsTempValueOnImage.Size = new System.Drawing.Size(200, 64);
            this.grpIsTempValueOnImage.TabIndex = 2;
            this.grpIsTempValueOnImage.TabStop = false;
            this.grpIsTempValueOnImage.Text = "是否显示测温数据";
            // 
            // FrmTemperParamConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 650);
            this.Controls.Add(this.grpIsTempValueOnImage);
            this.Controls.Add(this.pbxScreen);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTemperParamConfig";
            this.Text = "FrmTemperParamConfig";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTemperParamConfig_FormClosing);
            this.Load += new System.EventHandler(this.FrmTemperParamConfig_Load);
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxScreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pbxScreen;
        private System.Windows.Forms.GroupBox grpIsTempValueOnImage;
    }
}