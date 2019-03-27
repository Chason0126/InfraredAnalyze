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
        public FrmHistoricalTemperData()
        {
            InitializeComponent();
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

        public int CameraID { get => cameraID; set => cameraID = value; }

        SqlCreate sqlCreate = new SqlCreate();
        string[] AreaType ={ "测温点", "测温线", "测温区域" };
        Series seriesPoint_1 = new Series("测温点2");
        Series seriesPoint_2 = new Series("测温点3");
        Series seriesPoint_3 = new Series("测温点4");
        Series seriesPoint_4 = new Series("测温点5");
        Series seriesLine = new Series("测温线1");
        Series seriesArea_1 = new Series("测温区域6");
        Series seriesArea_2 = new Series("测温区域7");
        Series seriesArea_3 = new Series("测温区域8");
        Series seriesArea_4 = new Series("测温区域9");

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
            seriesArea_4.ChartType = SeriesChartType.Spline;
            chartHisrotricalData.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "yyyy-MM-dd-HH:mm:ss";
        }


        private void ChartHisrotricalData(StructClass.StructTemperData structTemper)
        {
            
            if (structTemper.Type == 0 && structTemper.Number == 1)
            {
                seriesPoint_1.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
            }
            else if (structTemper.Type == 0 && structTemper.Number == 2)
            {
                seriesPoint_2.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
            }
            else if (structTemper.Type == 0 && structTemper.Number == 3)
            {
                seriesPoint_3.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
            }
            else if (structTemper.Type == 0 && structTemper.Number == 4)
            {
                seriesPoint_4.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
            }
            else if (structTemper.Type == 1 && structTemper.Number == 0)
            {
                seriesLine.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
            }
            else if (structTemper.Type == 2 && structTemper.Number == 5)
            {
                seriesArea_1.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
            }
            else if (structTemper.Type == 2 && structTemper.Number == 6)
            {
                seriesArea_2.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
            }
            else if (structTemper.Type == 2 && structTemper.Number == 7)
            {
                seriesArea_3.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
            }
            else if (structTemper.Type == 2 && structTemper.Number == 8)
            {
                seriesArea_4.Points.AddXY(structTemper.dateTime, Convert.ToDouble(Convert.ToDecimal(structTemper.Temper) / 100));
            }
        }

        private void ChartHis_AddSeries()
        {
            seriesArea_1.IsValueShownAsLabel = true;
            seriesArea_2.IsValueShownAsLabel = true;
            seriesArea_3.IsValueShownAsLabel = true;
            seriesArea_4.IsValueShownAsLabel = true;
            seriesPoint_1.IsValueShownAsLabel = true;
            seriesPoint_2.IsValueShownAsLabel = true;
            seriesPoint_3.IsValueShownAsLabel = true;
            seriesPoint_4.IsValueShownAsLabel = true;
            seriesLine.IsValueShownAsLabel = true;
            chartHisrotricalData.Series.Add(seriesArea_1);
            chartHisrotricalData.Series.Add(seriesArea_2);
            chartHisrotricalData.Series.Add(seriesArea_3);
            chartHisrotricalData.Series.Add(seriesArea_4);
            chartHisrotricalData.Series.Add(seriesPoint_1);
            chartHisrotricalData.Series.Add(seriesPoint_2);
            chartHisrotricalData.Series.Add(seriesPoint_3);
            chartHisrotricalData.Series.Add(seriesPoint_4);
            chartHisrotricalData.Series.Add(seriesLine);
        }

        private void FrmHistoricalTemperData_Load(object sender, EventArgs e)
        {
            //cameraID = 1;
            cbxAreaNum.SelectedIndex = 4;
            cbxAreaType.SelectedIndex = 3;
            chartHisrotricalData.Series.Clear();//清除默认的series
            ChartType();
            if (cameraID != 0)
            {
                ArrayList arrayList = sqlCreate.Select_TemperData(cameraID, dtpStart.Value, dtpEnd.Value);
                dgvHistoricalData.Rows.Clear();
                foreach(StructClass.StructTemperData structTemper in arrayList)
                {
                    dgvHistoricalData.Rows.Add(structTemper.CameraID, structTemper.IPAddress, structTemper.dateTime, AreaType[structTemper.Type], structTemper.Number + 1, Convert.ToDecimal(structTemper.Temper) / 100, structTemper.Status);
                    ChartHisrotricalData(structTemper);
                }
                ChartHis_AddSeries();
            }
        }

        private void cbxAreaType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxAreaNum.Text = "";
            switch (cbxAreaType.SelectedIndex)
            {
                case 0:
                    cbxAreaNum.Items.Clear();
                    for (int i = 2; i <= 5; i++)
                    {
                        cbxAreaNum.Items.Add(i);
                    }
                    cbxAreaNum.Items.Add("所有");
                    cbxAreaNum.SelectedIndex = 0;
                    break;
                case 1:
                    cbxAreaNum.Items.Clear();
                    cbxAreaNum.Items.Add(1);
                    cbxAreaNum.SelectedIndex = 0;
                    break;
                case 2:
                    cbxAreaNum.Items.Clear();
                    for (int i = 6; i <= 8; i++)
                    {
                        cbxAreaNum.Items.Add(i);
                    }
                    cbxAreaNum.Items.Add("所有");
                    cbxAreaNum.SelectedIndex = 0;
                    break;
                case 3:
                    cbxAreaNum.Items.Clear();
                    cbxAreaNum.Items.Add("所有");
                    cbxAreaNum.SelectedIndex = 0;
                    break;
            }
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
            ArrayList arrayList = sqlCreate.Select_TemperData(1, cbxAreaType.SelectedIndex, cbxAreaNum.SelectedIndex, dtpStart.Value, dtpEnd.Value);
            dgvHistoricalData.Rows.Clear();
            foreach(var series in chartHisrotricalData.Series)
            {
                series.Points.Clear();
            }
            foreach (StructClass.StructTemperData structTemper in arrayList)
            {
                dgvHistoricalData.Rows.Add(structTemper.CameraID, structTemper.IPAddress, structTemper.dateTime, AreaType[structTemper.Type], structTemper.Number + 1, Convert.ToDecimal(structTemper.Temper) / 100, structTemper.Status);
                ChartHisrotricalData(structTemper);
            }
        }
        
    }
}
