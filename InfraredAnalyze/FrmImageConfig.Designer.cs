namespace InfraredAnalyze
{
    partial class FrmImageConfig
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pbxScreen = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxTempOnImage = new System.Windows.Forms.ComboBox();
            this.cbxVideoOutType = new System.Windows.Forms.ComboBox();
            this.cbxVideoMode = new System.Windows.Forms.ComboBox();
            this.cbxTempUnit = new System.Windows.Forms.ComboBox();
            this.cbxISOColor = new System.Windows.Forms.ComboBox();
            this.tbxISOTemp = new System.Windows.Forms.TextBox();
            this.tbxISOHight = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxPallette = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbxPalority = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbxZoom = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbxGFZ = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tbxBright = new System.Windows.Forms.TextBox();
            this.tbxContrast = new System.Windows.Forms.TextBox();
            this.btnBrightUp = new System.Windows.Forms.Button();
            this.btnBrightDown = new System.Windows.Forms.Button();
            this.btnContrastUp = new System.Windows.Forms.Button();
            this.btnContrasDown = new System.Windows.Forms.Button();
            this.btnAutoAd = new System.Windows.Forms.Button();
            this.btnAutoFocus = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1009, 35);
            this.pnlHeader.TabIndex = 3;
            this.pnlHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseDown);
            this.pnlHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(63, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "图像设置";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::InfraredAnalyze.Properties.Resources.关闭;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(974, 0);
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
            this.pbxScreen.TabIndex = 4;
            this.pbxScreen.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(695, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "显示温度：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(710, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "制式：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(710, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "模式：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(695, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 14);
            this.label6.TabIndex = 5;
            this.label6.Text = "等温温度：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(695, 215);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 14);
            this.label7.TabIndex = 5;
            this.label7.Text = "等温高度：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(703, 255);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 14);
            this.label8.TabIndex = 5;
            this.label8.Text = "等温色：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(695, 293);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 14);
            this.label9.TabIndex = 5;
            this.label9.Text = "温度单位：";
            // 
            // cbxTempOnImage
            // 
            this.cbxTempOnImage.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxTempOnImage.FormattingEnabled = true;
            this.cbxTempOnImage.Items.AddRange(new object[] {
            "隐藏",
            "显示"});
            this.cbxTempOnImage.Location = new System.Drawing.Point(795, 52);
            this.cbxTempOnImage.Name = "cbxTempOnImage";
            this.cbxTempOnImage.Size = new System.Drawing.Size(118, 22);
            this.cbxTempOnImage.TabIndex = 6;
            this.cbxTempOnImage.SelectedIndexChanged += new System.EventHandler(this.cbxTempOnImage_SelectedIndexChanged);
            // 
            // cbxVideoOutType
            // 
            this.cbxVideoOutType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxVideoOutType.FormattingEnabled = true;
            this.cbxVideoOutType.Items.AddRange(new object[] {
            "PAL",
            "NTSC"});
            this.cbxVideoOutType.Location = new System.Drawing.Point(795, 91);
            this.cbxVideoOutType.Name = "cbxVideoOutType";
            this.cbxVideoOutType.Size = new System.Drawing.Size(118, 22);
            this.cbxVideoOutType.TabIndex = 6;
            this.cbxVideoOutType.SelectedIndexChanged += new System.EventHandler(this.cbxVideoOutType_SelectedIndexChanged);
            // 
            // cbxVideoMode
            // 
            this.cbxVideoMode.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxVideoMode.FormattingEnabled = true;
            this.cbxVideoMode.Items.AddRange(new object[] {
            "手动",
            "自动"});
            this.cbxVideoMode.Location = new System.Drawing.Point(795, 131);
            this.cbxVideoMode.Name = "cbxVideoMode";
            this.cbxVideoMode.Size = new System.Drawing.Size(118, 22);
            this.cbxVideoMode.TabIndex = 6;
            this.cbxVideoMode.SelectedIndexChanged += new System.EventHandler(this.cbxVideoMode_SelectedIndexChanged);
            // 
            // cbxTempUnit
            // 
            this.cbxTempUnit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxTempUnit.FormattingEnabled = true;
            this.cbxTempUnit.Items.AddRange(new object[] {
            "°C",
            "°F"});
            this.cbxTempUnit.Location = new System.Drawing.Point(795, 290);
            this.cbxTempUnit.Name = "cbxTempUnit";
            this.cbxTempUnit.Size = new System.Drawing.Size(118, 22);
            this.cbxTempUnit.TabIndex = 6;
            this.cbxTempUnit.SelectedIndexChanged += new System.EventHandler(this.cbxTempUnit_SelectedIndexChanged);
            // 
            // cbxISOColor
            // 
            this.cbxISOColor.Enabled = false;
            this.cbxISOColor.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxISOColor.FormattingEnabled = true;
            this.cbxISOColor.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.cbxISOColor.Location = new System.Drawing.Point(795, 252);
            this.cbxISOColor.Name = "cbxISOColor";
            this.cbxISOColor.Size = new System.Drawing.Size(118, 22);
            this.cbxISOColor.TabIndex = 6;
            // 
            // tbxISOTemp
            // 
            this.tbxISOTemp.Enabled = false;
            this.tbxISOTemp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxISOTemp.Location = new System.Drawing.Point(795, 172);
            this.tbxISOTemp.Name = "tbxISOTemp";
            this.tbxISOTemp.Size = new System.Drawing.Size(118, 23);
            this.tbxISOTemp.TabIndex = 7;
            // 
            // tbxISOHight
            // 
            this.tbxISOHight.Enabled = false;
            this.tbxISOHight.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxISOHight.Location = new System.Drawing.Point(795, 212);
            this.tbxISOHight.Name = "tbxISOHight";
            this.tbxISOHight.Size = new System.Drawing.Size(118, 23);
            this.tbxISOHight.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(710, 332);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 14);
            this.label5.TabIndex = 5;
            this.label5.Text = "色标：";
            // 
            // cbxPallette
            // 
            this.cbxPallette.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxPallette.FormattingEnabled = true;
            this.cbxPallette.Items.AddRange(new object[] {
            "色标0",
            "色标1",
            "色标2",
            "色标3",
            "色标4",
            "色标5",
            "色标6",
            "色标7",
            "色标8",
            "色标9"});
            this.cbxPallette.Location = new System.Drawing.Point(795, 329);
            this.cbxPallette.Name = "cbxPallette";
            this.cbxPallette.Size = new System.Drawing.Size(118, 22);
            this.cbxPallette.TabIndex = 6;
            this.cbxPallette.SelectedIndexChanged += new System.EventHandler(this.cbxPallette_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(695, 370);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 14);
            this.label10.TabIndex = 5;
            this.label10.Text = "热图模式：";
            // 
            // cbxPalority
            // 
            this.cbxPalority.Enabled = false;
            this.cbxPalority.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxPalority.FormattingEnabled = true;
            this.cbxPalority.Items.AddRange(new object[] {
            "黑热",
            "白热"});
            this.cbxPalority.Location = new System.Drawing.Point(795, 367);
            this.cbxPalority.Name = "cbxPalority";
            this.cbxPalority.Size = new System.Drawing.Size(118, 22);
            this.cbxPalority.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(695, 408);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 14);
            this.label11.TabIndex = 5;
            this.label11.Text = "放大倍数：";
            // 
            // tbxZoom
            // 
            this.tbxZoom.Enabled = false;
            this.tbxZoom.Location = new System.Drawing.Point(795, 408);
            this.tbxZoom.Name = "tbxZoom";
            this.tbxZoom.Size = new System.Drawing.Size(118, 21);
            this.tbxZoom.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(695, 457);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 14);
            this.label12.TabIndex = 5;
            this.label12.Text = "冻结状态：";
            // 
            // cbxGFZ
            // 
            this.cbxGFZ.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxGFZ.FormattingEnabled = true;
            this.cbxGFZ.Items.AddRange(new object[] {
            "冻结",
            "非冻结"});
            this.cbxGFZ.Location = new System.Drawing.Point(795, 449);
            this.cbxGFZ.Name = "cbxGFZ";
            this.cbxGFZ.Size = new System.Drawing.Size(118, 22);
            this.cbxGFZ.TabIndex = 6;
            this.cbxGFZ.SelectedIndexChanged += new System.EventHandler(this.cbxGFZ_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(710, 507);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 14);
            this.label13.TabIndex = 5;
            this.label13.Text = "亮度：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(710, 546);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 14);
            this.label14.TabIndex = 5;
            this.label14.Text = "增益：";
            // 
            // tbxBright
            // 
            this.tbxBright.Enabled = false;
            this.tbxBright.Location = new System.Drawing.Point(813, 506);
            this.tbxBright.Name = "tbxBright";
            this.tbxBright.Size = new System.Drawing.Size(61, 21);
            this.tbxBright.TabIndex = 9;
            this.tbxBright.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxContrast
            // 
            this.tbxContrast.Enabled = false;
            this.tbxContrast.Location = new System.Drawing.Point(813, 545);
            this.tbxContrast.Name = "tbxContrast";
            this.tbxContrast.Size = new System.Drawing.Size(61, 21);
            this.tbxContrast.TabIndex = 9;
            this.tbxContrast.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnBrightUp
            // 
            this.btnBrightUp.BackgroundImage = global::InfraredAnalyze.Properties.Resources.up;
            this.btnBrightUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrightUp.Enabled = false;
            this.btnBrightUp.FlatAppearance.BorderSize = 0;
            this.btnBrightUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrightUp.Location = new System.Drawing.Point(930, 507);
            this.btnBrightUp.Name = "btnBrightUp";
            this.btnBrightUp.Size = new System.Drawing.Size(23, 23);
            this.btnBrightUp.TabIndex = 10;
            this.btnBrightUp.UseVisualStyleBackColor = true;
            this.btnBrightUp.Click += new System.EventHandler(this.btnBrightUp_Click);
            // 
            // btnBrightDown
            // 
            this.btnBrightDown.BackgroundImage = global::InfraredAnalyze.Properties.Resources.down;
            this.btnBrightDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrightDown.Enabled = false;
            this.btnBrightDown.FlatAppearance.BorderSize = 0;
            this.btnBrightDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrightDown.Location = new System.Drawing.Point(890, 506);
            this.btnBrightDown.Name = "btnBrightDown";
            this.btnBrightDown.Size = new System.Drawing.Size(23, 23);
            this.btnBrightDown.TabIndex = 10;
            this.btnBrightDown.UseVisualStyleBackColor = true;
            this.btnBrightDown.Click += new System.EventHandler(this.btnBrightDown_Click);
            // 
            // btnContrastUp
            // 
            this.btnContrastUp.BackgroundImage = global::InfraredAnalyze.Properties.Resources.up;
            this.btnContrastUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnContrastUp.Enabled = false;
            this.btnContrastUp.FlatAppearance.BorderSize = 0;
            this.btnContrastUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContrastUp.Location = new System.Drawing.Point(930, 543);
            this.btnContrastUp.Name = "btnContrastUp";
            this.btnContrastUp.Size = new System.Drawing.Size(23, 23);
            this.btnContrastUp.TabIndex = 10;
            this.btnContrastUp.UseVisualStyleBackColor = true;
            this.btnContrastUp.Click += new System.EventHandler(this.btnContrastUp_Click);
            // 
            // btnContrasDown
            // 
            this.btnContrasDown.BackgroundImage = global::InfraredAnalyze.Properties.Resources.down;
            this.btnContrasDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnContrasDown.Enabled = false;
            this.btnContrasDown.FlatAppearance.BorderSize = 0;
            this.btnContrasDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContrasDown.Location = new System.Drawing.Point(890, 543);
            this.btnContrasDown.Name = "btnContrasDown";
            this.btnContrasDown.Size = new System.Drawing.Size(23, 23);
            this.btnContrasDown.TabIndex = 10;
            this.btnContrasDown.UseVisualStyleBackColor = true;
            this.btnContrasDown.Click += new System.EventHandler(this.btnContrasDown_Click);
            // 
            // btnAutoAd
            // 
            this.btnAutoAd.Location = new System.Drawing.Point(25, 536);
            this.btnAutoAd.Name = "btnAutoAd";
            this.btnAutoAd.Size = new System.Drawing.Size(75, 23);
            this.btnAutoAd.TabIndex = 11;
            this.btnAutoAd.Text = "触发调零";
            this.btnAutoAd.UseVisualStyleBackColor = true;
            this.btnAutoAd.Click += new System.EventHandler(this.btnAutoAd_Click);
            // 
            // btnAutoFocus
            // 
            this.btnAutoFocus.Location = new System.Drawing.Point(149, 535);
            this.btnAutoFocus.Name = "btnAutoFocus";
            this.btnAutoFocus.Size = new System.Drawing.Size(75, 23);
            this.btnAutoFocus.TabIndex = 12;
            this.btnAutoFocus.Text = "自动调焦";
            this.btnAutoFocus.UseVisualStyleBackColor = true;
            this.btnAutoFocus.Click += new System.EventHandler(this.btnAutoFocus_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(278, 535);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "焦点拉近";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(278, 566);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 14;
            this.button4.Text = "焦点拉远";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(408, 534);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(85, 23);
            this.button5.TabIndex = 13;
            this.button5.Text = "焦点持续拉近";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Enabled = false;
            this.button6.Location = new System.Drawing.Point(408, 565);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(85, 23);
            this.button6.TabIndex = 14;
            this.button6.Text = "焦点持续拉远";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Enabled = false;
            this.button7.Location = new System.Drawing.Point(408, 605);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(85, 23);
            this.button7.TabIndex = 15;
            this.button7.Text = "停止调焦";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // FrmImageConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 651);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnAutoFocus);
            this.Controls.Add(this.btnAutoAd);
            this.Controls.Add(this.btnContrasDown);
            this.Controls.Add(this.btnContrastUp);
            this.Controls.Add(this.btnBrightDown);
            this.Controls.Add(this.btnBrightUp);
            this.Controls.Add(this.tbxContrast);
            this.Controls.Add(this.tbxBright);
            this.Controls.Add(this.tbxZoom);
            this.Controls.Add(this.tbxISOHight);
            this.Controls.Add(this.tbxISOTemp);
            this.Controls.Add(this.cbxISOColor);
            this.Controls.Add(this.cbxPalority);
            this.Controls.Add(this.cbxGFZ);
            this.Controls.Add(this.cbxPallette);
            this.Controls.Add(this.cbxTempUnit);
            this.Controls.Add(this.cbxVideoMode);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cbxVideoOutType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbxTempOnImage);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pbxScreen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmImageConfig";
            this.Text = "FrmImageConfig";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmImageConfig_FormClosed);
            this.Load += new System.EventHandler(this.FrmImageConfig_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxScreen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pbxScreen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbxTempOnImage;
        private System.Windows.Forms.ComboBox cbxVideoOutType;
        private System.Windows.Forms.ComboBox cbxVideoMode;
        private System.Windows.Forms.ComboBox cbxTempUnit;
        private System.Windows.Forms.ComboBox cbxISOColor;
        private System.Windows.Forms.TextBox tbxISOTemp;
        private System.Windows.Forms.TextBox tbxISOHight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxPallette;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbxPalority;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbxZoom;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbxGFZ;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbxBright;
        private System.Windows.Forms.TextBox tbxContrast;
        private System.Windows.Forms.Button btnBrightUp;
        private System.Windows.Forms.Button btnBrightDown;
        private System.Windows.Forms.Button btnContrastUp;
        private System.Windows.Forms.Button btnContrasDown;
        private System.Windows.Forms.Button btnAutoAd;
        private System.Windows.Forms.Button btnAutoFocus;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
    }
}