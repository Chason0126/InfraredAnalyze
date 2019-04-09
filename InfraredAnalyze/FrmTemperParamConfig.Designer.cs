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
            this.cbxMeasureClass = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpRefeTemp = new System.Windows.Forms.GroupBox();
            this.tbxRefeTemp = new System.Windows.Forms.TextBox();
            this.cbxRefeType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbxReviseTemp = new System.Windows.Forms.TextBox();
            this.tbxReviseParam = new System.Windows.Forms.TextBox();
            this.tbxAmbientHumidity = new System.Windows.Forms.TextBox();
            this.tbxObjDistance = new System.Windows.Forms.TextBox();
            this.tbxAmbientTemp = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.grpRefeTemp.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxMeasureClass
            // 
            this.cbxMeasureClass.FormattingEnabled = true;
            this.cbxMeasureClass.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cbxMeasureClass.Location = new System.Drawing.Point(145, 24);
            this.cbxMeasureClass.Name = "cbxMeasureClass";
            this.cbxMeasureClass.Size = new System.Drawing.Size(81, 20);
            this.cbxMeasureClass.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "测温档位：";
            // 
            // grpRefeTemp
            // 
            this.grpRefeTemp.Controls.Add(this.tbxRefeTemp);
            this.grpRefeTemp.Enabled = false;
            this.grpRefeTemp.Location = new System.Drawing.Point(52, 125);
            this.grpRefeTemp.Name = "grpRefeTemp";
            this.grpRefeTemp.Size = new System.Drawing.Size(185, 52);
            this.grpRefeTemp.TabIndex = 6;
            this.grpRefeTemp.TabStop = false;
            this.grpRefeTemp.Text = "自定义参考温度";
            // 
            // tbxRefeTemp
            // 
            this.tbxRefeTemp.Location = new System.Drawing.Point(74, 20);
            this.tbxRefeTemp.Name = "tbxRefeTemp";
            this.tbxRefeTemp.Size = new System.Drawing.Size(100, 21);
            this.tbxRefeTemp.TabIndex = 0;
            // 
            // cbxRefeType
            // 
            this.cbxRefeType.FormattingEnabled = true;
            this.cbxRefeType.Items.AddRange(new object[] {
            "关闭",
            "自定义",
            "点1",
            "点2",
            "点3",
            "点4",
            "区域1",
            "区域2",
            "区域3"});
            this.cbxRefeType.Location = new System.Drawing.Point(145, 82);
            this.cbxRefeType.Name = "cbxRefeType";
            this.cbxRefeType.Size = new System.Drawing.Size(81, 20);
            this.cbxRefeType.TabIndex = 5;
            this.cbxRefeType.SelectedIndexChanged += new System.EventHandler(this.cbxRefeType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "参考温度类型：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(236, 215);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 16);
            this.label11.TabIndex = 21;
            this.label11.Text = "m";
            // 
            // tbxReviseTemp
            // 
            this.tbxReviseTemp.Location = new System.Drawing.Point(133, 489);
            this.tbxReviseTemp.Name = "tbxReviseTemp";
            this.tbxReviseTemp.Size = new System.Drawing.Size(93, 21);
            this.tbxReviseTemp.TabIndex = 19;
            this.tbxReviseTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxReviseParam
            // 
            this.tbxReviseParam.Location = new System.Drawing.Point(132, 417);
            this.tbxReviseParam.Name = "tbxReviseParam";
            this.tbxReviseParam.Size = new System.Drawing.Size(93, 21);
            this.tbxReviseParam.TabIndex = 20;
            this.tbxReviseParam.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxAmbientHumidity
            // 
            this.tbxAmbientHumidity.Location = new System.Drawing.Point(133, 339);
            this.tbxAmbientHumidity.Name = "tbxAmbientHumidity";
            this.tbxAmbientHumidity.Size = new System.Drawing.Size(92, 21);
            this.tbxAmbientHumidity.TabIndex = 18;
            this.tbxAmbientHumidity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxObjDistance
            // 
            this.tbxObjDistance.Location = new System.Drawing.Point(133, 215);
            this.tbxObjDistance.Name = "tbxObjDistance";
            this.tbxObjDistance.Size = new System.Drawing.Size(93, 21);
            this.tbxObjDistance.TabIndex = 17;
            this.tbxObjDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxAmbientTemp
            // 
            this.tbxAmbientTemp.Location = new System.Drawing.Point(132, 275);
            this.tbxAmbientTemp.Name = "tbxAmbientTemp";
            this.tbxAmbientTemp.Size = new System.Drawing.Size(93, 21);
            this.tbxAmbientTemp.TabIndex = 16;
            this.tbxAmbientTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(61, 492);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "修正温度：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(61, 420);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "修正系数：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(61, 342);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "环境湿度：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 278);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "环境温度：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(61, 218);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "环境距离：";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(132, 545);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 22;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(236, 342);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 16);
            this.label1.TabIndex = 21;
            this.label1.Text = "%";
            // 
            // FrmTemperParamConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 595);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbxReviseTemp);
            this.Controls.Add(this.tbxReviseParam);
            this.Controls.Add(this.tbxAmbientHumidity);
            this.Controls.Add(this.tbxObjDistance);
            this.Controls.Add(this.tbxAmbientTemp);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.grpRefeTemp);
            this.Controls.Add(this.cbxRefeType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxMeasureClass);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTemperParamConfig";
            this.Text = "FrmTemperParamConfig";
            this.Load += new System.EventHandler(this.FrmTemperParamConfig_Load);
            this.grpRefeTemp.ResumeLayout(false);
            this.grpRefeTemp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxMeasureClass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpRefeTemp;
        private System.Windows.Forms.TextBox tbxRefeTemp;
        private System.Windows.Forms.ComboBox cbxRefeType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbxReviseTemp;
        private System.Windows.Forms.TextBox tbxReviseParam;
        private System.Windows.Forms.TextBox tbxAmbientHumidity;
        private System.Windows.Forms.TextBox tbxObjDistance;
        private System.Windows.Forms.TextBox tbxAmbientTemp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label label1;
    }
}