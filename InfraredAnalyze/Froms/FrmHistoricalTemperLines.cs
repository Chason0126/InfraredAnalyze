using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace InfraredAnalyze
{
    public partial class FrmHistoricalTemperLines : Form
    {
        public FrmHistoricalTemperLines(int CameraID)
        {
            InitializeComponent();
            dtpEnd.MinDate = DateTime.Now.Date.AddDays(-14);
            dtpEnd.MaxDate = DateTime.Now.Date;
            dtpStart.MinDate = DateTime.Now.Date.AddDays(-14);
            dtpStart.MaxDate = DateTime.Now.Date;
            cameraID = CameraID;
            seriesPoint_1.ChartType = SeriesChartType.Spline;
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

            chartHisrotricalData.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd-HH:mm:ss";
            //chartHisrotricalData.MouseWheel += ChartHisrotricalData_MouseWheel;//数据量太大了 太卡了 解决不了
            //chartHisrotricalData.ChartAreas[0].AxisX.Interval = 5;
            chartHisrotricalData.ChartAreas[0].AxisY.Interval = 5;
            //chartHisrotricalData.ChartAreas[0].AxisX.ScaleView.Zoom(2, 3);
            //chartHisrotricalData.ChartAreas[0].CursorX.IsUserEnabled = true;
            //chartHisrotricalData.ChartAreas[0].CursorY.IsUserEnabled = true;
            //chartHisrotricalData.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartHisrotricalData.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartHisrotricalData.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            chartHisrotricalData.ChartAreas[0].CursorX.AutoScroll = true;
            //chartHisrotricalData.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            //chartHisrotricalData.ChartAreas[0].AxisX.ScrollBar.Size = 10;
            //chartHisrotricalData.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;
            //chartHisrotricalData.ChartAreas[0].AxisX.ScaleView.SmallScrollSize = double.NaN;
            chartHisrotricalData.ChartAreas[0].AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Seconds;
            chartHisrotricalData.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chartHisrotricalData.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chartHisrotricalData.Paint += ChartHisrotricalData_Paint;

            chartHisrotricalData.Series.Clear();
            chartHisrotricalData.Series.Add(seriesArea_1);
            chartHisrotricalData.Series.Add(seriesArea_2);
            chartHisrotricalData.Series.Add(seriesArea_3);
            chartHisrotricalData.Series.Add(seriesPoint_1);
            chartHisrotricalData.Series.Add(seriesPoint_2);
            chartHisrotricalData.Series.Add(seriesPoint_3);
            chartHisrotricalData.Series.Add(seriesPoint_4);
            chartHisrotricalData.Series.Add(seriesLine);
            cbxPercent.SelectedIndex = 0;
            cbxType.SelectedIndex = 0;
            tbxCameraID.Text = cameraID.ToString();
            AddHistoricalLines();
            thread = new Thread(showDialog);
            thread.Start();
        }

        #region
        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
            thread = new Thread(showDialog);
            thread.Start();
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

        private int cameraID;
        SqlCreate sqlCreate = new SqlCreate();
        Series seriesPoint_1 = new Series("测温点S2");
        Series seriesPoint_2 = new Series("测温点S3");
        Series seriesPoint_3 = new Series("测温点S4");
        Series seriesPoint_4 = new Series("测温点S5");
        Series seriesLine = new Series("测温线L1");
        Series seriesArea_1 = new Series("测温区域A6");
        Series seriesArea_2 = new Series("测温区域A7");
        Series seriesArea_3 = new Series("测温区域A8");
        Thread thread;

        private void FrmHistoricalTemperData_Load(object sender, EventArgs e)
        {
            
        }


        private void btnQuery_MouseEnter(object sender, EventArgs e)
        {
            btnQuery.BackColor = Color.Yellow;
        }

        private void btnQuery_MouseLeave(object sender, EventArgs e)
        {
            btnQuery.BackColor = Color.LightGray;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            l1 = 0; s2 = 0;s3 = 0;s4 = 0;s5 = 0;a6 = 0;a7 = 0;a8 = 0;
            seriesPoint_1.Points.Clear();
            seriesPoint_2.Points.Clear();
            seriesPoint_3.Points.Clear();
            seriesPoint_4.Points.Clear();
            seriesArea_1.Points.Clear();
            seriesArea_2.Points.Clear();
            seriesArea_3.Points.Clear();
            seriesLine.Points.Clear();
            AddHistoricalLines();
            thread = new Thread(showDialog);
            thread.Start();
        }

        CancellationTokenSource cancellation = new CancellationTokenSource();
        private void ChartHisrotricalData_Paint(object sender, PaintEventArgs e)
        {
            if (thread != null)
            {
                thread.Abort();
            }
        }

        List<StructClass.StructTemperData> list;
        private void AddHistoricalLines()
        {
            list = sqlCreate.Select_TemperData_ASC(cameraID, dtpStart.Value, dtpEnd.Value, StaticClass.DataBaseName);
            foreach (StructClass.StructTemperData structTemper in list)
            {
                ChartHisrotricalData(structTemper);
            }
        }

        int l1 = 0;
        int s2 = 0;
        int s3 = 0;
        int s4 = 0;
        int s5 = 0;
        int a6 = 0;
        int a7 = 0;
        int a8 = 0;
        int percent;

        private void cbxPercent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxPercent.SelectedIndex == 0)
            {
                percent = 10;
            }
            else if(cbxPercent.SelectedIndex == 1)
            {
                percent = 2;
            }
            else if (cbxPercent.SelectedIndex == 2)
            {
                percent = 1;
            }
        }

        private void ChartHisrotricalData(StructClass.StructTemperData structTemper)
        {
            if (structTemper.Type == "S2")
            {
                s2++;
                if(s2% percent == 0)
                {
                    seriesPoint_1.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
                }
            }
            else if (structTemper.Type == "S3")
            {
                s3++;
                if (s3 % percent == 0)
                {
                    seriesPoint_2.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
                }
            }
            else if (structTemper.Type == "S4")
            {
                s4++;
                if (s4 % percent == 0)
                {
                    seriesPoint_3.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
                }
            }
            else if (structTemper.Type == "S5")
            {
                s5++;
                if (s5 % percent == 0)
                {
                    seriesPoint_4.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
                }
            }
            else if (structTemper.Type == "L1")
            {
                l1++;
                if (l1 % percent == 0)
                {
                    seriesLine.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
                }
            }
            else if (structTemper.Type == "A6")
            {
                a6++;
                if (a6 % percent == 0)
                {
                    seriesArea_1.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
                }
            }
            else if (structTemper.Type == "A7")
            {
                a7++;
                if (a7 % percent == 0)
                {
                    seriesArea_2.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
                }
            }
            else if (structTemper.Type == "A8")
            {
                a8++;
                if (a8 % percent == 0)
                {
                    seriesArea_3.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
                }
            }
        }

        //FrmIsRunning isRunning;
        private void showDialog()
        {
            try
            {
                FrmIsRunning isRunning = new FrmIsRunning();
                isRunning.ShowDialog();
            }
            catch(ThreadAbortException ex)
            {

            }
            catch (Exception ex)
            {

            }
        }

        List<Zoom_StartAndFinish> arrayList_Zoom = new List<Zoom_StartAndFinish>();
        struct Zoom_StartAndFinish
        {
            public double posXStart;
            public double posXFinish;
        }
        private void ChartHisrotricalData_MouseWheel(object sender, MouseEventArgs e)
        {
            //try
            //{
            //    Zoom_StartAndFinish zoom_StartFinish = new Zoom_StartAndFinish();
            //    double XMin = chartHisrotricalData.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
            //    double XMax = chartHisrotricalData.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
            //    double posXStart = chartHisrotricalData.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) - (XMax - XMin) / 4;
            //    double posXFinish = chartHisrotricalData.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) + (XMax - XMin) / 4;
            //    zoom_StartFinish.posXStart = posXStart;
            //    zoom_StartFinish.posXFinish = posXFinish;

            //    if (e.Delta < 0 && arrayList_Zoom.Count >= 2)
            //    {
            //        zoom_StartFinish = arrayList_Zoom[arrayList_Zoom.Count - 2];
            //        chartHisrotricalData.ChartAreas[0].AxisX.ScaleView.Zoom(zoom_StartFinish.posXStart, zoom_StartFinish.posXFinish);
            //        arrayList_Zoom.RemoveAt(arrayList_Zoom.Count - 1);
            //    }
            //    if (arrayList_Zoom.Count == 1)
            //    {
            //        chartHisrotricalData.ChartAreas[0].AxisX.ScaleView.ZoomReset();
            //    }
            //    if (e.Delta > 0)
            //    {
            //        arrayList_Zoom.Add(zoom_StartFinish);
            //        chartHisrotricalData.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message + ex.StackTrace);
            //}
        }

        private void chartHisrotricalData_MouseDown(object sender, MouseEventArgs e)
        {
            //HitTestResult hitTestResult = chartHisrotricalData.HitTest(e.X, e.Y, ChartElementType.DataPoint);
            //if (hitTestResult.Series == null)
            //{
            //    tbxHisDateTime.Text = "无";
            //    tbxHisTemper.Text = "无";
            //}
            //if (hitTestResult.ChartElementType == ChartElementType.DataPoint)
            //{
            //    int i = hitTestResult.PointIndex;
            //    DataPoint dataPoint = hitTestResult.Series.Points[i];
            //    tbxHisDateTime.Text = DateTime.FromOADate(dataPoint.XValue).ToString().Replace(' ', '/');
            //    tbxHisTemper.Text = dataPoint.YValues[0].ToString();
            //}
        }

       
    }
}
