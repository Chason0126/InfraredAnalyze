namespace InfraredAnalyze
{
    partial class FrmCameraConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCameraConfig));
            this.panHeader = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxMAC = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.IPAddressGateWay = new InfraredAnalyze.IpAddressTextBox();
            this.IPAddressSbNetMask = new InfraredAnalyze.IpAddressTextBox();
            this.IPAddressIP = new InfraredAnalyze.IpAddressTextBox();
            this.panHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panHeader.Controls.Add(this.btnClose);
            this.panHeader.Controls.Add(this.label1);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(680, 30);
            this.panHeader.TabIndex = 1;
            this.panHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panHeader_MouseDown);
            this.panHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panHeader_MouseMove);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = global::InfraredAnalyze.Properties.Resources.关闭;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(650, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(30, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(287, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "网络参数设置";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(179, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "IP地址：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(170, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "子网掩码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(179, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 14);
            this.label4.TabIndex = 2;
            this.label4.Text = "网关：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(179, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 14);
            this.label5.TabIndex = 2;
            this.label5.Text = "MAC地址：";
            // 
            // tbxMAC
            // 
            this.tbxMAC.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxMAC.Location = new System.Drawing.Point(248, 150);
            this.tbxMAC.Name = "tbxMAC";
            this.tbxMAC.ReadOnly = true;
            this.tbxMAC.Size = new System.Drawing.Size(227, 23);
            this.tbxMAC.TabIndex = 3;
            this.tbxMAC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.Location = new System.Drawing.Point(300, 189);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(90, 30);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            // 
            // IPAddressGateWay
            // 
            this.IPAddressGateWay.IPAdd = ((System.Net.IPAddress)(resources.GetObject("IPAddressGateWay.IPAdd")));
            this.IPAddressGateWay.Location = new System.Drawing.Point(248, 116);
            this.IPAddressGateWay.Name = "IPAddressGateWay";
            this.IPAddressGateWay.Size = new System.Drawing.Size(241, 26);
            this.IPAddressGateWay.TabIndex = 3;
            this.IPAddressGateWay.Load += new System.EventHandler(this.IPAddressGateWay_Load);
            // 
            // IPAddressSbNetMask
            // 
            this.IPAddressSbNetMask.IPAdd = ((System.Net.IPAddress)(resources.GetObject("IPAddressSbNetMask.IPAdd")));
            this.IPAddressSbNetMask.Location = new System.Drawing.Point(248, 84);
            this.IPAddressSbNetMask.Name = "IPAddressSbNetMask";
            this.IPAddressSbNetMask.Size = new System.Drawing.Size(241, 26);
            this.IPAddressSbNetMask.TabIndex = 2;
            this.IPAddressSbNetMask.Load += new System.EventHandler(this.IPAddressSbNetMask_Load);
            // 
            // IPAddressIP
            // 
            this.IPAddressIP.IPAdd = ((System.Net.IPAddress)(resources.GetObject("IPAddressIP.IPAdd")));
            this.IPAddressIP.Location = new System.Drawing.Point(248, 47);
            this.IPAddressIP.Name = "IPAddressIP";
            this.IPAddressIP.Size = new System.Drawing.Size(241, 26);
            this.IPAddressIP.TabIndex = 1;
            // 
            // FrmCameraConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(680, 235);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.tbxMAC);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.IPAddressGateWay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IPAddressSbNetMask);
            this.Controls.Add(this.panHeader);
            this.Controls.Add(this.IPAddressIP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCameraConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCameraConfig";
            this.Load += new System.EventHandler(this.FrmCameraConfig_Load);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private IpAddressTextBox IPAddressIP;
        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private IpAddressTextBox IPAddressSbNetMask;
        private System.Windows.Forms.Label label3;
        private IpAddressTextBox IPAddressGateWay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxMAC;
        private System.Windows.Forms.Button btnConfirm;
    }
}