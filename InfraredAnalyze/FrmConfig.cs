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
    public partial class FrmConfig : Form
    {
        public FrmConfig()
        {
            InitializeComponent();
            this.Disposed += FrmConfig_Disposed;
        }

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
            DialogResult = DialogResult.OK;
            this.Close();
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

        UCPbx uCPbx = new UCPbx();
        private void FrmConfig_Load(object sender, EventArgs e)
        {
            DMSDK.DM_Init();
            DMSDK.DM_PlayerInit(uCPbx.Handle);
            StaticClass.tempIntPtr = uCPbx.Handle;
            if (DMSDK.DM_CheckOnline(StaticClass.Temper_Ip, 5000) < 0)
            {
                MessageBox.Show("该探测器未连接，无法进行其他设置！");
            }
            else
            {
                StaticClass.tempConnect = DMSDK.DM_Connect(uCPbx.Handle, StaticClass.Temper_Ip, 80);
                if (StaticClass.tempConnect > 0)
                {
                    StaticClass.tempMonitor = DMSDK.DM_OpenMonitor(uCPbx.Handle, StaticClass.Temper_Ip, 5000, 0);
                }
            }
            grpConfig.Text = "网络设置";
            btnNetConfig.Tag = 1;
            btnNetConfig.BackColor = Color.Yellow;
            btnNetConfig.PerformClick();
        }

        private void pnlLeftMenu_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)pnlLeftMenu.GetChildAtPoint(new Point(e.X, e.Y));
        }

        private void btnBackColor_Init(Button button)
        {
            grpConfig.Text = button.Text;
            foreach (Button btn in pnlLeftMenu.Controls)
            {
                btn.BackColor = Color.Transparent;
                btn.Tag = 0;//没单击
            }
            button.BackColor = Color.Yellow;
            button.Tag = 1;//单击过
        }

        #region//鼠标进入事件
        private void btnNetConfig_MouseEnter(object sender, EventArgs e)
        {
            btnNetConfig.BackColor = Color.Yellow;
        }

        private void btnNetConfig_MouseLeave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnNetConfig.Tag) == 0)
                btnNetConfig.BackColor = Color.Transparent;
        }

        private void btnMeasureConfig_MouseEnter(object sender, EventArgs e)
        {
            btnMeasureConfig.BackColor = Color.Yellow;
        }

        private void btnMeasureConfig_MouseLeave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnMeasureConfig.Tag) == 0)
                btnMeasureConfig.BackColor = Color.Transparent;
        }

        private void btnDataConfig_MouseEnter(object sender, EventArgs e)
        {
            btnDataConfig.BackColor = Color.Yellow;
        }

        private void btnDataConfig_MouseLeave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnDataConfig.Tag) == 0)
                btnDataConfig.BackColor = Color.Transparent;
        }

        private void btnImageConfig_MouseEnter(object sender, EventArgs e)
        {
            btnImageConfig.BackColor = Color.Yellow;
        }

        private void btnImageConfig_MouseLeave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnImageConfig.Tag) == 0)
                btnImageConfig.BackColor = Color.Transparent;
        }

        private void btnTemperParam_MouseEnter(object sender, EventArgs e)
        {
            btnTemperParam.BackColor = Color.Yellow;
        }

        private void btnTemperParam_MouseLeave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnTemperParam.Tag) == 0)
                btnTemperParam.BackColor = Color.Transparent;
        }

        private void btnAlarmConfig_MouseEnter(object sender, EventArgs e)
        {
            btnAlarmConfig.BackColor = Color.Yellow;
        }

        private void btnAlarmConfig_MouseLeave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnAlarmConfig.Tag) == 0)
                btnAlarmConfig.BackColor = Color.Transparent;
        }

        private void btnSystemConfig_MouseEnter(object sender, EventArgs e)
        {
            btnSystemConfig.BackColor = Color.Yellow;
        }

        private void btnSystemConfig_MouseLeave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnSystemConfig.Tag) == 0)
                btnSystemConfig.BackColor = Color.Transparent;
        }

        private void btnVideoConfig_MouseEnter(object sender, EventArgs e)
        {
            btnVideoConfig.BackColor = Color.Yellow;
        }

        private void btnVideoConfig_MouseLeave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnVideoConfig.Tag) == 0)
                btnVideoConfig.BackColor = Color.Transparent;
        }

        private void btnPTZConfig_MouseEnter(object sender, EventArgs e)
        {
            btnPTZConfig.BackColor = Color.Yellow;
        }

        private void btnPTZConfig_MouseLeave(object sender, EventArgs e)
        {
            if (Convert.ToInt32( btnPTZConfig.Tag) == 0) 
                btnPTZConfig.BackColor = Color.Transparent;
        }

        #endregion


        #region//单击事件
        private void btnNetConfig_Click(object sender, EventArgs e)//网络设置
        {
            grpConfig.Controls.Clear();
            btnBackColor_Init(sender as Button);
            FrmCameraNetConfig frmCameraNetConfig = new FrmCameraNetConfig(uCPbx);
            frmCameraNetConfig.closefather += new childclose(this.closethis);
            frmCameraNetConfig.TopLevel = false;
            grpConfig.Controls.Add(frmCameraNetConfig);
            frmCameraNetConfig.BringToFront();
            frmCameraNetConfig.Location = new Point(4, grpConfig.Height / 4);
            frmCameraNetConfig.Show();
                
        }

        public void closethis()
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btnMeasureConfig_Click(object sender, EventArgs e)//测温设置
        {
            grpConfig.Controls.Clear();
            btnBackColor_Init(sender as Button);
            FrmMeasureTemperConfig frmMeasureTemperConfig = new FrmMeasureTemperConfig();
            frmMeasureTemperConfig.TopLevel = false;
            grpConfig.Controls.Add(frmMeasureTemperConfig);
            frmMeasureTemperConfig.pnlMonitor.Width = 640;
            frmMeasureTemperConfig.pnlMonitor.Height = 480;
            frmMeasureTemperConfig.pnlMonitor.Location = new Point(40, 20);
            frmMeasureTemperConfig.tabArea.Location = new Point(frmMeasureTemperConfig.tabArea.Location.X, frmMeasureTemperConfig.tabArea.Location.Y - 30);
            frmMeasureTemperConfig.BringToFront();
            frmMeasureTemperConfig.Location = new Point(30, 30);
            frmMeasureTemperConfig.Show();
        }

        private void btnDataConfig_Click(object sender, EventArgs e)//保存图片设置
        {
            grpConfig.Controls.Clear();
            btnBackColor_Init(sender as Button);
            FrmSaveImageConfig frmSaveImageConfig = new FrmSaveImageConfig(uCPbx);
            frmSaveImageConfig.TopLevel = false;
            grpConfig.Controls.Add(frmSaveImageConfig);
            frmSaveImageConfig.BringToFront();
            frmSaveImageConfig.Location = new Point(20, 20);
            frmSaveImageConfig.Show();
        }

        private void btnImageConfig_Click(object sender, EventArgs e)//图像设置
        {
            grpConfig.Controls.Clear();
            btnBackColor_Init(sender as Button);
            FrmImageConfig frmImageConfig = new FrmImageConfig();
            frmImageConfig.TopLevel = false;
            grpConfig.Controls.Add(frmImageConfig);
            frmImageConfig.BringToFront();
            frmImageConfig.Location = new Point(100, 50);
            frmImageConfig.Show();
        }

        private void btnTemperParam_Click(object sender, EventArgs e)
        {
            grpConfig.Controls.Clear();
            btnBackColor_Init(sender as Button);
            FrmTemperParamConfig frmTemperParamConfig = new FrmTemperParamConfig();
            frmTemperParamConfig.TopLevel = false;
            grpConfig.Controls.Add(frmTemperParamConfig);
            frmTemperParamConfig.BringToFront();
            frmTemperParamConfig.Location = new Point(200, 50);
            frmTemperParamConfig.Show();
        }

        private void btnAlarmConfig_Click(object sender, EventArgs e)
        {
            grpConfig.Controls.Clear();
            btnBackColor_Init(sender as Button);
            FrmAlarmConfig frmAlarmConfig = new FrmAlarmConfig();
            frmAlarmConfig.TopLevel = false;
            grpConfig.Controls.Add(frmAlarmConfig);
            frmAlarmConfig.BringToFront();
            frmAlarmConfig.Location = new Point(5, 150);
            frmAlarmConfig.Show();
        }

        private void btnSystemConfig_Click(object sender, EventArgs e)
        {
            grpConfig.Controls.Clear();
            btnBackColor_Init(sender as Button);
            FrmSystemConfig frmSystemConfig = new FrmSystemConfig(uCPbx);
            frmSystemConfig.TopLevel = false;
            grpConfig.Controls.Add(frmSystemConfig);
            frmSystemConfig.BringToFront();
            frmSystemConfig.Location = new Point(5, 20);
            frmSystemConfig.Show();
        }

        private void btnVideoConfig_Click(object sender, EventArgs e)
        {
            grpConfig.Controls.Clear();
            btnBackColor_Init(sender as Button);
            FrmSaveVideoConfig frmSaveVideoConfig = new FrmSaveVideoConfig(uCPbx);
            frmSaveVideoConfig.TopLevel = false;
            grpConfig.Controls.Add(frmSaveVideoConfig);
            frmSaveVideoConfig.BringToFront();
            frmSaveVideoConfig.Location = new Point(5, 20);
            frmSaveVideoConfig.Show();
        }

        private void btnPTZConfig_Click(object sender, EventArgs e)
        {
            grpConfig.Controls.Clear();
            PictureBox pbx = new PictureBox();
            pbx.Dock = DockStyle.Fill;
            pbx.BackgroundImageLayout = ImageLayout.Stretch;
            pbx.BackgroundImage = Properties.Resources.Function_Disable;
            grpConfig.Controls.Add(pbx);
            btnBackColor_Init(sender as Button);

        }
        #endregion

        private void btnMaintain_Click(object sender, EventArgs e)
        {
            grpConfig.Controls.Clear();
            btnBackColor_Init(sender as Button);
            FrmMaintain frmMaintain = new FrmMaintain();
            frmMaintain.TopLevel = false;
            grpConfig.Controls.Add(frmMaintain);
            frmMaintain.BringToFront();
            frmMaintain.Location = new Point(150, 20);
            frmMaintain.Show();
        }

        private void FrmConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }

        private void FrmConfig_Disposed(object sender, EventArgs e)
        {
            DMSDK.DM_Disconnect(StaticClass.tempConnect);
            DMSDK.DM_CloseMonitor(StaticClass.tempMonitor);
            DMSDK.DM_PlayerCleanup();
            StaticClass.Temper_Ip = "";
            StaticClass.Temper_CameraId = 0;
            StaticClass.Temper_CameraName = "";
            StaticClass.Temper_IsEnanle = false;
            StaticClass.tempConnect = -1;
            StaticClass.tempMonitor = -1;
        }
    }
}
