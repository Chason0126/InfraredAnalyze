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
using Microsoft.DirectX.PrivateImplementationDetails;
using System.Configuration;
using System.Collections;
using System.Threading;
using System.Diagnostics;

namespace InfraredAnalyze
{

    interface IFrmStyle//窗体样式控制  无边框  拖动 改变位置  不方便  算了
    {
        void FrmBorderStyle(Form form);//传入窗体改变 窗题的borderstyle
        void FrmMove(Panel panel);//按住panle 改变窗体的位置
    }
    public partial class FrmMain : Form
    {
        SqlCreate sqlCreate = new SqlCreate();
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


        #region//构造函数
        public FrmMain()
        {
            InitializeComponent();
            timer1.Start();
            DMSDK.DM_Init();//关闭时需要释放资源
            DMSDK.DM_PlayerInit(spcScreen.Handle);//初始化视频  只能调用一次
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
            DMSDK.DM_PlayerCleanup();
            this.Close();
            Environment.Exit(0);
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
            //if (mouseDirection == MouseDirection.Declining)
            //{
            //    this.Cursor = Cursors.SizeNWSE;
            //    this.Width = MousePosition.X - this.Left;
            //    this.Height = MousePosition.Y - this.Top;
            //}
            //if (mouseDirection == MouseDirection.Herizontal)
            //{
            //    this.Cursor = Cursors.SizeWE;
            //    this.Width = MousePosition.X - this.Left;
            //}
            //if (mouseDirection == MouseDirection.Vertical)
            //{
            //    this.Cursor = Cursors.SizeNS;
            //    this.Height = MousePosition.Y - this.Top;
            //}
            //else
            //{
            //    this.Cursor = Cursors.Arrow;
            //}
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
            //tlpRunningStatus.Controls.Clear();
            //spcRunningStatus.Panel2.Controls.Clear();
            //spcRunningStatus.Panel2.Controls.Add(tlpRunningStatus);
            //tlpRunningStatus.Dock = DockStyle.Fill;

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
            for (int i = 0; i < num; i++)
            {
                UCPbx uCPbx = new UCPbx();
                //UCStatusPanel uCStatus = new UCStatusPanel();
                uCPbx = (UCPbx)FromHandle(StaticClass.intPtrs_UCPbx[i]);
                //uCStatus = (UCStatusPanel)FromHandle(StaticClass.intPtrs_UCStatusPanel[i]);
                uCPbx.Id = i + 1;
                //uCStatus.Id = i + 1;
                uCPbx.DoubleClick += new EventHandler(UCPbx_DoubleClick);
                tlpScreen.Controls.Add(uCPbx);
                //tlpRunningStatus.Controls.Add(uCStatus);
            }
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

        #region//全屏显示
        private void UCPbx_DoubleClick(object sender, EventArgs e)
        {
            UCPbx uCPbx = (UCPbx)sender;
            tlpScreen.Controls.Clear();
            spcScreen.Panel1.Controls.Clear();
            TableLayoutPanel layoutPanel = new TableLayoutPanel();
            spcScreen.Panel1.Controls.Add(layoutPanel);
            layoutPanel.Dock = DockStyle.Fill;
            layoutPanel.RowCount = 1;
            layoutPanel.ColumnCount = 1;
            uCPbx.Height = spcScreen.Panel1.Height;
            uCPbx.Width = spcScreen.Panel1.Width;
            layoutPanel.Controls.Add(uCPbx);  //双击事件会被执行两次？
            return;//直接return
        }
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
            ArrayList temp_arrayList = sqlCreate.Select_All_SMInfraredConfig();
            try
            {
                tvwSensor.Nodes.Clear();
                if(temp_arrayList.Count>0)
                {
                    foreach(StructClass.StructIAnalyzeConfig structIAnalyzeConfig in temp_arrayList)
                    {
                        TreeNode temp_Node = new TreeNode();
                        StructClass.StructSM7003Tag structSM7003Tag = new StructClass.StructSM7003Tag();
                        temp_Node.Text = structIAnalyzeConfig.CameraName;
                        structSM7003Tag.CameraID = structIAnalyzeConfig.CameraID;
                        structSM7003Tag.IP = structIAnalyzeConfig.IP;
                        structSM7003Tag.Port = structIAnalyzeConfig.Port;
                        structSM7003Tag.NodeID = structIAnalyzeConfig.NodeID;
                        structSM7003Tag.Reamrks = structIAnalyzeConfig.Reamrks;
                        structSM7003Tag.Enable = structIAnalyzeConfig.Enable;
                        if(structSM7003Tag.Enable==false)
                        {
                            temp_Node.ForeColor = Color.Gray;
                            temp_Node.NodeFont = new Font("微软雅黑", 10, FontStyle.Strikeout);
                        }
                        temp_Node.Tag = structSM7003Tag;
                        temp_Node.ContextMenuStrip = cmsIPCameraConfig;
                        tvwSensor.Nodes.Add(temp_Node);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载探测器列表失败！" + ex.Message);
            }
        }
        #endregion

        #region// 树视图点击事件
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
                }else if(e.Button==MouseButtons.Right)
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
        #endregion

        #region//树视图双击事件
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
                    sqlCreate.Delete_Node_SMInfraredConfig(CameraId, NodeId);//修改大于选项的CameraId
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
                frmConfig.ShowDialog();
            }
        }
        #endregion

        #region//系统参数设置
        private void 测温参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                pnl frmTemperParamConfig = new pnl();
                tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
                structSM7003Tag = new StructClass.StructSM7003Tag();
                if (tvwSensor.SelectedNode != null)
                {
                    structSM7003Tag = (StructClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
                    frmTemperParamConfig.Ip = structSM7003Tag.IP;
                    frmTemperParamConfig.CameraId = structSM7003Tag.CameraID;
                    frmTemperParamConfig.ShowDialog();
                }
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

        #region//连接
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
                    StaticClass.intPtrs_Connect[structSM7003Tag.CameraID - 1] = DMSDK.DM_OpenMonitor(StaticClass.intPtrs_UCPbx[structSM7003Tag.CameraID - 1], structSM7003Tag.IP, 5000);
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
            FrmVideoConfig frmVideoConfig = new FrmVideoConfig();
            structSM7003Tag = new StructClass.StructSM7003Tag();
            tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
            if(tvwSensor.SelectedNode!=null)
            {
                structSM7003Tag = (StructClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
                frmVideoConfig.IPCameraId = structSM7003Tag.CameraID;
                frmVideoConfig.IPAddress = structSM7003Tag.IP;
                frmVideoConfig.CName = tvwSensor.SelectedNode.Text;
                if(frmVideoConfig.ShowDialog()==DialogResult.OK)
                {
                    LoadTreeView();
                }
            }

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
        #endregion

        #region//历史数据
        private void 历史数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHistoricalTemperData frmHistoricalTemperData = new FrmHistoricalTemperData();
            StructClass.StructSM7003Tag structSM7003Tag = new StructClass.StructSM7003Tag();
            tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
            if (tvwSensor.SelectedNode != null)
            {
                structSM7003Tag = (StructClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
                frmHistoricalTemperData.CameraID = structSM7003Tag.CameraID;
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

        #region//配置IP
        private void 配置IPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(StaticClass.SelectedNode==0)
            {
                MessageBox.Show("请选择需要操作的探测器！");
                return;
            }
            FrmIpConfig frmIpConfig = new FrmIpConfig();
            frmIpConfig.ShowDialog();
        }
        #endregion

        #region//清空数据库
        private void 清空数据库ToolStripMenuItem_Click(object sender, EventArgs e)//谨慎操作（仅当数据库异常时供操作）
        {
            //if (MessageBox.Show("确定要清空数据库吗？这将删除所有数据，并且不可恢复，请谨慎操作！", "不可恢复的操作", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            //{
            //    thread = new Thread(showIsRunning);//显示请勿操作界面 防止误操作
            //    thread.IsBackground = true;
            //    thread.Start();
            //    sqlCreate.Drop_AllDatabase();
            //    thread.Abort();
            //    MessageBox.Show("请重新启动软件来完成初始化数据库！");
            //}
        }
        #endregion

        #region//显示时间 火警数量 故障数量
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
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
                if (StaticClass.SelectedNode == 0)
                {
                    btnCameraConfig.BackgroundImage = Properties.Resources.cameraConfig_disable;
                }
                else
                {
                    btnCameraConfig.BackgroundImage = Properties.Resources.cameraConfig;
                }
                if (dgvWarning.RowCount != StaticClass.arrayList_FireData.Count)
                {
                    dgvWarning.Rows.Clear();
                    for (int i = StaticClass.arrayList_FireData.Count; i > 0; i--)
                    {
                        fireData = (StructClass.StructFireData)StaticClass.arrayList_FireData[i - 1];
                        dgvWarning.Rows.Add(fireData.dateTime, fireData.Type, fireData.IPAddress, fireData.Message);
                    }
                    fireCount = StaticClass.arrayList_FireData.Count;
                }
                if (dgvError.RowCount != StaticClass.arrayList_ErrData.Count)
                {
                    dgvError.Rows.Clear();
                    for (int i = StaticClass.arrayList_ErrData.Count; i > 0; i--)
                    {
                        errData = (StructClass.StructErrData)StaticClass.arrayList_ErrData[i - 1];
                        dgvError.Rows.Add(errData.dateTime, errData.Type, errData.IPAddress, errData.Message);
                    }
                    fireCount = StaticClass.arrayList_FireData.Count;
                }
                for (int i = 0; i < 16; i++)
                {
                    if (StaticClass.intPtrs_Status[i] == (int)RunningStatus.先火警再故障)//两个都算
                    {
                        firecount++;
                        errcount++;
                    }
                    if (StaticClass.intPtrs_Status[i] == (int)RunningStatus.火警)
                    {
                        firecount++;
                        for (int j = 0; j < StaticClass.arrayList_ErrData.Count; j++)
                        {
                            errData = (StructClass.StructErrData)StaticClass.arrayList_ErrData[j];
                            if (errData.IPAddress == StaticClass.intPtrs_Ip[i])//在故障数组里出现了
                            {
                                StaticClass.arrayList_ErrData.Remove(errData);
                            }
                        }
                    }
                    else if (StaticClass.intPtrs_Status[i] == (int)RunningStatus.故障)
                    {
                        errcount++;
                    }
                    else if (StaticClass.intPtrs_Status[i] == (int)RunningStatus.正常)
                    {
                        for (int j = 0; j < StaticClass.arrayList_ErrData.Count; j++)
                        {
                            errData = (StructClass.StructErrData)StaticClass.arrayList_ErrData[j];
                            if (errData.IPAddress == StaticClass.intPtrs_Ip[i])//在故障数组里出现了
                            {
                                StaticClass.arrayList_ErrData.Remove(errData);
                            }
                        }
                    }
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
                lblFireCount.Text = "火警数量：" + StaticClass.FireCount.ToString();
                lblErrCount.Text = "故障数量：" + StaticClass.ErrCount.ToString();
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
            StaticClass.FireCount = 0;
            StaticClass.ErrCount = 0;
            for (int i = 0; i < 16; i++)
            {
                if (StaticClass.intPtrs_Enable[i])
                {
                    StaticClass.intPtrs_Status[i] = (int)RunningStatus.正常;
                }
            }
            StaticClass.arrayList_FireData.Clear();
            StaticClass.arrayList_ErrData.Clear();
            dgvError.Rows.Clear();
            dgvWarning.Rows.Clear();
        }
        #endregion

        #region//全屏显示
        private void btnFullScreen_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region//历史记录（包括火警  故障）
        private void btnHistoryRecord_Click(object sender, EventArgs e)
        {
            FrmHisRecords frmHisRecords = new FrmHisRecords();
            frmHisRecords.CameraId = StaticClass.SelectedNode;
            frmHisRecords.Show();
        }

        private void btnHistoryRecord_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnHistoryRecord, "历史记录");
        }
        #endregion


        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadTreeView();
            ScreenNum = Convert.ToInt32(ConfigurationManager.AppSettings["ScreenNum"]);
            Refresh_Screen(ScreenNum);
        }


        private void btnCameraConfig_Click(object sender, EventArgs e)
        {
            if (StaticClass.SelectedNode == 0)
            {
                MessageBox.Show("请先选择需要启用的探测器!");
            }
            else
            {
                FrmAddIPCamera frmAddIPCamera = new FrmAddIPCamera();
                StructClass.StructIAnalyzeConfig temp_structIAnalyzeConfig = new StructClass.StructIAnalyzeConfig();
                ArrayList arrayList= sqlCreate.Select_SMInfraredConfig(StaticClass.SelectedNode);
                foreach(StructClass.StructIAnalyzeConfig structIAnalyzeConfig in arrayList)
                {
                    temp_structIAnalyzeConfig = structIAnalyzeConfig;
                }
                frmAddIPCamera.IP = temp_structIAnalyzeConfig.IP;
                frmAddIPCamera.Num = StaticClass.SelectedNode;
                frmAddIPCamera.CameraName = temp_structIAnalyzeConfig.CameraName;
                if (frmAddIPCamera.ShowDialog() == DialogResult.OK)
                {
                    LoadTreeView();
                }
            }
        }

       


        private void btnStart_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnStart, "一键链接");
        }

        ArrayList temp_arrayList;
        Thread thread;//提示请勿操作线程
        Thread threadStatus;//检测状态数组线程
        Thread threadCheckOnline;//掉线检测线程
        Thread threadCheckAlarm;//温度判断线程 用于报警检测  很关键
        Thread threadGetTemper;//获取温度数据（连续获取 数据量太多了）
        private void btnStart_Click(object sender, EventArgs e)
        {
            thread = new Thread(showIsRunning);//显示请勿操作界面 防止误操作
            thread.IsBackground = true;
            thread.Start();
            try
            {
                if (btnStart.Tag.ToString() == "Start")//开始
                {
                    temp_arrayList = sqlCreate.Select_All_SMInfraredConfig();//按CameraId降序排列
                    for(int i = 0; i < 16; i++)
                    {
                        StaticClass.intPtrs_AlarmConfig[i] = sqlCreate.Select_AlarmConfig(i + 1);//告警区域设置数组（从数据库中获取 温度数据 用于告警判断）
                    }
                    foreach (StructClass.StructIAnalyzeConfig structIAnalyzeConfig in temp_arrayList)
                    {
                        StaticClass.intPtrs_Enable[structIAnalyzeConfig.CameraID - 1] = structIAnalyzeConfig.Enable;//启用数组
                        StaticClass.intPtrs_CameraName[structIAnalyzeConfig.CameraID - 1] = structIAnalyzeConfig.CameraName;//名称数组
                        StaticClass.intPtrs_Ip[structIAnalyzeConfig.CameraID - 1] = structIAnalyzeConfig.IP;//IP数组
                        StaticClass.intPtrs_NodeId[structIAnalyzeConfig.NodeID - 1] = structIAnalyzeConfig.CameraID - 1;//NodeID数组 原本打算为 显示顺序预留 现在不需要
                    }
                    threadStatus = new Thread(StatusJudgment);
                    threadCheckOnline = new Thread(CheckOnline);
                    threadCheckAlarm = new Thread(CheckAlarm);
                    threadGetTemper = new Thread(GetTemper);
                    threadStatus.IsBackground = true;
                    threadCheckOnline.IsBackground = true;
                    threadCheckAlarm.IsBackground = true;
                    threadGetTemper.IsBackground = true;
                    for (int y = 0; y < 16; y++)
                    {
                        if (StaticClass.intPtrs_Enable[y])//如果该相机启用了
                        {
                            StaticClass.intPtrs_Connect[y] = DMSDK.DM_OpenMonitor(StaticClass.intPtrs_UCPbx[y], StaticClass.intPtrs_Ip[y], 5000);// 返回值为视频操作句柄
                            StaticClass.intPtrs_Operate[y] = DMSDK.DM_Connect(StaticClass.intPtrs_UCPbx[y], StaticClass.intPtrs_Ip[y], 80);
                            if (StaticClass.intPtrs_Connect[y] < 0 || StaticClass.intPtrs_Operate[y] <= 0)//连接失败
                            {
                                //StaticClass.intPtrs_Status[y] = (int)RunningStatus.故障;// 由在线检测完成对其是否在线的监测
                            }
                            else//连接成功
                            {
                                StaticClass.intPtrs_Status[y] = (int)RunningStatus.正常;
                                DMSDK.DM_GetTemp(StaticClass.intPtrs_Operate[y], 1);//获取一次温度数据
                                fMessCallBack = new DMSDK.fMessCallBack(dmMessCallBack);//回调函数实例
                                DMSDK.DM_SetAllMessCallBack(fMessCallBack, 0);
                            }
                        }
                        else//未启用
                        {
                            StaticClass.intPtrs_Status[y] = (int)RunningStatus.未启用;
                        }
                    }
                    threadStatus.Start();
                    threadCheckOnline.Start();
                    threadCheckAlarm.Start();
                    //threadGetTemper.Start();
                    btnStart.BackgroundImage = Properties.Resources.Pause;
                    btnStart.Tag = "Pause";
                    RunStaus = true;
                }
                else if (btnStart.Tag.ToString() == "Pause")
                {
                    threadCheckOnline.Abort();
                    threadCheckAlarm.Abort();
                    for (int i = 0; i < 16; i++)
                    {
                        if (StaticClass.intPtrs_Enable[i])
                        {
                            DMSDK.DM_CloseMonitor(StaticClass.intPtrs_Connect[i]);//关闭视频连接
                            DMSDK.DM_Disconnect(StaticClass.intPtrs_Operate[i]);//关闭操作连接
                        }
                        StaticClass.intPtrs_Status[i] =(int)RunningStatus.停止;//无图像
                    }
                    btnStart.Tag = "Start";
                    btnStart.BackgroundImage = Properties.Resources.start;
                    RunStaus = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("系统启动异常！"+ex.Message);
            }
            finally
            {
                thread.Abort();
            }
        }

        FrmIsRunning frmIsRunning;
        private void showIsRunning()
        {
            try
            {
                frmIsRunning = new FrmIsRunning();
                frmIsRunning.ShowDialog();
            }
            catch (Exception ex)
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
                            case 2://故障 （掉线）
                                StaticClass.intPtrs_Connect[i] = DMSDK.DM_OpenMonitor(StaticClass.intPtrs_UCPbx[i], StaticClass.intPtrs_Ip[i], 5000);//疯狂重连
                                StaticClass.intPtrs_Operate[i] = DMSDK.DM_Connect(StaticClass.intPtrs_UCPbx[i], StaticClass.intPtrs_Ip[i], 80);
                                if (StaticClass.intPtrs_Connect[i] >= 0 && StaticClass.intPtrs_Operate[i] > 0)//重连成功
                                {
                                    StaticClass.intPtrs_Status[i] = (int)RunningStatus.正常;
                                    Data_Err(i + 1, StaticClass.intPtrs_Ip[i], DateTime.Now, "通讯恢复", StaticClass.intPtrs_CameraName[i],false);
                                    DMSDK.DM_GetTemp(StaticClass.intPtrs_Operate[i], 1);
                                    fMessCallBack = new DMSDK.fMessCallBack(dmMessCallBack);
                                    DMSDK.DM_SetAllMessCallBack(fMessCallBack, 0);
                                }
                                else
                                {
                                    Change_Status(i, 2);
                                }
                                break;
                            case 3://火警  + 故障
                                StaticClass.intPtrs_Connect[i] = DMSDK.DM_OpenMonitor(StaticClass.intPtrs_UCPbx[i], StaticClass.intPtrs_Ip[i], 5000);//故障了就疯狂重连
                                StaticClass.intPtrs_Operate[i] = DMSDK.DM_Connect(StaticClass.intPtrs_UCPbx[i], StaticClass.intPtrs_Ip[i], 80);
                                if (StaticClass.intPtrs_Connect[i] >= 0 && StaticClass.intPtrs_Operate[i] > 0)//重连成功
                                {
                                    StaticClass.intPtrs_Status[i] = (int)RunningStatus.火警;
                                    Data_Err(i + 1, StaticClass.intPtrs_Ip[i], DateTime.Now, "通讯恢复", StaticClass.intPtrs_CameraName[i],false);
                                    DMSDK.DM_GetTemp(StaticClass.intPtrs_Operate[i], 1);
                                    fMessCallBack = new DMSDK.fMessCallBack(dmMessCallBack);
                                    DMSDK.DM_SetAllMessCallBack(fMessCallBack, 0);
                                }
                                else
                                {
                                    Change_Status(i, 3);
                                }
                                break;
                            case 4://正常
                                break;
                            case 5://停止工作
                                Change_Status(i, 5);
                                break;
                        }
                        Thread.Sleep(50);
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
            }
        }

        private void CheckOnline()//在线检测
        {
            while (true)
            {
                try
                {
                    for (int j = 0; j < 16; j++)
                    {
                        if (StaticClass.intPtrs_Enable[j])
                        {
                            if (StaticClass.intPtrs_Ip[j] != null)//这里需要等一等  等到把IP地址信息扔进去再开始
                            {
                                if (DMSDK.DM_CheckOnline(StaticClass.intPtrs_Ip[j], 5000) < 0)//检测结果 离线
                                {
                                    DMSDK.DM_CloseMonitor(StaticClass.intPtrs_Connect[j]);
                                    DMSDK.DM_Disconnect(StaticClass.intPtrs_Operate[j]);
                                    if ((StaticClass.intPtrs_Status[j] != (int)RunningStatus.故障) && (StaticClass.intPtrs_Status[j] != (int)RunningStatus.先火警再故障))//不是通讯故障
                                    {
                                        if (StaticClass.intPtrs_Status[j] != (int)RunningStatus.火警)//不是火警
                                        {
                                            StaticClass.intPtrs_Status[j] = (int)RunningStatus.故障;
                                        }
                                        else//有火警
                                        {
                                            StaticClass.intPtrs_Status[j] = (int)RunningStatus.先火警再故障;
                                        }
                                        Data_Err(j + 1, StaticClass.intPtrs_Ip[j], DateTime.Now, "通讯故障", StaticClass.intPtrs_CameraName[j],true);
                                    }
                                }
                                else//在线了
                                {
                                    if(StaticClass.intPtrs_Connect[j] < 0 || StaticClass.intPtrs_Operate[j] <= 0)//但是连接返回值与 操作返回值 却是失败的（即为 在线 启用连接失败或者为连接状态）
                                    {
                                        if ((StaticClass.intPtrs_Status[j] != (int)RunningStatus.故障) && (StaticClass.intPtrs_Status[j] != (int)RunningStatus.先火警再故障))//不是通讯故障
                                        {
                                            if (StaticClass.intPtrs_Status[j] != (int)RunningStatus.火警)//不是火警
                                            {
                                                StaticClass.intPtrs_Status[j] = (int)RunningStatus.故障;
                                            }
                                            else
                                            {
                                                StaticClass.intPtrs_Status[j] = (int)RunningStatus.先火警再故障;
                                            }
                                            Data_Err(j + 1, StaticClass.intPtrs_Ip[j], DateTime.Now, "通讯故障", StaticClass.intPtrs_CameraName[j], true);
                                        }
                                    }
                                }
                            }
                            Thread.Sleep(200);
                        }
                    }
                }
                catch(Exception ex)
                {
                    if (ex.Message == "正在中止线程。")
                    {

                    }
                    else
                    {
                        MessageBox.Show(ex.Message + "主机在线检测异常");
                    }
                }
            }
        }


        private void CheckAlarm()//判断火警使用
        {
            while (true)
            {
                try {
                    //Stopwatch stopwatch = new Stopwatch();
                    //stopwatch.Start();
                    for (int i = 0; i < 16; i++)
                    {
                        if (StaticClass.intPtrs_Enable[i] && StaticClass.intPtrs_Status[i] != 1)//该探测器启用  无火警
                        {
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
                                    if (StaticClass.intPtrs_structCameraAlarmCounts[i].areaAlarmCounts[j].AreaCount >= 3)//超过3次 就告警
                                    {
                                        if (StaticClass.intPtrs_Status[i] == (int)RunningStatus.正常)//原来不是报警 
                                        {
                                            if (StaticClass.FireCount == 0)//全局火警计数
                                            {
                                                Data_Frie(i + 1, StaticClass.intPtrs_Ip[i], DateTime.Now, "首警", StaticClass.intPtrs_CameraName[i]);
                                            }
                                            else if (StaticClass.FireCount >= 1)
                                            {
                                                Data_Frie(i + 1, StaticClass.intPtrs_Ip[i], DateTime.Now, "火警", StaticClass.intPtrs_CameraName[i]);
                                            }
                                            StaticClass.intPtrs_Status[i] = (int)RunningStatus.火警;//原来不报警 现在报警
                                        }
                                        areaAlarmCount.AreaCount = 3;
                                    }
                                }
                            }
                        }
                        Thread.Sleep(50);
                    }
                    //stopwatch.Stop();
                    //string time = stopwatch.ElapsedMilliseconds.ToString();
                }
                catch(Exception ex)
                {
                    if (ex.Message == "正在中止线程。")
                    {

                    }
                    else
                    {
                        MessageBox.Show(ex.Message + "火警监测异常");
                    }
                }
            }
        }

        private void GetTemper()
        {
            while (true)
            {
                for(int i = 0; i < 16; i++)
                {
                    if (StaticClass.intPtrs_Operate[i] > 0 && StaticClass.intPtrs_Connect[i] >= 0)
                    {
                        DMSDK.DM_GetTemp(StaticClass.intPtrs_Operate[i], 0);//获取一次温度数据
                        fMessCallBack = new DMSDK.fMessCallBack(dmMessCallBack);//回调函数实例
                        DMSDK.DM_SetAllMessCallBack(fMessCallBack, 0);
                        int j = DMSDK.DM_GetUpdateMeaTemp(StaticClass.intPtrs_Operate[i]);
                    }
                    Thread.Sleep(5000);
                }
            }
        }
        #region//回调函数
        /*
         * 多台仪器的告警信息无法获取（判断是那一台的告警信息），需要程序自己判断（大立公司人员确认过了）
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
            }
        }
        #endregion

        bool IsCallBack = false;

        private void Data_Frie(int cameraId,string Ip,DateTime dateTime,string type,string message) 
        {
            fireData.CameraID = cameraId;
            fireData.IPAddress = Ip;
            fireData.dateTime = dateTime;
            fireData.Type = type;
            fireData.Message = message;
            StaticClass.arrayList_FireData.Add(fireData);
            sqlCreate.Insert_HisRecords(fireData.CameraID, fireData.IPAddress, fireData.dateTime, fireData.Type, fireData.Message);
        }

        private void Data_Err(int cameraId, string Ip, DateTime dateTime, string type, string message,bool IsShowOnDgv)
        {
            errData.CameraID = cameraId;
            errData.IPAddress = Ip;
            errData.dateTime = dateTime;
            errData.Type = type;
            errData.Message = message;
            if (IsShowOnDgv)//通讯故障是需要展示在dgv上的
            {
                StaticClass.arrayList_ErrData.Add(errData);
            }
            sqlCreate.Insert_HisRecords(errData.CameraID, errData.IPAddress, errData.dateTime, errData.Type, errData.Message);
        }

        private void btnSaveVideo_Click(object sender, EventArgs e)//保存视频.avi
        {
            //StaticClass.intPtrs_Operate[1] = DMSDK.DM_OpenMonitor(StaticClass.intPtrs_UCPbx[1], "192.168.1.2", 5000);
            //DMSDK.DM_Record(StaticClass.intPtrs_Operate[1], "F:\\热成像仪光盘内容");
            //Thread.Sleep(5000);
            //DMSDK.DM_StopRecord(StaticClass.intPtrs_Operate[1]);
        }

        
    }
}
