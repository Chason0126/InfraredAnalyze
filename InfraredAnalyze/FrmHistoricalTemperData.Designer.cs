namespace InfraredAnalyze
{
    partial class FrmHistoricalTemperData
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxAreaNum = new System.Windows.Forms.ComboBox();
            this.cbxAreaType = new System.Windows.Forms.ComboBox();
            this.tabHistroicalData = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvHistoricalData = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chartHisrotricalData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcHistoricalData)).BeginInit();
            this.spcHistoricalData.Panel1.SuspendLayout();
            this.spcHistoricalData.Panel2.SuspendLayout();
            this.spcHistoricalData.SuspendLayout();
            this.tabHistroicalData.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistoricalData)).BeginInit();
            this.tabPage2.SuspendLayout();
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
            this.label1.Text = "历史温度数据";
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
            this.spcHistoricalData.Location = new System.Drawing.Point(0, 35);
            this.spcHistoricalData.Margin = new System.Windows.Forms.Padding(0);
            this.spcHistoricalData.Name = "spcHistoricalData";
            this.spcHistoricalData.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spcHistoricalData.Panel1
            // 
            this.spcHistoricalData.Panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.spcHistoricalData.Panel1.Controls.Add(this.btnQuery);
            this.spcHistoricalData.Panel1.Controls.Add(this.tbxCameraID);
            this.spcHistoricalData.Panel1.Controls.Add(this.dtpEnd);
            this.spcHistoricalData.Panel1.Controls.Add(this.label6);
            this.spcHistoricalData.Panel1.Controls.Add(this.dtpStart);
            this.spcHistoricalData.Panel1.Controls.Add(this.label5);
            this.spcHistoricalData.Panel1.Controls.Add(this.label4);
            this.spcHistoricalData.Panel1.Controls.Add(this.label3);
            this.spcHistoricalData.Panel1.Controls.Add(this.label2);
            this.spcHistoricalData.Panel1.Controls.Add(this.cbxAreaNum);
            this.spcHistoricalData.Panel1.Controls.Add(this.cbxAreaType);
            // 
            // spcHistoricalData.Panel2
            // 
            this.spcHistoricalData.Panel2.Controls.Add(this.tabHistroicalData);
            this.spcHistoricalData.Size = new System.Drawing.Size(1100, 601);
            this.spcHistoricalData.SplitterDistance = 62;
            this.spcHistoricalData.TabIndex = 1;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnQuery.BackColor = System.Drawing.Color.LightGray;
            this.btnQuery.Location = new System.Drawing.Point(1001, 21);
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
            this.dtpEnd.Location = new System.Drawing.Point(851, 22);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(126, 21);
            this.dtpEnd.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(807, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "截止：";
            // 
            // dtpStart
            // 
            this.dtpStart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpStart.Location = new System.Drawing.Point(654, 22);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(126, 21);
            this.dtpStart.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(610, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "起始：";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(427, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "区域编号：";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(230, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "区域类型：";
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
            this.cbxAreaNum.Location = new System.Drawing.Point(498, 23);
            this.cbxAreaNum.Name = "cbxAreaNum";
            this.cbxAreaNum.Size = new System.Drawing.Size(83, 20);
            this.cbxAreaNum.TabIndex = 0;
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
            this.cbxAreaType.Location = new System.Drawing.Point(301, 22);
            this.cbxAreaType.Name = "cbxAreaType";
            this.cbxAreaType.Size = new System.Drawing.Size(83, 20);
            this.cbxAreaType.TabIndex = 0;
            this.cbxAreaType.SelectedIndexChanged += new System.EventHandler(this.cbxAreaType_SelectedIndexChanged);
            // 
            // tabHistroicalData
            // 
            this.tabHistroicalData.Controls.Add(this.tabPage1);
            this.tabHistroicalData.Controls.Add(this.tabPage2);
            this.tabHistroicalData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabHistroicalData.Location = new System.Drawing.Point(0, 0);
            this.tabHistroicalData.Margin = new System.Windows.Forms.Padding(0);
            this.tabHistroicalData.Name = "tabHistroicalData";
            this.tabHistroicalData.SelectedIndex = 0;
            this.tabHistroicalData.Size = new System.Drawing.Size(1100, 535);
            this.tabHistroicalData.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvHistoricalData);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1092, 509);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据表";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvHistoricalData
            // 
            this.dgvHistoricalData.AllowUserToAddRows = false;
            this.dgvHistoricalData.AllowUserToDeleteRows = false;
            this.dgvHistoricalData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHistoricalData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHistoricalData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistoricalData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dgvHistoricalData.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvHistoricalData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistoricalData.Location = new System.Drawing.Point(3, 3);
            this.dgvHistoricalData.Margin = new System.Windows.Forms.Padding(0);
            this.dgvHistoricalData.Name = "dgvHistoricalData";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvHistoricalData.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvHistoricalData.RowTemplate.Height = 23;
            this.dgvHistoricalData.Size = new System.Drawing.Size(1086, 503);
            this.dgvHistoricalData.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "探测器编号";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "IP地址";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "时间";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "区域类型";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "区域编号";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "温度";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "状态";
            this.Column7.Name = "Column7";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chartHisrotricalData);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1092, 509);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "波形图";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chartHisrotricalData
            // 
            this.chartHisrotricalData.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chartHisrotricalData.BorderlineWidth = 2;
            chartArea1.Name = "ChartArea1";
            this.chartHisrotricalData.ChartAreas.Add(chartArea1);
            this.chartHisrotricalData.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartHisrotricalData.Legends.Add(legend1);
            this.chartHisrotricalData.Location = new System.Drawing.Point(3, 3);
            this.chartHisrotricalData.Margin = new System.Windows.Forms.Padding(0);
            this.chartHisrotricalData.Name = "chartHisrotricalData";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartHisrotricalData.Series.Add(series1);
            this.chartHisrotricalData.Size = new System.Drawing.Size(1086, 503);
            this.chartHisrotricalData.TabIndex = 0;
            this.chartHisrotricalData.Text = "chart1";
            // 
            // FrmHistoricalTemperData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1100, 636);
            this.Controls.Add(this.spcHistoricalData);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmHistoricalTemperData";
            this.Text = "FrmHistoricalTemperData";
            this.Load += new System.EventHandler(this.FrmHistoricalTemperData_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.spcHistoricalData.Panel1.ResumeLayout(false);
            this.spcHistoricalData.Panel1.PerformLayout();
            this.spcHistoricalData.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcHistoricalData)).EndInit();
            this.spcHistoricalData.ResumeLayout(false);
            this.tabHistroicalData.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistoricalData)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartHisrotricalData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnWindow;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.SplitContainer spcHistoricalData;
        private System.Windows.Forms.DataGridView dgvHistoricalData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxAreaNum;
        private System.Windows.Forms.ComboBox cbxAreaType;
        private System.Windows.Forms.TextBox tbxCameraID;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TabControl tabHistroicalData;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartHisrotricalData;
    }
}