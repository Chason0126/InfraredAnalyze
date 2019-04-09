namespace InfraredAnalyze
{
    partial class FrmSystemConfig
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
            this.label1 = new System.Windows.Forms.Label();
            this.pnlCameraName = new System.Windows.Forms.Panel();
            this.rdbNotCameraName = new System.Windows.Forms.RadioButton();
            this.rdbIsCameraName = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlTime = new System.Windows.Forms.Panel();
            this.rdbNotTime = new System.Windows.Forms.RadioButton();
            this.rdbIsTime = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxCameraName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnUpdateTime = new System.Windows.Forms.Button();
            this.tbxFrameRate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpCameraDateTime = new System.Windows.Forms.DateTimePicker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxBitrateType = new System.Windows.Forms.ComboBox();
            this.cbxResolution = new System.Windows.Forms.ComboBox();
            this.cbxBitrate = new System.Windows.Forms.ComboBox();
            this.pnlLocation = new System.Windows.Forms.Panel();
            this.rdbCTimeLocation = new System.Windows.Forms.RadioButton();
            this.rdbCNameLocation = new System.Windows.Forms.RadioButton();
            this.btnLocationDown = new System.Windows.Forms.Button();
            this.btnLocationRight = new System.Windows.Forms.Button();
            this.btnLocationLeft = new System.Windows.Forms.Button();
            this.btnLocationUp = new System.Windows.Forms.Button();
            this.pbxVideo = new System.Windows.Forms.PictureBox();
            this.trbLocation = new System.Windows.Forms.TrackBar();
            this.lbltrbValue = new System.Windows.Forms.Label();
            this.pnlEncodingInfoChoose = new System.Windows.Forms.Panel();
            this.rdbMinor = new System.Windows.Forms.RadioButton();
            this.rdbMajor = new System.Windows.Forms.RadioButton();
            this.pnlEncodingInfo = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlCameraName.SuspendLayout();
            this.pnlTime.SuspendLayout();
            this.pnlLocation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbLocation)).BeginInit();
            this.pnlEncodingInfoChoose.SuspendLayout();
            this.pnlEncodingInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(86, 390);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "相机名称：";
            // 
            // pnlCameraName
            // 
            this.pnlCameraName.Controls.Add(this.rdbNotCameraName);
            this.pnlCameraName.Controls.Add(this.rdbIsCameraName);
            this.pnlCameraName.Controls.Add(this.label2);
            this.pnlCameraName.Location = new System.Drawing.Point(90, 475);
            this.pnlCameraName.Name = "pnlCameraName";
            this.pnlCameraName.Size = new System.Drawing.Size(259, 37);
            this.pnlCameraName.TabIndex = 2;
            // 
            // rdbNotCameraName
            // 
            this.rdbNotCameraName.AutoSize = true;
            this.rdbNotCameraName.Location = new System.Drawing.Point(212, 10);
            this.rdbNotCameraName.Name = "rdbNotCameraName";
            this.rdbNotCameraName.Size = new System.Drawing.Size(35, 16);
            this.rdbNotCameraName.TabIndex = 6;
            this.rdbNotCameraName.Text = "否";
            this.rdbNotCameraName.UseVisualStyleBackColor = true;
            this.rdbNotCameraName.CheckedChanged += new System.EventHandler(this.rdbNotCameraName_CheckedChanged);
            // 
            // rdbIsCameraName
            // 
            this.rdbIsCameraName.AutoSize = true;
            this.rdbIsCameraName.Location = new System.Drawing.Point(156, 10);
            this.rdbIsCameraName.Name = "rdbIsCameraName";
            this.rdbIsCameraName.Size = new System.Drawing.Size(35, 16);
            this.rdbIsCameraName.TabIndex = 5;
            this.rdbIsCameraName.Text = "是";
            this.rdbIsCameraName.UseVisualStyleBackColor = true;
            this.rdbIsCameraName.CheckedChanged += new System.EventHandler(this.rdbIsCameraName_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "是否显示相机名称：";
            // 
            // pnlTime
            // 
            this.pnlTime.Controls.Add(this.rdbNotTime);
            this.pnlTime.Controls.Add(this.rdbIsTime);
            this.pnlTime.Controls.Add(this.label3);
            this.pnlTime.Location = new System.Drawing.Point(90, 528);
            this.pnlTime.Name = "pnlTime";
            this.pnlTime.Size = new System.Drawing.Size(259, 40);
            this.pnlTime.TabIndex = 2;
            // 
            // rdbNotTime
            // 
            this.rdbNotTime.AutoSize = true;
            this.rdbNotTime.Location = new System.Drawing.Point(212, 15);
            this.rdbNotTime.Name = "rdbNotTime";
            this.rdbNotTime.Size = new System.Drawing.Size(35, 16);
            this.rdbNotTime.TabIndex = 8;
            this.rdbNotTime.Text = "否";
            this.rdbNotTime.UseVisualStyleBackColor = true;
            this.rdbNotTime.CheckedChanged += new System.EventHandler(this.rdbNotTime_CheckedChanged);
            // 
            // rdbIsTime
            // 
            this.rdbIsTime.AutoSize = true;
            this.rdbIsTime.Location = new System.Drawing.Point(156, 15);
            this.rdbIsTime.Name = "rdbIsTime";
            this.rdbIsTime.Size = new System.Drawing.Size(35, 16);
            this.rdbIsTime.TabIndex = 7;
            this.rdbIsTime.Text = "是";
            this.rdbIsTime.UseVisualStyleBackColor = true;
            this.rdbIsTime.CheckedChanged += new System.EventHandler(this.rdbIsTime_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(19, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 21);
            this.label3.TabIndex = 1;
            this.label3.Text = "是否显示时间：";
            // 
            // tbxCameraName
            // 
            this.tbxCameraName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxCameraName.Location = new System.Drawing.Point(181, 387);
            this.tbxCameraName.Name = "tbxCameraName";
            this.tbxCameraName.Size = new System.Drawing.Size(172, 23);
            this.tbxCameraName.TabIndex = 2;
            this.tbxCameraName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxCameraName.Leave += new System.EventHandler(this.tbxCameraName_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(508, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "（改变显示位置）";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.Location = new System.Drawing.Point(323, 595);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(80, 30);
            this.btnConfirm.TabIndex = 22;
            this.btnConfirm.Text = "保存设置";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnUpdateTime
            // 
            this.btnUpdateTime.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUpdateTime.Location = new System.Drawing.Point(376, 429);
            this.btnUpdateTime.Name = "btnUpdateTime";
            this.btnUpdateTime.Size = new System.Drawing.Size(60, 23);
            this.btnUpdateTime.TabIndex = 4;
            this.btnUpdateTime.Text = "校准";
            this.btnUpdateTime.UseVisualStyleBackColor = true;
            this.btnUpdateTime.Click += new System.EventHandler(this.btnUpdateTime_Click);
            // 
            // tbxFrameRate
            // 
            this.tbxFrameRate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxFrameRate.Location = new System.Drawing.Point(103, 130);
            this.tbxFrameRate.Name = "tbxFrameRate";
            this.tbxFrameRate.Size = new System.Drawing.Size(121, 23);
            this.tbxFrameRate.TabIndex = 12;
            this.tbxFrameRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxFrameRate.Leave += new System.EventHandler(this.tbxFrameRate_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(22, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 21);
            this.label5.TabIndex = 11;
            this.label5.Text = "帧数：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(86, 431);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 21);
            this.label6.TabIndex = 12;
            this.label6.Text = "系统时间：";
            // 
            // dtpCameraDateTime
            // 
            this.dtpCameraDateTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpCameraDateTime.Enabled = false;
            this.dtpCameraDateTime.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpCameraDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCameraDateTime.Location = new System.Drawing.Point(182, 431);
            this.dtpCameraDateTime.Name = "dtpCameraDateTime";
            this.dtpCameraDateTime.Size = new System.Drawing.Size(173, 23);
            this.dtpCameraDateTime.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(22, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 21);
            this.label7.TabIndex = 11;
            this.label7.Text = "码流：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(14, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 21);
            this.label8.TabIndex = 11;
            this.label8.Text = "分辨率：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(6, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 21);
            this.label9.TabIndex = 11;
            this.label9.Text = "编码类型：";
            // 
            // cbxBitrateType
            // 
            this.cbxBitrateType.FormattingEnabled = true;
            this.cbxBitrateType.Items.AddRange(new object[] {
            "可变编码",
            "固定编码"});
            this.cbxBitrateType.Location = new System.Drawing.Point(103, 13);
            this.cbxBitrateType.Name = "cbxBitrateType";
            this.cbxBitrateType.Size = new System.Drawing.Size(121, 20);
            this.cbxBitrateType.TabIndex = 9;
            this.cbxBitrateType.SelectedIndexChanged += new System.EventHandler(this.cbxBitrateType_SelectedIndexChanged);
            // 
            // cbxResolution
            // 
            this.cbxResolution.FormattingEnabled = true;
            this.cbxResolution.Items.AddRange(new object[] {
            "320x240",
            "384x288",
            "640x480",
            "720x480",
            "720x576"});
            this.cbxResolution.Location = new System.Drawing.Point(103, 58);
            this.cbxResolution.Name = "cbxResolution";
            this.cbxResolution.Size = new System.Drawing.Size(121, 20);
            this.cbxResolution.TabIndex = 10;
            this.cbxResolution.SelectedIndexChanged += new System.EventHandler(this.cbxResolution_SelectedIndexChanged);
            // 
            // cbxBitrate
            // 
            this.cbxBitrate.FormattingEnabled = true;
            this.cbxBitrate.Items.AddRange(new object[] {
            "128",
            "256 ",
            "512",
            "1024"});
            this.cbxBitrate.Location = new System.Drawing.Point(103, 96);
            this.cbxBitrate.Name = "cbxBitrate";
            this.cbxBitrate.Size = new System.Drawing.Size(121, 20);
            this.cbxBitrate.TabIndex = 11;
            this.cbxBitrate.SelectedIndexChanged += new System.EventHandler(this.cbxBitrate_SelectedIndexChanged);
            // 
            // pnlLocation
            // 
            this.pnlLocation.Controls.Add(this.rdbCTimeLocation);
            this.pnlLocation.Controls.Add(this.rdbCNameLocation);
            this.pnlLocation.Location = new System.Drawing.Point(526, 39);
            this.pnlLocation.Name = "pnlLocation";
            this.pnlLocation.Size = new System.Drawing.Size(102, 88);
            this.pnlLocation.TabIndex = 16;
            // 
            // rdbCTimeLocation
            // 
            this.rdbCTimeLocation.AutoSize = true;
            this.rdbCTimeLocation.Location = new System.Drawing.Point(14, 54);
            this.rdbCTimeLocation.Name = "rdbCTimeLocation";
            this.rdbCTimeLocation.Size = new System.Drawing.Size(47, 16);
            this.rdbCTimeLocation.TabIndex = 21;
            this.rdbCTimeLocation.Text = "时间";
            this.rdbCTimeLocation.UseVisualStyleBackColor = true;
            // 
            // rdbCNameLocation
            // 
            this.rdbCNameLocation.AutoSize = true;
            this.rdbCNameLocation.Checked = true;
            this.rdbCNameLocation.Location = new System.Drawing.Point(14, 16);
            this.rdbCNameLocation.Name = "rdbCNameLocation";
            this.rdbCNameLocation.Size = new System.Drawing.Size(47, 16);
            this.rdbCNameLocation.TabIndex = 20;
            this.rdbCNameLocation.TabStop = true;
            this.rdbCNameLocation.Text = "名称";
            this.rdbCNameLocation.UseVisualStyleBackColor = true;
            // 
            // btnLocationDown
            // 
            this.btnLocationDown.BackgroundImage = global::InfraredAnalyze.Properties.Resources.down;
            this.btnLocationDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLocationDown.FlatAppearance.BorderSize = 0;
            this.btnLocationDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocationDown.Location = new System.Drawing.Point(563, 232);
            this.btnLocationDown.Name = "btnLocationDown";
            this.btnLocationDown.Size = new System.Drawing.Size(40, 40);
            this.btnLocationDown.TabIndex = 17;
            this.btnLocationDown.UseVisualStyleBackColor = true;
            this.btnLocationDown.Click += new System.EventHandler(this.btnLocationDown_Click);
            // 
            // btnLocationRight
            // 
            this.btnLocationRight.BackgroundImage = global::InfraredAnalyze.Properties.Resources.right;
            this.btnLocationRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLocationRight.FlatAppearance.BorderSize = 0;
            this.btnLocationRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocationRight.Location = new System.Drawing.Point(609, 194);
            this.btnLocationRight.Name = "btnLocationRight";
            this.btnLocationRight.Size = new System.Drawing.Size(40, 40);
            this.btnLocationRight.TabIndex = 19;
            this.btnLocationRight.UseVisualStyleBackColor = true;
            this.btnLocationRight.Click += new System.EventHandler(this.btnLocationRight_Click);
            // 
            // btnLocationLeft
            // 
            this.btnLocationLeft.BackgroundImage = global::InfraredAnalyze.Properties.Resources.left;
            this.btnLocationLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLocationLeft.FlatAppearance.BorderSize = 0;
            this.btnLocationLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocationLeft.Location = new System.Drawing.Point(517, 194);
            this.btnLocationLeft.Name = "btnLocationLeft";
            this.btnLocationLeft.Size = new System.Drawing.Size(40, 40);
            this.btnLocationLeft.TabIndex = 18;
            this.btnLocationLeft.UseVisualStyleBackColor = true;
            this.btnLocationLeft.Click += new System.EventHandler(this.btnLocationLeft_Click);
            // 
            // btnLocationUp
            // 
            this.btnLocationUp.BackgroundImage = global::InfraredAnalyze.Properties.Resources.up;
            this.btnLocationUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLocationUp.FlatAppearance.BorderSize = 0;
            this.btnLocationUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocationUp.Location = new System.Drawing.Point(563, 159);
            this.btnLocationUp.Name = "btnLocationUp";
            this.btnLocationUp.Size = new System.Drawing.Size(40, 40);
            this.btnLocationUp.TabIndex = 16;
            this.btnLocationUp.UseVisualStyleBackColor = true;
            this.btnLocationUp.Click += new System.EventHandler(this.btnLocationUp_Click);
            // 
            // pbxVideo
            // 
            this.pbxVideo.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pbxVideo.Location = new System.Drawing.Point(12, 12);
            this.pbxVideo.Name = "pbxVideo";
            this.pbxVideo.Size = new System.Drawing.Size(480, 360);
            this.pbxVideo.TabIndex = 4;
            this.pbxVideo.TabStop = false;
            // 
            // trbLocation
            // 
            this.trbLocation.BackColor = System.Drawing.SystemColors.ControlLight;
            this.trbLocation.Location = new System.Drawing.Point(526, 278);
            this.trbLocation.Minimum = 1;
            this.trbLocation.Name = "trbLocation";
            this.trbLocation.Size = new System.Drawing.Size(104, 45);
            this.trbLocation.TabIndex = 15;
            this.trbLocation.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trbLocation.Value = 1;
            this.trbLocation.Scroll += new System.EventHandler(this.trbLocation_Scroll);
            // 
            // lbltrbValue
            // 
            this.lbltrbValue.AutoSize = true;
            this.lbltrbValue.Location = new System.Drawing.Point(637, 294);
            this.lbltrbValue.Name = "lbltrbValue";
            this.lbltrbValue.Size = new System.Drawing.Size(11, 12);
            this.lbltrbValue.TabIndex = 19;
            this.lbltrbValue.Text = "5";
            // 
            // pnlEncodingInfoChoose
            // 
            this.pnlEncodingInfoChoose.Controls.Add(this.rdbMinor);
            this.pnlEncodingInfoChoose.Controls.Add(this.rdbMajor);
            this.pnlEncodingInfoChoose.Location = new System.Drawing.Point(83, 159);
            this.pnlEncodingInfoChoose.Name = "pnlEncodingInfoChoose";
            this.pnlEncodingInfoChoose.Size = new System.Drawing.Size(136, 135);
            this.pnlEncodingInfoChoose.TabIndex = 16;
            // 
            // rdbMinor
            // 
            this.rdbMinor.AutoSize = true;
            this.rdbMinor.Location = new System.Drawing.Point(14, 54);
            this.rdbMinor.Name = "rdbMinor";
            this.rdbMinor.Size = new System.Drawing.Size(59, 16);
            this.rdbMinor.TabIndex = 14;
            this.rdbMinor.Text = "子码流";
            this.rdbMinor.UseVisualStyleBackColor = true;
            this.rdbMinor.CheckedChanged += new System.EventHandler(this.rdbMinor_CheckedChanged);
            // 
            // rdbMajor
            // 
            this.rdbMajor.AutoSize = true;
            this.rdbMajor.Location = new System.Drawing.Point(14, 16);
            this.rdbMajor.Name = "rdbMajor";
            this.rdbMajor.Size = new System.Drawing.Size(59, 16);
            this.rdbMajor.TabIndex = 13;
            this.rdbMajor.Text = "主码流";
            this.rdbMajor.UseVisualStyleBackColor = true;
            this.rdbMajor.CheckedChanged += new System.EventHandler(this.rdbMajor_CheckedChanged);
            // 
            // pnlEncodingInfo
            // 
            this.pnlEncodingInfo.Controls.Add(this.tbxFrameRate);
            this.pnlEncodingInfo.Controls.Add(this.label5);
            this.pnlEncodingInfo.Controls.Add(this.label7);
            this.pnlEncodingInfo.Controls.Add(this.label8);
            this.pnlEncodingInfo.Controls.Add(this.label9);
            this.pnlEncodingInfo.Controls.Add(this.cbxBitrateType);
            this.pnlEncodingInfo.Controls.Add(this.cbxResolution);
            this.pnlEncodingInfo.Controls.Add(this.pnlEncodingInfoChoose);
            this.pnlEncodingInfo.Controls.Add(this.cbxBitrate);
            this.pnlEncodingInfo.Location = new System.Drawing.Point(515, 349);
            this.pnlEncodingInfo.Name = "pnlEncodingInfo";
            this.pnlEncodingInfo.Size = new System.Drawing.Size(42, 176);
            this.pnlEncodingInfo.TabIndex = 20;
            this.pnlEncodingInfo.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(360, 390);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(161, 12);
            this.label10.TabIndex = 23;
            this.label10.Text = "（不能包含特殊字符和空格）";
            // 
            // FrmSystemConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(660, 650);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.pnlEncodingInfo);
            this.Controls.Add(this.lbltrbValue);
            this.Controls.Add(this.trbLocation);
            this.Controls.Add(this.btnLocationDown);
            this.Controls.Add(this.btnLocationRight);
            this.Controls.Add(this.btnLocationLeft);
            this.Controls.Add(this.btnLocationUp);
            this.Controls.Add(this.pnlLocation);
            this.Controls.Add(this.btnUpdateTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpCameraDateTime);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pbxVideo);
            this.Controls.Add(this.tbxCameraName);
            this.Controls.Add(this.pnlTime);
            this.Controls.Add(this.pnlCameraName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSystemConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmVideoConfig";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmVideoConfig_FormClosed);
            this.Load += new System.EventHandler(this.FrmVideoConfig_Load);
            this.pnlCameraName.ResumeLayout(false);
            this.pnlCameraName.PerformLayout();
            this.pnlTime.ResumeLayout(false);
            this.pnlTime.PerformLayout();
            this.pnlLocation.ResumeLayout(false);
            this.pnlLocation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbLocation)).EndInit();
            this.pnlEncodingInfoChoose.ResumeLayout(false);
            this.pnlEncodingInfoChoose.PerformLayout();
            this.pnlEncodingInfo.ResumeLayout(false);
            this.pnlEncodingInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlCameraName;
        private System.Windows.Forms.RadioButton rdbNotCameraName;
        private System.Windows.Forms.RadioButton rdbIsCameraName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlTime;
        private System.Windows.Forms.RadioButton rdbNotTime;
        private System.Windows.Forms.RadioButton rdbIsTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxCameraName;
        private System.Windows.Forms.PictureBox pbxVideo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnUpdateTime;
        private System.Windows.Forms.TextBox tbxFrameRate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpCameraDateTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbxBitrateType;
        private System.Windows.Forms.ComboBox cbxResolution;
        private System.Windows.Forms.ComboBox cbxBitrate;
        private System.Windows.Forms.Panel pnlLocation;
        private System.Windows.Forms.RadioButton rdbCTimeLocation;
        private System.Windows.Forms.RadioButton rdbCNameLocation;
        private System.Windows.Forms.Button btnLocationUp;
        private System.Windows.Forms.Button btnLocationLeft;
        private System.Windows.Forms.Button btnLocationRight;
        private System.Windows.Forms.Button btnLocationDown;
        private System.Windows.Forms.TrackBar trbLocation;
        private System.Windows.Forms.Label lbltrbValue;
        private System.Windows.Forms.Panel pnlEncodingInfoChoose;
        private System.Windows.Forms.RadioButton rdbMinor;
        private System.Windows.Forms.RadioButton rdbMajor;
        private System.Windows.Forms.Panel pnlEncodingInfo;
        private System.Windows.Forms.Label label10;
    }
}