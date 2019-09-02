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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRealTimeTemperData));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnMin = new System.Windows.Forms.Button();
            this.btnWindow = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.spcRealTimeData = new System.Windows.Forms.SplitContainer();
            this.tbxCameraID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.grpChoosePointTemper = new System.Windows.Forms.GroupBox();
            this.tbxChoosePointTemper = new System.Windows.Forms.TextBox();
            this.chartRealTimeData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cmsTempLine_Visiable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcRealTimeData)).BeginInit();
            this.spcRealTimeData.Panel1.SuspendLayout();
            this.spcRealTimeData.Panel2.SuspendLayout();
            this.spcRealTimeData.SuspendLayout();
            this.grpChoosePointTemper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRealTimeData)).BeginInit();
            this.cmsTempLine_Visiable.SuspendLayout();
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
            this.btnMin.Visible = false;
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
            this.spcRealTimeData.Panel1.Controls.Add(this.tbxCameraID);
            this.spcRealTimeData.Panel1.Controls.Add(this.label2);
            // 
            // spcRealTimeData.Panel2
            // 
            this.spcRealTimeData.Panel2.Controls.Add(this.lblEndTime);
            this.spcRealTimeData.Panel2.Controls.Add(this.lblStartTime);
            this.spcRealTimeData.Panel2.Controls.Add(this.grpChoosePointTemper);
            this.spcRealTimeData.Panel2.Controls.Add(this.chartRealTimeData);
            this.spcRealTimeData.Size = new System.Drawing.Size(1059, 603);
            this.spcRealTimeData.SplitterDistance = 43;
            this.spcRealTimeData.TabIndex = 1;
            // 
            // tbxCameraID
            // 
            this.tbxCameraID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbxCameraID.Location = new System.Drawing.Point(510, 11);
            this.tbxCameraID.Name = "tbxCameraID";
            this.tbxCameraID.ReadOnly = true;
            this.tbxCameraID.Size = new System.Drawing.Size(58, 21);
            this.tbxCameraID.TabIndex = 10;
            this.tbxCameraID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(426, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "探测器编号：";
            // 
            // lblEndTime
            // 
            this.lblEndTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.BackColor = System.Drawing.Color.Transparent;
            this.lblEndTime.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEndTime.Location = new System.Drawing.Point(841, 524);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(63, 14);
            this.lblEndTime.TabIndex = 3;
            this.lblEndTime.Text = "结束时间";
            // 
            // lblStartTime
            // 
            this.lblStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.BackColor = System.Drawing.Color.Transparent;
            this.lblStartTime.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStartTime.Location = new System.Drawing.Point(82, 524);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(63, 14);
            this.lblStartTime.TabIndex = 3;
            this.lblStartTime.Text = "开始时间";
            // 
            // grpChoosePointTemper
            // 
            this.grpChoosePointTemper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpChoosePointTemper.BackColor = System.Drawing.Color.White;
            this.grpChoosePointTemper.Controls.Add(this.tbxChoosePointTemper);
            this.grpChoosePointTemper.Location = new System.Drawing.Point(922, 281);
            this.grpChoosePointTemper.Name = "grpChoosePointTemper";
            this.grpChoosePointTemper.Size = new System.Drawing.Size(102, 55);
            this.grpChoosePointTemper.TabIndex = 2;
            this.grpChoosePointTemper.TabStop = false;
            this.grpChoosePointTemper.Text = "温度信息";
            // 
            // tbxChoosePointTemper
            // 
            this.tbxChoosePointTemper.Location = new System.Drawing.Point(23, 20);
            this.tbxChoosePointTemper.Name = "tbxChoosePointTemper";
            this.tbxChoosePointTemper.ReadOnly = true;
            this.tbxChoosePointTemper.Size = new System.Drawing.Size(61, 21);
            this.tbxChoosePointTemper.TabIndex = 1;
            this.tbxChoosePointTemper.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chartRealTimeData
            // 
            this.chartRealTimeData.BackColor = System.Drawing.Color.Azure;
            chartArea1.AxisX.LineWidth = 2;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisX2.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.LineWidth = 2;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY2.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.BackColor = System.Drawing.Color.Azure;
            chartArea1.BorderColor = System.Drawing.Color.White;
            chartArea1.CursorX.LineColor = System.Drawing.Color.Black;
            chartArea1.CursorY.LineColor = System.Drawing.Color.Black;
            chartArea1.Name = "ChartArea1";
            this.chartRealTimeData.ChartAreas.Add(chartArea1);
            this.chartRealTimeData.ContextMenuStrip = this.cmsTempLine_Visiable;
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
            this.chartRealTimeData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chartRealTimeData_MouseDown);
            this.chartRealTimeData.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chartRealTimeData_MouseMove);
            // 
            // cmsTempLine_Visiable
            // 
            this.cmsTempLine_Visiable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripSeparator3,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8});
            this.cmsTempLine_Visiable.Name = "cmsTempLine_Visiable";
            this.cmsTempLine_Visiable.Size = new System.Drawing.Size(140, 192);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Checked = true;
            this.toolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(139, 22);
            this.toolStripMenuItem1.Text = "测温线L1";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(136, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Checked = true;
            this.toolStripMenuItem2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(139, 22);
            this.toolStripMenuItem2.Text = "测温点S2";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Checked = true;
            this.toolStripMenuItem3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(139, 22);
            this.toolStripMenuItem3.Text = "测温点S3";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Checked = true;
            this.toolStripMenuItem4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(139, 22);
            this.toolStripMenuItem4.Text = "测温点S4";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Checked = true;
            this.toolStripMenuItem5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(139, 22);
            this.toolStripMenuItem5.Text = "测温点S5";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(136, 6);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Checked = true;
            this.toolStripMenuItem6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(139, 22);
            this.toolStripMenuItem6.Text = "测温区域A6";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Checked = true;
            this.toolStripMenuItem7.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(139, 22);
            this.toolStripMenuItem7.Text = "测温区域A7";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Checked = true;
            this.toolStripMenuItem8.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(139, 22);
            this.toolStripMenuItem8.Text = "测温区域A8";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.toolStripMenuItem8_Click);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRealTimeTemperData";
            this.Text = "实时温度曲线";
            this.Load += new System.EventHandler(this.FrmRealTimeTemperData_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.spcRealTimeData.Panel1.ResumeLayout(false);
            this.spcRealTimeData.Panel1.PerformLayout();
            this.spcRealTimeData.Panel2.ResumeLayout(false);
            this.spcRealTimeData.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcRealTimeData)).EndInit();
            this.spcRealTimeData.ResumeLayout(false);
            this.grpChoosePointTemper.ResumeLayout(false);
            this.grpChoosePointTemper.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRealTimeData)).EndInit();
            this.cmsTempLine_Visiable.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer spcRealTimeData;
        private System.Windows.Forms.TextBox tbxCameraID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRealTimeData;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnWindow;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox grpChoosePointTemper;
        private System.Windows.Forms.TextBox tbxChoosePointTemper;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.ContextMenuStrip cmsTempLine_Visiable;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
    }
}