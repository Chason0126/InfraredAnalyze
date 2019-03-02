using System;
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
    public partial class FrmTemperParamConfig : Form
    {
        public FrmTemperParamConfig()
        {
            InitializeComponent();
        }
        private int cameraId;
        private string ip;
        int tempOperateIntptr;
        int tempConnectIntptr;
        int isTempValueOnImage = 0;//仪器上是否显示测温数据(0隐藏 1显示)

        public int CameraId { get => cameraId; set => cameraId = value; }
        public string Ip { get => ip; set => ip = value; }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void FrmTemperParamConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            DMSDK.DM_CloseMonitor(tempConnectIntptr);
            DMSDK.DM_Disconnect(tempOperateIntptr);
        }

        private void FrmTemperParamConfig_Load(object sender, EventArgs e)
        {
            DMSDK.DM_Init();
            tempOperateIntptr = DMSDK.DM_Connect(pbxScreen.Handle, ip, 80);
            int InitValue = DMSDK.DM_PlayerInit(pbxScreen.Handle);
            if (tempOperateIntptr <= 0 || InitValue < 0)
            {
                MessageBox.Show("连接失败，请检查线路或者修改参数后重试！");
                this.Close();
                this.Dispose();
            }
            else
            {
                tempConnectIntptr = DMSDK.DM_OpenMonitor(pbxScreen.Handle, ip, 5000);
                isTempValueOnImage = DMSDK.DM_GetTempValueOnImageStatus(tempOperateIntptr);//获取是否在屏幕上显示测温数据。
            }
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
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

        
    }
}
