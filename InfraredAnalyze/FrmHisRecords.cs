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
        public FrmHisRecords()
        {
            InitializeComponent();
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

        public int CameraId { get => cameraId; set => cameraId = value; }

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
            ArrayList arrayList = sqlCreate.Select_HisRecords(cbxCameraID.SelectedIndex, "tbxIPAddress.Text", cbxAlarmType.Text, dtpStart.Value, dtpEnd.Value);
            dgvHisRecords.Rows.Clear();
            foreach (StructClass.StructRecordsData structRecords in arrayList)
            {
                dgvHisRecords.Rows.Add(structRecords.CameraID, structRecords.IPAddress, structRecords.dateTime, structRecords.Type, structRecords.Message);
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            dgvHisRecords.Rows.Clear();
            ArrayList arrayList = sqlCreate.Select_HisRecords(cbxCameraID.SelectedIndex, "tbxIPAddress.Text", cbxAlarmType.Text, dtpStart.Value, dtpEnd.Value);
            foreach (StructClass.StructRecordsData structRecords in arrayList)
            {
                dgvHisRecords.Rows.Add(structRecords.CameraID, structRecords.IPAddress, structRecords.dateTime, structRecords.Type, structRecords.Message);
            }
        }
    }
}
