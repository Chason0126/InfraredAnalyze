namespace InfraredAnalyze
{
    partial class FrmHistoricalTemperLines
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHistoricalTemperLines));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMin = new System.Windows.Forms.Button();
            this.btnWindow = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.spcHistoricalData = new System.Windows.Forms.SplitContainer();
            this.btnQuery = new System.Windows.Forms.Button();
            this.tbxCameraID = new System.Windows.Forms.TextBox();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxType = new System.Windows.Forms.ComboBox();
            this.pnlHistoricalData = new System.Windows.Forms.Panel();
            this.grpHisTemper = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxHisDateTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxHisTemper = new System.Windows.Forms.TextBox();
            this.chartHisrotricalData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cbxPercent = new System.Windows.Forms.ComboBox();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcHistoricalData)).BeginInit();
            this.spcHistoricalData.Panel1.SuspendLayout();
            this.spcHistoricalData.Panel2.SuspendLayout();
            this.spcHistoricalData.SuspendLayout();
            this.pnlHistoricalData.SuspendLayout();
            this.grpHisTemper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartHisrotricalData)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Controls.Add(this.btnMin);
            this.pnlHeader.Controls.Add(this.btnWindow);
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1100, 35);
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseDown);
            this.pnlHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseMove);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(495, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "历史温度曲线";
            // 
            // btnMin
            // 
            this.btnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMin.BackColor = System.Drawing.Color.Transparent;
            this.btnMin.BackgroundImage = global::InfraredAnalyze.Properties.Resources.最小化;
            this.btnMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.Location = new System.Drawing.Point(986, 3);
            this.btnMin.Margin = new System.Windows.Forms.Padding(0);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(35, 35);
            this.btnMin.TabIndex = 2;
            this.btnMin.UseVisualStyleBackColor = false;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            this.btnMin.MouseEnter += new System.EventHandler(this.btnMin_MouseEnter);
            this.btnMin.MouseLeave += new System.EventHandler(this.btnMin_MouseLeave);
            // 
            // btnWindow
            // 
            this.btnWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWindow.BackColor = System.Drawing.Color.Transparent;
            this.btnWindow.BackgroundImage = global::InfraredAnalyze.Properties.Resources.最大化;
            this.btnWindow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnWindow.FlatAppearance.BorderSize = 0;
            this.btnWindow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWindow.Location = new System.Drawing.Point(1024, 0);
            this.btnWindow.Margin = new System.Windows.Forms.Padding(0);
            this.btnWindow.Name = "btnWindow";
            this.btnWindow.Size = new System.Drawing.Size(35, 35);
            this.btnWindow.TabIndex = 1;
            this.btnWindow.UseVisualStyleBackColor = false;
            this.btnWindow.Click += new System.EventHandler(this.btnWindow_Click);
            this.btnWindow.MouseEnter += new System.EventHandler(this.btnWindow_MouseEnter);
            this.btnWindow.MouseLeave += new System.EventHandler(this.btnWindow_MouseLeave);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = global::InfraredAnalyze.Properties.Resources.关闭;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(1065, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(35, 35);
            this.btnClose.TabIndex = 0;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // spcHistoricalData
            // 
            this.spcHistoricalData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcHistoricalData.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spcHistoricalData.Location = new System.Drawing.Point(0, 35);
            this.spcHistoricalData.Margin = new System.Windows.Forms.Padding(0);
            this.spcHistoricalData.Name = "spcHistoricalData";
            this.spcHistoricalData.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spcHistoricalData.Panel1
            // 
            this.spcHistoricalData.Panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.spcHistoricalData.Panel1.Controls.Add(this.cbxPercent);
            this.spcHistoricalData.Panel1.Controls.Add(this.btnQuery);
            this.spcHistoricalData.Panel1.Controls.Add(this.tbxCameraID);
            this.spcHistoricalData.Panel1.Controls.Add(this.dtpEnd);
            this.spcHistoricalData.Panel1.Controls.Add(this.label6);
            this.spcHistoricalData.Panel1.Controls.Add(this.dtpStart);
            this.spcHistoricalData.Panel1.Controls.Add(this.label5);
            this.spcHistoricalData.Panel1.Controls.Add(this.label3);
            this.spcHistoricalData.Panel1.Controls.Add(this.label2);
            this.spcHistoricalData.Panel1.Controls.Add(this.cbxType);
            // 
            // spcHistoricalData.Panel2
            // 
            this.spcHistoricalData.Panel2.Controls.Add(this.pnlHistoricalData);
            this.spcHistoricalData.Size = new System.Drawing.Size(1100, 601);
            this.spcHistoricalData.SplitterDistance = 62;
            this.spcHistoricalData.SplitterWidth = 1;
            this.spcHistoricalData.TabIndex = 1;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnQuery.BackColor = System.Drawing.Color.LightGray;
            this.btnQuery.Location = new System.Drawing.Point(865, 21);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 5;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            this.btnQuery.MouseEnter += new System.EventHandler(this.btnQuery_MouseEnter);
            this.btnQuery.MouseLeave += new System.EventHandler(this.btnQuery_MouseLeave);
            // 
            // tbxCameraID
            // 
            this.tbxCameraID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbxCameraID.Location = new System.Drawing.Point(110, 23);
            this.tbxCameraID.Name = "tbxCameraID";
            this.tbxCameraID.ReadOnly = true;
            this.tbxCameraID.Size = new System.Drawing.Size(58, 21);
            this.tbxCameraID.TabIndex = 4;
            this.tbxCameraID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(698, 22);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(126, 21);
            this.dtpEnd.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(654, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "截止：";
            // 
            // dtpStart
            // 
            this.dtpStart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpStart.CustomFormat = "";
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(464, 22);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(126, 21);
            this.dtpStart.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(420, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "起始：";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(207, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "区域编号：";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "探测器编号：";
            // 
            // cbxType
            // 
            this.cbxType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbxType.AutoCompleteCustomSource.AddRange(new string[] {
            "L1",
            "S2",
            "S3",
            "S4",
            "S5",
            "A6",
            "A7",
            "A8"});
            this.cbxType.FormattingEnabled = true;
            this.cbxType.Items.AddRange(new object[] {
            "全部",
            "L1",
            "S2",
            "S3",
            "S4",
            "S5",
            "A6",
            "A7",
            "A8"});
            this.cbxType.Location = new System.Drawing.Point(278, 22);
            this.cbxType.Name = "cbxType";
            this.cbxType.Size = new System.Drawing.Size(83, 20);
            this.cbxType.TabIndex = 0;
            // 
            // pnlHistoricalData
            // 
            this.pnlHistoricalData.BackColor = System.Drawing.Color.Azure;
            this.pnlHistoricalData.Controls.Add(this.grpHisTemper);
            this.pnlHistoricalData.Controls.Add(this.chartHisrotricalData);
            this.pnlHistoricalData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHistoricalData.Location = new System.Drawing.Point(0, 0);
            this.pnlHistoricalData.Name = "pnlHistoricalData";
            this.pnlHistoricalData.Size = new System.Drawing.Size(1100, 538);
            this.pnlHistoricalData.TabIndex = 3;
            // 
            // grpHisTemper
            // 
            this.grpHisTemper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpHisTemper.Controls.Add(this.label8);
            this.grpHisTemper.Controls.Add(this.tbxHisDateTime);
            this.grpHisTemper.Controls.Add(this.label7);
            this.grpHisTemper.Controls.Add(this.tbxHisTemper);
            this.grpHisTemper.Location = new System.Drawing.Point(958, 197);
            this.grpHisTemper.Name = "grpHisTemper";
            this.grpHisTemper.Size = new System.Drawing.Size(130, 138);
            this.grpHisTemper.TabIndex = 4;
            this.grpHisTemper.TabStop = false;
            this.grpHisTemper.Text = "温度数据";
            this.grpHisTemper.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "时间：";
            // 
            // tbxHisDateTime
            // 
            this.tbxHisDateTime.Location = new System.Drawing.Point(6, 100);
            this.tbxHisDateTime.Name = "tbxHisDateTime";
            this.tbxHisDateTime.Size = new System.Drawing.Size(120, 21);
            this.tbxHisDateTime.TabIndex = 0;
            this.tbxHisDateTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "温度：";
            // 
            // tbxHisTemper
            // 
            this.tbxHisTemper.Location = new System.Drawing.Point(53, 40);
            this.tbxHisTemper.Name = "tbxHisTemper";
            this.tbxHisTemper.Size = new System.Drawing.Size(64, 21);
            this.tbxHisTemper.TabIndex = 0;
            this.tbxHisTemper.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chartHisrotricalData
            // 
            this.chartHisrotricalData.BackColor = System.Drawing.Color.Azure;
            this.chartHisrotricalData.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chartHisrotricalData.BorderlineWidth = 2;
            chartArea1.AxisX.LineWidth = 2;
            chartArea1.AxisY.LineWidth = 2;
            chartArea1.BackColor = System.Drawing.Color.Azure;
            chartArea1.Name = "ChartArea1";
            this.chartHisrotricalData.ChartAreas.Add(chartArea1);
            this.chartHisrotricalData.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.chartHisrotricalData.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartHisrotricalData.Legends.Add(legend1);
            this.chartHisrotricalData.Location = new System.Drawing.Point(0, 0);
            this.chartHisrotricalData.Margin = new System.Windows.Forms.Padding(0);
            this.chartHisrotricalData.Name = "chartHisrotricalData";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartHisrotricalData.Series.Add(series1);
            this.chartHisrotricalData.Size = new System.Drawing.Size(1100, 538);
            this.chartHisrotricalData.TabIndex = 3;
            this.chartHisrotricalData.Text = "chart1";
            // 
            // cbxPercent
            // 
            this.cbxPercent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPercent.FormattingEnabled = true;
            this.cbxPercent.Items.AddRange(new object[] {
            "10%",
            "50%",
            "100%"});
            this.cbxPercent.Location = new System.Drawing.Point(995, 21);
            this.cbxPercent.Name = "cbxPercent";
            this.cbxPercent.Size = new System.Drawing.Size(64, 20);
            this.cbxPercent.TabIndex = 6;
            this.cbxPercent.SelectedIndexChanged += new System.EventHandler(this.cbxPercent_SelectedIndexChanged);
            // 
            // FrmHistoricalTemperLines
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1100, 636);
            this.Controls.Add(this.spcHistoricalData);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmHistoricalTemperLines";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "历史温度曲线";
            this.Load += new System.EventHandler(this.FrmHistoricalTemperData_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.spcHistoricalData.Panel1.ResumeLayout(false);
            this.spcHistoricalData.Panel1.PerformLayout();
            this.spcHistoricalData.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcHistoricalData)).EndInit();
            this.spcHistoricalData.ResumeLayout(false);
            this.pnlHistoricalData.ResumeLayout(false);
            this.grpHisTemper.ResumeLayout(false);
            this.grpHisTemper.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartHisrotricalData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnWindow;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.SplitContainer spcHistoricalData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxType;
        private System.Windows.Forms.TextBox tbxCameraID;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Panel pnlHistoricalData;
        private System.Windows.Forms.GroupBox grpHisTemper;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbxHisDateTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxHisTemper;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartHisrotricalData;
        private System.Windows.Forms.ComboBox cbxPercent;
    }
}