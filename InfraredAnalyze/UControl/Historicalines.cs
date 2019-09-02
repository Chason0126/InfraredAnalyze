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
    public partial class Historicalines : UserControl
    {
        public Historicalines(List<StructClass.StructTemperData> list)
        {
            InitializeComponent();
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
            chartHisrotricalData.MouseWheel += ChartHisrotricalData_MouseWheel;
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
            this.list = list;
            chartHisrotricalData.Paint += ChartHisrotricalData_Paint;
            chartHisrotricalData.Invalidated += ChartHisrotricalData_Invalidated;
        }

        private void ChartHisrotricalData_Invalidated(object sender, InvalidateEventArgs e)
        {
            isAwait = true;
        }

        private void ChartHisrotricalData_Paint(object sender, PaintEventArgs e)
        {
            isAwait = false;
        }

     

        Series seriesPoint_1 = new Series("测温点S2");
        Series seriesPoint_2 = new Series("测温点S3");
        Series seriesPoint_3 = new Series("测温点S4");
        Series seriesPoint_4 = new Series("测温点S5");
        Series seriesLine = new Series("测温线L1");
        Series seriesArea_1 = new Series("测温区域A6");
        Series seriesArea_2 = new Series("测温区域A7");
        Series seriesArea_3 = new Series("测温区域A8");
        List<StructClass.StructTemperData> list = new List<StructClass.StructTemperData>();
        private bool isAwait;

        List<Zoom_StartAndFinish> arrayList_Zoom = new List<Zoom_StartAndFinish>();

        public bool IsAwait { get => isAwait; set => isAwait = value; }

        struct Zoom_StartAndFinish
        {
            public double posXStart;
            public double posXFinish;
        }
        private void ChartHisrotricalData_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                Zoom_StartAndFinish zoom_StartFinish = new Zoom_StartAndFinish();
                double XMin = chartHisrotricalData.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                double XMax = chartHisrotricalData.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                double posXStart = chartHisrotricalData.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) - (XMax - XMin) / 4;
                double posXFinish = chartHisrotricalData.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) + (XMax - XMin) / 4;
                zoom_StartFinish.posXStart = posXStart;
                zoom_StartFinish.posXFinish = posXFinish;

                if (e.Delta < 0 && arrayList_Zoom.Count >= 2)
                {
                    zoom_StartFinish = arrayList_Zoom[arrayList_Zoom.Count - 2];
                    chartHisrotricalData.ChartAreas[0].AxisX.ScaleView.Zoom(zoom_StartFinish.posXStart, zoom_StartFinish.posXFinish);
                    arrayList_Zoom.RemoveAt(arrayList_Zoom.Count - 1);
                }
                if (arrayList_Zoom.Count == 1)
                {
                    chartHisrotricalData.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                }
                if (e.Delta > 0)
                {
                    arrayList_Zoom.Add(zoom_StartFinish);
                    chartHisrotricalData.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void ChartHisrotricalData(StructClass.StructTemperData structTemper)
        {
            if (structTemper.Type == "S2")
            {
                seriesPoint_1.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
            }
            else if (structTemper.Type == "S3")
            {
                seriesPoint_2.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
            }
            else if (structTemper.Type == "S4")
            {
                seriesPoint_3.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
            }
            else if (structTemper.Type == "S5")
            {
                seriesPoint_4.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
            }
            else if (structTemper.Type == "L1")
            {
                seriesLine.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
            }
            else if (structTemper.Type == "A6")
            {
                seriesArea_1.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
            }
            else if (structTemper.Type == "A7")
            {
                seriesArea_2.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
            }
            else if (structTemper.Type == "A8")
            {
                seriesArea_3.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
            }
        }

        private void Historicalines_Load(object sender, EventArgs e)
        {
            foreach (StructClass.StructTemperData structTemper in list)
            {
                ChartHisrotricalData(structTemper);
            }
            chartHisrotricalData.Series.Clear();
            chartHisrotricalData.Series.Add(seriesArea_1);
            chartHisrotricalData.Series.Add(seriesArea_2);
            chartHisrotricalData.Series.Add(seriesArea_3);
            chartHisrotricalData.Series.Add(seriesPoint_1);
            chartHisrotricalData.Series.Add(seriesPoint_2);
            chartHisrotricalData.Series.Add(seriesPoint_3);
            chartHisrotricalData.Series.Add(seriesPoint_4);
            chartHisrotricalData.Series.Add(seriesLine);
        }

        private void chartHisrotricalData_MouseDown(object sender, MouseEventArgs e)
        {
            HitTestResult hitTestResult = chartHisrotricalData.HitTest(e.X, e.Y, ChartElementType.DataPoint);
            if (hitTestResult.Series == null)
            {
                tbxHisDateTime.Text = "无";
                tbxHisTemper.Text = "无";
            }
            if (hitTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                int i = hitTestResult.PointIndex;
                DataPoint dataPoint = hitTestResult.Series.Points[i];
                tbxHisDateTime.Text = DateTime.FromOADate(dataPoint.XValue).ToString().Replace(' ', '/');
                tbxHisTemper.Text = dataPoint.YValues[0].ToString();
            }
        }
       
    }
}
