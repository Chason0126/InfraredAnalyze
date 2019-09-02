using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace InfraredAnalyze
{
    public partial class FrmHistoricalTemperData : Form
    {
        public FrmHistoricalTemperData(int CameraID)
        {
            InitializeComponent();
            dtpEnd.MinDate = DateTime.Now.Date.AddDays(-14);
            dtpEnd.MaxDate = DateTime.Now.Date;

            dtpStart.MinDate = DateTime.Now.Date.AddDays(-14);
            dtpStart.MaxDate = DateTime.Now.Date;
            cameraID = CameraID;
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

        private void ChartType()
        {
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

            chartHisrotricalData.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "yyyy-MM-dd-HH:mm:ss";
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
        }

        List<Zoom_StartAndFinish> arrayList_Zoom = new List<Zoom_StartAndFinish>();
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
               
                if (e.Delta < 0&& arrayList_Zoom.Count >= 2)
                {
                    zoom_StartFinish = arrayList_Zoom[arrayList_Zoom.Count - 2];
                    chartHisrotricalData.ChartAreas[0].AxisX.ScaleView.Zoom(zoom_StartFinish.posXStart, zoom_StartFinish.posXFinish);
                    arrayList_Zoom.RemoveAt(arrayList_Zoom.Count-1);
                }
                if (arrayList_Zoom.Count==1)
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
            else if (structTemper.Type =="S4")
            {
                seriesPoint_3.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
            }
            else if (structTemper.Type =="S5")
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
            else if (structTemper.Type =="A8")
            {
                seriesArea_3.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
            }
        }

        private void ChartHis_AddSeries()
        {
            //seriesArea_1.IsValueShownAsLabel = false;
            //seriesArea_2.IsValueShownAsLabel = false;
            //seriesArea_3.IsValueShownAsLabel = false;
            //seriesArea_4.IsValueShownAsLabel = false;
            //seriesPoint_1.IsValueShownAsLabel = false;
            //seriesPoint_2.IsValueShownAsLabel = false;
            //seriesPoint_3.IsValueShownAsLabel = false;
            //seriesPoint_4.IsValueShownAsLabel = false;
            //seriesLine.IsValueShownAsLabel = false;
            chartHisrotricalData.Series.Add(seriesArea_1);
            chartHisrotricalData.Series.Add(seriesArea_2);
            chartHisrotricalData.Series.Add(seriesArea_3);
            chartHisrotricalData.Series.Add(seriesPoint_1);
            chartHisrotricalData.Series.Add(seriesPoint_2);
            chartHisrotricalData.Series.Add(seriesPoint_3);
            chartHisrotricalData.Series.Add(seriesPoint_4);
            chartHisrotricalData.Series.Add(seriesLine);
        }

        private void FrmHistoricalTemperData_Load(object sender, EventArgs e)
        {
            //cameraID = 1;
            //tabHistroicalData.Region = new Region(new RectangleF(tabPage1.Left, tabPage1.Top, tabPage1.Width, tabPage1.Height));//把上面隐藏
            cbxType.SelectedIndex = 0;
            chartHisrotricalData.Series.Clear();//清除默认的series
            ChartType();
            tbxCameraID.Text = cameraID.ToString();
          
            List<StructClass.StructTemperData> list = new List<StructClass.StructTemperData>();
            list = sqlCreate.Select_TemperData(cameraID, dtpStart.Value, dtpEnd.Value, StaticClass.DataBaseName);
            dgvHisData.Rows.Clear();
            foreach (StructClass.StructTemperData structTemper in list)
            {
                dgvHisData.Rows.Add(structTemper.CameraID, structTemper.IPAddress, structTemper.dateTime, structTemper.Type, Convert.ToDecimal(structTemper.Temper) / 100, structTemper.Status);
            }
            list = sqlCreate.Select_TemperData_ASC(cameraID, dtpStart.Value, dtpEnd.Value, StaticClass.DataBaseName);
            int i = 0;
            foreach (StructClass.StructTemperData structTemper in list)
            {
                i++;
                if (i % 5 == 0)
                {
                    ChartHisrotricalData(structTemper);
                }
            }
            ChartHis_AddSeries();
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
            List<StructClass.StructTemperData> list = new List<StructClass.StructTemperData>();
            list = sqlCreate.Select_TemperData(cameraID, cbxType.Text, dtpStart.Value, dtpEnd.Value, StaticClass.DataBaseName);
            dgvHisData.Rows.Clear();
            foreach (var series in chartHisrotricalData.Series)
            {
                series.Points.Clear();
            }
            foreach (StructClass.StructTemperData structTemper in list)
            {
                dgvHisData.Rows.Add(structTemper.CameraID, structTemper.IPAddress, structTemper.dateTime, structTemper.Type, Convert.ToDecimal(structTemper.Temper) / 100, structTemper.Status);
            }
            list = sqlCreate.Select_TemperData_ASC(cameraID, dtpStart.Value, dtpEnd.Value, StaticClass.DataBaseName);
            int i = 0;
            foreach (StructClass.StructTemperData structTemper in list)
            {
                i++;
                if (i % 5 == 0)
                {
                    ChartHisrotricalData(structTemper);
                }
               
            }
        }

        
        private void btnExport_Click(object sender, EventArgs e)//导出数据
        {
            string[] filePath = null;
            filePath = ShowSaveFileDialog();
            if (filePath[0] != null && filePath[1] != null)
            {
                DataTable dt = new DataTable();
                // 列强制转换
                for (int count = 0; count < dgvHisData.Columns.Count; count++)
                {
                    DataColumn dc = new DataColumn(dgvHisData.Columns[count].Name.ToString());
                    dt.Columns.Add(dc);
                }
                // 循环行
                for (int count = 0; count < dgvHisData.Rows.Count; count++)
                {
                    DataRow dr = dt.NewRow();
                    for (int countsub = 0; countsub < dgvHisData.Columns.Count; countsub++)
                    {
                        dr[countsub] = Convert.ToString(dgvHisData.Rows[count].Cells[countsub].Value);
                    }
                    dt.Rows.Add(dr);
                }
                dt.Columns[0].ColumnName = "探测器编号";
                dt.Columns[1].ColumnName = "IP地址";
                dt.Columns[2].ColumnName = "时间";
                dt.Columns[3].ColumnName = "区域编号";
                dt.Columns[4].ColumnName = "温度";
                dt.Columns[5].ColumnName = "状态";
                Excel.ExportToExcel(dt, filePath[0], filePath[1]);
                MessageBox.Show("导出成功！");
                dgvHisData.Dock = DockStyle.Fill;
            }
        }

        private string[] ShowSaveFileDialog()
        {
            string localFilePath = "";
            string[] strPathFile = new string[2];
            //string localFilePath, fileNameExt, newFileName, FilePath; 
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel表格（*.xls）|*.xls";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                localFilePath = sfd.FileName.ToString();
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
                string path = localFilePath.Substring(0, localFilePath.LastIndexOf("\\") + 1);
                strPathFile[0] = path;
                strPathFile[1] = fileNameExt;
            }
            return strPathFile;
        }

        private void rdbTable_CheckedChanged(object sender, EventArgs e)//切换显示曲线还是数据表
        {
            if (rdbTable.Checked)
            {
                tabHistroicalData.SelectedTab = tabPage1;
            }
            else
            {
                tabHistroicalData.SelectedTab = tabPage2;
            }
        }

        private void chartHisrotricalData_GetToolTipText(object sender, ToolTipEventArgs e)//卡顿
        {
            //HitTestResult hitTestResult = chartHisrotricalData.HitTest(e.X, e.Y, ChartElementType.DataPoint);
            //if (hitTestResult.ChartElementType == ChartElementType.DataPoint)
            //{
            //    int i = hitTestResult.PointIndex;
            //    DataPoint dataPoint = hitTestResult.Series.Points[i];
            //    string XValue = DateTime.FromOADate(dataPoint.XValue).ToString();
            //    string YValue = dataPoint.YValues[0].ToString();
            //    e.Text = "时间：" + XValue + "\r\n温度：" + YValue;
            //}
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
