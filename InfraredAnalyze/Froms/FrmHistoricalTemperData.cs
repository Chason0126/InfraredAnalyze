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
        Thread thread;

        private void FrmHistoricalTemperData_Load(object sender, EventArgs e)
        {
            cbxType.SelectedIndex = 0;
            tbxCameraID.Text = cameraID.ToString();
            AddHistoricalDGV();
         
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
            AddHistoricalDGV();
           
        }

        List<StructClass.StructTemperData> list;
        private void AddHistoricalDGV()
        {
            thread = new Thread(showDialog);
            thread.Start();
            list = new List<StructClass.StructTemperData>();
            list = sqlCreate.Select_TemperData(cameraID, dtpStart.Value, dtpEnd.Value, StaticClass.DataBaseName);
            dgvHisData.Rows.Clear();
            foreach (StructClass.StructTemperData structTemperData in list)
            {
                dgvHisData.Rows.Add(structTemperData.CameraID, structTemperData.IPAddress, structTemperData.dateTime, structTemperData.Type, structTemperData.Temper/100, structTemperData.Status);
            }
            thread.Abort();
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

        FrmIsRunning isRunning;
        private void showDialog()
        {
            try
            {
                isRunning = new FrmIsRunning();
                isRunning.ShowDialog();
            }
            catch (Exception ex)
            {

            }
        }
        
    }
}
