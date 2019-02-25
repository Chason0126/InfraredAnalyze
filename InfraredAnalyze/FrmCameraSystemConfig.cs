using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace InfraredAnalyze
{
    public partial class FrmCameraSystemConfig : Form
    {
        public FrmCameraSystemConfig()
        {
            InitializeComponent();
        }

        private int iPCameraID;
        private string iP;

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        Point point;

        public int IPCameraID { get => iPCameraID; set => iPCameraID = value; }
        public string IP { get => iP; set => iP = value; }

        private void panHeader_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }

        private void panHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            {
                Point tempPoint = MousePosition;
                tempPoint.Offset(-point.X, -point.Y);
                this.Location = tempPoint;
            }
        }

        private void tbxFrames_Leave(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[1-9]\d*$");
            Match match = regex.Match(tbxFrames.Text);
            if(!match.Success)
            {
                MessageBox.Show("请输入正整数！");
                tbxFrames.Focus();
            }
        }

        private void FrmCameraSystemConfig_Load(object sender, EventArgs e)
        {
            DMSDK.DM_Init();
            StaticClass.intPtrs_Operate[iPCameraID - 1] = DMSDK.DM_Connect(StaticClass.intPtrs_UCPbx[iPCameraID - 1], IP, 80);
            StringBuilder dateTime = new StringBuilder();
            if (StaticClass.intPtrs_Operate[iPCameraID - 1]<=0)
            {
                MessageBox.Show("连接失败，请检查线路,或修改参数后新连接试！");
                this.Close();

            }else
            {
                DMSDK.DM_GetDateTime(StaticClass.intPtrs_Operate[iPCameraID - 1], dateTime);
                if(dateTime==null)
                {
                    MessageBox.Show("获取系统时间失败！");
                }else
                {
                    dtpCameraDateTime.Value = Convert.ToDateTime(dateTime.ToString());
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            System.DateTime dateTime = new DateTime();
            dateTime = dtpCameraDateTime.Value;
            DMSDK.DM_SetDateTime(StaticClass.intPtrs_Operate[iPCameraID - 1], dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        private void FrmCameraSystemConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            DMSDK.DM_Disconnect(StaticClass.intPtrs_Operate[iPCameraID - 1]);
            timer1.Stop();
        }

        private void btnUpdateTime_Click(object sender, EventArgs e)
        {
            dtpCameraDateTime.Value = System.DateTime.Now;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dtpCameraDateTime.Value = System.DateTime.Now;
        }
    }
}
