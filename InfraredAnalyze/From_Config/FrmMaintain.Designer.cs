namespace InfraredAnalyze
{
    partial class FrmMaintain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMaintain));
            this.btnReset = new System.Windows.Forms.Button();
            this.btnLoadDefault = new System.Windows.Forms.Button();
            this.lblSystemInfo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReset.Location = new System.Drawing.Point(109, 193);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(80, 30);
            this.btnReset.TabIndex = 0;
            this.btnReset.Text = "重启仪器";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            this.btnReset.MouseEnter += new System.EventHandler(this.btnReset_MouseEnter);
            this.btnReset.MouseLeave += new System.EventHandler(this.btnReset_MouseLeave);
            // 
            // btnLoadDefault
            // 
            this.btnLoadDefault.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLoadDefault.Location = new System.Drawing.Point(87, 256);
            this.btnLoadDefault.Name = "btnLoadDefault";
            this.btnLoadDefault.Size = new System.Drawing.Size(114, 30);
            this.btnLoadDefault.TabIndex = 0;
            this.btnLoadDefault.Text = "恢复出厂设置";
            this.btnLoadDefault.UseVisualStyleBackColor = true;
            this.btnLoadDefault.Click += new System.EventHandler(this.btnLoadDefault_Click);
            this.btnLoadDefault.MouseEnter += new System.EventHandler(this.btnLoadDefault_MouseEnter);
            this.btnLoadDefault.MouseLeave += new System.EventHandler(this.btnLoadDefault_MouseLeave);
            // 
            // lblSystemInfo
            // 
            this.lblSystemInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSystemInfo.AutoSize = true;
            this.lblSystemInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSystemInfo.Location = new System.Drawing.Point(109, 95);
            this.lblSystemInfo.Name = "lblSystemInfo";
            this.lblSystemInfo.Size = new System.Drawing.Size(80, 16);
            this.lblSystemInfo.TabIndex = 1;
            this.lblSystemInfo.Text = "版本信息:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "仪器版本信息:";
            // 
            // FrmMaintain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSystemInfo);
            this.Controls.Add(this.btnLoadDefault);
            this.Controls.Add(this.btnReset);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMaintain";
            this.Text = "FrmMaintain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMaintain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMaintain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnLoadDefault;
        private System.Windows.Forms.Label lblSystemInfo;
        private System.Windows.Forms.Label label1;
    }
}