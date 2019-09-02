namespace InfraredAnalyze
{
    partial class FrmConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfig));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlLeftMenu = new System.Windows.Forms.Panel();
            this.btnMaintain = new System.Windows.Forms.Button();
            this.btnPTZConfig = new System.Windows.Forms.Button();
            this.btnVideoConfig = new System.Windows.Forms.Button();
            this.btnSystemConfig = new System.Windows.Forms.Button();
            this.btnAlarmConfig = new System.Windows.Forms.Button();
            this.btnTemperParam = new System.Windows.Forms.Button();
            this.btnImageConfig = new System.Windows.Forms.Button();
            this.btnDataConfig = new System.Windows.Forms.Button();
            this.btnMeasureConfig = new System.Windows.Forms.Button();
            this.btnNetConfig = new System.Windows.Forms.Button();
            this.grpConfig = new System.Windows.Forms.GroupBox();
            this.pnlHeader.SuspendLayout();
            this.pnlLeftMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1000, 35);
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseDown);
            this.pnlHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseMove);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = global::InfraredAnalyze.Properties.Resources.关闭;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(965, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(35, 35);
            this.btnClose.TabIndex = 1;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(476, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "设置";
            // 
            // pnlLeftMenu
            // 
            this.pnlLeftMenu.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlLeftMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLeftMenu.Controls.Add(this.btnMaintain);
            this.pnlLeftMenu.Controls.Add(this.btnPTZConfig);
            this.pnlLeftMenu.Controls.Add(this.btnVideoConfig);
            this.pnlLeftMenu.Controls.Add(this.btnSystemConfig);
            this.pnlLeftMenu.Controls.Add(this.btnAlarmConfig);
            this.pnlLeftMenu.Controls.Add(this.btnTemperParam);
            this.pnlLeftMenu.Controls.Add(this.btnImageConfig);
            this.pnlLeftMenu.Controls.Add(this.btnDataConfig);
            this.pnlLeftMenu.Controls.Add(this.btnMeasureConfig);
            this.pnlLeftMenu.Controls.Add(this.btnNetConfig);
            this.pnlLeftMenu.Location = new System.Drawing.Point(12, 210);
            this.pnlLeftMenu.Name = "pnlLeftMenu";
            this.pnlLeftMenu.Size = new System.Drawing.Size(148, 400);
            this.pnlLeftMenu.TabIndex = 2;
            this.pnlLeftMenu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlLeftMenu_MouseDown);
            // 
            // btnMaintain
            // 
            this.btnMaintain.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMaintain.FlatAppearance.BorderSize = 0;
            this.btnMaintain.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMaintain.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMaintain.Location = new System.Drawing.Point(0, 360);
            this.btnMaintain.Margin = new System.Windows.Forms.Padding(0);
            this.btnMaintain.Name = "btnMaintain";
            this.btnMaintain.Padding = new System.Windows.Forms.Padding(3);
            this.btnMaintain.Size = new System.Drawing.Size(146, 40);
            this.btnMaintain.TabIndex = 9;
            this.btnMaintain.Tag = "0";
            this.btnMaintain.Text = "系统维护";
            this.btnMaintain.UseVisualStyleBackColor = true;
            this.btnMaintain.Click += new System.EventHandler(this.btnMaintain_Click);
            // 
            // btnPTZConfig
            // 
            this.btnPTZConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPTZConfig.FlatAppearance.BorderSize = 0;
            this.btnPTZConfig.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPTZConfig.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPTZConfig.Location = new System.Drawing.Point(0, 320);
            this.btnPTZConfig.Margin = new System.Windows.Forms.Padding(0);
            this.btnPTZConfig.Name = "btnPTZConfig";
            this.btnPTZConfig.Padding = new System.Windows.Forms.Padding(3);
            this.btnPTZConfig.Size = new System.Drawing.Size(146, 40);
            this.btnPTZConfig.TabIndex = 8;
            this.btnPTZConfig.Tag = "0";
            this.btnPTZConfig.Text = "云台控制";
            this.btnPTZConfig.UseVisualStyleBackColor = true;
            this.btnPTZConfig.Click += new System.EventHandler(this.btnPTZConfig_Click);
            this.btnPTZConfig.MouseEnter += new System.EventHandler(this.btnPTZConfig_MouseEnter);
            this.btnPTZConfig.MouseLeave += new System.EventHandler(this.btnPTZConfig_MouseLeave);
            // 
            // btnVideoConfig
            // 
            this.btnVideoConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVideoConfig.FlatAppearance.BorderSize = 0;
            this.btnVideoConfig.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnVideoConfig.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnVideoConfig.Location = new System.Drawing.Point(0, 280);
            this.btnVideoConfig.Margin = new System.Windows.Forms.Padding(0);
            this.btnVideoConfig.Name = "btnVideoConfig";
            this.btnVideoConfig.Size = new System.Drawing.Size(146, 40);
            this.btnVideoConfig.TabIndex = 7;
            this.btnVideoConfig.Tag = "0";
            this.btnVideoConfig.Text = "视频采集设置";
            this.btnVideoConfig.UseVisualStyleBackColor = true;
            this.btnVideoConfig.Click += new System.EventHandler(this.btnVideoConfig_Click);
            this.btnVideoConfig.MouseEnter += new System.EventHandler(this.btnVideoConfig_MouseEnter);
            this.btnVideoConfig.MouseLeave += new System.EventHandler(this.btnVideoConfig_MouseLeave);
            // 
            // btnSystemConfig
            // 
            this.btnSystemConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSystemConfig.FlatAppearance.BorderSize = 0;
            this.btnSystemConfig.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSystemConfig.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSystemConfig.Location = new System.Drawing.Point(0, 240);
            this.btnSystemConfig.Margin = new System.Windows.Forms.Padding(0);
            this.btnSystemConfig.Name = "btnSystemConfig";
            this.btnSystemConfig.Size = new System.Drawing.Size(146, 40);
            this.btnSystemConfig.TabIndex = 6;
            this.btnSystemConfig.Tag = "0";
            this.btnSystemConfig.Text = "系统设置";
            this.btnSystemConfig.UseVisualStyleBackColor = true;
            this.btnSystemConfig.Click += new System.EventHandler(this.btnSystemConfig_Click);
            this.btnSystemConfig.MouseEnter += new System.EventHandler(this.btnSystemConfig_MouseEnter);
            this.btnSystemConfig.MouseLeave += new System.EventHandler(this.btnSystemConfig_MouseLeave);
            // 
            // btnAlarmConfig
            // 
            this.btnAlarmConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAlarmConfig.FlatAppearance.BorderSize = 0;
            this.btnAlarmConfig.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAlarmConfig.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAlarmConfig.Location = new System.Drawing.Point(0, 200);
            this.btnAlarmConfig.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlarmConfig.Name = "btnAlarmConfig";
            this.btnAlarmConfig.Size = new System.Drawing.Size(146, 40);
            this.btnAlarmConfig.TabIndex = 5;
            this.btnAlarmConfig.Tag = "0";
            this.btnAlarmConfig.Text = "报警设置";
            this.btnAlarmConfig.UseVisualStyleBackColor = true;
            this.btnAlarmConfig.Click += new System.EventHandler(this.btnAlarmConfig_Click);
            this.btnAlarmConfig.MouseEnter += new System.EventHandler(this.btnAlarmConfig_MouseEnter);
            this.btnAlarmConfig.MouseLeave += new System.EventHandler(this.btnAlarmConfig_MouseLeave);
            // 
            // btnTemperParam
            // 
            this.btnTemperParam.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTemperParam.FlatAppearance.BorderSize = 0;
            this.btnTemperParam.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTemperParam.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTemperParam.Location = new System.Drawing.Point(0, 160);
            this.btnTemperParam.Margin = new System.Windows.Forms.Padding(0);
            this.btnTemperParam.Name = "btnTemperParam";
            this.btnTemperParam.Size = new System.Drawing.Size(146, 40);
            this.btnTemperParam.TabIndex = 4;
            this.btnTemperParam.Tag = "0";
            this.btnTemperParam.Text = "测温参数设置";
            this.btnTemperParam.UseVisualStyleBackColor = true;
            this.btnTemperParam.Click += new System.EventHandler(this.btnTemperParam_Click);
            this.btnTemperParam.MouseEnter += new System.EventHandler(this.btnTemperParam_MouseEnter);
            this.btnTemperParam.MouseLeave += new System.EventHandler(this.btnTemperParam_MouseLeave);
            // 
            // btnImageConfig
            // 
            this.btnImageConfig.BackColor = System.Drawing.Color.Transparent;
            this.btnImageConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnImageConfig.FlatAppearance.BorderSize = 0;
            this.btnImageConfig.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnImageConfig.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnImageConfig.Location = new System.Drawing.Point(0, 120);
            this.btnImageConfig.Margin = new System.Windows.Forms.Padding(0);
            this.btnImageConfig.Name = "btnImageConfig";
            this.btnImageConfig.Size = new System.Drawing.Size(146, 40);
            this.btnImageConfig.TabIndex = 3;
            this.btnImageConfig.Tag = "0";
            this.btnImageConfig.Text = "图像设置";
            this.btnImageConfig.UseVisualStyleBackColor = false;
            this.btnImageConfig.Click += new System.EventHandler(this.btnImageConfig_Click);
            this.btnImageConfig.MouseEnter += new System.EventHandler(this.btnImageConfig_MouseEnter);
            this.btnImageConfig.MouseLeave += new System.EventHandler(this.btnImageConfig_MouseLeave);
            // 
            // btnDataConfig
            // 
            this.btnDataConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDataConfig.FlatAppearance.BorderSize = 0;
            this.btnDataConfig.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDataConfig.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDataConfig.Location = new System.Drawing.Point(0, 80);
            this.btnDataConfig.Margin = new System.Windows.Forms.Padding(0);
            this.btnDataConfig.Name = "btnDataConfig";
            this.btnDataConfig.Size = new System.Drawing.Size(146, 40);
            this.btnDataConfig.TabIndex = 2;
            this.btnDataConfig.Tag = "0";
            this.btnDataConfig.Text = "图像采集设置";
            this.btnDataConfig.UseVisualStyleBackColor = true;
            this.btnDataConfig.Click += new System.EventHandler(this.btnDataConfig_Click);
            this.btnDataConfig.MouseEnter += new System.EventHandler(this.btnDataConfig_MouseEnter);
            this.btnDataConfig.MouseLeave += new System.EventHandler(this.btnDataConfig_MouseLeave);
            // 
            // btnMeasureConfig
            // 
            this.btnMeasureConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMeasureConfig.FlatAppearance.BorderSize = 0;
            this.btnMeasureConfig.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMeasureConfig.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMeasureConfig.Location = new System.Drawing.Point(0, 40);
            this.btnMeasureConfig.Margin = new System.Windows.Forms.Padding(0);
            this.btnMeasureConfig.Name = "btnMeasureConfig";
            this.btnMeasureConfig.Size = new System.Drawing.Size(146, 40);
            this.btnMeasureConfig.TabIndex = 1;
            this.btnMeasureConfig.Tag = "0";
            this.btnMeasureConfig.Text = "测温设置";
            this.btnMeasureConfig.UseVisualStyleBackColor = true;
            this.btnMeasureConfig.Click += new System.EventHandler(this.btnMeasureConfig_Click);
            this.btnMeasureConfig.MouseEnter += new System.EventHandler(this.btnMeasureConfig_MouseEnter);
            this.btnMeasureConfig.MouseLeave += new System.EventHandler(this.btnMeasureConfig_MouseLeave);
            // 
            // btnNetConfig
            // 
            this.btnNetConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNetConfig.FlatAppearance.BorderSize = 0;
            this.btnNetConfig.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNetConfig.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNetConfig.Location = new System.Drawing.Point(0, 0);
            this.btnNetConfig.Margin = new System.Windows.Forms.Padding(0);
            this.btnNetConfig.Name = "btnNetConfig";
            this.btnNetConfig.Size = new System.Drawing.Size(146, 40);
            this.btnNetConfig.TabIndex = 0;
            this.btnNetConfig.Tag = "0";
            this.btnNetConfig.Text = "网络设置";
            this.btnNetConfig.UseVisualStyleBackColor = true;
            this.btnNetConfig.Click += new System.EventHandler(this.btnNetConfig_Click);
            this.btnNetConfig.MouseEnter += new System.EventHandler(this.btnNetConfig_MouseEnter);
            this.btnNetConfig.MouseLeave += new System.EventHandler(this.btnNetConfig_MouseLeave);
            // 
            // grpConfig
            // 
            this.grpConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpConfig.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpConfig.Location = new System.Drawing.Point(187, 53);
            this.grpConfig.Margin = new System.Windows.Forms.Padding(0);
            this.grpConfig.Name = "grpConfig";
            this.grpConfig.Size = new System.Drawing.Size(801, 840);
            this.grpConfig.TabIndex = 3;
            this.grpConfig.TabStop = false;
            this.grpConfig.Text = "网络设置";
            // 
            // FrmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 900);
            this.Controls.Add(this.grpConfig);
            this.Controls.Add(this.pnlLeftMenu);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "探测器设置";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmConfig_FormClosed);
            this.Load += new System.EventHandler(this.FrmConfig_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlLeftMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlLeftMenu;
        private System.Windows.Forms.GroupBox grpConfig;
        private System.Windows.Forms.Button btnPTZConfig;
        private System.Windows.Forms.Button btnVideoConfig;
        private System.Windows.Forms.Button btnSystemConfig;
        private System.Windows.Forms.Button btnAlarmConfig;
        private System.Windows.Forms.Button btnTemperParam;
        private System.Windows.Forms.Button btnImageConfig;
        private System.Windows.Forms.Button btnDataConfig;
        private System.Windows.Forms.Button btnMeasureConfig;
        private System.Windows.Forms.Button btnNetConfig;
        private System.Windows.Forms.Button btnMaintain;
    }
}