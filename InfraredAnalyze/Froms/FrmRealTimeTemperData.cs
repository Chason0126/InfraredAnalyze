using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace InfraredAnalyze
{
    public partial class FrmRealTimeTemperData : Form
    {
        public FrmRealTimeTemperData()
        {
            InitializeComponent();
        }

        private int cameraID;
        public int CameraID { get => cameraID; set => cameraID = value; }

        #region
        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        private void btnWindow_MouseEnter(object sender, EventArgs e)
        {
            btnWindow.BackColor = Color.Green;
        }

        private void btnWindow_MouseLeave(object sender, EventArgs e)
        {
            btnWindow.BackColor = Color.Transparent;
        }

        private void btnMin_MouseEnter(object sender, EventArgs e)
        {
            btnMin.BackColor = Color.Yellow;
        }

        private void btnMin_MouseLeave(object sender, EventArgs e)
        {
            btnMin.BackColor = Color.Transparent;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            thread_AddTemperData.Abort();
            this.Close();
            this.Dispose();
        }

        private void btnWindow_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                btnWindow.BackgroundImage = Properties.Resources.窗口化;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                btnWindow.BackgroundImage = Properties.Resources.最大化;
            }
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        Point point;
        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }

        private void pnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point tempPoint = MousePosition;
                tempPoint.Offset(-point.X, -point.Y);
                this.Location = tempPoint;
            }
        }
        #endregion

        Series seriesPoint_1 = new Series("测温点S2");
        Series seriesPoint_2 = new Series("测温点S3");
        Series seriesPoint_3 = new Series("测温点S4");
        Series seriesPoint_4 = new Series("测温点S5");
        Series seriesLine = new Series("测温线L1");
        Series seriesArea_1 = new Series("测温区域A6");
        Series seriesArea_2 = new Series("测温区域A7");
        Series seriesArea_3 = new Series("测温区域A8");

        private  struct Temper_Time
        {
           public DateTime Time;
           public double Temper;

        }

        Queue<Temper_Time> queuePoint_1 = new Queue<Temper_Time>(3000);
        Queue<Temper_Time> queuePoint_2 = new Queue<Temper_Time>(3000);
        Queue<Temper_Time> queuePoint_3 = new Queue<Temper_Time>(3000);
        Queue<Temper_Time> queuePoint_4 = new Queue<Temper_Time>(3000);
        Queue<Temper_Time> queueLine = new Queue<Temper_Time>(3000);
        Queue<Temper_Time> queueArea_1 = new Queue<Temper_Time>(3000);
        Queue<Temper_Time> queueArea_2 = new Queue<Temper_Time>(3000);
        Queue<Temper_Time> queueArea_3 = new Queue<Temper_Time>(3000);

        private void ChartType()
        {
            seriesPoint_1.ChartType = SeriesChartType.Spline;
            seriesPoint_1.BorderWidth = 2;
            seriesPoint_2.ChartType = SeriesChartType.Spline;
            seriesPoint_2.BorderWidth = 2;
            seriesPoint_3.ChartType = SeriesChartType.Spline;
            seriesPoint_3.BorderWidth = 2;
            seriesPoint_4.ChartType = SeriesChartType.Spline;
            seriesPoint_4.BorderWidth = 2;

            seriesLine.ChartType = SeriesChartType.Spline;
            seriesLine.BorderWidth = 2;

            chartRealTimeData.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chartRealTimeData.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            seriesArea_1.ChartType = SeriesChartType.Line;
            seriesArea_1.BorderWidth = 2;
            seriesArea_2.ChartType = SeriesChartType.Spline;
            seriesArea_2.BorderWidth = 2;
            seriesArea_3.ChartType = SeriesChartType.Spline;
            seriesArea_3.BorderWidth = 2;
         
            chartRealTimeData.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
            chartRealTimeData.ChartAreas[0].AxisX.LineWidth = 2;
            chartRealTimeData.ChartAreas[0].AxisY.LineWidth = 2;
            chartRealTimeData.ChartAreas[0].AxisX.Title = "时间:(S)";
            chartRealTimeData.ChartAreas[0].AxisY.Title = "温度:(℃)";
            //chartRealTimeData.ChartAreas[0].AxisY.Minimum = -30;//Y轴的最小值
            //chartRealTimeData.ChartAreas[0].AxisY.Maximum = 200;//Y 轴的最大值
            chartRealTimeData.ChartAreas[0].AxisX.Minimum = 0;//X轴的最小值
            chartRealTimeData.ChartAreas[0].AxisX.Maximum = 3000;//X轴的最大值
            //chartRealTimeData.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd-HH:mm:ss";
            chartRealTimeData.ChartAreas[0].AxisX.Interval = 100;//X轴的间隔
            chartRealTimeData.ChartAreas[0].AxisY.Interval = 5;//Y轴的间隔
            chartRealTimeData.ChartAreas[0].CursorX.IsUserEnabled = true;
            chartRealTimeData.ChartAreas[0].CursorY.IsUserEnabled = true;
            chartRealTimeData.ChartAreas[0].CursorX.LineColor = Color.Black;
            chartRealTimeData.ChartAreas[0].CursorY.LineColor = Color.Black;
            chartRealTimeData.ChartAreas[0].CursorX.LineWidth = 1;
            chartRealTimeData.ChartAreas[0].CursorY.LineWidth = 1;

            chartRealTimeData.Titles.Clear();
        }

        private void ChartHis_AddSeries()
        {
            //seriesArea_1.IsValueShownAsLabel = true;
            //seriesArea_2.IsValueShownAsLabel = true;
            //seriesArea_3.IsValueShownAsLabel = true;
            //seriesArea_4.IsValueShownAsLabel = true;
            //seriesPoint_1.IsValueShownAsLabel = true;
            //seriesPoint_2.IsValueShownAsLabel = true;
            //seriesPoint_3.IsValueShownAsLabel = true;
            //seriesPoint_4.IsValueShownAsLabel = true;
            //seriesLine.IsValueShownAsLabel = true;
            chartRealTimeData.Series.Add(seriesArea_1);
            chartRealTimeData.Series.Add(seriesArea_2);
            chartRealTimeData.Series.Add(seriesArea_3);
            chartRealTimeData.Series.Add(seriesPoint_1);
            chartRealTimeData.Series.Add(seriesPoint_2);
            chartRealTimeData.Series.Add(seriesPoint_3);
            chartRealTimeData.Series.Add(seriesPoint_4);
            chartRealTimeData.Series.Add(seriesLine);
        }

        //private DMSDK.fMessCallBack fMessCallBack;
        Thread thread_AddTemperData;
        private void FrmRealTimeTemperData_Load(object sender, EventArgs e)
        {
            if (StaticClass.intPtrs_Operate[cameraID - 1] <= 0)
            {
                MessageBox.Show("设备未连接！");
                this.Dispose();
                return;
            }

            thread_AddTemperData = new Thread(AddTemperData);
            timer1.Start();
            tbxCameraID.Text = cameraID.ToString();
            thread_AddTemperData.Start();
            chartRealTimeData.Series.Clear();//清除默认的series
            ChartHis_AddSeries();
            ChartType();

            seriesLine.Enabled = toolStripMenuItem1.Checked;
            seriesPoint_1.Enabled = toolStripMenuItem2.Checked;
            seriesPoint_2.Enabled = toolStripMenuItem3.Checked;
            seriesPoint_3.Enabled = toolStripMenuItem4.Checked;
            seriesPoint_4.Enabled = toolStripMenuItem5.Checked;
            seriesArea_1.Enabled = toolStripMenuItem6.Checked;
            seriesArea_2.Enabled = toolStripMenuItem7.Checked;
            seriesArea_3.Enabled = toolStripMenuItem8.Checked;
        }

        object obj = new object();
        private void AddTemperData()
        {
            StructClass.realTimeStructTemper realTimeStructTemper = new StructClass.realTimeStructTemper();
            StructClass.realTimeTemper[] realTimeTempers = new StructClass.realTimeTemper[8];
            StructClass.realTimeTemper realTimeTemper = new StructClass.realTimeTemper();
            Temper_Time temper_Time = new Temper_Time();
            while (true)
            {
                try
                {
                    lock (obj)
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
                        if (queueArea_1.Count > 3000)
                        {
                            for (int b = 0; b < 5; b++)
                            {
                                queueArea_1.Dequeue();
                            }
                        }
                        if (queueArea_2.Count > 3000)
                        {
                            for (int b = 0; b < 5; b++)
                            {
                                queueArea_2.Dequeue();
                            }
                        }
                        if (queueArea_3.Count > 3000)
                        {
                            for (int b = 0; b < 5; b++)
                            {
                                queueArea_3.Dequeue();
                            }
                        }
                        if (queuePoint_1.Count > 3000)
                        {
                            for (int b = 0; b < 5; b++)
                            {
                                queuePoint_1.Dequeue();
                            }
                        }
                        if (queuePoint_2.Count > 3000)
                        {
                            for (int b = 0; b < 5; b++)
                            {
                                queuePoint_2.Dequeue();
                            }
                        }
                        if (queuePoint_3.Count > 3000)
                        {
                            for (int b = 0; b < 5; b++)
                            {
                                queuePoint_3.Dequeue();
                            }
                        }
                        if (queuePoint_4.Count > 3000)
                        {
                            for (int b = 0; b < 5; b++)
                            {
                                queuePoint_4.Dequeue();
                            }
                        }
                        if (queueLine.Count > 3000)
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
            try
            {
                lock (obj)
                {
                    seriesPoint_1.Points.Clear();
                    seriesPoint_2.Points.Clear();
                    seriesPoint_3.Points.Clear();
                    seriesPoint_4.Points.Clear();
                    seriesLine.Points.Clear();
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
                    Show_Temper(tbxChoosePointTemper, lblStartTime);
                    lblEndTime.Text = DateTime.Now.ToString("T");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("绘制曲线异常" + ex.Message + ex.StackTrace);
            }
        }

        Series Temp_series = new Series();
        int Temp_index;
        private void chartRealTimeData_MouseDown(object sender, MouseEventArgs e)//根据点击的点坐标 返回 series和点的index值
        {
            try
            {
                Point Temperpoint = new Point(e.X, e.Y);
                //HitTestResult hitTestResult = chartRealTimeData.HitTest(e.X, e.Y, ChartElementType.DataPoint);
                //if (hitTestResult.ChartElementType == ChartElementType.DataPoint)
                //{
                //    int i = hitTestResult.PointIndex;
                //    DataPoint dataPoint = hitTestResult.Series.Points[i];
                //    string XValue = dataPoint.XValue.ToString();
                //    string YValue = dataPoint.YValues[0].ToString();
                //    tbxChoosePointTemper.Text = YValue;
                //}
                Get_SeriesAndIndex(Temperpoint, out Temp_series, out Temp_index);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }

        private void Get_SeriesAndIndex(Point point,out Series Temp_series, out int count)//点击一下以后 获取到 那个点的series 和点的Index值
        {
            try
            {
                HitTestResult hitTestResult = chartRealTimeData.HitTest(point.X, point.Y, ChartElementType.DataPoint);
                if (hitTestResult.ChartElementType == ChartElementType.DataPoint)
                {
                    count = hitTestResult.PointIndex;
                    Temp_series = hitTestResult.Series;
                }
                else
                {
                    count = 1;
                    Temp_series = null;
                }
            }
            catch (Exception ex)
            {
                count = 1;
                Temp_series = null;
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            
        }

        private void Show_Temper(TextBox textBox,Label lblStartTime)//根据series和index 显示温度
        {
            try
            {
                if (Temp_series==null|| Temp_series.Name == "")
                {
                    textBox.Text = "无";
                    lblStartTime.Text = "无";
                }
                else
                {
                    DataPoint dataPoint = Temp_series.Points[Temp_index];
                    textBox.Text = dataPoint.YValues[0].ToString();
                    if (Temp_series.Name == "测温区域A6")
                    {
                        lblStartTime.Text = queueArea_1.ElementAt(0).Time.ToString("T");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
           
        }

        ToolTip toolTip = new ToolTip();
        private void chartRealTimeData_MouseMove(object sender, MouseEventArgs e)
        {
            //Point point = new Point(e.X, e.Y);
            //Get_SeriesAndIndex(point, out Temp_series, out Temp_index);
            //chartRealTimeData.ChartAreas[0].CursorX.SetCursorPixelPosition(point, true);
            //chartRealTimeData.ChartAreas[0].CursorY.SetCursorPixelPosition(point, true);
            //chartRealTimeData.ChartAreas[0].CursorX.LineColor = Color.Black;
            //chartRealTimeData.ChartAreas[0].CursorY.LineColor = Color.Black;
            //chartRealTimeData.ChartAreas[0].CursorX.LineWidth = 1;
            //chartRealTimeData.ChartAreas[0].CursorY.LineWidth = 1;
            //var result = chartRealTimeData.HitTest(e.X, e.Y, ChartElementType.DataPoint);
            //if (result.ChartElementType == ChartElementType.DataPoint)
            //{
            //    int i = result.PointIndex;
            //    DataPoint dataPoint = result.Series.Points[i];
            //    Temp_series = result.Series;
            //    toolTip.AutoPopDelay = 1000;
            //    toolTip.SetToolTip(chartRealTimeData, dataPoint.YValues[0].ToString() + "°C");
            //}
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripMenuItem1.Checked = !toolStripMenuItem1.Checked;
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
    }
}