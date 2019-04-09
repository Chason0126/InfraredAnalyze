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

        private DMSDK.fMessCallBack fMessCallBack;
        private void FrmRealTimeTemperData_Load(object sender, EventArgs e)
        {
            if (StaticClass.intPtrs_Operate[cameraID - 1] <= 0)
            {
                MessageBox.Show("设备未连接！");
                this.Dispose();
                return;
            }
            cbxAreaNum.SelectedIndex = 4;
            cbxAreaType.SelectedIndex = 3;
            tbxCameraID.Text = cameraID.ToString();
            timer1.Start();
            chartRealTimeData.Series.Clear();//清除默认的series
            ChartHis_AddSeries();
            ChartType();
            fMessCallBack = new DMSDK.fMessCallBack(dmMessCallBack);//回调函数实例
            DMSDK.DM_GetTemp(StaticClass.intPtrs_Operate[cameraID - 1], 1);//连续获取测温对象的数据
            DMSDK.DM_SetAllMessCallBack(fMessCallBack, 0);
        }

        DMSDK.tagTempMessage tempMessage;
        DMSDK.tagTemperature tagTemperature;
        DMSDK.tagAlarm tagAlarm;
        int len;
        private void dmMessCallBack(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)
        {
            int Msg = msg - 0x8000;
            switch (Msg)
            {
                case 0x3051://错误

                    break;
                case 0x3053://温度数据
                    tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
                    len = tempMessage.len;
                    if (tempMessage.dvrIP == StaticClass.intPtrs_Ip[cameraID - 1])
                    {
                        for (int i = 0; i < len; i++)
                        {
                            tagTemperature = new DMSDK.tagTemperature();
                            tagTemperature = tempMessage.temperInfo[i];
                            int ID = tagTemperature.number + 1;
                            switch (ID)
                            {
                                case 1://线
                                    queueLine.Enqueue(Convert.ToDouble(tagTemperature.temper) / 100);
                                    break;
                                case 2:
                                    queuePoint_1.Enqueue(Convert.ToDouble(tagTemperature.temper) / 100);
                                    break;
                                case 3:
                                    queuePoint_2.Enqueue(Convert.ToDouble(tagTemperature.temper) / 100);
                                    break;
                                case 4:
                                    queuePoint_3.Enqueue(Convert.ToDouble(tagTemperature.temper) / 100);
                                    break;
                                case 5:
                                    queuePoint_4.Enqueue(Convert.ToDouble(tagTemperature.temper) / 100);
                                    break;
                                case 6:
                                    queueArea_1.Enqueue(Convert.ToDouble(tagTemperature.temper) / 100);//
                                    break;
                                case 7:
                                    queueArea_2.Enqueue(Convert.ToDouble(tagTemperature.temper) / 100);
                                    break;
                                case 8:
                                    queueArea_3.Enqueue(Convert.ToDouble(tagTemperature.temper) / 100);
                                    break;
                                case 9:
                                    queueArea_4.Enqueue(Convert.ToDouble(tagTemperature.temper) / 100);
                                    break;
                            }
                        }
                    }
                    break;
                case 0x3054://报警消息
                    tagAlarm = (DMSDK.tagAlarm)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagAlarm));
                    int ErrId = tagAlarm.AlarmID;
                    break;
            }
        }

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
                for (int i = 0; i < queueArea_1.Count; i++)
                {
                    seriesArea_1.Points.AddXY(i + 1, queueArea_1.ElementAt(i));//DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss")
                }
                for (int i = 0; i < queueArea_2.Count; i++)
                {
                    seriesArea_2.Points.AddXY(i + 1, queueArea_2.ElementAt(i));
                }
                for (int i = 0; i < queueArea_3.Count; i++)
                {
                    seriesArea_3.Points.AddXY(i + 1, queueArea_3.ElementAt(i));
                }
                for (int i = 0; i < queueArea_4.Count; i++)
                {
                    seriesArea_4.Points.AddXY(i + 1, queueArea_4.ElementAt(i));
                }
                for (int i = 0; i < queuePoint_1.Count; i++)
                {
                    seriesPoint_1.Points.AddXY(i + 1, queuePoint_1.ElementAt(i));
                }
                for (int i = 0; i < queuePoint_2.Count; i++)
                {
                    seriesPoint_2.Points.AddXY(i + 1, queuePoint_2.ElementAt(i));

                }
                for (int i = 0; i < queuePoint_3.Count; i++)
                {
                    seriesPoint_3.Points.AddXY(i + 1, queuePoint_3.ElementAt(i));
                }
                for (int i = 0; i < queuePoint_4.Count; i++)
                {
                    seriesPoint_4.Points.AddXY(i + 1, queuePoint_4.ElementAt(i));
                }
                for (int i = 0; i < queueLine.Count; i++)
                {
                    seriesLine.Points.AddXY(i + 1, queueLine.ElementAt(i));
                }
                Dequ(queuePoint_1);
                Dequ(queuePoint_2);
                Dequ(queuePoint_3);
                Dequ(queuePoint_4);
                Dequ(queueArea_1);
                Dequ(queueArea_2);
                Dequ(queueArea_3);
                Dequ(queueArea_4);
                Dequ(queueLine);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Dequ(Queue<double> que)//数量超过250 压出五个
        {
            if (que.Count > 250)
            {
                for (int b = 0; b < 5; b++)
                {
                    que.Dequeue();
                }
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
            if (cbxAreaType.SelectedIndex == 0&& cbxAreaNum.SelectedIndex == 0)
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
    }
}
