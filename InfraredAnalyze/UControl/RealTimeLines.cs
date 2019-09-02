using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;

namespace InfraredAnalyze
{
    public partial class RealTimeLines : UserControl
    {
        public RealTimeLines(int CameraID,string CameraName)
        {
            InitializeComponent();
            cameraID = CameraID;
            cameraName = CameraName;
            thread = new Thread(AddTempToQueue);
            thread.Start();
            timer1.Start();
        }

        Series seriesPoint_1 = new Series("测温点S2");
        Series seriesPoint_2 = new Series("测温点S3");
        Series seriesPoint_3 = new Series("测温点S4");
        Series seriesPoint_4 = new Series("测温点S5");
        Series seriesLine = new Series("测温线L1");
        Series seriesArea_1 = new Series("测温区域A6");
        Series seriesArea_2 = new Series("测温区域A7");
        Series seriesArea_3 = new Series("测温区域A8");

        Thread thread;
        private int cameraID;
        private string cameraName;

        Queue<Temper_Time> queuePoint_1 = new Queue<Temper_Time>(600);
        Queue<Temper_Time> queuePoint_2 = new Queue<Temper_Time>(600);
        Queue<Temper_Time> queuePoint_3 = new Queue<Temper_Time>(600);
        Queue<Temper_Time> queuePoint_4 = new Queue<Temper_Time>(600);
        Queue<Temper_Time> queueLine = new Queue<Temper_Time>(600);
        Queue<Temper_Time> queueArea_1 = new Queue<Temper_Time>(600);
        Queue<Temper_Time> queueArea_2 = new Queue<Temper_Time>(600);
        Queue<Temper_Time> queueArea_3 = new Queue<Temper_Time>(600);

        object obj_lock = new object();

        private void RealTimeLines_Load(object sender, EventArgs e)
        {
            chart.Series.Clear();

            chart.Series.Add(seriesArea_1);
            chart.Series.Add(seriesArea_2);
            chart.Series.Add(seriesArea_3);
            chart.Series.Add(seriesPoint_1);
            chart.Series.Add(seriesPoint_2);
            chart.Series.Add(seriesPoint_3);
            chart.Series.Add(seriesPoint_4);
            chart.Series.Add(seriesLine);

            seriesPoint_1.ChartType= SeriesChartType.Spline; 
            seriesPoint_2.ChartType = SeriesChartType.Spline; 
            seriesPoint_3.ChartType = SeriesChartType.Spline; 
            seriesPoint_4.ChartType = SeriesChartType.Spline; 
            seriesLine.ChartType = SeriesChartType.Spline; 
            seriesArea_1.ChartType = SeriesChartType.Spline; 
            seriesArea_2.ChartType = SeriesChartType.Spline; 
            seriesArea_3.ChartType = SeriesChartType.Spline; 

            seriesPoint_1.BorderWidth = 2;
            seriesPoint_2.BorderWidth = 2;
            seriesPoint_3.BorderWidth = 2;
            seriesPoint_4.BorderWidth = 2;
            seriesLine.BorderWidth = 2;
            seriesArea_1.BorderWidth = 2;
            seriesArea_2.BorderWidth = 2;
            seriesArea_3.BorderWidth = 2;
            

            chart.ChartAreas[0].AxisX.Interval = 30;
            chart.ChartAreas[0].AxisX.Maximum = 600;
            chart.ChartAreas[0].AxisX.Minimum = 0;
            chart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;

            

            chart.ChartAreas[0].AxisX.Title = "时间";
            chart.ChartAreas[0].AxisY.Title = "温度(°C)";

            chart.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            lblNum.Text = cameraName;

            seriesLine.Enabled = toolStripMenuItem1.Checked;
            seriesPoint_1.Enabled = toolStripMenuItem2.Checked;
            seriesPoint_2.Enabled = toolStripMenuItem3.Checked;
            seriesPoint_3.Enabled = toolStripMenuItem4.Checked;
            seriesPoint_4.Enabled = toolStripMenuItem5.Checked;
            seriesArea_1.Enabled = toolStripMenuItem6.Checked;
            seriesArea_2.Enabled = toolStripMenuItem7.Checked;
            seriesArea_3.Enabled = toolStripMenuItem8.Checked;
        }

        private struct Temper_Time
        {
            public DateTime Time;
            public double Temper;

        }

        Temper_Time temper_Time;
        private void AddTempToQueue()
        {
            StructClass.realTimeStructTemper realTimeStructTemper = new StructClass.realTimeStructTemper();
            StructClass.realTimeTemper[] realTimeTempers = new StructClass.realTimeTemper[8];
            StructClass.realTimeTemper realTimeTemper = new StructClass.realTimeTemper();
            temper_Time = new Temper_Time();
            while (true)
            {
                try
                {
                    lock (obj_lock)
                    {
                        realTimeStructTemper = StaticClass.intPtrs_RealtimeTemper[cameraID - 1];
                        realTimeTempers = realTimeStructTemper.realTimeTemper;
                        for (int i = 0; i < 8; i++)
                        {
                            realTimeTemper = realTimeTempers[i];
                            if (realTimeTemper.temper != 0)
                            {
                                temper_Time.Time = DateTime.Now;
                                temper_Time.Temper = Convert.ToDouble(realTimeTemper.temper) / 100;
                                switch (realTimeTemper.number + 1)
                                {
                                    case 1://编号
                                        queueLine.Enqueue(temper_Time);
                                        break;
                                    case 2:
                                        queuePoint_1.Enqueue(temper_Time);
                                        break;
                                    case 3:
                                        queuePoint_2.Enqueue(temper_Time);
                                        break;
                                    case 4:
                                        queuePoint_3.Enqueue(temper_Time);
                                        break;
                                    case 5:
                                        queuePoint_4.Enqueue(temper_Time);
                                        break;
                                    case 6:
                                        queueArea_1.Enqueue(temper_Time);
                                        break;
                                    case 7:
                                        queueArea_2.Enqueue(temper_Time);
                                        break;
                                    case 8:
                                        queueArea_3.Enqueue(temper_Time);
                                        break;
                                }
                            }
                        }
                        #region
                        if (queueArea_1.Count > 600)
                        {
                            for (int b = 0; b < 5; b++)
                            {
                                queueArea_1.Dequeue();
                            }
                        }
                        if (queueArea_2.Count > 600)
                        {
                            for (int b = 0; b < 5; b++)
                            {
                                queueArea_2.Dequeue();
                            }
                        }
                        if (queueArea_3.Count > 600)
                        {
                            for (int b = 0; b < 5; b++)
                            {
                                queueArea_3.Dequeue();
                            }
                        }
                        if (queuePoint_1.Count > 600)
                        {
                            for (int b = 0; b < 5; b++)
                            {
                                queuePoint_1.Dequeue();
                            }
                        }
                        if (queuePoint_2.Count > 600)
                        {
                            for (int b = 0; b < 5; b++)
                            {
                                queuePoint_2.Dequeue();
                            }
                        }
                        if (queuePoint_3.Count > 600)
                        {
                            for (int b = 0; b < 5; b++)
                            {
                                queuePoint_3.Dequeue();
                            }
                        }
                        if (queuePoint_4.Count > 600)
                        {
                            for (int b = 0; b < 5; b++)
                            {
                                queuePoint_4.Dequeue();
                            }
                        }
                        if (queueLine.Count > 600)
                        {
                            for (int b = 0; b < 5; b++)
                            {
                                queueLine.Dequeue();
                            }
                        }
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "正在中止线程。")
                    {

                    }
                    else
                    {
                        MessageBox.Show("获取实时温度数据异常！" + ex.Message + ex.StackTrace);
                    }
                }
                Thread.Sleep(100);
            }
        }
      
        private void timer1_Tick(object sender, EventArgs e)
        {
            lock (obj_lock)
            {
                seriesLine.Points.Clear();
                seriesPoint_1.Points.Clear();
                seriesPoint_2.Points.Clear();
                seriesPoint_3.Points.Clear();
                seriesPoint_4.Points.Clear();
                seriesArea_1.Points.Clear();
                seriesArea_2.Points.Clear();
                seriesArea_3.Points.Clear();
                for (int i = 0; i < queueArea_1.Count; i++)
                {
                    seriesArea_1.Points.AddXY(i + 1, queueArea_1.ElementAt(i).Temper);
                }
                for (int i = 0; i < queueArea_2.Count; i++)
                {
                    seriesArea_2.Points.AddXY(i + 1, queueArea_2.ElementAt(i).Temper);
                }
                for (int i = 0; i < queueArea_3.Count; i++)
                {
                    seriesArea_3.Points.AddXY(i + 1, queueArea_3.ElementAt(i).Temper);
                }
                for (int i = 0; i < queuePoint_1.Count; i++)
                {
                    seriesPoint_1.Points.AddXY(i + 1, queuePoint_1.ElementAt(i).Temper);
                }
                for (int i = 0; i < queuePoint_2.Count; i++)
                {
                    seriesPoint_2.Points.AddXY(i + 1, queuePoint_2.ElementAt(i).Temper);
                }
                for (int i = 0; i < queuePoint_3.Count; i++)
                {
                    seriesPoint_3.Points.AddXY(i + 1, queuePoint_3.ElementAt(i).Temper);
                }
                for (int i = 0; i < queuePoint_4.Count; i++)
                {
                    seriesPoint_4.Points.AddXY(i + 1, queuePoint_4.ElementAt(i).Temper);
                }
                for (int i = 0; i < queueLine.Count; i++)
                {
                    seriesLine.Points.AddXY(i + 1, queueLine.ElementAt(i).Temper);
                }
                lblEndTime.Text = DateTime.Now.ToString("T");
                if (Temp_series == null || Temp_series.Name == "")
                {
                    lblStartTime.Text = " ";
                }
                else
                {
                    if (Temp_series.Name == "测温线L1")
                    {
                        lblStartTime.Text = queueLine.ElementAt(0).Time.ToString("T");
                    }
                    if (Temp_series.Name == "测温点S2")
                    {
                        lblStartTime.Text = queuePoint_1.ElementAt(0).Time.ToString("T");
                    }
                    if (Temp_series.Name == "测温点S3")
                    {
                        lblStartTime.Text = queuePoint_2.ElementAt(0).Time.ToString("T");
                    }
                    if (Temp_series.Name == "测温点S4")
                    {
                        lblStartTime.Text = queuePoint_3.ElementAt(0).Time.ToString("T");
                    }
                    if (Temp_series.Name == "测温点S5")
                    {
                        lblStartTime.Text = queuePoint_4.ElementAt(0).Time.ToString("T");
                    }
                    if (Temp_series.Name == "测温区域A6")
                    {
                        lblStartTime.Text = queueArea_1.ElementAt(0).Time.ToString("T");
                    }
                    if (Temp_series.Name == "测温区域A7")
                    {
                        lblStartTime.Text = queueArea_2.ElementAt(0).Time.ToString("T");
                    }
                    if (Temp_series.Name == "测温区域A8")
                    {
                        lblStartTime.Text = queueArea_3.ElementAt(0).Time.ToString("T");
                    }
                }
            }
        }


        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        ToolTip toolTip = new ToolTip();
        Series Temp_series;
        private void chart_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = new Point(e.X, e.Y);
            chart.ChartAreas[0].CursorX.SetCursorPixelPosition(point, true);
            chart.ChartAreas[0].CursorY.SetCursorPixelPosition(point, true);
            chart.ChartAreas[0].CursorX.LineColor = Color.Black;
            chart.ChartAreas[0].CursorY.LineColor = Color.Black;
            chart.ChartAreas[0].CursorX.LineWidth = 1;
            chart.ChartAreas[0].CursorY.LineWidth = 1;
            var result = chart.HitTest(e.X, e.Y,ChartElementType.DataPoint);
            if (result.ChartElementType==ChartElementType.DataPoint)
            {
                int i = result.PointIndex;
                DataPoint dataPoint = result.Series.Points[i];
                Temp_series = result.Series;
                toolTip.AutoPopDelay = 1000;
                toolTip.SetToolTip(chart, dataPoint.YValues[0].ToString()+"°C");
            }

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripMenuItem1.Checked= !toolStripMenuItem1.Checked;
            seriesLine.Enabled = toolStripMenuItem1.Checked;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            toolStripMenuItem2.Checked = !toolStripMenuItem2.Checked;
            seriesPoint_1.Enabled = toolStripMenuItem2.Checked;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            toolStripMenuItem3.Checked = !toolStripMenuItem3.Checked;
            seriesPoint_2.Enabled = toolStripMenuItem3.Checked;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            toolStripMenuItem4.Checked = !toolStripMenuItem4.Checked;
            seriesPoint_3.Enabled = toolStripMenuItem4.Checked;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            toolStripMenuItem5.Checked = !toolStripMenuItem5.Checked;
            seriesPoint_4.Enabled = toolStripMenuItem5.Checked;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            toolStripMenuItem6.Checked = !toolStripMenuItem6.Checked;
            seriesArea_1.Enabled = toolStripMenuItem6.Checked;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            toolStripMenuItem7.Checked = !toolStripMenuItem7.Checked;
            seriesArea_2.Enabled = toolStripMenuItem7.Checked;
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            toolStripMenuItem8.Checked = !toolStripMenuItem8.Checked;
            seriesArea_3.Enabled = toolStripMenuItem8.Checked;
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
