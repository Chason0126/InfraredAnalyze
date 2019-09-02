namespace InfraredAnalyze
{
    partial class Historicalines
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartHisrotricalData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.grpHisTemper = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxHisDateTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxHisTemper = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartHisrotricalData)).BeginInit();
            this.grpHisTemper.SuspendLayout();
            this.SuspendLayout();
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
            this.chartHisrotricalData.Cursor = System.Windows.Forms.Cursors.Cross;
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
            this.chartHisrotricalData.Size = new System.Drawing.Size(871, 579);
            this.chartHisrotricalData.TabIndex = 1;
            this.chartHisrotricalData.Text = "chart1";
            this.chartHisrotricalData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chartHisrotricalData_MouseDown);
            // 
            // grpHisTemper
            // 
            this.grpHisTemper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpHisTemper.Controls.Add(this.label8);
            this.grpHisTemper.Controls.Add(this.tbxHisDateTime);
            this.grpHisTemper.Controls.Add(this.label7);
            this.grpHisTemper.Controls.Add(this.tbxHisTemper);
            this.grpHisTemper.Location = new System.Drawing.Point(733, 245);
            this.grpHisTemper.Name = "grpHisTemper";
            this.grpHisTemper.Size = new System.Drawing.Size(130, 138);
            this.grpHisTemper.TabIndex = 2;
            this.grpHisTemper.TabStop = false;
            this.grpHisTemper.Text = "温度数据";
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
            // Historicalines
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpHisTemper);
            this.Controls.Add(this.chartHisrotricalData);
            this.DoubleBuffered = true;
            this.Name = "Historicalines";
            this.Size = new System.Drawing.Size(871, 579);
            this.Load += new System.EventHandler(this.Historicalines_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartHisrotricalData)).EndInit();
            this.grpHisTemper.ResumeLayout(false);
            this.grpHisTemper.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartHisrotricalData;
        private System.Windows.Forms.GroupBox grpHisTemper;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbxHisDateTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxHisTemper;
    }
}
