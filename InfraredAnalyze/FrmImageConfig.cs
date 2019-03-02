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
    public partial class FrmImageConfig : Form
    {
        public FrmImageConfig()
        {
            InitializeComponent();
        }

        private int cameraId;
        private string ip;
        int tempOperateIntptr;
        int tempConnectIntptr;
        int Pallette = 0;//仪器的伪彩色色标 色标号（0-9）
        int VideoOutType = 0;//视频输出格式 0PAL 1NTSC
        int VideoMode = 0;//图像手动自动模式0 手动 2自动
        int TempValueOnImageStatus = 0;//仪器是否显示测温数据 0显示 1隐藏 
        int ISOTemp = 0;//仪器的等温温度值
        int ISOHight = 0;//等温高度
        int ISOColor = 0;//等温色号
        int TempUnit = 0;//温度单位0C 1F
        int Bright = 0;//亮度
        int Contrast = 0;//对比度
        int ZoomStatus = 0;//电子放大倍数
        int Palority = 0;//热图像模式
        int GFZ = 0;//冻结状态
        public int CameraId { get => cameraId; set => cameraId = value; }
        public string Ip { get => ip; set => ip = value; }

        #region//窗体操作事件
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion

        #region//获取相机图像参数
        public void GetImageConfigParam()
        {
            Pallette = DMSDK.DM_GetPallette(tempOperateIntptr);//获取伪彩色色标
            VideoOutType = DMSDK.DM_GetVideoOutType(tempOperateIntptr);//获取视频的输出格式
            VideoMode = DMSDK.DM_GetVideoMode(tempOperateIntptr);//获取图像手动自动模式
            TempValueOnImageStatus = DMSDK.DM_GetTempValueOnImageStatus(tempOperateIntptr);//仪器否显示测温数据
            ISOTemp = DMSDK.DM_GetISOTemp(tempOperateIntptr);//等温温度
            ISOHight = DMSDK.DM_GetISOHight(tempOperateIntptr);//等温高度
            ISOColor = DMSDK.DM_GetISOColor(tempOperateIntptr);//等温色号
            TempUnit = DMSDK.DM_GetTempUnit(tempOperateIntptr);//温度单位
            Bright = DMSDK.DM_GetBright(tempOperateIntptr);//亮度
            Contrast = DMSDK.DM_GetContrast(tempOperateIntptr);//对比度
            ZoomStatus = DMSDK.DM_GetZoomStatus(tempOperateIntptr);//电子放大倍数
            Palority = DMSDK.DM_GetPalority(tempOperateIntptr);//热图像模式
            GFZ = DMSDK.DM_GetGFZ(tempOperateIntptr);//冻结状态
        }
        #endregion

        private void FrmImageConfig_Load(object sender, EventArgs e)
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
                GetImageConfigParam();
            }
        }

      

    }
}
