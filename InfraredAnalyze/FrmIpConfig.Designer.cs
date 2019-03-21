namespace InfraredAnalyze
{
    partial class FrmIpConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIpConfig));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ucPbx1 = new InfraredAnalyze.UCPbx();
            this.ipAddressTextBox1 = new InfraredAnalyze.IpAddressTextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(800, 35);
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseDown);
            this.pnlHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseMove);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(39, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "配置IP地址";
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = global::InfraredAnalyze.Properties.Resources.关闭;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(765, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(35, 35);
            this.btnClose.TabIndex = 0;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(163, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(497, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "注意：该功能只用于修改IP地址为默认的仪器，设置时请保证只连接一台仪器。";
            // 
            // ucPbx1
            // 
            this.ucPbx1.BackColor = System.Drawing.Color.Gainsboro;
            this.ucPbx1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucPbx1.BackgroundImage")));
            this.ucPbx1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ucPbx1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucPbx1.Id = 0;
            this.ucPbx1.Location = new System.Drawing.Point(9, 79);
            this.ucPbx1.Margin = new System.Windows.Forms.Padding(0);
            this.ucPbx1.Name = "ucPbx1";
            this.ucPbx1.Size = new System.Drawing.Size(320, 240);
            this.ucPbx1.TabIndex = 2;
            // 
            // ipAddressTextBox1
            // 
            this.ipAddressTextBox1.IPAdd = ((System.Net.IPAddress)(resources.GetObject("ipAddressTextBox1.IPAdd")));
            this.ipAddressTextBox1.Location = new System.Drawing.Point(430, 137);
            this.ipAddressTextBox1.Name = "ipAddressTextBox1";
            this.ipAddressTextBox1.Size = new System.Drawing.Size(241, 26);
            this.ipAddressTextBox1.TabIndex = 3;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(500, 248);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "确认修改";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // FrmIpConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 340);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.ipAddressTextBox1);
            this.Controls.Add(this.ucPbx1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmIpConfig";
            this.Text = "FrmIpConfig";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmIpConfig_FormClosing);
            this.Load += new System.EventHandler(this.FrmIpConfig_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private UCPbx ucPbx1;
        private IpAddressTextBox ipAddressTextBox1;
        private System.Windows.Forms.Button btnConfirm;
    }
}