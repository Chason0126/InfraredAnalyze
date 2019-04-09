namespace InfraredAnalyze
{
    partial class FrmRealTimeTemperData
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnMin = new System.Windows.Forms.Button();
            this.btnWindow = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.spcRealTimeData = new System.Windows.Forms.SplitContainer();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.tbxCameraID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxAreaNum = new System.Windows.Forms.ComboBox();
            this.cbxAreaType = new System.Windows.Forms.ComboBox();
            this.chartRealTimeData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcRealTimeData)).BeginInit();
            this.spcRealTimeData.Panel1.SuspendLayout();
            this.spcRealTimeData.Panel2.SuspendLayout();
            this.spcRealTimeData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRealTimeData)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlHeader.Controls.Add(this.btnMin);
            this.pnlHeader.Controls.Add(this.btnWindow);
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1059, 35);
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseDown);
            this.pnlHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseMove);
            // 
            // btnMin
            // 
            this.btnMin.BackColor = System.Drawing.Color.Transparent;
            this.btnMin.BackgroundImage = global::InfraredAnalyze.Properties.Resources.最小化;
            this.btnMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMin.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.Location = new System.Drawing.Point(954, 0);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(35, 35);
            this.btnMin.TabIndex = 3;
            this.btnMin.UseVisualStyleBackColor = false;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            this.btnMin.MouseEnter += new System.EventHandler(this.btnMin_MouseEnter);
            this.btnMin.MouseLeave += new System.EventHandler(this.btnMin_MouseLeave);
            // 
            // btnWindow
            // 
            this.btnWindow.BackColor = System.Drawing.Color.Transparent;
            this.btnWindow.BackgroundImage = global::InfraredAnalyze.Properties.Resources.最大化;
            this.btnWindow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnWindow.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnWindow.FlatAppearance.BorderSize = 0;
            this.btnWindow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWindow.Location = new System.Drawing.Point(989, 0);
            this.btnWindow.Name = "btnWindow";
            this.btnWindow.Size = new System.Drawing.Size(35, 35);
            this.btnWindow.TabIndex = 2;
            this.btnWindow.UseVisualStyleBackColor = false;
            this.btnWindow.Click += new System.EventHandler(this.btnWindow_Click);
            this.btnWindow.MouseEnter += new System.EventHandler(this.btnWindow_MouseEnter);
            this.btnWindow.MouseLeave += new System.EventHandler(this.btnWindow_MouseLeave);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::InfraredAnalyze.Properties.Resources.关闭;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(1024, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(35, 35);
            this.btnClose.TabIndex = 1;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(463, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "实时温度数据";
            // 
            // spcRealTimeData
            // 
            this.spcRealTimeData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcRealTimeData.Location = new System.Drawing.Point(0, 35);
            this.spcRealTimeData.Margin = new System.Windows.Forms.Padding(0);
            this.spcRealTimeData.Name = "spcRealTimeData";
            this.spcRealTimeData.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spcRealTimeData.Panel1
            // 
            this.spcRealTimeData.Panel1.Controls.Add(this.btnConfirm);
            this.spcRealTimeData.Panel1.Controls.Add(this.tbxCameraID);
            this.spcRealTimeData.Panel1.Controls.Add(this.label4);
            this.spcRealTimeData.Panel1.Controls.Add(this.label3);
            this.spcRealTimeData.Panel1.Controls.Add(this.label2);
            this.spcRealTimeData.Panel1.Controls.Add(this.cbxAreaNum);
            this.spcRealTimeData.Panel1.Controls.Add(this.cbxAreaType);
            // 
            // spcRealTimeData.Panel2
            // 
            this.spcRealTimeData.Panel2.Controls.Add(this.chartRealTimeData);
            this.spcRealTimeData.Size = new System.Drawing.Size(1059, 603);
            this.spcRealTimeData.SplitterDistance = 43;
            this.spcRealTimeData.TabIndex = 1;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnConfirm.Location = new System.Drawing.Point(940, 11);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 11;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // tbxCameraID
            // 
            this.tbxCameraID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbxCameraID.Location = new System.Drawing.Point(336, 11);
            this.tbxCameraID.Name = "tbxCameraID";
            this.tbxCameraID.ReadOnly = true;
            this.tbxCameraID.Size = new System.Drawing.Size(58, 21);
            this.tbxCameraID.TabIndex = 10;
            this.tbxCameraID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(653, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "区域编号：";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(456, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "区域类型：";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(252, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "探测器编号：";
            // 
            // cbxAreaNum
            // 
            this.cbxAreaNum.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbxAreaNum.FormattingEnabled = true;
            this.cbxAreaNum.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "所有"});
            this.cbxAreaNum.Location = new System.Drawing.Point(724, 11);
            this.cbxAreaNum.Name = "cbxAreaNum";
            this.cbxAreaNum.Size = new System.Drawing.Size(83, 20);
            this.cbxAreaNum.TabIndex = 5;
            // 
            // cbxAreaType
            // 
            this.cbxAreaType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbxAreaType.FormattingEnabled = true;
            this.cbxAreaType.Items.AddRange(new object[] {
            "点",
            "线",
            "区域",
            "所有"});
            this.cbxAreaType.Location = new System.Drawing.Point(527, 10);
            this.cbxAreaType.Name = "cbxAreaType";
            this.cbxAreaType.Size = new System.Drawing.Size(83, 20);
            this.cbxAreaType.TabIndex = 6;
            this.cbxAreaType.SelectedIndexChanged += new System.EventHandler(this.cbxAreaType_SelectedIndexChanged);
            // 
            // chartRealTimeData
            // 
            chartArea1.Name = "ChartArea1";
            this.chartRealTimeData.ChartAreas.Add(chartArea1);
            this.chartRealTimeData.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartRealTimeData.Legends.Add(legend1);
            this.chartRealTimeData.Location = new System.Drawing.Point(0, 0);
            this.chartRealTimeData.Name = "chartRealTimeData";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartRealTimeData.Series.Add(series1);
            this.chartRealTimeData.Size = new System.Drawing.Size(1059, 556);
            this.chartRealTimeData.TabIndex = 0;
            this.chartRealTimeData.Text = "chart1";
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmRealTimeTemperData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1059, 638);
            this.Controls.Add(this.spcRealTimeData);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmRealTimeTemperData";
            this.Text = "FrmRealTimeTemperData";
            this.Load += new System.EventHandler(this.FrmRealTimeTemperData_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.spcRealTimeData.Panel1.ResumeLayout(false);
            this.spcRealTimeData.Panel1.PerformLayout();
            this.spcRealTimeData.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcRealTimeData)).EndInit();
            this.spcRealTimeData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartRealTimeData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer spcRealTimeData;
        private System.Windows.Forms.TextBox tbxCameraID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxAreaNum;
        private System.Windows.Forms.ComboBox cbxAreaType;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRealTimeData;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnWindow;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnConfirm;
    }
}