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
using System.IO;
using System.Configuration;
using System.Collections;
using System.Threading;
using System.Diagnostics;
using System.Media;

namespace InfraredAnalyze
{

    public partial class FrmMain : Form
    {
        SqlCreate sqlCreate = new SqlCreate();
        Chason_Log _Log = new Chason_Log();
        int ScreenNum = 1;

        StructClass.StructAlarm structAlarm;
        StructClass.StructAlarmconfig structAlarmconfig;
        StructClass.StructAlarmconfig[] structAlarmconfigs;

        StructClass.realTimeStructTemper realTimeStructTemper;//实时温度数据
        StructClass.realTimeTemper realTimeTemper;//某一台探测器的实时温度（最多8个区域）
        StructClass.realTimeTemper[] realTimeTempers;

        StructClass.AreaAlarmCount areaAlarmCount;
        StructClass.alarmStructCount alarmStructCount;
        StructClass.AreaAlarmCount[] areaAlarmCounts;

        StructClass.StructErrData errData;
        StructClass.StructFireData fireData;
        int fireCount = StaticClass.arrayList_FireData.Count;
        bool RunStaus = false;
        bool IsAlarmSoundOn = true;
        int ComnErr_TopLimit = 4;//通讯故障报出时间  4

        #region//构造函数
        public FrmMain()
        {
            ThreadPool.SetMaxThreads(16, 16);//搞个线程池  不好玩  
            InitializeComponent();
            timer1.Start();
            timer2.Start();
           
            try//为list赋初值
            {
                structAlarm = new StructClass.StructAlarm();
                structAlarmconfig = new StructClass.StructAlarmconfig();
               

                realTimeStructTemper = new StructClass.realTimeStructTemper();
                realTimeTemper = new StructClass.realTimeTemper();
                
                areaAlarmCount = new StructClass.AreaAlarmCount();
                alarmStructCount = new StructClass.alarmStructCount();
              

                for (int i = 0; i < 16; i++)
                {
                    //UCPbx uCPbx = new UCPbx();
                    //StaticClass.intPtrs_UCPbx[i] = uCPbx.Handle;//会返回null值
                    structAlarmconfigs = new StructClass.StructAlarmconfig[8];
                    structAlarm.structAlarmconfigs = structAlarmconfigs;
                    structAlarm.CameraId = i + 1;
                    StaticClass.intPtrs_AlarmConfig.Add(structAlarm);

                    realTimeTempers = new StructClass.realTimeTemper[8];
                    realTimeStructTemper.realTimeTemper = realTimeTempers;
                    realTimeStructTemper.CameraId = i + 1;
                    StaticClass.intPtrs_RealtimeTemper.Add(realTimeStructTemper);

                    areaAlarmCounts = new StructClass.AreaAlarmCount[8];
                    alarmStructCount.areaAlarmCounts = areaAlarmCounts;
                    alarmStructCount.CameraId = i + 1;
                    StaticClass.intPtrs_structCameraAlarmCounts.Add(alarmStructCount);
                }
                StaticClass.intPtrs_UCPbx[0] = ucPbx1.Handle;
                StaticClass.intPtrs_UCPbx[1] = ucPbx2.Handle;
                StaticClass.intPtrs_UCPbx[2] = ucPbx3.Handle;
                StaticClass.intPtrs_UCPbx[3] = ucPbx4.Handle;
                StaticClass.intPtrs_UCPbx[4] = ucPbx5.Handle;
                StaticClass.intPtrs_UCPbx[5] = ucPbx6.Handle;
                StaticClass.intPtrs_UCPbx[6] = ucPbx7.Handle;
                StaticClass.intPtrs_UCPbx[7] = ucPbx8.Handle;
                StaticClass.intPtrs_UCPbx[8] = ucPbx9.Handle;
                StaticClass.intPtrs_UCPbx[9] = ucPbx10.Handle;
                StaticClass.intPtrs_UCPbx[10] = ucPbx11.Handle;
                StaticClass.intPtrs_UCPbx[11] = ucPbx12.Handle;
                StaticClass.intPtrs_UCPbx[12] = ucPbx13.Handle;
                StaticClass.intPtrs_UCPbx[13] = ucPbx14.Handle;
                StaticClass.intPtrs_UCPbx[14] = ucPbx15.Handle;
                StaticClass.intPtrs_UCPbx[15] = ucPbx16.Handle;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region//窗体右上角按钮功能
        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
            this.Cursor = Cursors.Arrow;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        private void btnClose_Click(object sender, EventArgs e)//关闭窗体 退出程序
        {
            FrmPwd frmPwd = new FrmPwd();
            frmPwd.PwdLevel = 2;
            if (frmPwd.ShowDialog() == DialogResult.OK)
            {
                this.Close();
                Environment.Exit(0);
            }
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            DMSDK.DM_PlayerCleanup();
            iconInfrared.Dispose();
        }

        private void btnWindow_MouseEnter(object sender, EventArgs e)
        {
            btnWindow.BackColor = Color.Green;
            this.Cursor = Cursors.Arrow;
        }

        private void btnWindow_MouseLeave(object sender, EventArgs e)
        {
            btnWindow.BackColor = Color.Transparent;
        }

        private void btnMin_MouseEnter(object sender, EventArgs e)
        {
            btnMin.BackColor = Color.Yellow;
            this.Cursor = Cursors.Arrow;
        }

        private void btnMin_MouseLeave(object sender, EventArgs e)
        {
            btnMin.BackColor = Color.Transparent;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnWindow_Click(object sender, EventArgs e)//窗口全屏、恢复按钮 
        {
            if (this.WindowState==FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                btnWindow.BackgroundImage = Properties.Resources.窗口化;
                Refresh_Screen(ScreenNum);
            }
            else if (this.WindowState==FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                btnWindow.BackgroundImage = Properties.Resources.最大化;
            }
        }
        #endregion

        #region//双击头部 变换窗体模式  最大化或者为 窗体化
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
            Refresh_Screen(ScreenNum);
        }
        #endregion

        #region//按住头部 拖动窗体
        Point point;
        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }

        private void pnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point temp_Point = MousePosition;
                temp_Point.Offset(-point.X, -point.Y);
                this.Location = temp_Point;
            }
        }
        #endregion

        #region//拖动鼠标 改变窗体大小
        public enum MouseDirection
        {
            Herizontal,
            Vertical,
            Declining,
            None
        }

        MouseDirection mouseDirection = new MouseDirection();
        bool IsMouseDown = false;

        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
        }

        private void FrmMain_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
            mouseDirection = MouseDirection.None;
        }

        private void FrmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Location.X >= this.Width - 5 && e.Location.Y > this.Height - 5)
            {
                this.Cursor = Cursors.SizeNWSE;
                mouseDirection = MouseDirection.Declining;
            }
            else if (e.Location.X > this.Width - 5)
            {
                this.Cursor = Cursors.SizeWE;
                mouseDirection = MouseDirection.Herizontal;
            }
            else if (e.Location.Y > this.Height - 5)
            {
                this.Cursor = Cursors.SizeNS;
                mouseDirection = MouseDirection.Vertical;
            }
            else
            {
                this.Cursor = Cursors.Arrow;
            }
            ResizeWindow();
        }

        private void ResizeWindow()
        {
            if (!IsMouseDown)
            {
                return;
            }
            if (mouseDirection == MouseDirection.Declining)
            {
                this.Cursor = Cursors.SizeNWSE;
                this.Width = MousePosition.X - this.Left;
                this.Height = MousePosition.Y - this.Top;
            }
            if (mouseDirection == MouseDirection.Herizontal)
            {
                this.Cursor = Cursors.SizeWE;
                this.Width = MousePosition.X - this.Left;
            }
            if (mouseDirection == MouseDirection.Vertical)
            {
                this.Cursor = Cursors.SizeNS;
                this.Height = MousePosition.Y - this.Top;
            }
            else
            {
                this.Cursor = Cursors.Arrow;
            }
        }
        Rectangle rc;
        private void pnlBottom_Paint(object sender, PaintEventArgs e)//画一个手柄
        {
            //rc = new Rectangle(pnlBottom.Width - 16, pnlBottom.Height - 16, 16, 16);
            //ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
        }

        private void pnlBottom_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Location.X >= pnlBottom.Width - 16 && e.Location.Y > pnlBottom.Height - 16)
            {
                pnlBottom.Cursor = Cursors.SizeNWSE;
                mouseDirection = MouseDirection.Declining;
            }
            else
            {
                pnlBottom.Cursor = Cursors.Arrow;
            }
            ResizeWindow();
        }

        private void pnlBottom_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
        }

        private void pnlBottom_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
            mouseDirection = MouseDirection.None;
        }
        #endregion//拖动 鼠标 改变窗体大小

        #region//根据数量 分割主窗体 显示监控画面
        public void Refresh_Screen(int num)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);//保存配置文件中当前屏幕数量
            config.AppSettings.Settings["ScreenNum"].Value = num.ToString();
            ConfigurationManager.RefreshSection("appSettings");
            config.Save(ConfigurationSaveMode.Modified);

            tlpScreen.Controls.Clear();
            spcScreen.Panel1.Controls.Clear();
            spcScreen.Panel1.Controls.Add(tlpScreen);
            tlpScreen.Dock = DockStyle.Fill;
            switch (num)
            {
                case 1:
                    tlpScreen.ColumnCount = 1;
                    tlpScreen.RowCount = 1;
                    Add_UCPbx(num);
                    foreach (Control control in tlpScreen.Controls)
                    {
                        control.Height = tlpScreen.Height;
                        control.Width = tlpScreen.Width;
                    }
                    break;
                case 2:
                    tlpScreen.ColumnCount = 2;
                    tlpScreen.RowCount = 1;
                    Add_UCPbx(num);
                    foreach (Control control in tlpScreen.Controls)
                    {
                        control.Width = tlpScreen.Width / 2;
                        control.Height = tlpScreen.Height / 1;
                    }
                    break;
                case 4:
                    tlpScreen.ColumnCount = 2;
                    tlpScreen.RowCount = 2;
                    Add_UCPbx(num);
                    foreach (Control control in tlpScreen.Controls)
                    {
                        control.Width = tlpScreen.Width / 2;
                        control.Height = tlpScreen.Height / 2;
                    }
                    break;
                case 6:
                    tlpScreen.ColumnCount = 3;
                    tlpScreen.RowCount = 2;
                    Add_UCPbx(num);
                    foreach (Control control in tlpScreen.Controls)
                    {
                        control.Width = tlpScreen.Width / 3;
                        control.Height = tlpScreen.Height / 2;
                    }
                    break;
                case 9:
                    tlpScreen.ColumnCount = 3;
                    tlpScreen.RowCount = 3;
                    Add_UCPbx(num);
                    foreach (Control control in tlpScreen.Controls)
                    {
                        control.Width = tlpScreen.Width / 3;
                        control.Height = tlpScreen.Height / 3;
                    }
                    break;
                case 12:
                    tlpScreen.ColumnCount = 4;
                    tlpScreen.RowCount = 3;
                    Add_UCPbx(num);
                    foreach (Control control in tlpScreen.Controls)
                    {
                        control.Width = tlpScreen.Width / 4;
                        control.Height = tlpScreen.Height / 3;
                    }
                    break;
                case 16:
                    tlpScreen.ColumnCount = 4;
                    tlpScreen.RowCount = 4;
                    Add_UCPbx(num);
                    foreach (Control control in tlpScreen.Controls)
                    {
                        control.Width = tlpScreen.Width / 4;
                        control.Height = tlpScreen.Height / 4;
                    }
                    break;
            }
            ScreenNum = Convert.ToInt32(ConfigurationManager.AppSettings["ScreenNum"]);
        }

        public void Add_UCPbx(int num)//按数量在主屏幕中添加UCPbx
        {
         
            //UCPbx uCPbx = new UCPbx();
            //uCPbx = (UCPbx)FromHandle(StaticClass.intPtrs_UCPbx[i]);
            //uCPbx.Id = i + 1;
            //uCPbx.DoubleClick += new EventHandler(UCPbx_DoubleClick);//注册完事件  
            //uCPbx.KeyDown += new KeyEventHandler(UCPbx_KeyDown);//这个也是同样的道理
            //uCPbx.ContextMenuStrip = cmsShowNum;
            tlpScreen.Controls.Add(ucPbx1);
            if (num == 1)
                return;
            tlpScreen.Controls.Add(ucPbx2);
            if (num == 2)
                return;
            tlpScreen.Controls.Add(ucPbx3);
            tlpScreen.Controls.Add(ucPbx4);
            if (num == 4)
                return;
            tlpScreen.Controls.Add(ucPbx5);
            tlpScreen.Controls.Add(ucPbx6);
            if (num == 6)
                return;
            tlpScreen.Controls.Add(ucPbx7);
            tlpScreen.Controls.Add(ucPbx8);
            tlpScreen.Controls.Add(ucPbx9);
            if (num == 9)
                return;
            tlpScreen.Controls.Add(ucPbx10);
            tlpScreen.Controls.Add(ucPbx11);
            tlpScreen.Controls.Add(ucPbx12);
            if (num == 12)
                return;
            tlpScreen.Controls.Add(ucPbx13);
            tlpScreen.Controls.Add(ucPbx14);
            tlpScreen.Controls.Add(ucPbx15);
            tlpScreen.Controls.Add(ucPbx16);
        }
        #endregion

        #region//消除控件加载时的闪烁问题  但是会卡住呢
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;
        //        return cp;
        //    }
        //}
        #endregion

        #region//选中的画面全屏显示
        bool IsSingleScreenFull = false;
        private void UCPbx_DoubleClick(object sender)
        {
            if (!IsSingleScreenFull)
            {
                tlpScreen.Controls.Clear();
                spcScreen.Panel1.Controls.Clear();
                UCPbx uCPbx = (UCPbx)sender;
                TableLayoutPanel layoutPanel = new TableLayoutPanel();
                spcScreen.Panel1.Controls.Add(layoutPanel);
                layoutPanel.Dock = DockStyle.Fill;
                layoutPanel.RowCount = 1;
                layoutPanel.ColumnCount = 1;
                uCPbx.Dock = DockStyle.Fill;

                layoutPanel.Controls.Add(uCPbx);  //双击事件会被执行很多次  事件未销毁
                IsSingleScreenFull = true;
                return;
            }
            else
            {
                Refresh_Screen(ScreenNum);
                IsSingleScreenFull = false;
                return;
            }
        }
      
        private void UCPbx_KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Exit_FullScreen();
            }
        }

        #region//每个控件的双击与 按键事件
        private void ucPbx1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            UCPbx_DoubleClick(sender);
        }

        private void ucPbx1_KeyDown(object sender, KeyEventArgs e)
        {
            UCPbx_KeyDown(e);
        }
        
        #endregion

        #endregion

        #region//清除事件绑定
        //public void Clear_Spc_PbxEvents(TableLayoutPanel tlpScreen)
        //{
        //    try
        //    {
        //        foreach (Control control in tlpScreen.Controls)
        //        {
        //            if (control != null)
        //            {
        //                control.DoubleClick -= new EventHandler(UCPbx_DoubleClick);
        //                control.KeyDown -= new KeyEventHandler(UCPbx_KeyDown);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message + ex.StackTrace);
        //    }

        //}
        #endregion

        #region//计算分割的点
        Point[] points;
        /// <summary>
        /// 返回分割好的坐标点数组
        /// </summary>
        /// <param name="num">输入需要分割显示的数量</param>
        /// <returns></returns>
        public Point[] Calc_points(int num)
        {
            switch (num)
            {
                case 1:
                    points = new Point[1];
                    points[0] = new Point(0, 0);
                    break;
                case 4:
                    points = new Point[4];
                    points[0] = new Point(0, 0);
                    points[1] = new Point(0, spcScreen.Panel1.Width / 2);
                    points[2] = new Point(spcScreen.Panel1.Height / 2, 0);
                    points[3] = new Point(spcScreen.Panel1.Width / 2, spcScreen.Panel1.Height / 2);
                    break;
                case 9:
                    points = new Point[9];
                    points[0] = new Point(0, 0);
                    points[1] = new Point(0, spcScreen.Panel1.Width / 3);
                    points[2] = new Point(0, spcScreen.Panel1.Width / 3 * 2);

                    points[3] = new Point(spcScreen.Panel1.Height / 3, 0);
                    points[4] = new Point(spcScreen.Panel1.Height / 3, spcScreen.Panel1.Width / 3);
                    points[5] = new Point(spcScreen.Panel1.Height / 3, spcScreen.Panel1.Width / 3 * 2);

                    points[6] = new Point(spcScreen.Panel1.Height / 3 * 2, 0);
                    points[7] = new Point(spcScreen.Panel1.Height / 3 * 2, spcScreen.Panel1.Width / 3);
                    points[8] = new Point(spcScreen.Panel1.Height / 3 * 2, spcScreen.Panel1.Width / 3 * 2);
                    break;
                case 16:
                    points = new Point[16];
                    points[0] = new Point(0, 0);
                    points[1] = new Point(0, spcScreen.Panel1.Width / 4);
                    points[2] = new Point(0, spcScreen.Panel1.Width / 4 * 2);
                    points[3] = new Point(0, spcScreen.Panel1.Width / 4 * 3);

                    points[4] = new Point(spcScreen.Panel1.Height / 4, 0);
                    points[5] = new Point(spcScreen.Panel1.Height / 4, spcScreen.Panel1.Width / 4);
                    points[6] = new Point(spcScreen.Panel1.Height / 4, spcScreen.Panel1.Width / 4 * 2);
                    points[7] = new Point(spcScreen.Panel1.Height / 4, spcScreen.Panel1.Width / 4 * 3);

                    points[8] = new Point(spcScreen.Panel1.Height / 2, 0);
                    points[9] = new Point(spcScreen.Panel1.Height / 2, spcScreen.Panel1.Width / 4);
                    points[10] = new Point(spcScreen.Panel1.Height / 2, spcScreen.Panel1.Width / 4 * 2);
                    points[11] = new Point(spcScreen.Panel1.Height / 2, spcScreen.Panel1.Width / 4 * 3);

                    points[12] = new Point(spcScreen.Panel1.Height / 4 * 3, 0);
                    points[13] = new Point(spcScreen.Panel1.Height / 4 * 3, spcScreen.Panel1.Width / 4);
                    points[14] = new Point(spcScreen.Panel1.Height / 4 * 3, spcScreen.Panel1.Width / 4 * 2);
                    points[15] = new Point(spcScreen.Panel1.Height / 4 * 3, spcScreen.Panel1.Width / 4 * 3);
                    break;
            }
            return points;
        }

        #endregion

        #region//从数据库中加载探测器类表并转换为树视图
        public void LoadTreeView()
        {
            List<StructClass.StructIAnalyzeConfig> temp_list = new List<StructClass.StructIAnalyzeConfig>();
            temp_list = sqlCreate.Select_All_SMInfrared_ProjConfig(StaticClass.DataBaseName);
            try
            {
                tvwSensor.Nodes.Clear();
                tvwData.Nodes.Clear();
                if(temp_list.Count>0)
                {
                    foreach(StructClass.StructIAnalyzeConfig structIAnalyzeConfig in temp_list)
                    {
                        TreeNode camera_Node = new TreeNode();
                        TreeNode data_Node = new TreeNode();
                        StructClass.StructSM7003Tag structSM7003Tag = new StructClass.StructSM7003Tag();

                        camera_Node.Text = structIAnalyzeConfig.CameraName;
                        data_Node.Text = structIAnalyzeConfig.CameraName;

                        structSM7003Tag.CameraID = structIAnalyzeConfig.CameraID;
                        structSM7003Tag.IP = structIAnalyzeConfig.IP;
                        structSM7003Tag.Port = structIAnalyzeConfig.Port;
                        structSM7003Tag.NodeID = structIAnalyzeConfig.NodeID;
                        structSM7003Tag.Reamrks = structIAnalyzeConfig.Reamrks;
                        structSM7003Tag.Enable = structIAnalyzeConfig.Enable;

                        camera_Node.Tag = structSM7003Tag;
                        data_Node.Tag = structSM7003Tag;
                        camera_Node.ContextMenuStrip = cmsSensor;
                        data_Node.ContextMenuStrip = cmsData;

                        if (structSM7003Tag.Enable==false)
                        {
                            camera_Node.ForeColor = Color.Gray;
                            camera_Node.NodeFont = new Font("微软雅黑", 10, FontStyle.Strikeout);
                        }
                        tvwSensor.Nodes.Add(camera_Node);
                        tvwData.Nodes.Add(data_Node);//先加载到数据栏
                    }
                    for(int i = 0; i < 16; i++)
                    {
                        StructClass.StructIAnalyzeConfig structIAnalyzeConfig = temp_list[i];
                        if (!structIAnalyzeConfig.Enable)
                        {
                            StaticClass.intPtrs_Status[i] = (int)RunningStatus.未启用;
                            UCPbx uCPbx = (UCPbx)FromHandle(StaticClass.intPtrs_UCPbx[i]);
                            uCPbx.BackgroundImage = Properties.Resources.NotEnabled;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("树视图列表失败！" + ex.Message);
            }
        }
        #endregion

        #region//探测器树视图点击事件
        StructClass.StructSM7003Tag structSM7003Tag;
       
        Point tvwPoint;
        private void tvwSensor_MouseDown(object sender, MouseEventArgs e)
        {
            tvwPoint = new Point(e.X, e.Y);
            structSM7003Tag = new StructClass.StructSM7003Tag();
            try
            {
                if(e.Button==MouseButtons.Left)
                {
                    tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
                    if(tvwSensor.SelectedNode==null)
                    {
                        tvwSensor.SelectedNode = null;//取消选中的树视图节点
                        StaticClass.SelectedNode = 0;
                    }
                    else
                    {
                        structSM7003Tag = (StructClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
                        StaticClass.SelectedNode = structSM7003Tag.CameraID;
                    }
                }else if(e.Button==MouseButtons.Right)//废弃喽 
                {
                    tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
                    if (tvwSensor.SelectedNode != null)
                    {
                        structSM7003Tag = (StructClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
                        StaticClass.SelectedNode = structSM7003Tag.CameraID;
                        if (tvwSensor.SelectedNode.Index == 0)
                        {
                            cmsIPCameraConfig.Items[4].Visible = false;
                        }
                        else if (tvwSensor.SelectedNode.Index > 0 && tvwSensor.SelectedNode.Index < tvwSensor.Nodes.Count-1)
                        {
                            cmsIPCameraConfig.Items[4].Visible = true;
                            cmsIPCameraConfig.Items[5].Visible = true;
                        }
                        else if (tvwSensor.SelectedNode.Index == tvwSensor.Nodes.Count-1)
                        {
                            cmsIPCameraConfig.Items[5].Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("探测器列表异常！" + ex.Message);
            }
        }

        private void tvwData_MouseDown(object sender, MouseEventArgs e)
        {
            tvwPoint = new Point(e.X, e.Y);
        }

        #endregion

        #region//探测器树视图双击事件
        private void tvwSensor_DoubleClick(object sender, EventArgs e)
        {
            //TreeNode treeNode = tvwSensor.GetNodeAt(tvwPoint);
            //FrmCameraNetConfig frmCameraConfig = new FrmCameraNetConfig();
            //if (treeNode != null)
            //{
            //    StructClass.StructSM7003Tag sM7003Tag = (StructClass.StructSM7003Tag)treeNode.Tag;//将所双击的树视图的节点的 cameraID赋值给弹出窗体的ID属性
            //    frmCameraConfig.IPCameraID = sM7003Tag.CameraID;
            //    frmCameraConfig.ShowDialog();
            //}
        }
        #endregion

        #region//探测器列表操作(上移、下移、删除)
        #region//删除探测器节点
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StructClass.StructSM7003Tag structSM7003Tag = new StructClass.StructSM7003Tag();
                tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
                if (tvwSensor.SelectedNode != null)
                {
                    structSM7003Tag = (StructClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
                    int CameraId = structSM7003Tag.CameraID;
                    int NodeId = structSM7003Tag.NodeID;
                    sqlCreate.Delete_Node_SMInfraredConfig(CameraId, NodeId, StaticClass.DataBaseName);//修改大于选项的CameraId
                    LoadTreeView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败：" + ex.Message);
            }
        }
        #endregion

        #region//探测器节点上移
        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
            //    StaticClass.StructSM7003Tag structSM7003Tag = new StaticClass.StructSM7003Tag();
            //    if (tvwSensor.SelectedNode != null)
            //    {
            //        structSM7003Tag = (StaticClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
            //        int CameraId = structSM7003Tag.CameraID;
            //        int NodeId = structSM7003Tag.NodeID;
            //        sqlCreate.Move_Node_Up(NodeId);
            //        LoadTreeView();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("上移失败：" + ex.Message);
            //}
        }
        #endregion

        #region//探测器下移
        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
            //    StaticClass.StructSM7003Tag structSM7003Tag = new StaticClass.StructSM7003Tag();
            //    if (tvwSensor.SelectedNode != null)
            //    {
            //        structSM7003Tag = (StaticClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
            //        int CameraId = structSM7003Tag.CameraID;
            //        int NodeId = structSM7003Tag.NodeID;
            //        sqlCreate.Move_Node_Down(NodeId);
            //        LoadTreeView();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("下移失败：" + ex.Message);
            //}
        }
        #endregion
        #endregion

        #region//设置
        private void 设置ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
            structSM7003Tag = new StructClass.StructSM7003Tag();
            if (tvwSensor.SelectedNode != null)
            {
                structSM7003Tag = (StructClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
                FrmConfig frmConfig = new FrmConfig();
                StaticClass.Temper_CameraId= structSM7003Tag.CameraID;
                StaticClass.Temper_Ip = structSM7003Tag.IP;
                StaticClass.Temper_CameraName = tvwSensor.SelectedNode.Text;
                StaticClass.Temper_IsEnanle = structSM7003Tag.Enable;
                if(frmConfig.ShowDialog() == DialogResult.OK)
                {
                    LoadTreeView();
                }
            }
        }
        #endregion

        #region//系统参数设置
        private void 测温参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
              
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region//图像设置
        private void 图像设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmImgConfig frmImageConfig = new FrmImgConfig();
            structSM7003Tag = new StructClass.StructSM7003Tag();
            tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
            if (tvwSensor.SelectedNode != null)
            {
                structSM7003Tag = (StructClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
                frmImageConfig.CameraId = structSM7003Tag.CameraID;
                frmImageConfig.Ip = structSM7003Tag.IP;
                frmImageConfig.ShowDialog();
            }

        }
        #endregion

        #region//连接  鸡肋
        private void 连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            structSM7003Tag = new StructClass.StructSM7003Tag();
            tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
            try
            {
                if (tvwSensor.SelectedNode != null)
                {
                    structSM7003Tag = (StructClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
                    //int InitValue = DMSDK.DM_PlayerInit(StaticClass.intPtrs_UCPbx[structSM7003Tag.CameraID - 1]);
                    StaticClass.intPtrs_Connect[structSM7003Tag.CameraID - 1] = DMSDK.DM_OpenMonitor(StaticClass.intPtrs_UCPbx[structSM7003Tag.CameraID - 1], structSM7003Tag.IP, 5000, 0);
                    if (StaticClass.intPtrs_Connect[structSM7003Tag.CameraID - 1] < 0)
                    {
                        MessageBox.Show("探测器连接失败");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region//视频参数设置
        private void 视频设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmSystemConfig frmVideoConfig = new FrmSystemConfig();
            //structSM7003Tag = new StructClass.StructSM7003Tag();
            //tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
            //if(tvwSensor.SelectedNode!=null)
            //{
            //    structSM7003Tag = (StructClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
            //    frmVideoConfig.IPCameraId = structSM7003Tag.CameraID;
            //    frmVideoConfig.IPAddress = structSM7003Tag.IP;
            //    if(frmVideoConfig.ShowDialog()==DialogResult.OK)
            //    {
            //        LoadTreeView();
            //    }
            //}
        }
        #endregion

        #region//断开连接
        private void 断开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            structSM7003Tag = new StructClass.StructSM7003Tag();
            tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
            if (tvwSensor.SelectedNode != null)
            {
                structSM7003Tag = (StructClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
                DMSDK.DM_CloseMonitor(StaticClass.intPtrs_Connect[structSM7003Tag.CameraID - 1]);
            }
        }
        #endregion

        #region//显示画面数量切换
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Refresh_Screen(9);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Refresh_Screen(4);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Refresh_Screen(1);
        }

        private void cmsShowNum_4_Click(object sender, EventArgs e)
        {
            Refresh_Screen(16);
        }

        private void 单画面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Refresh_Screen(1);
        }

        private void 四画面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Refresh_Screen(4);
        }

        private void 九画面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Refresh_Screen(9);
        }

        private void 十六画面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Refresh_Screen(16);
        }

        #region//显示设置
        private void toolStripMenuItem13_Click(object sender, EventArgs e)//1
        {
            Refresh_Screen(1);
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)//2
        {
            Refresh_Screen(2);
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)//4
        {
            Refresh_Screen(4);
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)//6
        {
            Refresh_Screen(6);
        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)//9
        {
            Refresh_Screen(9);
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)//12
        {
            Refresh_Screen(12);
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)//16
        {
            Refresh_Screen(16);
        }
        #endregion
        #endregion

        #region//历史数据
        private void 历史数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHistoricalTemperData frmHistoricalTemperData;
            StructClass.StructSM7003Tag structSM7003Tag = new StructClass.StructSM7003Tag();
            tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
            if (tvwSensor.SelectedNode != null)
            {
                structSM7003Tag = (StructClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
                frmHistoricalTemperData = new FrmHistoricalTemperData(structSM7003Tag.CameraID);
                frmHistoricalTemperData.ShowDialog();
            }
        }
        #endregion

        #region //实时数据 （曲线）
        private void 实时数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRealTimeTemperData frmRealTimeTemperData = new FrmRealTimeTemperData();
            structSM7003Tag = new StructClass.StructSM7003Tag();
            tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
            if (tvwSensor.SelectedNode != null)
            {
                structSM7003Tag = (StructClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
                frmRealTimeTemperData.CameraID = structSM7003Tag.CameraID;
                frmRealTimeTemperData.ShowDialog();
            }
        }
        #endregion

        #region//菜单栏 选项文件
        private void toolStripMenuItem9_Click(object sender, EventArgs e)//温度数据  根据选中的树视图来确定
        {
            try
            {
                if (StaticClass.SelectedNode == 0)
                {
                    MessageBox.Show("请先选择需要查看的探测器!");
                    return;
                }
                FrmHistoricalTemperData frmHistoricalTemperData;
                StructClass.StructSM7003Tag structSM7003Tag = new StructClass.StructSM7003Tag();
                tvwData.SelectedNode = tvwData.GetNodeAt(tvwPoint);
                if (tvwData.SelectedNode != null)
                {
                    structSM7003Tag = (StructClass.StructSM7003Tag)tvwData.SelectedNode.Tag;
                    frmHistoricalTemperData = new FrmHistoricalTemperData(StaticClass.SelectedNode);
                    frmHistoricalTemperData.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)//报警数据
        {
            try
            {
                FrmHisRecords frmHisRecords = new FrmHisRecords(0);
                frmHisRecords.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)//图片数据
        {
            try
            {
                if (StaticClass.SelectedNode == 0)
                {
                    MessageBox.Show("请先选择需要设置的探测器!");
                    return;
                }
                StructClass.StructSM7003Tag structSM7003Tag = new StructClass.StructSM7003Tag();
                tvwData.SelectedNode = tvwData.GetNodeAt(tvwPoint);
                if (tvwData.SelectedNode != null)
                {
                    structSM7003Tag = (StructClass.StructSM7003Tag)tvwData.SelectedNode.Tag;
                    if (!Directory.Exists(ConfigurationManager.AppSettings["ImageSavePath"] + "SMCameraPic" + "\\" + StaticClass.ProjName + "\\" + "Camera" + structSM7003Tag.CameraID.ToString()))
                    {
                        Directory.CreateDirectory(ConfigurationManager.AppSettings["ImageSavePath"] + "SMCameraPic" + "\\" + StaticClass.ProjName + "\\" + "Camera" + structSM7003Tag.CameraID.ToString());
                    }
                    Process.Start(ConfigurationManager.AppSettings["ImageSavePath"] + "SMCameraPic" + "\\" + StaticClass.ProjName + "\\" + "Camera" + structSM7003Tag.CameraID.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region//配置IP
        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        {
            if (RunStaus)
            {
                MessageBox.Show("系统运行中！");
                return;
            }
            FrmIpConfig frmIpConfig = new FrmIpConfig();
            frmIpConfig.ShowDialog();
        }
        #endregion

        #region//清空数据库
        private void 清空数据库ToolStripMenuItem_Click(object sender, EventArgs e)//谨慎操作（仅当数据库异常时供操作）
        {
            if (MessageBox.Show("确定要清空数据库吗？这将删除所有数据，并且不可恢复，请谨慎操作！", "不可恢复的操作", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                FrmPwd frmPwd = new FrmPwd();
                frmPwd.PwdLevel = 1;
                if (frmPwd.ShowDialog() == DialogResult.OK)
                {
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.WorkerSupportsCancellation = true;
                    frmIsRunning = new FrmIsRunning(worker);
                    worker.DoWork += new DoWorkEventHandler(DropAllDataBase);
                    worker.RunWorkerAsync();
                    frmIsRunning.ShowDialog();
                    if(MessageBox.Show("请重新启动软件来完成初始化数据库！","请重启软件！", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        DMSDK.DM_PlayerCleanup();
                        Environment.Exit(0);
                    }
                }
            }
        }

        private void DropAllDataBase(object sender,DoWorkEventArgs e)
        {
            sqlCreate.Drop_AllDatabase(StaticClass.DataBaseName);
        }

        #endregion

        #region//软件版本
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            FrmVersion frmVersion = new FrmVersion();
            frmVersion.ShowDialog();
        }
        #endregion

        #region//显示时间 火警数量 故障数量
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                lblQueCount.Text = StaticClass.QueueLength.ToString();
                int firecount = 0;
                int errcount = 0;
                if (StaticClass.FireCount > 0)
                {
                    pbxFireCount.BackgroundImage = Properties.Resources.红灯光;
                }
                else
                {
                    pbxFireCount.BackgroundImage = Properties.Resources.灯光;
                }
                if (StaticClass.ErrCount > 0)
                {
                    pbxErrCount.BackgroundImage = Properties.Resources.黄灯光;
                }
                else
                {
                    pbxErrCount.BackgroundImage = Properties.Resources.灯光;
                }
               
                for (int i = 0; i < 16; i++)
                {
                    if (StaticClass.IsCameraFireAlarm[i])
                    {
                        firecount++;
                    }
                    if (StaticClass.intPtrs_Status[i] == (int)RunningStatus.先火警再故障)//两个都算
                    {
                        errcount++;
                    }
                    if (StaticClass.intPtrs_Status[i] == (int)RunningStatus.火警)
                    {

                    }
                    else if (StaticClass.intPtrs_Status[i] == (int)RunningStatus.故障)
                    {
                        errcount++;
                    }
                    else if (StaticClass.intPtrs_Status[i] == (int)RunningStatus.正常)
                    {
                       
                    }
                    StaticClass.Is_CallBack[i] = false;//万一不插入数据
                }
                if (RunStaus)
                {
                    lblRunningStatus.Text = "运行中";
                    pbxRunningStatus.BackgroundImage = Properties.Resources.green;
                }else
                {
                    lblRunningStatus.Text = "未运行";
                    pbxRunningStatus.BackgroundImage = Properties.Resources.Gray;
                }
                StaticClass.FireCount = firecount;
                StaticClass.ErrCount = errcount;
                lblTime.Text = DateTime.Now.ToString("F");
                lblFireCount.Text = "火警数量：" + firecount.ToString();
                lblErrCount.Text = "故障数量：" + errcount.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "界面信息更新异常！");
            }
        }
        #endregion

        #region//复位
        private void btnReset_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnReset, "软件复位");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            FrmPwd frmPwd = new FrmPwd();
            frmPwd.PwdLevel = 2;
            if (frmPwd.ShowDialog() == DialogResult.OK)
            {
                StaticClass.FireCount = 0;
                StaticClass.ErrCount = 0;
                for (int i = 0; i < 16; i++)
                {
                    StaticClass.Offline_Count[i] = 0;//故障清零
                    if (StaticClass.intPtrs_Enable[i])//启用了
                    {
                        if (RunStaus)
                        {
                            StaticClass.intPtrs_Status[i] = (int)RunningStatus.检测中;
                        }
                        else
                        {
                            StaticClass.intPtrs_Status[i] = (int)RunningStatus.停止;
                        }
                    }
                    StaticClass.IsCameraFireAlarm[i] = false;//火警清否
                    for(int j = 0; j < 8; j++)
                    {
                        StaticClass.intPtrs_structCameraAlarmCounts[i].areaAlarmCounts[j].AreaCount = 0;
                    }
                }
                StaticClass.arrayList_FireData.Clear();
                StaticClass.arrayList_ErrData.Clear();
                temp_listErr.Clear();
                temp_listFire.Clear();

                dgvError.Rows.Clear();
                dgvWarning.Rows.Clear();

                IsErrSoundOn = false;
                IsFireSoundOn = false;
                IsAlarmSoundOn = true;
                btnAlarmSound.BackgroundImage = Properties.Resources.Sound_on;
            }
        }
        #endregion

        #region//密码管理
        private void btnPwd_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnPwd, "密码管理");
        }

        private void btnPwd_Click(object sender, EventArgs e)
        {
            FrmPwd frmPwd = new FrmPwd();
            frmPwd.PwdLevel = 1;
            if (frmPwd.ShowDialog() == DialogResult.OK)
            {
                FrmPwdManage frmPwdManage = new FrmPwdManage();
                frmPwdManage.ShowDialog();
            }
        }
        #endregion

        #region//全屏显示

        bool IsScreenFull = false;

        private void Full_Screen()
        {
            if (this.WindowState != FormWindowState.Maximized)
            {
                btnWindow.PerformClick();
            }
            spcScreen.Panel2Collapsed = true;
            spcMain.Panel1Collapsed = true;
            spcFather.Panel1Collapsed = true;
            Refresh_Screen(ScreenNum);
            IsScreenFull = true;
        }

        private void Exit_FullScreen()
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                spcScreen.Panel2Collapsed = false;
                spcMain.Panel1Collapsed = false;
                spcFather.Panel1Collapsed = false;
            }));
            IsScreenFull = false;
        }

        private void btnFullScreen_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnFullScreen, "全屏显示");
        }

        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            Full_Screen();
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            Full_Screen();
        }

        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            Exit_FullScreen();
        }

        private void ucPbx1_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsScreenFull)
            {
                if (e.Button == MouseButtons.Right)
                {
                    cmsShowNum.Items[4].Visible = true;
                }
            }
            else
            {
                cmsShowNum.Items[4].Visible = false;
            }
        }

        #endregion

        #region//声音启用/禁用
        private void btnAlarmSound_MouseEnter(object sender, EventArgs e)
        {
            if (IsAlarmSoundOn)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.SetToolTip(btnAlarmSound, "启用声音");
            }
            else
            {
                ToolTip toolTip = new ToolTip();
                toolTip.SetToolTip(btnAlarmSound, "禁用声音");
            }
        }
        private void btnAlarmSound_Click(object sender, EventArgs e)
        {
            IsAlarmSoundOn = !IsAlarmSoundOn;
            if (IsAlarmSoundOn)
            {
                btnAlarmSound.BackgroundImage = Properties.Resources.Sound_on;
            }
            else
            {
                btnAlarmSound.BackgroundImage = Properties.Resources.Sound_off;
            }
        }
        #endregion

        #region//历史记录（包括火警  故障）
        private void btnHistoryRecord_Click(object sender, EventArgs e)//全部的历史告警记录
        {
            FrmHisRecords frmHisRecords = new FrmHisRecords(0);
            frmHisRecords.Show();
        }

        private void btnHistoryRecord_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnHistoryRecord, "历史记录");
        }
        #endregion

        #region//探测器设置
        private void btnCameraConfig_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnCameraConfig, "探测器设置");
        }

        private void btnCameraConfig_Click(object sender, EventArgs e)
        {
            if (RunStaus)
            {
                MessageBox.Show("系统运行中！");
                return;
            }
            FrmPwd frmPwd = new FrmPwd();
            frmPwd.PwdLevel = 2;
            if (frmPwd.ShowDialog() == DialogResult.OK)
            {
                FrmConfig frmConfig = new FrmConfig();
                List<StructClass.StructIAnalyzeConfig> list = sqlCreate.Select_SMInfrared_ProjConfig(StaticClass.DataBaseName);
                FrmSelectCamera frmSelectCamera = new FrmSelectCamera(list,StaticClass.SelectedNode);
                if (frmSelectCamera.ShowDialog() == DialogResult.OK)
                {
                    if (frmConfig.ShowDialog() == DialogResult.OK)
                    {
                        LoadTreeView();
                        //Application.Exit();
                        //System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                      
                    }
                }
            }
        }
        #endregion

        #region//新增树视图快捷菜单的右击事件
        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)//单个探测器连接事件
        {
            if (RunStaus)
            {
                MessageBox.Show("系统运行中！");
                return;
            }
            structSM7003Tag = new StructClass.StructSM7003Tag();
            tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
            try
            {
                if (tvwSensor.SelectedNode != null)
                {
                    structSM7003Tag = (StructClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
                    if (structSM7003Tag.Enable == false)
                    {
                        MessageBox.Show("探测器未启用");
                        return;
                    }
                    StaticClass.intPtrs_Connect[structSM7003Tag.CameraID - 1] = DMSDK.DM_OpenMonitor(StaticClass.intPtrs_UCPbx[structSM7003Tag.CameraID - 1], structSM7003Tag.IP, 5000, 0);
                    if (StaticClass.intPtrs_Connect[structSM7003Tag.CameraID - 1] < 0)
                    {
                        MessageBox.Show("探测器连接失败");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)//单个探测器断开连接
        {
            if (RunStaus)
            {
                MessageBox.Show("系统运行中！");
                return;
            }
            structSM7003Tag = new StructClass.StructSM7003Tag();
            tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
            if (tvwSensor.SelectedNode != null)
            {
                structSM7003Tag = (StructClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
                if (structSM7003Tag.Enable == false)
                {
                    MessageBox.Show("探测器未启用");
                    return;
                }
                DMSDK.DM_CloseMonitor(StaticClass.intPtrs_Connect[structSM7003Tag.CameraID - 1]);
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)//探测器设置
        {
            if (RunStaus)
            {
                MessageBox.Show("系统运行中！");
                return;
            }
            tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
            structSM7003Tag = new StructClass.StructSM7003Tag();
            if (tvwSensor.SelectedNode != null)
            {
                structSM7003Tag = (StructClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
                FrmConfig frmConfig = new FrmConfig();
                StaticClass.Temper_CameraId = structSM7003Tag.CameraID;
                StaticClass.Temper_Ip = structSM7003Tag.IP;
                StaticClass.Temper_CameraName = tvwSensor.SelectedNode.Text;
                StaticClass.Temper_IsEnanle = structSM7003Tag.Enable;
                if (frmConfig.ShowDialog() == DialogResult.OK)
                {
                    LoadTreeView();
                }
            }
        }

        TreeNode Temp_tvwNode_HisData;
        private void toolStripMenuItem4_Click_1(object sender, EventArgs e)//历史温度数据
        {
            try
            {
                tvwData.SelectedNode = tvwData.GetNodeAt(tvwPoint);
                if (tvwData.SelectedNode != null)
                {
                    Temp_tvwNode_HisData = tvwData.SelectedNode;
                    HisData_Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("历史数据加载失败：" + ex.Message + ex.StackTrace);
            }
        }

        private void HisData_Show()
        {
            try
            {
                FrmHistoricalTemperData frmHistoricalTemperData;
                StructClass.StructSM7003Tag structSM7003Tag = new StructClass.StructSM7003Tag();
                structSM7003Tag = (StructClass.StructSM7003Tag)Temp_tvwNode_HisData.Tag;
                frmHistoricalTemperData = new FrmHistoricalTemperData(structSM7003Tag.CameraID);
                frmHistoricalTemperData.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("跨线程：" + ex.Message + ex.StackTrace);
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)//实时数据
        {
            if (!RunStaus)
            {
                MessageBox.Show("系统未运行！");
                return;
            }
            FrmRealTimeTemperData frmRealTimeTemperData = new FrmRealTimeTemperData();
            structSM7003Tag = new StructClass.StructSM7003Tag();
            tvwData.SelectedNode = tvwData.GetNodeAt(tvwPoint);
            if (tvwData.SelectedNode != null)
            {
                structSM7003Tag = (StructClass.StructSM7003Tag)tvwData.SelectedNode.Tag;
                frmRealTimeTemperData.CameraID = structSM7003Tag.CameraID;
                frmRealTimeTemperData.ShowDialog();
            }

        }

        private void toolStripMenuItem23_Click(object sender, EventArgs e)//历史温度曲线
        {
            try
            {
                tvwData.SelectedNode = tvwData.GetNodeAt(tvwPoint);
                if (tvwData.SelectedNode != null)
                {
                    Temp_tvwNode_HisData = tvwData.SelectedNode;
                    FrmHistoricalTemperLines frmHistoricalTemperLines;
                    StructClass.StructSM7003Tag structSM7003Tag = new StructClass.StructSM7003Tag();
                    structSM7003Tag = (StructClass.StructSM7003Tag)Temp_tvwNode_HisData.Tag;
                    frmHistoricalTemperLines = new FrmHistoricalTemperLines(structSM7003Tag.CameraID);
                    frmHistoricalTemperLines.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("历史曲线加载失败：" + ex.Message + ex.StackTrace);
            }
        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {
            if (!RunStaus)
            {
                MessageBox.Show("系统未运行");
                return;
            }
            FrmRealTimeData frmRealTimeData = new FrmRealTimeData();
            frmRealTimeData.Show();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)//告警数据
        {
            FrmHisRecords frmHisRecords;
            structSM7003Tag = new StructClass.StructSM7003Tag();
            tvwData.SelectedNode = tvwData.GetNodeAt(tvwPoint);
            if (tvwData.SelectedNode != null)
            {
                structSM7003Tag = (StructClass.StructSM7003Tag)tvwData.SelectedNode.Tag;
                frmHisRecords = new FrmHisRecords(structSM7003Tag.CameraID);
                frmHisRecords.Show();
            }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)//图像数据
        {
            try
            {
                StructClass.StructSM7003Tag structSM7003Tag = new StructClass.StructSM7003Tag();
                tvwData.SelectedNode = tvwData.GetNodeAt(tvwPoint);
                if (tvwData.SelectedNode != null)
                {
                    structSM7003Tag = (StructClass.StructSM7003Tag)tvwData.SelectedNode.Tag;
                    if (!Directory.Exists(ConfigurationManager.AppSettings["ImageSavePath"] + "SMCameraPic" + "\\" + StaticClass.ProjName + "\\" + "Camera" + structSM7003Tag.CameraID.ToString()))
                    {
                        Directory.CreateDirectory(ConfigurationManager.AppSettings["ImageSavePath"] + "SMCameraPic" + "\\" + StaticClass.ProjName + "\\" + "Camera" + structSM7003Tag.CameraID.ToString());
                    }
                    Process.Start(ConfigurationManager.AppSettings["ImageSavePath"] + "SMCameraPic" + "\\" + StaticClass.ProjName + "\\" + "Camera" + structSM7003Tag.CameraID.ToString());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region//主窗体加载事件
        private void FrmMain_Load(object sender, EventArgs e)
        {
            lblLogo.Text = lblLogo.Text + "-" + StaticClass.ProjName;
            LoadTreeView();
            ScreenNum = Convert.ToInt32(ConfigurationManager.AppSettings["ScreenNum"]);
            Refresh_Screen(ScreenNum);
        }
        #endregion

        #region//初始化事件
        Thread thread;//提示请勿操作线程
        Thread threadStatus;//检测状态数组线程
        Thread threadCheckOnline;//掉线检测线程
        Thread threadCheckAlarm;//温度判断线程 用于报警检测  很关键
        Thread threadAlarmSound;
        Thread threadUpdateGdv;
        CancellationTokenSource cancellation_threadCheckOnline;//用于终止在线检测线程
        CancellationTokenSource cancellation_threadUpdateGdv;//用于终止
        CancellationTokenSource cancellation_threadAlarm;//用于终止
        FrmIsRunning frmIsRunning;

        private void Initialization()//开始初始化 一通操作
        {
            try
            {
                List<StructClass.StructIAnalyzeConfig> temp_list = new List<StructClass.StructIAnalyzeConfig>();
                temp_list = sqlCreate.Select_All_SMInfrared_ProjConfig(StaticClass.DataBaseName);//按CameraId降序排列
                for (int i = 0; i < 16; i++)
                {
                    StaticClass.intPtrs_AlarmConfig[i] = sqlCreate.Select_AlarmConfig(i + 1,StaticClass.DataBaseName);//告警区域设置数组（从数据库中获取 温度数据 用于告警判断）
                }
                foreach (StructClass.StructIAnalyzeConfig structIAnalyzeConfig in temp_list)
                {
                    StaticClass.intPtrs_Enable[structIAnalyzeConfig.CameraID - 1] = structIAnalyzeConfig.Enable;//启用数组
                    StaticClass.intPtrs_CameraName[structIAnalyzeConfig.CameraID - 1] = structIAnalyzeConfig.CameraName;//名称数组
                    StaticClass.intPtrs_Ip[structIAnalyzeConfig.CameraID - 1] = structIAnalyzeConfig.IP;//IP数组
                    StaticClass.intPtrs_NodeId[structIAnalyzeConfig.NodeID - 1] = structIAnalyzeConfig.CameraID - 1;//NodeID数组 原本打算为 显示顺序预留 现在不需要
                }
                threadStatus = new Thread(StatusJudgment);
                threadCheckOnline = new Thread(CheckOnline);//原来是个线程  懒得改了
                threadCheckAlarm = new Thread(IsAlarm_Checked);
                threadAlarmSound = new Thread(AlarmSound);
                threadUpdateGdv = new Thread(UpdateDgv);
                threadStatus.IsBackground = true;
                threadCheckOnline.IsBackground = true;
                threadCheckAlarm.IsBackground = true;
                threadAlarmSound.IsBackground = true;

                fMessCallBack = new DMSDK.fMessCallBack(dmMessCallBack);//回调函数
                DMSDK.DM_SetAllMessCallBack(fMessCallBack, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化异常"+ex.Message);
            }
        }
        #endregion

        #region//开始与停止按钮事件
        private void btnStart_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            if (btnStart.Tag.ToString() == "Start")//开始
            {
                toolTip.SetToolTip(btnStart, "开始");
            }
            else
            {
                toolTip.SetToolTip(btnStart, "停止");
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnStart.Tag.ToString() == "Start")//开始
                {
                    DMSDK.DM_Init();
                    foreach (IntPtr intPtr in StaticClass.intPtrs_UCPbx)
                    {
                        DMSDK.DM_PlayerInit(intPtr);
                    }
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.WorkerSupportsCancellation = true;
                    frmIsRunning = new FrmIsRunning(worker);
                    worker.DoWork += new DoWorkEventHandler(StartClick);
                    worker.RunWorkerAsync();
                    frmIsRunning.ShowDialog();
                }
                else if (btnStart.Tag.ToString() == "Pause")
                {
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.WorkerSupportsCancellation = true;
                    frmIsRunning = new FrmIsRunning(worker);
                    worker.DoWork += new DoWorkEventHandler(PauseClick);
                    worker.RunWorkerAsync();
                    frmIsRunning.ShowDialog();
                    DMSDK.DM_PlayerCleanup();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("系统启动异常！"+ex.Message);
            }
        }


        private void StartClick(object sender,DoWorkEventArgs e)
        {
            Initialization();//初始化 获得一堆数据
            for (int y = 0; y < 16; y++)
            {
                if (StaticClass.intPtrs_Enable[y])//启用
                {
                    if (DMSDK.DM_CheckOnline(StaticClass.intPtrs_Ip[y], 5000) > 0)//在线检测
                    {
                        StaticClass.intPtrs_Operate[y] = DMSDK.DM_Connect(StaticClass.intPtrs_UCPbx[y], StaticClass.intPtrs_Ip[y], 80);//
                        if (StaticClass.intPtrs_Operate[y] > 0)
                        {
                            StaticClass.intPtrs_Connect[y] = DMSDK.DM_OpenMonitor(StaticClass.intPtrs_UCPbx[y], StaticClass.intPtrs_Ip[y], 5000, 0);// 返回值为视频操作句柄
                        }
                        else
                        {
                            StaticClass.intPtrs_Connect[y] = -1;
                        }
                        if (StaticClass.intPtrs_Connect[y] < 0 || StaticClass.intPtrs_Operate[y] <= 0)//连接失败
                        {
                            StaticClass.intPtrs_Status[y] = (int)RunningStatus.检测中;
                        }
                        else//连接成功
                        {
                            StaticClass.intPtrs_Status[y] = (int)RunningStatus.正常;
                            DMSDK.DM_GetTemp(StaticClass.intPtrs_Operate[y], 1);//获取温度数据
                        }
                    }
                    else
                    {
                        StaticClass.intPtrs_Status[y] = (int)RunningStatus.检测中;
                    }
                }
                else//未启用
                {
                    StaticClass.intPtrs_Status[y] = (int)RunningStatus.未启用;
                }
            }
            threadStatus.Start();
            cancellation_threadCheckOnline = new CancellationTokenSource();
            cancellation_threadUpdateGdv = new CancellationTokenSource();
            cancellation_threadAlarm = new CancellationTokenSource();
            threadCheckOnline.Start();
            threadCheckAlarm.Start();
            threadAlarmSound.Start();
            threadUpdateGdv.Start();
            btnStart.BackgroundImage = Properties.Resources.Pause;
            btnStart.Tag = "Pause";
            RunStaus = true;
        }

        private void PauseClick(object sender,DoWorkEventArgs e)
        {
            cancellation_threadCheckOnline.Cancel();
            cancellation_threadUpdateGdv.Cancel();
            cancellation_threadAlarm.Cancel();
            for (int i = 0; i < 16; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    StaticClass.intPtrs_RealtimeTemper[i].realTimeTemper[j].number = 0;
                    StaticClass.intPtrs_RealtimeTemper[i].realTimeTemper[j].temper = 0;
                    StaticClass.intPtrs_RealtimeTemper[i].realTimeTemper[j].type = 0;
                }
                if (StaticClass.intPtrs_Enable[i])
                {
                    if (StaticClass.intPtrs_Operate[i] > 0)
                    {
                        DMSDK.DM_Disconnect(StaticClass.intPtrs_Operate[i]);
                        StaticClass.intPtrs_Operate[i] = -1;
                        DMSDK.DM_CloseMonitor(StaticClass.intPtrs_Connect[i]);
                        StaticClass.intPtrs_Connect[i] = -1;
                    }
                    if (StaticClass.intPtrs_Status[i] == (int)RunningStatus.正常|| StaticClass.intPtrs_Status[i] == (int)RunningStatus.检测中)
                    {
                        StaticClass.intPtrs_Status[i] = (int)RunningStatus.停止;
                    }
                }
            }
            btnStart.Tag = "Start";
            btnStart.BackgroundImage = Properties.Resources.start;
            RunStaus = false;
        }
        #endregion

        #region//状态判断
        private void StatusJudgment()//状态判断 调用回调函数 获取数据
        {
            while (true)
            {
                try
                {
                    for (int i = 0; i < 16; i++)
                    {
                        switch (StaticClass.intPtrs_Status[i])
                        {
                            case -1:
                                Change_Status(i, -1);
                                break;
                            case 0://未启用 
                                Change_Status(i, 0);
                                break;
                            case 1://火警
                                Change_Status(i, 1);
                                break;
                            case 2://故障
                                Change_Status(i, 2);
                                break;
                            case 3://火警  + 故障
                                Change_Status(i, 3);
                                break;
                            case 4://正常
                                break;
                            case 5://停止工作
                                Change_Status(i, 5);
                                break;
                            case 6://停止工作
                                Change_Status(i, 6);
                                break;
                        }
                        //if (RunStaus)
                        //{
                        //    if (StaticClass.Is_CallBack[i] == false && StaticClass.intPtrs_Enable[i])//回调函数不稳定会停止
                        //    {
                        //        if (StaticClass.intPtrs_Connect[i] > 0 && StaticClass.intPtrs_Operate[i] > 0)//重连成功
                        //        {
                        //            DMSDK.DM_GetTemp(StaticClass.intPtrs_Operate[i], 1);
                        //        }
                        //    }
                        //}
                        Thread.Sleep(100);
                    }
                }catch(Exception ex)
                {
                    if (ex.Message == "正在中止线程。")
                    {

                    }
                    else
                    {
                        MessageBox.Show(ex.Message + "状态监测异常");
                    }
                }
            }
        }

        private void Change_Status(int i,int status)
        {
            UCPbx uCPbx = (UCPbx)FromHandle(StaticClass.intPtrs_UCPbx[i]);
            switch (status)
            {
                case 0://未启用
                    uCPbx.BackgroundImage = Properties.Resources.NotEnabled;
                    break;
                case 1://火警（不需要操作）
                    break;
                case 2://故障
                    uCPbx.BackgroundImage = Properties.Resources.CameraErr;
                    break;
                case 3://火警导致的故障
                    uCPbx.BackgroundImage = Properties.Resources.CameraErr_Fire;
                    break;
                case 4://正常（不需要操作）
                    break;
                case 5://停止工作
                    uCPbx.BackgroundImage = Properties.Resources.nopicture;
                    break;
                case 6://检测中
                    uCPbx.BackgroundImage = Properties.Resources.Checking;
                    break;
            }
        }
        #endregion

        #region//探测器在线监测（通讯故障）
        private void CheckOnline()//在线检测
        {
            try
            {
                for (int j = 0; j < 16; j++)
                {
                    //Stopwatch stopwatch = new Stopwatch();
                    //stopwatch.Start();
                    StaticClass.Is_CallBack[j] = true;//用于判断回调函数是否正常调用
                    if (StaticClass.intPtrs_Enable[j])//启用了的
                    {
                        if (StaticClass.intPtrs_Ip[j] != null)
                        {
                            //ThreadPool.QueueUserWorkItem(IsOnline, j);
                            //多线程同时检测   会闪退  尝试使用一个线程进行检测
                            //Thread thread_IsOnline_Checked = new Thread(new ParameterizedThreadStart(IsOnline_Checked));//每个相机一个独立的线程来判断通讯故障 
                            //thread_IsOnline_Checked.Start(j);
                            //
                            while (!cancellation_threadCheckOnline.IsCancellationRequested)
                            {
                                if (DMSDK.DM_CheckOnline(StaticClass.intPtrs_Ip[j], 5000) < 0)// 离线检测  为离线
                                {
                                    if (StaticClass.Offline_Count[j] < ComnErr_TopLimit)//没达到通讯故障数量上限
                                    {
                                        if (StaticClass.intPtrs_Connect[j] > 0 && StaticClass.intPtrs_Operate[j] > 0)
                                        {
                                            //DMSDK.DM_Disconnect(StaticClass.intPtrs_Operate[j]);//关闭操作连接
                                            //StaticClass.intPtrs_Operate[j] = -1;
                                            DMSDK.DM_CloseMonitor(StaticClass.intPtrs_Connect[j]);//关闭视频连接
                                            StaticClass.intPtrs_Connect[j] = -1;
                                        }
                                        StaticClass.Offline_Count[j]++;//检测到一次
                                    }
                                    else//通讯故障
                                    {
                                        StaticClass.Offline_Count[j] = ComnErr_TopLimit;
                                        if (StaticClass.intPtrs_Status[j] == (int)RunningStatus.火警)
                                        {
                                            StaticClass.intPtrs_Status[j] = (int)RunningStatus.先火警再故障;
                                            Data_Err(j + 1, StaticClass.intPtrs_Ip[j], DateTime.Now, "通讯故障", StaticClass.intPtrs_CameraName[j]);
                                            Exit_FullScreen();
                                        }
                                        else if (StaticClass.intPtrs_Status[j] == (int)RunningStatus.正常 || StaticClass.intPtrs_Status[j] == (int)RunningStatus.检测中)
                                        {
                                            StaticClass.intPtrs_Status[j] = (int)RunningStatus.故障;
                                            Data_Err(j + 1, StaticClass.intPtrs_Ip[j], DateTime.Now, "通讯故障", StaticClass.intPtrs_CameraName[j]);
                                            Exit_FullScreen();
                                        }
                                    }
                                }
                                if (DMSDK.DM_CheckOnline(StaticClass.intPtrs_Ip[j], 5000) > 0)//在线了
                                {
                                    if (StaticClass.Offline_Count[j] >= ComnErr_TopLimit)//通讯故障 在线  则开始重连
                                    {
                                        DMSDK.DM_Disconnect(StaticClass.intPtrs_Operate[j]);//关闭操作连接
                                        StaticClass.intPtrs_Operate[j] = -1;
                                        StaticClass.intPtrs_Operate[j] = DMSDK.DM_Connect(StaticClass.intPtrs_UCPbx[j], StaticClass.intPtrs_Ip[j], 80);
                                        if (StaticClass.intPtrs_Operate[j] > 0)
                                        {
                                            //WriteTxt(@"C:\\Users\\Administrator\\Desktop\test.txt", DateTime.Now.ToString() + StaticClass.intPtrs_Ip[j]+ StaticClass.Offline_Count[j]);
                                            StaticClass.intPtrs_Connect[j] = DMSDK.DM_OpenMonitor(StaticClass.intPtrs_UCPbx[j], StaticClass.intPtrs_Ip[j], 5000, 0);//
                                        }
                                        else
                                        {
                                            StaticClass.intPtrs_Connect[j] = -1;
                                            StaticClass.Offline_Count[j]++;
                                        }
                                        if (StaticClass.intPtrs_Connect[j] > 0 && StaticClass.intPtrs_Operate[j] > 0)//重连成功
                                        {
                                            if (StaticClass.intPtrs_Status[j] == (int)RunningStatus.故障)
                                            {
                                                StaticClass.intPtrs_Status[j] = (int)RunningStatus.正常;
                                            }
                                            else if (StaticClass.intPtrs_Status[j] == (int)RunningStatus.先火警再故障)
                                            {
                                                StaticClass.intPtrs_Status[j] = (int)RunningStatus.火警;
                                            }
                                            else if (StaticClass.intPtrs_Status[j] == (int)RunningStatus.检测中)
                                            {
                                                StaticClass.intPtrs_Status[j] = (int)RunningStatus.正常;
                                            }
                                            Data_Err(j + 1, StaticClass.intPtrs_Ip[j], DateTime.Now, "通讯恢复", StaticClass.intPtrs_CameraName[j]);
                                            DMSDK.DM_GetTemp(StaticClass.intPtrs_Operate[j], 1);
                                            StaticClass.Offline_Count[j] = 0;//置零
                                        }
                                    }
                                    else if (StaticClass.Offline_Count[j] > 0)//表示 检测到几次断开 不满足通讯故障条件  需要重连
                                    {
                                        DMSDK.DM_Disconnect(StaticClass.intPtrs_Operate[j]);//关闭操作连接
                                        StaticClass.intPtrs_Operate[j] = -1;
                                        StaticClass.intPtrs_Operate[j] = DMSDK.DM_Connect(StaticClass.intPtrs_UCPbx[j], StaticClass.intPtrs_Ip[j], 80);
                                        if (StaticClass.intPtrs_Operate[j] > 0)
                                        {
                                            //WriteTxt(@"C:\\Users\\Administrator\\Desktop\test.txt", DateTime.Now.ToString() + StaticClass.intPtrs_Ip[j] + StaticClass.Offline_Count[j]);
                                            StaticClass.intPtrs_Connect[j] = DMSDK.DM_OpenMonitor(StaticClass.intPtrs_UCPbx[j], StaticClass.intPtrs_Ip[j], 5000, 0);//
                                        }
                                        else
                                        {
                                            StaticClass.intPtrs_Connect[j] = -1;
                                            StaticClass.Offline_Count[j]++;
                                        }
                                        if (StaticClass.intPtrs_Connect[j] > 0 && StaticClass.intPtrs_Operate[j] > 0)//重连成功
                                        {
                                            if (StaticClass.intPtrs_Status[j] == (int)RunningStatus.故障)
                                            {
                                                StaticClass.intPtrs_Status[j] = (int)RunningStatus.正常;
                                            }
                                            else if (StaticClass.intPtrs_Status[j] == (int)RunningStatus.先火警再故障)
                                            {
                                                StaticClass.intPtrs_Status[j] = (int)RunningStatus.火警;
                                            }
                                            else if (StaticClass.intPtrs_Status[j] == (int)RunningStatus.检测中)
                                            {
                                                StaticClass.intPtrs_Status[j] = (int)RunningStatus.正常;
                                            }
                                            Data_Err(j + 1, StaticClass.intPtrs_Ip[j], DateTime.Now, "通讯恢复", StaticClass.intPtrs_CameraName[j]);
                                            DMSDK.DM_GetTemp(StaticClass.intPtrs_Operate[j], 1);
                                            StaticClass.Offline_Count[j] = 0;//置零
                                        }
                                    }
                                    else if (StaticClass.Offline_Count[j] == 0)//复位操作 改变状态
                                    {
                                        if (StaticClass.intPtrs_Connect[j] > 0 && StaticClass.intPtrs_Operate[j] > 0)//重连成功
                                        {
                                            if (StaticClass.intPtrs_Status[j] == (int)RunningStatus.故障)
                                            {
                                                StaticClass.intPtrs_Status[j] = (int)RunningStatus.正常;
                                                Data_Err(j + 1, StaticClass.intPtrs_Ip[j], DateTime.Now, "通讯恢复", StaticClass.intPtrs_CameraName[j]);
                                            }
                                            else if (StaticClass.intPtrs_Status[j] == (int)RunningStatus.先火警再故障)
                                            {
                                                StaticClass.intPtrs_Status[j] = (int)RunningStatus.火警;
                                                Data_Err(j + 1, StaticClass.intPtrs_Ip[j], DateTime.Now, "通讯恢复", StaticClass.intPtrs_CameraName[j]);
                                            }
                                            else if (StaticClass.intPtrs_Status[j] == (int)RunningStatus.检测中)
                                            {
                                                StaticClass.intPtrs_Status[j] = (int)RunningStatus.正常;
                                                Data_Err(j + 1, StaticClass.intPtrs_Ip[j], DateTime.Now, "通讯恢复", StaticClass.intPtrs_CameraName[j]);
                                            }
                                            if (!StaticClass.Is_CallBack[j])
                                            {
                                                DMSDK.DM_GetTemp(StaticClass.intPtrs_Operate[j], 1);
                                            }
                                        }

                                    }
                                }
                            }
                        }
                        Thread.Sleep(5000);//暂停16秒  16*4  64秒 
                    }
                    //stopwatch.Stop();
                    //string time = stopwatch.ElapsedMilliseconds.ToString();
                    //WriteTxt(@"C:\Users\hasee\Desktop\新建文本文档 (2).txt", DateTime.Now +"@"+ time);
                }
            }catch(Exception ex)
            {
               
                MessageBox.Show(ex.Message + "线检测异常");
            }
        }

        public void IsOnline_Checked(object i)//判断在线不
        {
            try
            {
                int j = Convert.ToInt32(i);
                while (!cancellation_threadCheckOnline.IsCancellationRequested)
                {
                    if (DMSDK.DM_CheckOnline(StaticClass.intPtrs_Ip[j], 5000) < 0)// 离线检测  为离线
                    {
                        if (StaticClass.Offline_Count[j] < ComnErr_TopLimit)//没达到通讯故障数量上限
                        {
                            if (StaticClass.intPtrs_Connect[j] > 0 && StaticClass.intPtrs_Operate[j] > 0)
                            {
                                //DMSDK.DM_Disconnect(StaticClass.intPtrs_Operate[j]);//关闭操作连接
                                //StaticClass.intPtrs_Operate[j] = -1;
                                DMSDK.DM_CloseMonitor(StaticClass.intPtrs_Connect[j]);//关闭视频连接
                                StaticClass.intPtrs_Connect[j] = -1;
                            }
                            StaticClass.Offline_Count[j]++;//检测到一次
                        }
                        else//通讯故障
                        {
                            StaticClass.Offline_Count[j] = ComnErr_TopLimit;
                            if (StaticClass.intPtrs_Status[j] == (int)RunningStatus.火警)
                            {
                                StaticClass.intPtrs_Status[j] = (int)RunningStatus.先火警再故障;
                                Data_Err(j + 1, StaticClass.intPtrs_Ip[j], DateTime.Now, "通讯故障", StaticClass.intPtrs_CameraName[j]);
                                Exit_FullScreen();
                            }
                            else if(StaticClass.intPtrs_Status[j] == (int)RunningStatus.正常 || StaticClass.intPtrs_Status[j] == (int)RunningStatus.检测中)
                            {
                                StaticClass.intPtrs_Status[j] = (int)RunningStatus.故障;
                                Data_Err(j + 1, StaticClass.intPtrs_Ip[j], DateTime.Now, "通讯故障", StaticClass.intPtrs_CameraName[j]);
                                Exit_FullScreen();
                            }

                        }
                    }
                    if (DMSDK.DM_CheckOnline(StaticClass.intPtrs_Ip[j], 5000) > 0)//在线了
                    {
                        if (StaticClass.Offline_Count[j] >= ComnErr_TopLimit)//通讯故障 在线  则开始重连
                        {
                            DMSDK.DM_Disconnect(StaticClass.intPtrs_Operate[j]);//关闭操作连接
                            StaticClass.intPtrs_Operate[j] = -1;
                            StaticClass.intPtrs_Operate[j] = DMSDK.DM_Connect(StaticClass.intPtrs_UCPbx[j], StaticClass.intPtrs_Ip[j], 80);
                            if (StaticClass.intPtrs_Operate[j] > 0)
                            {
                                //WriteTxt(@"C:\\Users\\Administrator\\Desktop\test.txt", DateTime.Now.ToString() + StaticClass.intPtrs_Ip[j]+ StaticClass.Offline_Count[j]);
                                StaticClass.intPtrs_Connect[j] = DMSDK.DM_OpenMonitor(StaticClass.intPtrs_UCPbx[j], StaticClass.intPtrs_Ip[j], 5000, 0);//
                            }
                            else
                            {
                                StaticClass.intPtrs_Connect[j] = -1;
                                StaticClass.Offline_Count[j]++;
                            }
                            if (StaticClass.intPtrs_Connect[j] > 0 && StaticClass.intPtrs_Operate[j] > 0)//重连成功
                            {
                                if (StaticClass.intPtrs_Status[j] == (int)RunningStatus.故障)
                                {
                                    StaticClass.intPtrs_Status[j] = (int)RunningStatus.正常;
                                }
                                else if (StaticClass.intPtrs_Status[j] == (int)RunningStatus.先火警再故障)
                                {
                                    StaticClass.intPtrs_Status[j] = (int)RunningStatus.火警;
                                }
                                else if (StaticClass.intPtrs_Status[j] == (int)RunningStatus.检测中)
                                {
                                    StaticClass.intPtrs_Status[j] = (int)RunningStatus.正常;
                                }
                                Data_Err(j + 1, StaticClass.intPtrs_Ip[j], DateTime.Now, "通讯恢复", StaticClass.intPtrs_CameraName[j]);
                                DMSDK.DM_GetTemp(StaticClass.intPtrs_Operate[j], 1);
                                StaticClass.Offline_Count[j] = 0;//置零
                            }
                        }
                        else if (StaticClass.Offline_Count[j] > 0)//表示 检测到几次断开 不满足通讯故障条件  需要重连
                        {
                            DMSDK.DM_Disconnect(StaticClass.intPtrs_Operate[j]);//关闭操作连接
                            StaticClass.intPtrs_Operate[j] = -1;
                            StaticClass.intPtrs_Operate[j] = DMSDK.DM_Connect(StaticClass.intPtrs_UCPbx[j], StaticClass.intPtrs_Ip[j], 80);
                            if (StaticClass.intPtrs_Operate[j] > 0)
                            {
                                //WriteTxt(@"C:\\Users\\Administrator\\Desktop\test.txt", DateTime.Now.ToString() + StaticClass.intPtrs_Ip[j] + StaticClass.Offline_Count[j]);
                                StaticClass.intPtrs_Connect[j] = DMSDK.DM_OpenMonitor(StaticClass.intPtrs_UCPbx[j], StaticClass.intPtrs_Ip[j], 5000, 0);//
                            }
                            else
                            {
                                StaticClass.intPtrs_Connect[j] = -1;
                                StaticClass.Offline_Count[j]++;
                            }
                            if (StaticClass.intPtrs_Connect[j] > 0 && StaticClass.intPtrs_Operate[j] > 0)//重连成功
                            {
                                if (StaticClass.intPtrs_Status[j] == (int)RunningStatus.故障)
                                {
                                    StaticClass.intPtrs_Status[j] = (int)RunningStatus.正常;
                                }
                                else if (StaticClass.intPtrs_Status[j] == (int)RunningStatus.先火警再故障)
                                {
                                    StaticClass.intPtrs_Status[j] = (int)RunningStatus.火警;
                                }
                                else if (StaticClass.intPtrs_Status[j] == (int)RunningStatus.检测中)
                                {
                                    StaticClass.intPtrs_Status[j] = (int)RunningStatus.正常;
                                }
                                Data_Err(j + 1, StaticClass.intPtrs_Ip[j], DateTime.Now, "通讯恢复", StaticClass.intPtrs_CameraName[j]);
                                DMSDK.DM_GetTemp(StaticClass.intPtrs_Operate[j], 1);
                                StaticClass.Offline_Count[j] = 0;//置零
                            }
                        }
                        else if (StaticClass.Offline_Count[j] == 0)//复位操作 改变状态
                        {
                            if (StaticClass.intPtrs_Connect[j] > 0 && StaticClass.intPtrs_Operate[j] > 0)//重连成功
                            {
                                if (StaticClass.intPtrs_Status[j] == (int)RunningStatus.故障)
                                {
                                    StaticClass.intPtrs_Status[j] = (int)RunningStatus.正常;
                                    Data_Err(j + 1, StaticClass.intPtrs_Ip[j], DateTime.Now, "通讯恢复", StaticClass.intPtrs_CameraName[j]);
                                }
                                else if (StaticClass.intPtrs_Status[j] == (int)RunningStatus.先火警再故障)
                                {
                                    StaticClass.intPtrs_Status[j] = (int)RunningStatus.火警;
                                    Data_Err(j + 1, StaticClass.intPtrs_Ip[j], DateTime.Now, "通讯恢复", StaticClass.intPtrs_CameraName[j]);
                                }
                                else if (StaticClass.intPtrs_Status[j] == (int)RunningStatus.检测中)
                                {
                                    StaticClass.intPtrs_Status[j] = (int)RunningStatus.正常;
                                    Data_Err(j + 1, StaticClass.intPtrs_Ip[j], DateTime.Now, "通讯恢复", StaticClass.intPtrs_CameraName[j]);
                                }
                                if (!StaticClass.Is_CallBack[j])
                                {
                                    DMSDK.DM_GetTemp(StaticClass.intPtrs_Operate[j], 1);
                                }
                            }
                           
                        }
                    }
                    Thread.Sleep(5000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("通讯故障判断异常："+ex.Message + ex.StackTrace);
            }
        }
        #endregion

        #region//火警判断

        private void IsAlarm_Checked()
        {
            try
            {
                for (int j = 0; j < 16; j++)
                {
                    if (StaticClass.intPtrs_Enable[j])//启用了的
                    {
                        if (StaticClass.intPtrs_Ip[j] != null)
                        {
                            Thread thread_IsAlarm_Checked = new Thread(new ParameterizedThreadStart(CheckAlarm));//独立的线程来判火警
                            thread_IsAlarm_Checked.Start(j);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "火警检测异常");
            }
        }

        object objAlarmCount = new object();
        private void CheckAlarm(object k)//判断火警使用 
        {
            string path;
            int i = Convert.ToInt32(k);
            path = ConfigurationManager.AppSettings["ImageSavePath"] + "SMCameraPic" + "\\" + StaticClass.ProjName + "\\" + "Camera" + (i + 1).ToString() + "\\";
            int AlarmCount = Convert.ToInt32(ConfigurationManager.AppSettings["AlarmCount"]);//50毫秒判断一次 一秒钟判断20次  
            int alarmCount = AlarmCount * 20;
            while (!cancellation_threadAlarm.IsCancellationRequested)
            {
                try
                {
                    if (StaticClass.intPtrs_Status[i] == (int)RunningStatus.正常)//该探测器启用  且正常
                    {
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        for (int j = 0; j < 8; j++)
                        {
                            if (StaticClass.intPtrs_AlarmConfig[i].structAlarmconfigs[j].Enable)//该区域启用 
                            {
                                StaticClass.intPtrs_structCameraAlarmCounts[i].areaAlarmCounts[j].AreaId = j + 1;
                                if (StaticClass.intPtrs_AlarmConfig[i].structAlarmconfigs[j].Spark == 0)//触发方式 大于
                                {
                                    if (StaticClass.intPtrs_RealtimeTemper[i].realTimeTemper[j].temper >= StaticClass.intPtrs_AlarmConfig[i].structAlarmconfigs[j].AlarmTemper)//大于一次
                                    {
                                        StaticClass.intPtrs_structCameraAlarmCounts[i].areaAlarmCounts[j].AreaCount++;
                                    }
                                    else//小于一次  就清空告警计数
                                    {
                                        StaticClass.intPtrs_structCameraAlarmCounts[i].areaAlarmCounts[j].AreaCount = 0;
                                    }
                                }
                                else if (StaticClass.intPtrs_AlarmConfig[i].structAlarmconfigs[j].Spark == 1)//触发方式  小于
                                {
                                    if (StaticClass.intPtrs_RealtimeTemper[i].realTimeTemper[j].temper <= StaticClass.intPtrs_AlarmConfig[i].structAlarmconfigs[j].AlarmTemper)//小于一次
                                    {
                                        StaticClass.intPtrs_structCameraAlarmCounts[i].areaAlarmCounts[j].AreaCount++;
                                    }
                                    else//大于一次就清空计数
                                    {
                                        StaticClass.intPtrs_structCameraAlarmCounts[i].areaAlarmCounts[j].AreaCount = 0;
                                    }
                                }
                                if (StaticClass.intPtrs_structCameraAlarmCounts[i].areaAlarmCounts[j].AreaCount >= alarmCount)//超过4次 就告警
                                {
                                    lock (objAlarmCount)
                                    {
                                        if (StaticClass.FireCount == 0)
                                        {
                                            if (!StaticClass.IsCameraFireAlarm[i])
                                            {
                                                Data_Frie(i + 1, StaticClass.intPtrs_Ip[i], DateTime.Now, "首警", StaticClass.intPtrs_CameraName[i]);
                                                if (!Directory.Exists(path))
                                                {
                                                    Directory.CreateDirectory(path);
                                                }
                                                if (DMSDK.DM_Capture(StaticClass.intPtrs_Connect[i], path) < 0)
                                                {
                                                    MessageBox.Show("报警图片保存异常！");
                                                }
                                                Exit_FullScreen();
                                            }
                                        }
                                        else if (StaticClass.FireCount >= 1)
                                        {
                                            if (!StaticClass.IsCameraFireAlarm[i])
                                            {
                                                Data_Frie(i + 1, StaticClass.intPtrs_Ip[i], DateTime.Now, "火警", StaticClass.intPtrs_CameraName[i]);
                                                if (!Directory.Exists(path))
                                                {
                                                    Directory.CreateDirectory(path);
                                                }
                                                if (DMSDK.DM_Capture(StaticClass.intPtrs_Connect[i], path) < 0)
                                                {
                                                    MessageBox.Show("报警图片保存异常！");
                                                }
                                                Exit_FullScreen();
                                            }
                                        }
                                        StaticClass.intPtrs_Status[i] = (int)RunningStatus.火警;//原来不报警 现在报警
                                        StaticClass.IsCameraFireAlarm[i] = true;
                                        areaAlarmCount.AreaCount = alarmCount;
                                    }
                                }
                            }
                        }
                        Thread.Sleep(50);
                        stopwatch.Stop();
                        string time = stopwatch.ElapsedMilliseconds.ToString();
                    }
                    //WriteTxt(@"C:\Users\hasee\Desktop\新建文本文档 (2).txt", DateTime.Now +"火警时间@"+ time);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message + "火警监测异常");
                }
            }
        }
        #endregion

        #region//报警声音
        SoundPlayer soundPlayer;
        bool IsFireSoundOn = false;
        bool IsErrSoundOn = false;

        private void SoundInit()
        {
            if (StaticClass.FireCount > 0 && IsFireSoundOn==false)//有火警 且火警声音没响
            {
                soundPlayer = new SoundPlayer(Properties.Resources.FireSound);
                soundPlayer.PlayLooping();
                IsFireSoundOn = true;
            }
            if (StaticClass.FireCount == 0 && StaticClass.ErrCount > 0 && IsErrSoundOn==false)//只有故障
            {
                soundPlayer = new SoundPlayer(Properties.Resources.ErrSound);
                soundPlayer.PlayLooping();
                IsErrSoundOn = true;
            }
            if (StaticClass.FireCount == 0 && StaticClass.ErrCount == 0)//无声音
            {
                soundPlayer = new SoundPlayer();
                soundPlayer.Stop();
                IsFireSoundOn = false;
                IsErrSoundOn = false;
            }
        }
        private void AlarmSound()
        {
            while (true)
            {
                if (IsAlarmSoundOn)
                {
                    SoundInit();
                }
                else
                {
                    soundPlayer = new SoundPlayer();
                    soundPlayer.Stop();
                    IsFireSoundOn = false;
                    IsErrSoundOn = false;
                }
                Thread.Sleep(1000);
            }
        }
        #endregion

        #region//回调函数
        /*
         * 多台仪器的告警信息无法获取（判断是那一台的告警信息），需要程序自己判断（）
         * */
        DMSDK.fMessCallBack fMessCallBack;
        DMSDK.tagTempMessage tempMessage;
        DMSDK.tagAlarm tagAlarm;
        DMSDK.tagError tagError;
        ArrayList arrayList_Area = new ArrayList();
        SqlInsert sqlInsert = new SqlInsert();

        private void dmMessCallBack(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)//当为测温告警的时候 温度数据可以回调获取   频率在一秒钟20次上 （ 不能更改 跟大立人员确认过了）
        {
            int Msg = msg - 0x8000;
            switch (Msg)
            {
                case 0x3051://错误
                    tagError = (DMSDK.tagError)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagError));
                    int ErrID = tagError.ErrorID;
                    if(ErrID == 0x00004000)
                    {
                        //MessageBox.Show("系统异常：请尝试关闭防火墙！");
                    }
                    //StaticClass.intPtrs_Status[0] = (int)RunningStatus.故障;
                    break;
                case 0x3053://温度数据
                    tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
                    sqlInsert.InsertTemperDataToArrayList(DateTime.Now, tempMessage);//带时间戳 数据扔过去
                    break;
                case 0x3054://报警消息 将alarmID对应的byte置为1    针对单台仪器可以使用该方法  木得问题 测温目标设置限制，  //现在直接搞个数记录采集的温度 然后获取报警温度 自行判断（超温5秒 告警）
                            //tagAlarm = (DMSDK.tagAlarm)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagAlarm));
                            //byte AlaemId = (byte)(tagAlarm.AlarmID);
                            //StaticClass.intPtrs_AlarmId[0] = (byte)(StaticClass.intPtrs_AlarmId[0] | (0x80 >> AlaemId));// 将10000000右移 对应的ID位数 即将对应位置的置为1
                            //StaticClass.intPtrs_Status[0] = (int)RunningStatus.温度告警;
                            //BeginInvoke(new MethodInvoker(delegate
                            //{
                            //    dgvWarning.Rows.Add(DateTime.Now, "测温告警", StaticClass.intPtrs_Ip[0], StaticClass.intPtrs_CameraName[0] + "区域编号" + AlaemId.ToString());
                            //}));
                    break;
                case 0x3061://视频数据
                    break;
            }
        }
        #endregion

        #region//告警信息插入动态数组
        private void Data_Frie(int cameraId,string Ip,DateTime dateTime,string type,string message) 
        {
            try
            {
                lock (StaticClass.arrayList_FireData.SyncRoot)
                {
                    fireData.CameraID = cameraId;
                    fireData.IPAddress = Ip;
                    fireData.dateTime = dateTime;
                    fireData.Type = type;
                    fireData.Message = message;
                    StaticClass.arrayList_FireData.Add(fireData);
                }
            }catch(Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "arrayList_FireData插入异常：" + ex.Message + ex.StackTrace);
            }
        }

        private void Data_Err(int cameraId, string Ip, DateTime dateTime, string type, string message)
        {
            try
            {
                lock (StaticClass.arrayList_ErrData.SyncRoot)
                {
                    errData.CameraID = cameraId;
                    errData.IPAddress = Ip;
                    errData.dateTime = dateTime;
                    errData.Type = type;
                    errData.Message = message;
                    StaticClass.arrayList_ErrData.Add(errData);
                }
            }
            catch(Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "arrayList_ErrData插入异常：" + ex.Message + ex.StackTrace);
            }
        }
        #endregion

        #region//写入文本
        private void WriteTxt(string path,string contents)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            FileStream fs = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(contents);
            sw.Flush();
            sw.Close();
            fs.Close();
        }



        #endregion

        #region//数据库维护计划
        /// <summary>
        /// 数据库维护计划 保留的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        string[] AreaType = { "测温点", "测温线", "测温区域" };
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (DateTime.Now.ToLongTimeString() == "0:00:00")//凌晨 将一周前的那天的数据 导出来
                {
                    var date = DateTime.Now.AddDays(-7);//备份一周前的数据
                    var date_day = DateTime.Now.AddDays(-90).ToString("u").Split(' ');//删除三个月前的数据
                    for (int i = 0; i < 16; i++)
                    {
                        if (StaticClass.intPtrs_Enable[i])
                        {
                            DateTime dateTime = new DateTime();
                            dateTime = DateTime.Now;
                            if (ConfigurationManager.AppSettings["Automatictiming"] == "1")
                            {
                                DMSDK.DM_SetDateTime(StaticClass.intPtrs_Operate[i], dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);//校准时间
                            }
                            sqlCreate.Delete_HisData(i + 1, date_day[0], StaticClass.DataBaseName);
                            string path = ConfigurationManager.AppSettings["ImageSavePath"] + "SMCameraData" + "\\" + StaticClass.ProjName + "\\" + "Camera" + (i + 1).ToString() + "\\";
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            List<StructClass.StructTemperData> list = new List<StructClass.StructTemperData>();
                            list = sqlCreate.Select_TemperData(i + 1, date, date, StaticClass.DataBaseName);
                            DataGridView Temper_dataGrid = new DataGridView();
                            ArrayList List_Headertext = new ArrayList() { "探测器编号", "IP地址", "时间","区域编号", "温度", "状态" };
                            foreach (string hadertext in List_Headertext)
                            {
                                DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
                                dataGridViewTextBoxColumn.HeaderText = hadertext;
                                Temper_dataGrid.Columns.Add(dataGridViewTextBoxColumn);
                            }
                            foreach (StructClass.StructTemperData structTemper in list)
                            {
                                Temper_dataGrid.Rows.Add(structTemper.CameraID, structTemper.IPAddress, structTemper.dateTime, structTemper.Type, Convert.ToDecimal(structTemper.Temper) / 100, structTemper.Status);
                            }
                            if (path != null)
                            {
                                DataTable dt = new DataTable();
                                // 列强制转换
                                for (int count = 0; count < Temper_dataGrid.Columns.Count; count++)
                                {
                                    DataColumn dc = new DataColumn(Temper_dataGrid.Columns[count].Name.ToString());
                                    dt.Columns.Add(dc);
                                }
                                // 循环行
                                for (int count = 0; count < Temper_dataGrid.Rows.Count; count++)
                                {
                                    DataRow dr = dt.NewRow();
                                    for (int countsub = 0; countsub < Temper_dataGrid.Columns.Count; countsub++)
                                    {
                                        dr[countsub] = Convert.ToString(Temper_dataGrid.Rows[count].Cells[countsub].Value);
                                    }
                                    dt.Rows.Add(dr);
                                }
                                if (dt ==null)
                                {
                                    DataRow dr = dt.NewRow();
                                    dr[0] = "无数据";
                                    dt.Rows.Add(dr);
                                }
                                else
                                {
                                    dt.Columns[0].ColumnName = "探测器编号";
                                    dt.Columns[1].ColumnName = "IP地址";
                                    dt.Columns[2].ColumnName = "时间";
                                    dt.Columns[3].ColumnName = "区域编号";
                                    dt.Columns[4].ColumnName = "温度";
                                    dt.Columns[5].ColumnName = "状态";
                                }
                                Excel.ExportToExcel(dt, path, (DateTime.Now.ToString() + ".xls").Trim().Replace('/', '_').Replace(':', '_').Replace(' ', '_'));
                            }
                        }
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show("导出数据失败" + ex.Message + ex.StackTrace);
            }
        }
        #endregion

        #region//显示datagridview
        List<StructClass.StructErrData> temp_listErr = new List<StructClass.StructErrData>();
        List<StructClass.StructFireData> temp_listFire = new List<StructClass.StructFireData>();
        List<int> temp_IntErr;
        List<int> temp_IntFire;
        bool IsUpdate_DgvErr = false;
        bool IsUpdate_DgvFire = false;
        private void UpdateDgv()//向数据库中插入故障数据 火警数据
        {
            while (!cancellation_threadUpdateGdv.IsCancellationRequested)
            {
                if (StaticClass.arrayList_ErrData.Count > 0)
                {
                    temp_IntErr = new List<int>();
                    lock (StaticClass.arrayList_ErrData.SyncRoot)//舒服
                    {
                        errData = (StructClass.StructErrData)StaticClass.arrayList_ErrData[0];
                        if (errData.Type == "通讯故障")
                        {
                            foreach (StructClass.StructErrData tempErr in temp_listErr)
                            {
                                temp_IntErr.Add(tempErr.CameraID);
                            }
                            if (!temp_IntErr.Contains(errData.CameraID))//是新的通讯故障
                            {
                                temp_listErr.Add(errData);
                                sqlCreate.Insert_WarningRecords(errData.CameraID, errData.IPAddress, errData.dateTime, errData.Type, errData.Message, StaticClass.DataBaseName);
                                IsUpdate_DgvErr = true;
                            }
                        }
                        else if (errData.Type == "通讯恢复")
                        {
                            foreach (StructClass.StructErrData tempErr in temp_listErr)
                            {
                                if (errData.CameraID == tempErr.CameraID)
                                {
                                    temp_listErr.Remove(tempErr);//移出已经存在的通讯故障
                                    sqlCreate.Insert_WarningRecords(errData.CameraID, errData.IPAddress, errData.dateTime, errData.Type, errData.Message, StaticClass.DataBaseName);
                                    IsUpdate_DgvErr = true;
                                    break;
                                }
                            }
                        }
                        StaticClass.arrayList_ErrData.RemoveAt(0);
                        if (IsUpdate_DgvErr)//需要刷新一次
                        {
                            IsUpdate_DgvErr = false;
                            BeginInvoke(new MethodInvoker(delegate
                            {
                                dgvError.Rows.Clear();
                            }));
                            Thread.Sleep(200);
                            for (int i = temp_listErr.Count; i > 0; i--)
                            {
                                errData = temp_listErr[i - 1];
                                BeginInvoke(new MethodInvoker(delegate
                                {
                                    dgvError.Rows.Add(errData.dateTime, errData.Type, errData.IPAddress, errData.Message);
                                }));
                                Thread.Sleep(200);
                            }
                        }
                    }
                }
                if (StaticClass.arrayList_FireData.Count > 0)
                {
                    temp_IntFire = new List<int>();
                    lock (StaticClass.arrayList_FireData.SyncRoot)
                    {
                        fireData = (StructClass.StructFireData)StaticClass.arrayList_FireData[0];
                        foreach (StructClass.StructFireData tempFire in temp_listFire)
                        {
                            temp_IntFire.Add(tempFire.CameraID);
                        }
                        if (!temp_IntFire.Contains(fireData.CameraID))// 新的火警 火警该怎么处理  只报出一次
                        {
                            temp_listFire.Add(fireData);
                            sqlCreate.Insert_WarningRecords(fireData.CameraID, fireData.IPAddress, fireData.dateTime, fireData.Type, fireData.Message, StaticClass.DataBaseName);
                            IsUpdate_DgvFire = true;
                        }
                        StaticClass.arrayList_FireData.RemoveAt(0);
                        if (IsUpdate_DgvFire)//需要刷新一次
                        {
                            IsUpdate_DgvFire = false;
                            BeginInvoke(new MethodInvoker(delegate
                            {
                                dgvWarning.Rows.Clear();
                            }));
                            Thread.Sleep(200);
                            for (int i = temp_listFire.Count; i > 0; i--)
                            {
                                fireData = temp_listFire[i - 1];
                                BeginInvoke(new MethodInvoker(delegate
                                {
                                    dgvWarning.Rows.Add(fireData.dateTime, fireData.Type, fireData.IPAddress, fireData.Message);
                                }));
                                Thread.Sleep(200);
                            }
                        }
                    }
                }
                Thread.Sleep(200);
            }
           
        }

      
        #endregion

        //private void btnSaveVideo_Click(object sender, EventArgs e)//保存视频.avi
        //{
        //StaticClass.intPtrs_Operate[1] = DMSDK.DM_OpenMonitor(StaticClass.intPtrs_UCPbx[1], "192.168.1.2", 5000);
        //DMSDK.DM_Record(StaticClass.intPtrs_Operate[1], "F:\\热成像仪光盘内容");
        //Thread.Sleep(5000);
        //DMSDK.DM_StopRecord(StaticClass.intPtrs_Operate[1]);
        //}
    }
}
