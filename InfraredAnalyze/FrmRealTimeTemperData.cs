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

        Series seriesPoint_1 = new Series("测温点1");
        Series seriesPoint_2 = new Series("测温点2");
        Series seriesPoint_3 = new Series("测温点3");
        Series seriesPoint_4 = new Series("测温点4");
        Series seriesLine = new Series("测温线");
        Series seriesArea_1 = new Series("测温区域1");
        Series seriesArea_2 = new Series("测温区域2");
        Series seriesArea_3 = new Series("测温区域3");
        Series seriesArea_4 = new Series("测温区域4");

        Queue<double> queuePoint_1 = new Queue<double>(250);//一秒钟两次数据(大概)  两分钟的数据量
        Queue<double> queuePoint_2 = new Queue<double>(250);
        Queue<double> queuePoint_3 = new Queue<double>(250);
        Queue<double> queuePoint_4 = new Queue<double>(250);
        Queue<double> queueLine = new Queue<double>(250);
        Queue<double> queueArea_1 = new Queue<double>(250);
        Queue<double> queueArea_2 = new Queue<double>(250);
        Queue<double> queueArea_3 = new Queue<double>(250);
        Queue<double> queueArea_4 = new Queue<double>(250);
        int Count_queuePoint_1;//线程同步用
        int Count_queuePoint_2;
        int Count_queuePoint_3;
        int Count_queuePoint_4;
        int Count_queueArea_1;
        int Count_queueArea_2;
        int Count_queueArea_3;
        int Count_queueArea_4;
        int Count_queueLine;

        private void ChartType()
        {
            seriesPoint_1.ChartType = SeriesChartType.Spline;
            seriesArea_1.BorderWidth = 2;
            seriesPoint_2.ChartType = SeriesChartType.Spline;
            seriesArea_2.BorderWidth = 2;
            seriesPoint_3.ChartType = SeriesChartType.Spline;
            seriesArea_3.BorderWidth = 2;
            seriesPoint_4.ChartType = SeriesChartType.Spline;
            seriesArea_4.BorderWidth = 2;
            seriesLine.ChartType = SeriesChartType.Spline;
            seriesLine.BorderWidth = 2;
            seriesArea_1.ChartType = SeriesChartType.Line;
            seriesArea_1.BorderWidth = 2;
            seriesArea_2.ChartType = SeriesChartType.Spline;
            seriesArea_2.BorderWidth = 2;
            seriesArea_3.ChartType = SeriesChartType.Spline;
            seriesArea_3.BorderWidth = 2;
            seriesArea_4.ChartType = SeriesChartType.Spline;
            seriesArea_4.BorderWidth = 2;
            //chartRealTimeData.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "yyyy-MM-dd-HH:mm:ss";
            chartRealTimeData.ChartAreas[0].AxisX.LineWidth = 2;
            chartRealTimeData.ChartAreas[0].AxisY.LineWidth = 2;
            chartRealTimeData.ChartAreas[0].AxisX.Title = "时间:(S)";
            chartRealTimeData.ChartAreas[0].AxisY.Title = "温度:(℃)";
            //chartRealTimeData.ChartAreas[0].AxisY.Minimum = -30;//Y轴的最小值
            //chartRealTimeData.ChartAreas[0].AxisY.Maximum = 200;//Y 轴的最大值
            chartRealTimeData.ChartAreas[0].AxisX.Minimum = 0;//X轴的最小值
            chartRealTimeData.ChartAreas[0].AxisX.Maximum = 250;//X轴的最大值
            chartRealTimeData.ChartAreas[0].AxisX.Interval = 5;//X轴的间隔
            chartRealTimeData.ChartAreas[0].AxisY.Interval = 2;//Y轴的间隔
            chartRealTimeData.ChartAreas[0].CursorX.IsUserEnabled = true;
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
            chartRealTimeData.Series.Add(seriesArea_4);
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
            cbxAreaNum.SelectedIndex = 4;
            cbxAreaType.SelectedIndex = 3;
            tbxCameraID.Text = cameraID.ToString();
            thread_AddTemperData.Start();
            chartRealTimeData.Series.Clear();//清除默认的series
            ChartHis_AddSeries();
            ChartType();
        }

        private void AddTemperData()
        {
            StructClass.realTimeStructTemper realTimeStructTemper = new StructClass.realTimeStructTemper();
            StructClass.realTimeTemper[] realTimeTempers = new StructClass.realTimeTemper[8];
            StructClass.realTimeTemper realTimeTemper = new StructClass.realTimeTemper();
            while (true)
            {
                try
                {
                    realTimeStructTemper = StaticClass.intPtrs_RealtimeTemper[cameraID - 1];
                    realTimeTempers = realTimeStructTemper.realTimeTemper;
                    for (int i = 0; i < 8; i++)
                    {
                        realTimeTemper = realTimeTempers[i];
                        if (realTimeTemper.number != 0 && realTimeTemper.temper != 0 && realTimeTemper.type != 0)
                        {
                            switch (realTimeTemper.number + 1)
                            {
                                case 1://线
                                    queueLine.Enqueue(Convert.ToDouble(realTimeTemper.temper) / 100);
                                    tempList_Line = queueLine.ToList();
                                    Interlocked.Increment(ref Count_queueLine);
                                    break;
                                case 2:
                                    queuePoint_1.Enqueue(Convert.ToDouble(realTimeTemper.temper) / 100);
                                    tempList_Point1 = queuePoint_1.ToList();
                                    Interlocked.Increment(ref Count_queuePoint_1);
                                    break;
                                case 3:
                                    queuePoint_2.Enqueue(Convert.ToDouble(realTimeTemper.temper) / 100);
                                    tempList_Point2 = queuePoint_2.ToList();
                                    Interlocked.Increment(ref Count_queuePoint_2);
                                    break;
                                case 4:
                                    queuePoint_3.Enqueue(Convert.ToDouble(realTimeTemper.temper) / 100);
                                    tempList_Point3 = queuePoint_3.ToList();
                                    Interlocked.Increment(ref Count_queuePoint_3);
                                    break;
                                case 5:
                                    queuePoint_4.Enqueue(Convert.ToDouble(realTimeTemper.temper) / 100);
                                    tempList_Point4 = queuePoint_4.ToList();
                                    Interlocked.Increment(ref Count_queuePoint_4);
                                    break;
                                case 6:
                                    queueArea_1.Enqueue(Convert.ToDouble(realTimeTemper.temper) / 100);
                                    tempList_Area1 = queueArea_1.ToList();
                                    Interlocked.Increment(ref Count_queueArea_1);
                                    break;
                                case 7:
                                    queueArea_2.Enqueue(Convert.ToDouble(realTimeTemper.temper) / 100);
                                    tempList_Area2 = queueArea_2.ToList();
                                    Interlocked.Increment(ref Count_queueArea_1);
                                    break;
                                case 8:
                                    queueArea_3.Enqueue(Convert.ToDouble(realTimeTemper.temper) / 100);
                                    tempList_Area3 = queueArea_3.ToList();
                                    Interlocked.Increment(ref Count_queueArea_1);
                                    break;
                                case 9:
                                    queueArea_4.Enqueue(Convert.ToDouble(realTimeTemper.temper) / 100);
                                    tempList_Area4 = queueArea_4.ToList();
                                    Interlocked.Increment(ref Count_queueArea_1);
                                    break;
                            }
                        }
                    }
                    #region
                    if (Count_queueArea_1 > 250)
                    {
                        for (int b = 0; b < 5; b++)
                        {
                            queueArea_1.Dequeue();
                            Interlocked.Decrement(ref Count_queueArea_1);
                        }
                    }
                    if (Count_queueArea_2 > 250)
                    {
                        for (int b = 0; b < 5; b++)
                        {
                            queueArea_2.Dequeue();
                            Interlocked.Decrement(ref Count_queueArea_2);
                        }
                    }
                    if (Count_queueArea_3 > 250)
                    {
                        for (int b = 0; b < 5; b++)
                        {
                            queueArea_3.Dequeue();
                            Interlocked.Decrement(ref Count_queueArea_3);
                        }
                    }
                    if (Count_queueArea_4 > 250)
                    {
                        for (int b = 0; b < 5; b++)
                        {
                            queueArea_4.Dequeue();
                            Interlocked.Decrement(ref Count_queueArea_4);
                        }
                    }
                    if (Count_queuePoint_1 > 250)
                    {
                        for (int b = 0; b < 5; b++)
                        {
                            queuePoint_1.Dequeue();
                            Interlocked.Decrement(ref Count_queuePoint_1);
                        }
                    }
                    if (Count_queuePoint_2 > 250)
                    {
                        for (int b = 0; b < 5; b++)
                        {
                            queuePoint_1.Dequeue();
                            Interlocked.Decrement(ref Count_queuePoint_2);
                        }
                    }
                    if (Count_queuePoint_3 > 250)
                    {
                        for (int b = 0; b < 5; b++)
                        {
                            queuePoint_3.Dequeue();
                            Interlocked.Decrement(ref Count_queuePoint_3);
                        }
                    }
                    if (Count_queuePoint_4 > 250)
                    {
                        for (int b = 0; b < 5; b++)
                        {
                            queuePoint_4.Dequeue();
                            Interlocked.Decrement(ref Count_queuePoint_4);
                        }
                    }
                    if (Count_queueLine > 250)
                    {
                        for (int b = 0; b < 5; b++)
                        {
                            queueLine.Dequeue();
                            Interlocked.Decrement(ref Count_queueLine);
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show("获取实时温度数据异常！" + ex.Message);
                }
                Thread.Sleep(20);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            seriesPoint_1.Enabled = false;
            seriesPoint_2.Enabled = false;
            seriesPoint_3.Enabled = false;
            seriesPoint_4.Enabled = false;
            seriesLine.Enabled = false;
            seriesArea_1.Enabled = false;
            seriesArea_2.Enabled = false;
            seriesArea_3.Enabled = false;
            seriesArea_4.Enabled = false;
            if (cbxAreaType.SelectedIndex == 0 && cbxAreaNum.SelectedIndex == 0)
            {
                seriesPoint_1.Enabled = true;
            }
            else if (cbxAreaType.SelectedIndex == 0 && cbxAreaNum.SelectedIndex == 1)
            {
                seriesPoint_2.Enabled = true;
            }
            else if (cbxAreaType.SelectedIndex == 0 && cbxAreaNum.SelectedIndex == 2)
            {
                seriesPoint_3.Enabled = true;
            }
            else if (cbxAreaType.SelectedIndex == 0 && cbxAreaNum.SelectedIndex == 3)
            {
                seriesPoint_4.Enabled = true;
            }
            else if (cbxAreaType.SelectedIndex == 1)
            {
                seriesLine.Enabled = true;
            }
            else if (cbxAreaType.SelectedIndex == 2 && cbxAreaNum.SelectedIndex == 0)
            {
                seriesArea_1.Enabled = true;
            }
            else if (cbxAreaType.SelectedIndex == 2 && cbxAreaNum.SelectedIndex == 1)
            {
                seriesArea_2.Enabled = true;
            }
            else if (cbxAreaType.SelectedIndex == 2 && cbxAreaNum.SelectedIndex == 2)
            {
                seriesArea_3.Enabled = true;
            }
            else if (cbxAreaType.SelectedIndex == 2 && cbxAreaNum.SelectedIndex == 3)
            {
                seriesArea_4.Enabled = true;
            }
            else if (cbxAreaType.SelectedIndex == 3 && cbxAreaNum.SelectedIndex == 4)
            {
                seriesPoint_1.Enabled = true;
                seriesPoint_2.Enabled = true;
                seriesPoint_3.Enabled = true;
                seriesPoint_4.Enabled = true;
                seriesLine.Enabled = true;
                seriesArea_1.Enabled = true;
                seriesArea_2.Enabled = true;
                seriesArea_3.Enabled = true;
                seriesArea_4.Enabled = true;
            }
        }

        private void cbxAreaType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAreaType.SelectedIndex == 3)
            {
                cbxAreaNum.SelectedIndex = 4;
            }
        }

        List<double> tempList_Point1 = new List<double>();
        List<double> tempList_Point2 = new List<double>();
        List<double> tempList_Point3 = new List<double>();
        List<double> tempList_Point4 = new List<double>();
        List<double> tempList_Area1 = new List<double>();
        List<double> tempList_Area2 = new List<double>();
        List<double> tempList_Area3 = new List<double>();
        List<double> tempList_Area4 = new List<double>();
        List<double> tempList_Line = new List<double>();

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                seriesPoint_1.Points.Clear();
                seriesPoint_2.Points.Clear();
                seriesPoint_3.Points.Clear();
                seriesPoint_4.Points.Clear();
                seriesLine.Points.Clear();
                seriesArea_1.Points.Clear();
                seriesArea_2.Points.Clear();
                seriesArea_3.Points.Clear();
                seriesArea_4.Points.Clear();
               
                for (int i = 0; i < tempList_Area1.Count; i++)
                {
                    seriesArea_1.Points.AddXY(i + 1, tempList_Area1[i]);
                }

                for (int i = 0; i < tempList_Area2.Count; i++)
                {
                    seriesArea_2.Points.AddXY(i + 1, tempList_Area2[i]);
                }
                for (int i = 0; i < tempList_Area3.Count; i++)
                {
                    seriesArea_3.Points.AddXY(i + 1, tempList_Area3[i]);
                }
                for (int i = 0; i < tempList_Area4.Count; i++)
                {
                    seriesArea_4.Points.AddXY(i + 1, tempList_Area4[i]);
                }
                for (int i = 0; i < tempList_Point1.Count; i++)
                {
                    seriesPoint_1.Points.AddXY(i + 1, tempList_Point1[i]);
                }
                for (int i = 0; i < tempList_Point2.Count; i++)
                {
                    seriesPoint_2.Points.AddXY(i + 1, tempList_Point2[i]);

                }
                for (int i = 0; i < tempList_Point3.Count; i++)
                {
                    seriesPoint_3.Points.AddXY(i + 1, tempList_Point3[i]);
                }
                for (int i = 0; i < tempList_Point4.Count; i++)
                {
                    seriesPoint_4.Points.AddXY(i + 1, tempList_Point4[i]);
                }
                for (int i = 0; i < tempList_Line.Count; i++)
                {
                    seriesLine.Points.AddXY(i + 1, tempList_Line[i]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}