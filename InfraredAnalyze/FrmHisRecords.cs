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

namespace InfraredAnalyze
{
    public partial class FrmHisRecords : Form
    {
        public FrmHisRecords(int CamraID)
        {
            InitializeComponent();
            dtpStart.MaxDate = DateTime.Now.Date;
            dtpStart.MinDate = DateTime.Now.Date.AddDays(-90);

            dtpEnd.MaxDate = DateTime.Now.Date;
            dtpEnd.MinDate = DateTime.Now.Date.AddDays(-90);

            cameraId = CamraID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        private void btnWindow_MouseClick(object sender, MouseEventArgs e)
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

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMin_MouseEnter(object sender, EventArgs e)
        {
            btnMin.BackColor = Color.Yellow;
        }

        private void btnMin_MouseLeave(object sender, EventArgs e)
        {
            btnMin.BackColor = Color.Transparent;
        }

        private void pnlHeader_DoubleClick(object sender, EventArgs e)
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

        SqlCreate sqlCreate = new SqlCreate();
        private int cameraId;
        
        private void FrmHisRecords_Load(object sender, EventArgs e)
        {
            if (cameraId == 0)
            {
                cbxCameraID.SelectedIndex = 0;
            }
            else
            {
                cbxCameraID.SelectedIndex = cameraId;
            }
            cbxAlarmType.SelectedIndex = 0;
            List<StructClass.StructRecordsData> list = new List<StructClass.StructRecordsData>();
            list = sqlCreate.Select_WarningRecords(cbxCameraID.SelectedIndex, cbxAlarmType.Text, dtpStart.Value, dtpEnd.Value,StaticClass.DataBaseName);//ip地址没用到
            dgvHisRecords.Rows.Clear();
            foreach (StructClass.StructRecordsData structRecords in list)
            {
                dgvHisRecords.Rows.Add(structRecords.CameraID, structRecords.IPAddress, structRecords.dateTime, structRecords.Type, structRecords.Message);
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            dgvHisRecords.Rows.Clear();
            List<StructClass.StructRecordsData> list = new List<StructClass.StructRecordsData>();
            list = sqlCreate.Select_WarningRecords(cbxCameraID.SelectedIndex, cbxAlarmType.Text, dtpStart.Value, dtpEnd.Value,StaticClass.DataBaseName);
            foreach (StructClass.StructRecordsData structRecords in list)
            {
                dgvHisRecords.Rows.Add(structRecords.CameraID, structRecords.IPAddress, structRecords.dateTime, structRecords.Type, structRecords.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string[] filePath = null;
            filePath = ShowSaveFileDialog();
            if (filePath[0] != null && filePath[1] != null)
            {
                DataTable dt = new DataTable();
                // 列强制转换
                for (int count = 0; count < dgvHisRecords.Columns.Count; count++)
                {
                    DataColumn dc = new DataColumn(dgvHisRecords.Columns[count].Name.ToString());
                    dt.Columns.Add(dc);
                }
                // 循环行
                for (int count = 0; count < dgvHisRecords.Rows.Count; count++)
                {
                    DataRow dr = dt.NewRow();
                    for (int countsub = 0; countsub < dgvHisRecords.Columns.Count; countsub++)
                    {
                        dr[countsub] = Convert.ToString(dgvHisRecords.Rows[count].Cells[countsub].Value);
                    }
                    dt.Rows.Add(dr);
                }
                dt.Columns[0].ColumnName = "探测器编号";
                dt.Columns[1].ColumnName = "IP地址";
                dt.Columns[2].ColumnName = "时间";
                dt.Columns[3].ColumnName = "告警类型";
                dt.Columns[4].ColumnName = "告警内容";
                Excel.ExportToExcel(dt, filePath[0], filePath[1]);
                MessageBox.Show("导出成功！");
                dgvHisRecords.Dock = DockStyle.Fill;
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
    }
}
