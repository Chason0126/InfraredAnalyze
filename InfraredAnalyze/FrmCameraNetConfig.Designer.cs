namespace InfraredAnalyze
{
    partial class FrmCameraNetConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCameraNetConfig));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxMAC = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.chbModifyMac = new System.Windows.Forms.CheckBox();
            this.chbModifyGateWay = new System.Windows.Forms.CheckBox();
            this.chbModifyNetMask = new System.Windows.Forms.CheckBox();
            this.IPAddressGateWay = new InfraredAnalyze.IpAddressTextBox();
            this.IPAddressSubMask = new InfraredAnalyze.IpAddressTextBox();
            this.IPAddressIP = new InfraredAnalyze.IpAddressTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxIsEnable = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(209, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "IP地址：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(198, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "子网掩码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(225, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 14);
            this.label4.TabIndex = 2;
            this.label4.Text = "网关：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(198, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 14);
            this.label5.TabIndex = 2;
            this.label5.Text = "MAC地址：";
            // 
            // tbxMAC
            // 
            this.tbxMAC.Enabled = false;
            this.tbxMAC.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxMAC.Location = new System.Drawing.Point(289, 175);
            this.tbxMAC.Name = "tbxMAC";
            this.tbxMAC.ReadOnly = true;
            this.tbxMAC.Size = new System.Drawing.Size(264, 26);
            this.tbxMAC.TabIndex = 4;
            this.tbxMAC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.ForeColor = System.Drawing.Color.Black;
            this.btnConfirm.Location = new System.Drawing.Point(365, 227);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(105, 35);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Tag = "null";
            this.btnConfirm.Text = "确认修改";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // chbModifyMac
            // 
            this.chbModifyMac.AutoSize = true;
            this.chbModifyMac.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chbModifyMac.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chbModifyMac.Location = new System.Drawing.Point(577, 180);
            this.chbModifyMac.Name = "chbModifyMac";
            this.chbModifyMac.Size = new System.Drawing.Size(48, 16);
            this.chbModifyMac.TabIndex = 5;
            this.chbModifyMac.Text = "修改";
            this.chbModifyMac.UseVisualStyleBackColor = true;
            this.chbModifyMac.CheckedChanged += new System.EventHandler(this.chbModifyMac_CheckedChanged);
            // 
            // chbModifyGateWay
            // 
            this.chbModifyGateWay.AutoSize = true;
            this.chbModifyGateWay.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chbModifyGateWay.Location = new System.Drawing.Point(577, 141);
            this.chbModifyGateWay.Name = "chbModifyGateWay";
            this.chbModifyGateWay.Size = new System.Drawing.Size(54, 18);
            this.chbModifyGateWay.TabIndex = 7;
            this.chbModifyGateWay.Text = "修改";
            this.chbModifyGateWay.UseVisualStyleBackColor = true;
            this.chbModifyGateWay.CheckedChanged += new System.EventHandler(this.chbModifyGateWay_CheckedChanged);
            // 
            // chbModifyNetMask
            // 
            this.chbModifyNetMask.AutoSize = true;
            this.chbModifyNetMask.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chbModifyNetMask.Location = new System.Drawing.Point(577, 104);
            this.chbModifyNetMask.Name = "chbModifyNetMask";
            this.chbModifyNetMask.Size = new System.Drawing.Size(54, 18);
            this.chbModifyNetMask.TabIndex = 7;
            this.chbModifyNetMask.Text = "修改";
            this.chbModifyNetMask.UseVisualStyleBackColor = true;
            this.chbModifyNetMask.CheckedChanged += new System.EventHandler(this.chbModifyNetMask_CheckedChanged);
            // 
            // IPAddressGateWay
            // 
            this.IPAddressGateWay.Enabled = false;
            this.IPAddressGateWay.IPAdd = ((System.Net.IPAddress)(resources.GetObject("IPAddressGateWay.IPAdd")));
            this.IPAddressGateWay.Location = new System.Drawing.Point(289, 135);
            this.IPAddressGateWay.Name = "IPAddressGateWay";
            this.IPAddressGateWay.Size = new System.Drawing.Size(281, 30);
            this.IPAddressGateWay.TabIndex = 3;
            // 
            // IPAddressSubMask
            // 
            this.IPAddressSubMask.Enabled = false;
            this.IPAddressSubMask.IPAdd = ((System.Net.IPAddress)(resources.GetObject("IPAddressSubMask.IPAdd")));
            this.IPAddressSubMask.Location = new System.Drawing.Point(289, 98);
            this.IPAddressSubMask.Name = "IPAddressSubMask";
            this.IPAddressSubMask.Size = new System.Drawing.Size(281, 30);
            this.IPAddressSubMask.TabIndex = 2;
            // 
            // IPAddressIP
            // 
            this.IPAddressIP.IPAdd = ((System.Net.IPAddress)(resources.GetObject("IPAddressIP.IPAdd")));
            this.IPAddressIP.Location = new System.Drawing.Point(289, 55);
            this.IPAddressIP.Name = "IPAddressIP";
            this.IPAddressIP.Size = new System.Drawing.Size(281, 30);
            this.IPAddressIP.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(198, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 14);
            this.label7.TabIndex = 2;
            this.label7.Text = "是否启用：";
            // 
            // cbxIsEnable
            // 
            this.cbxIsEnable.FormattingEnabled = true;
            this.cbxIsEnable.Items.AddRange(new object[] {
            "否",
            "是"});
            this.cbxIsEnable.Location = new System.Drawing.Point(289, 12);
            this.cbxIsEnable.Name = "cbxIsEnable";
            this.cbxIsEnable.Size = new System.Drawing.Size(98, 22);
            this.cbxIsEnable.TabIndex = 11;
            // 
            // FrmCameraNetConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(793, 288);
            this.Controls.Add(this.cbxIsEnable);
            this.Controls.Add(this.chbModifyNetMask);
            this.Controls.Add(this.chbModifyGateWay);
            this.Controls.Add(this.chbModifyMac);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.tbxMAC);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.IPAddressGateWay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IPAddressSubMask);
            this.Controls.Add(this.IPAddressIP);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCameraNetConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCameraConfig";
            this.Load += new System.EventHandler(this.FrmCameraConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private IpAddressTextBox IPAddressIP;
        private System.Windows.Forms.Label label2;
        private IpAddressTextBox IPAddressSubMask;
        private System.Windows.Forms.Label label3;
        private IpAddressTextBox IPAddressGateWay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxMAC;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.CheckBox chbModifyMac;
        private System.Windows.Forms.CheckBox chbModifyGateWay;
        private System.Windows.Forms.CheckBox chbModifyNetMask;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbxIsEnable;
    }
}