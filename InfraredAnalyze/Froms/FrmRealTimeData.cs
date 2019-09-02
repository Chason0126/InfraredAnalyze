using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfraredAnalyze
{
    public partial class FrmRealTimeData : Form
    {
        SqlCreate sqlCreate = new SqlCreate();
        public FrmRealTimeData()
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

        private void btnMaxOrNormal_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            if (WindowState == FormWindowState.Normal)
            {
                btnMaxOrNormal.BackgroundImage = Properties.Resources.最大化;
            }
            else if (WindowState == FormWindowState.Maximized)
            {
                btnMaxOrNormal.BackgroundImage = Properties.Resources.窗口化;
            }
            Update_tlp();
        }

        #region//拖动
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void Frm_Move()
        {
            int WM_SYSCOMMAND = 0x0112;
            int SC_MOVE = 0xF010;
            int HTCAPTION = 0x0002;
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            Frm_Move();
        }

        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            Frm_Move();
        }

        #endregion

        List<StructClass.StructIAnalyzeConfig> structSMInfrared_Configs;
        private void FrmRealTimeData_Load(object sender, EventArgs e)
        {
            cbxSensor.Items.Clear();
            List<string> cameraName = new List<string>();

            structSMInfrared_Configs = sqlCreate.Select_All_SMInfrared_ProjConfig(StaticClass.DataBaseName);//全部启用的探测器
            foreach (StructClass.StructIAnalyzeConfig InfraredConfig in structSMInfrared_Configs)//把启用的探测器加进去
            {
                if (InfraredConfig.Enable)
                {
                    cameraName.Add(InfraredConfig.CameraName);
                }
            }
            foreach (string name in cameraName)
            {
                cbxSensor.Items.Add(name);
            }

            if (cbxSensor.Items.Count > 0)
            {
                cbxSensor.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("无可用的探测器！");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                string name;
                if (tlpRealTimeData.Controls.Count >= 16)
                {
                    MessageBox.Show("数量已达到上限!");
                    return;
                }
                if (cbxSensor.Text == "")
                {
                    MessageBox.Show("请选择需要添加的探测器名称！");
                    return;
                }
                name = cbxSensor.Text;
                foreach (StructClass.StructIAnalyzeConfig InfraredConfig in structSMInfrared_Configs)//把启用的探测器加进去
                {
                    if (InfraredConfig.CameraName==cbxSensor.Text)
                    {
                        i = InfraredConfig.CameraID;
                        break;
                    }
                }

                RealTimeLines realTimeLines = new RealTimeLines(i, name);
                realTimeLines.Disposed += RealTimeLines_Disposed;
                realTimeLines.Dock = DockStyle.Fill;
                tlpRealTimeData.Controls.Add(realTimeLines);
                Update_tlp();
            }
            catch(Exception ex)
            {
                MessageBox.Show("添加失败！" + ex.Message + ex.StackTrace);
            }
           
        }

        private void RealTimeLines_Disposed(object sender, EventArgs e)
        {
            Update_tlp();
        }

        private void Update_tlp()
        {
            if (tlpRealTimeData.Controls.Count == 1)
            {
                tlpRealTimeData.ColumnCount = 1;
                tlpRealTimeData.RowCount = 1;
                foreach (Control control in tlpRealTimeData.Controls)
                {
                    control.Height = tlpRealTimeData.Height;
                    control.Width = tlpRealTimeData.Width;
                }
            }
            else if (tlpRealTimeData.Controls.Count >= 2 && tlpRealTimeData.Controls.Count <= 4)
            {
                tlpRealTimeData.ColumnCount = 2;
                tlpRealTimeData.RowCount = 2;
                foreach (Control control in tlpRealTimeData.Controls)
                {
                    control.Height = tlpRealTimeData.Height / 2;
                    control.Width = tlpRealTimeData.Width / 2;
                }
            }
            else if (tlpRealTimeData.Controls.Count >= 5 && tlpRealTimeData.Controls.Count <= 9)
            {
                tlpRealTimeData.ColumnCount = 3;
                tlpRealTimeData.RowCount = 3;
                foreach (Control control in tlpRealTimeData.Controls)
                {
                    control.Height = tlpRealTimeData.Height / 3;
                    control.Width = tlpRealTimeData.Width / 3;
                }
            }
            else if (tlpRealTimeData.Controls.Count >= 10 && tlpRealTimeData.Controls.Count <= 16)
            {
                tlpRealTimeData.ColumnCount = 4;
                tlpRealTimeData.RowCount = 4;
                foreach (Control control in tlpRealTimeData.Controls)
                {
                    control.Height = tlpRealTimeData.Height / 4;
                    control.Width = tlpRealTimeData.Width / 4;
                }
            }

        }
    }
}
