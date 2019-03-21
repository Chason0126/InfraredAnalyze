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

        #region//构造函数
        public FrmMain()
        {
            InitializeComponent();
            DMSDK.DM_Init();//关闭时需要释放资源
            DMSDK.DM_PlayerInit(spcScreen.Handle);//初始化视频  只能调用一次
            try
            {
                //for (int i = 0; i < 16; i++)
                //{
                //    UCPbx uCPbx = new UCPbx();
                //    StaticClass.intPtrs_UCPbx[i] = uCPbx.Handle;//会返回null值
                //}
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

                StaticClass.intPtrs_UCStatusPanel[0] = ucStatusPanel1.Handle;
                StaticClass.intPtrs_UCStatusPanel[1] = ucStatusPanel2.Handle;
                StaticClass.intPtrs_UCStatusPanel[2] = ucStatusPanel3.Handle;
                StaticClass.intPtrs_UCStatusPanel[3] = ucStatusPanel4.Handle;
                StaticClass.intPtrs_UCStatusPanel[4] = ucStatusPanel5.Handle;
                StaticClass.intPtrs_UCStatusPanel[5] = ucStatusPanel6.Handle;
                StaticClass.intPtrs_UCStatusPanel[6] = ucStatusPanel7.Handle;
                StaticClass.intPtrs_UCStatusPanel[7] = ucStatusPanel8.Handle;
                StaticClass.intPtrs_UCStatusPanel[8] = ucStatusPanel9.Handle;
                StaticClass.intPtrs_UCStatusPanel[9] = ucStatusPanel10.Handle;
                StaticClass.intPtrs_UCStatusPanel[10] = ucStatusPanel11.Handle;
                StaticClass.intPtrs_UCStatusPanel[11] = ucStatusPanel12.Handle;
                StaticClass.intPtrs_UCStatusPanel[12] = ucStatusPanel13.Handle;
                StaticClass.intPtrs_UCStatusPanel[13] = ucStatusPanel14.Handle;
                StaticClass.intPtrs_UCStatusPanel[14] = ucStatusPanel15.Handle;
                StaticClass.intPtrs_UCStatusPanel[15] = ucStatusPanel16.Handle;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region//调增 运行显示状态的大小
        private void Resize_uCStatusPane()
        {
            foreach (UCStatusPanel uCStatusPanel in tlpRunningStatus.Controls)
            {
                uCStatusPanel.Height = tlpRunningStatus.Height / 16;
                uCStatusPanel.Width = tlpRunningStatus.Width;
            }
        }
        #endregion

        #region//窗体右上角按钮功能
        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
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

        private void btnWindow_Click(object sender, EventArgs e)//窗口全屏、恢复按钮 
        {
            if (this.WindowState==FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                btnWindow.BackgroundImage = Properties.Resources.窗口化;
                Refresh_Screen(ScreenNum);
                Resize_uCStatusPane();
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
                    foreach(StaticClass.StructIAnalyzeConfig structIAnalyzeConfig in temp_arrayList)
                    {
                        TreeNode temp_Node = new TreeNode();
                        StaticClass.StructSM7003Tag structSM7003Tag = new StaticClass.StructSM7003Tag();
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
                        }else
                        {
                           
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
        Point tvwPoint;
        private void tvwSensor_MouseDown(object sender, MouseEventArgs e)
        {
            tvwPoint = new Point(e.X, e.Y);
            try
            {
                if(e.Button==MouseButtons.Left)
                {
                    tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
                    if(tvwSensor.SelectedNode==null)
                    {
                        tvwSensor.SelectedNode = null;//取消选中的树视图节点
                    }
                    else
                    {
                       
                    }
                }else if(e.Button==MouseButtons.Right)
                {
                    tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
                    if (tvwSensor.SelectedNode != null)
                    {
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
            TreeNode treeNode = tvwSensor.GetNodeAt(tvwPoint);
            FrmCameraNetConfig frmCameraConfig = new FrmCameraNetConfig();
            if (treeNode != null)
            {
                StaticClass.StructSM7003Tag sM7003Tag = (StaticClass.StructSM7003Tag)treeNode.Tag;//将所双击的树视图的节点的 cameraID赋值给弹出窗体的ID属性
                frmCameraConfig.IPCameraID = sM7003Tag.CameraID;
                frmCameraConfig.ShowDialog();
            }
        }
        #endregion

        #region//探测器列表操作(上移、下移、删除)
        #region//删除探测器节点
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StaticClass.StructSM7003Tag structSM7003Tag = new StaticClass.StructSM7003Tag();
                tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
                if (tvwSensor.SelectedNode != null)
                {
                    structSM7003Tag = (StaticClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
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

        #region//系统参数设置
        private void 测温参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                pnl frmTemperParamConfig = new pnl();
                tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
                StaticClass.StructSM7003Tag structSM7003Tag = new StaticClass.StructSM7003Tag();
                if (tvwSensor.SelectedNode != null)
                {
                    structSM7003Tag = (StaticClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
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

        #region//网络参数设置
        private void 网络参数设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCameraNetConfig frmCameraConfig = new FrmCameraNetConfig();
            StaticClass.StructSM7003Tag structSM7003Tag = new StaticClass.StructSM7003Tag();
            tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
            if (tvwSensor.SelectedNode != null)
            {
                structSM7003Tag = (StaticClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
                frmCameraConfig.IPCameraID = structSM7003Tag.CameraID;
                frmCameraConfig.ShowDialog();
            }
        }
        #endregion

        #region//图像设置
        private void 图像设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmImageConfig frmImageConfig = new FrmImageConfig();
            StaticClass.StructSM7003Tag structSM7003Tag = new StaticClass.StructSM7003Tag();
            tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
            if (tvwSensor.SelectedNode != null)
            {
                structSM7003Tag = (StaticClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
                frmImageConfig.CameraId = structSM7003Tag.CameraID;
                frmImageConfig.Ip = structSM7003Tag.IP;
                frmImageConfig.ShowDialog();
            }

        }
        #endregion

        #region//连接
        private void 连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StaticClass.StructSM7003Tag structSM7003Tag = new StaticClass.StructSM7003Tag();
            tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
            try
            {
                if (tvwSensor.SelectedNode != null)
                {
                    structSM7003Tag = (StaticClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;

                    int InitValue = DMSDK.DM_PlayerInit(StaticClass.intPtrs_UCPbx[structSM7003Tag.CameraID - 1]);
                    if (InitValue < 0)
                    {
                        MessageBox.Show("探测器初始化失败");
                        return;
                    }
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
            StaticClass.StructSM7003Tag structSM7003Tag = new StaticClass.StructSM7003Tag();
            tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
            if(tvwSensor.SelectedNode!=null)
            {
                structSM7003Tag = (StaticClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
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
            StaticClass.StructSM7003Tag structSM7003Tag = new StaticClass.StructSM7003Tag();
            tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
            if (tvwSensor.SelectedNode != null)
            {
                structSM7003Tag = (StaticClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
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
            StaticClass.StructSM7003Tag structSM7003Tag = new StaticClass.StructSM7003Tag();
            tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
            if (tvwSensor.SelectedNode != null)
            {
                structSM7003Tag = (StaticClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
                frmHistoricalTemperData.CameraID = structSM7003Tag.CameraID;
                frmHistoricalTemperData.ShowDialog();
            }
        }
        #endregion

        #region //实时数据 （曲线）
        private void 实时数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRealTimeTemperData frmRealTimeTemperData = new FrmRealTimeTemperData();
            StaticClass.StructSM7003Tag structSM7003Tag = new StaticClass.StructSM7003Tag();
            tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
            if (tvwSensor.SelectedNode != null)
            {
                structSM7003Tag = (StaticClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
                frmRealTimeTemperData.CameraID = structSM7003Tag.CameraID;
                frmRealTimeTemperData.ShowDialog();
            }
        }
        #endregion

        #region//配置IP
        private void 配置IPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmIpConfig frmIpConfig = new FrmIpConfig();
            frmIpConfig.ShowDialog();
        }
        #endregion

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadTreeView();
            ScreenNum = Convert.ToInt32(ConfigurationManager.AppSettings["ScreenNum"]);
            Refresh_Screen(ScreenNum);
        }

        private void btnAddSensor_Click(object sender, EventArgs e)//添加探测器
        {
            int num = sqlCreate.Select_Num_SMInfraredConfig();
            if(num>=16)
            {
                MessageBox.Show("探测器数量不足，该版本最多只支持16台探测器！");
            }else
            {
                FrmAddIPCamera frmAddIPCamera = new FrmAddIPCamera();
                frmAddIPCamera.Num = num;
                if(frmAddIPCamera.ShowDialog()==DialogResult.OK)
                {
                    LoadTreeView();
                }
            }
        }

        private void btnCameraConfig_Click(object sender, EventArgs e)
        {
           
        }

        private void btnDisConnect_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnDisConnect, "全部断开");
        }

        private void btnDisConnect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= (tlpScreen.Controls.Count-1); i++)
            {
                int ReturnValue = DMSDK.DM_CloseMonitor(StaticClass.intPtrs_Connect[i]);
                if (ReturnValue < 0)
                {
                    MessageBox.Show("断开失败！");
                }
            }
        }

        private void btnStart_MouseEnter(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnStart, "一键链接");
        }

        ArrayList temp_arrayList;
        Thread thread;
        Thread threadStatus;
        Thread threadCheckOnline;
        private void btnStart_Click(object sender, EventArgs e)
        {
            thread = new Thread(showIsRunning);
            thread.IsBackground = true;
            thread.Start();
            try
            {
                temp_arrayList = sqlCreate.Select_All_SMInfraredConfig();//按NodeId降序排列
                if (btnStart.Tag.ToString() == "Start")
                {
                    threadStatus = new Thread(StatusJudgment);
                    threadCheckOnline = new Thread(CheckOnline);
                    threadStatus.IsBackground = true;
                    threadStatus.Start();
                    threadCheckOnline.IsBackground = true;
                    threadCheckOnline.Start();
                    foreach (StaticClass.StructIAnalyzeConfig structIAnalyzeConfig in temp_arrayList)
                    {
                        StaticClass.intPtrs_Enable[structIAnalyzeConfig.CameraID - 1] = structIAnalyzeConfig.Enable;
                        StaticClass.intPtrs_CameraName[structIAnalyzeConfig.CameraID - 1] = structIAnalyzeConfig.CameraName;
                        StaticClass.intPtrs_Ip[structIAnalyzeConfig.CameraID - 1] = structIAnalyzeConfig.IP;
                        StaticClass.intPtrs_NodeId[structIAnalyzeConfig.NodeID - 1] = structIAnalyzeConfig.CameraID - 1;

                        int Numbering = structIAnalyzeConfig.CameraID - 1;//与画面的顺序有关(暂时先不考虑顺序)  连接句柄与视屏句柄顺序有影响
                        if (structIAnalyzeConfig.Enable == true)//启用
                        {
                            DMSDK.DM_Init();//初始化连接
                            StaticClass.intPtrs_Connect[Numbering] = DMSDK.DM_OpenMonitor(StaticClass.intPtrs_UCPbx[Numbering], structIAnalyzeConfig.IP, 5000);// 返回值为视频操作句柄
                            StaticClass.intPtrs_Operate[Numbering] = DMSDK.DM_Connect(StaticClass.intPtrs_UCPbx[Numbering], structIAnalyzeConfig.IP, 80);
                            if (StaticClass.intPtrs_Connect[Numbering] < 0 || StaticClass.intPtrs_Operate[Numbering] <= 0)//连接失败
                            {
                                StaticClass.intPtrs_Status[Numbering] = (int)RunningStatus.故障;
                            }
                            else//连接成功
                            {
                                StaticClass.intPtrs_Status[Numbering] = (int)RunningStatus.正常;
                                DMSDK.DM_GetTemp(StaticClass.intPtrs_Operate[Numbering], 1);//连续获取测温对象的数据
                                CallBack(Numbering + 1);
                            }
                        }
                        else//未启用
                        {
                            StaticClass.intPtrs_Status[Numbering] = (int)RunningStatus.未启用;
                        }
                    }
                    btnStart.BackgroundImage = Properties.Resources.Pause;
                    btnStart.Tag = "Pause";
                }
                else if (btnStart.Tag.ToString() == "Pause")
                {
                    foreach (StaticClass.StructIAnalyzeConfig structIAnalyzeConfig in temp_arrayList)
                    {
                        int Numbering = structIAnalyzeConfig.NodeID - 1;
                        if (StaticClass.intPtrs_Connect[Numbering] >=0&& StaticClass.intPtrs_Operate[Numbering] > 0) 
                        {
                            DMSDK.DM_CloseMonitor(StaticClass.intPtrs_Connect[Numbering]);//关闭视频连接
                            DMSDK.DM_Disconnect(StaticClass.intPtrs_Operate[Numbering]);//关闭操作连接
                        }
                        StaticClass.intPtrs_Status[Numbering] = (int)RunningStatus.未启用;
                        UCPbx uCPbx = (UCPbx)FromHandle(StaticClass.intPtrs_UCPbx[Numbering]);
                        uCPbx.BackgroundImage = Properties.Resources.nopicture;
                    }
                    btnStart.Tag = "Start";
                    btnStart.BackgroundImage = Properties.Resources.start;
                    threadStatus.Abort();
                    threadCheckOnline.Abort();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                
            }
           
        }

        private void StatusJudgment()//状态判断
        {
            while (true)
            {
                for (int i = 0; i < 16; i++)
                {
                    UCPbx uCPbx = (UCPbx)FromHandle(StaticClass.intPtrs_UCPbx[i]);
                    UCStatusPanel uCStatus = (UCStatusPanel)FromHandle(StaticClass.intPtrs_UCStatusPanel[i]);
                    switch (StaticClass.intPtrs_Status[i])
                    {
                        case 0://未启用 
                            uCPbx.BackgroundImage = Properties.Resources.nopicture;
                            uCStatus.BackColor = Color.Gray;
                            break;
                        case 1://告警
                            uCStatus.BackColor = Color.Red;
                            break;
                        case 2://故障
                            uCPbx.BackgroundImage = Properties.Resources.disableCamera;
                            break;
                        case 3://正常
                            uCStatus.BackColor = Color.Blue;
                            break;
                        case 4://离线  疯狂重连 
                            uCStatus.BackColor = Color.Yellow;
                            DMSDK.DM_StopTemp(StaticClass.intPtrs_Operate[i]);//停止回调函数
                            uCPbx.BackgroundImage = Properties.Resources.nopicture;
                            StaticClass.intPtrs_Connect[i] = DMSDK.DM_OpenMonitor(StaticClass.intPtrs_UCPbx[i], StaticClass.intPtrs_Ip[i], 5000);
                            StaticClass.intPtrs_Operate[i] = DMSDK.DM_Connect(StaticClass.intPtrs_UCPbx[i], StaticClass.intPtrs_Ip[i], 80);
                            if (StaticClass.intPtrs_Connect[i] >= 0 || StaticClass.intPtrs_Operate[i] > 0)//重连成功
                            {
                                StaticClass.intPtrs_Status[i] = (int)RunningStatus.正常;
                                string ip = StaticClass.intPtrs_Ip[i];
                                string cname = StaticClass.intPtrs_CameraName[i];
                                BeginInvoke(new MethodInvoker(delegate
                                {
                                    dgvError.Rows.Add(DateTime.Now, "连接恢复", ip, cname);
                                }));
                                CallBack(i + 1);//启用回调函数  获取数据
                            }
                            break;
                    }
                    Thread.Sleep(200);
                }
            }
        }

        private void CheckOnline()
        {
            while (true)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (StaticClass.intPtrs_Enable[j])
                    {
                        if (StaticClass.intPtrs_Ip[j] != null)
                        {
                            if (DMSDK.DM_CheckOnline(StaticClass.intPtrs_Ip[j], 5000) < 0)//离线
                            {
                               
                                DMSDK.DM_CloseMonitor(StaticClass.intPtrs_Connect[j]);
                                DMSDK.DM_Disconnect(StaticClass.intPtrs_Operate[j]);
                                string ip = StaticClass.intPtrs_Ip[j];
                                string cname = StaticClass.intPtrs_CameraName[j];
                                if (StaticClass.intPtrs_Status[j] != 4)
                                {
                                    BeginInvoke(new MethodInvoker(delegate
                                {
                                    dgvError.Rows.Add(DateTime.Now, "连接断开", ip, cname);
                                }));
                                }
                                StaticClass.intPtrs_Status[j] = (int)RunningStatus.离线;
                            }
                            else if (StaticClass.intPtrs_Connect[j]<0 && StaticClass.intPtrs_Operate[j] <= 0)
                            {
                                StaticClass.intPtrs_Status[j] = (int)RunningStatus.离线;
                            }
                        }
                    }
                }
                Thread.Sleep(100);
            }
        }

        #region//回调函数
        /*
         * 当仪器告警时，利用回调函数获取到的告警信息中只有alarmID这一个信息，当测温点、测温区域的编号都从0开始时，则无法通过alarmID判断到底是具体的哪个测温目标发出的告警 
         * 所有 对添加的测温区域、测温点的编号进行限制，使得编号唯一 可以解析出具体的告警类型。使用一个8位的数 来记录告警状态 0表示无告警 1表示 测温告警
         * 但是这样限制了测温目标的数量 （先1测温线、2345测温点 678测温区域 可继续扩展 但是告警信息没办法解析出来具体是哪个的），设置报警的时候，区域9/10分别为全屏最冷点 全屏最热点（那就只能减少点的设置）
         * */
        private DMSDK.fMessCallBack fMessCallBack_1;//没得办法  测温告警的返回信息里只有一个ERRID信息  没办法判断 是哪一台仪器的告警信息 只能调用多个回调函数 丑了一点
        private DMSDK.fMessCallBack fMessCallBack_2;
        private DMSDK.fMessCallBack fMessCallBack_3;
        private DMSDK.fMessCallBack fMessCallBack_4;
        private DMSDK.fMessCallBack fMessCallBack_5;
        private DMSDK.fMessCallBack fMessCallBack_6;
        private DMSDK.fMessCallBack fMessCallBack_7;
        private DMSDK.fMessCallBack fMessCallBack_8;
        private DMSDK.fMessCallBack fMessCallBack_9;
        private DMSDK.fMessCallBack fMessCallBack_10;
        private DMSDK.fMessCallBack fMessCallBack_11;
        private DMSDK.fMessCallBack fMessCallBack_12;
        private DMSDK.fMessCallBack fMessCallBack_13;
        private DMSDK.fMessCallBack fMessCallBack_14;
        private DMSDK.fMessCallBack fMessCallBack_15;
        private DMSDK.fMessCallBack fMessCallBack_16;
        DMSDK.tagTempMessage tempMessage;
        DMSDK.tagAlarm tagAlarm;
        DMSDK.tagError tagError;
        ArrayList arrayList_Area = new ArrayList();
        SqlInsert sqlInsert = new SqlInsert();

        private void CallBack(int Numbering)
        {
            #region//回调函数
            switch (Numbering)
            {
                case 1:
                    fMessCallBack_1 = new DMSDK.fMessCallBack(dmMessCallBack_1);//回调函数实例
                    DMSDK.DM_SetAllMessCallBack(fMessCallBack_1, 0);
                    break;
                case 2:
                    fMessCallBack_2 = new DMSDK.fMessCallBack(dmMessCallBack_2);//回调函数实例
                    DMSDK.DM_SetAllMessCallBack(fMessCallBack_2, 0);
                    break;
                case 3:
                    fMessCallBack_3 = new DMSDK.fMessCallBack(dmMessCallBack_3);//回调函数实例
                    DMSDK.DM_SetAllMessCallBack(fMessCallBack_1, 0);
                    break;
                case 4:
                    fMessCallBack_4 = new DMSDK.fMessCallBack(dmMessCallBack_4);//回调函数实例
                    DMSDK.DM_SetAllMessCallBack(fMessCallBack_4, 0);
                    break;
                case 5:
                    fMessCallBack_5 = new DMSDK.fMessCallBack(dmMessCallBack_5);//回调函数实例
                    DMSDK.DM_SetAllMessCallBack(fMessCallBack_5, 0);
                    break;
                case 6:
                    fMessCallBack_6 = new DMSDK.fMessCallBack(dmMessCallBack_6);//回调函数实例
                    DMSDK.DM_SetAllMessCallBack(fMessCallBack_6, 0);
                    break;
                case 7:
                    fMessCallBack_7 = new DMSDK.fMessCallBack(dmMessCallBack_7);//回调函数实例
                    DMSDK.DM_SetAllMessCallBack(fMessCallBack_7, 0);
                    break;
                case 8:
                    fMessCallBack_8 = new DMSDK.fMessCallBack(dmMessCallBack_8);//回调函数实例
                    DMSDK.DM_SetAllMessCallBack(fMessCallBack_8, 0);
                    break;
                case 9:
                    fMessCallBack_9 = new DMSDK.fMessCallBack(dmMessCallBack_9);//回调函数实例
                    DMSDK.DM_SetAllMessCallBack(fMessCallBack_9, 0);
                    break;
                case 10:
                    fMessCallBack_10 = new DMSDK.fMessCallBack(dmMessCallBack_10);//回调函数实例
                    DMSDK.DM_SetAllMessCallBack(fMessCallBack_10, 0);
                    break;
                case 11:
                    fMessCallBack_11 = new DMSDK.fMessCallBack(dmMessCallBack_11);//回调函数实例
                    DMSDK.DM_SetAllMessCallBack(fMessCallBack_11, 0);
                    break;
                case 12:
                    fMessCallBack_12 = new DMSDK.fMessCallBack(dmMessCallBack_12);//回调函数实例
                    DMSDK.DM_SetAllMessCallBack(fMessCallBack_12, 0);
                    break;
                case 13:
                    fMessCallBack_13 = new DMSDK.fMessCallBack(dmMessCallBack_13);//回调函数实例
                    DMSDK.DM_SetAllMessCallBack(fMessCallBack_13, 0);
                    break;
                case 14:
                    fMessCallBack_14 = new DMSDK.fMessCallBack(dmMessCallBack_14);//回调函数实例
                    DMSDK.DM_SetAllMessCallBack(fMessCallBack_14, 0);
                    break;
                case 15:
                    fMessCallBack_15 = new DMSDK.fMessCallBack(dmMessCallBack_15);//回调函数实例
                    DMSDK.DM_SetAllMessCallBack(fMessCallBack_15, 0);
                    break;
                case 16:
                    fMessCallBack_16 = new DMSDK.fMessCallBack(dmMessCallBack_16);//回调函数实例
                    DMSDK.DM_SetAllMessCallBack(fMessCallBack_16, 0);
                    break;
            }
            #endregion

        }

        private void dmMessCallBack_1(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)//当为测温告警的时候 温度数据可以回调获取  
        {
            msg = msg - 0x8000;
            switch (msg)
            {
                case 0x3051://错误
                    tagError = (DMSDK.tagError)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagError));
                    int ErrID = tagError.ErrorID;
                    //StaticClass.intPtrs_Status[0] = (int)RunningStatus.故障;
                    break;
                case 0x3053://温度数据
                    tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
                    sqlInsert.InsertTemperDataToArrayList(tempMessage);
                    break;
                case 0x3054://报警消息 将alarmID对应的byte置为1
                    tagAlarm = (DMSDK.tagAlarm)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagAlarm));
                    byte AlaemId = (byte)(tagAlarm.AlarmID);
                    StaticClass.intPtrs_AlarmId[0] = (byte)(StaticClass.intPtrs_AlarmId[0] | (0x80>>AlaemId));// 将10000000右移 对应的ID位数 即将对应位置的置为1
                    StaticClass.intPtrs_Status[0] = (int)RunningStatus.温度告警;
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        dgvWarning.Rows.Add(DateTime.Now, "测温告警", StaticClass.intPtrs_Ip[0], StaticClass.intPtrs_CameraName[0] + AlaemId.ToString());
                    }));
                    break;
            }
        }

        private void dmMessCallBack_2(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)
        {
            msg = msg - 0x8000;
            switch (msg)
            {
                case 0x3051://错误
                    StaticClass.intPtrs_Status[1] = (int)RunningStatus.故障;
                    break;
                case 0x3053://温度数据
                    tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
                    sqlInsert.InsertTemperDataToArrayList(tempMessage);
                    break;
                case 0x3054://报警消息
                    tagAlarm = (DMSDK.tagAlarm)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagAlarm));
                    byte AlaemId = (byte)(tagAlarm.AlarmID);
                    StaticClass.intPtrs_AlarmId[1] = (byte)(StaticClass.intPtrs_AlarmId[1] | (0x80 >> AlaemId));
                    StaticClass.intPtrs_Status[1] = (int)RunningStatus.温度告警;
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        dgvWarning.Rows.Add(DateTime.Now, "测温告警", StaticClass.intPtrs_Ip[1], StaticClass.intPtrs_CameraName[1] + AlaemId.ToString());
                    }));
                    break;
            }
        }
        private void dmMessCallBack_3(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)
        {
            msg = msg - 0x8000;
            switch (msg)
            {
                case 0x3051://错误
                    StaticClass.intPtrs_Status[2] = (int)RunningStatus.故障;
                    break;
                case 0x3053://温度数据
                    tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
                    sqlInsert.InsertTemperDataToArrayList(tempMessage);
                    break;
                case 0x3054://报警消息
                    tagAlarm = (DMSDK.tagAlarm)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagAlarm));
                    byte AlaemId = (byte)(tagAlarm.AlarmID);
                    StaticClass.intPtrs_AlarmId[2] = (byte)(StaticClass.intPtrs_AlarmId[2] | (0x80 >> AlaemId));
                    StaticClass.intPtrs_Status[2] = (int)RunningStatus.温度告警;
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        dgvWarning.Rows.Add(DateTime.Now, "测温告警", StaticClass.intPtrs_Ip[2], StaticClass.intPtrs_CameraName[2] + AlaemId.ToString());
                    }));
                    break;
            }
        }

        private void dmMessCallBack_4(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)
        {
            msg = msg - 0x8000;
            switch (msg)
            {
                case 0x3051://错误
                    StaticClass.intPtrs_Status[3] = (int)RunningStatus.故障;
                    break;
                case 0x3053://温度数据
                    tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
                    sqlInsert.InsertTemperDataToArrayList(tempMessage);
                    break;
                case 0x3054://报警消息
                    tagAlarm = (DMSDK.tagAlarm)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagAlarm));
                    byte AlaemId = (byte)(tagAlarm.AlarmID);
                    StaticClass.intPtrs_AlarmId[3] = (byte)(StaticClass.intPtrs_AlarmId[3] | (0x80 >> AlaemId));
                    StaticClass.intPtrs_Status[3] = (int)RunningStatus.温度告警;
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        dgvWarning.Rows.Add(DateTime.Now, "测温告警", StaticClass.intPtrs_Ip[3], StaticClass.intPtrs_CameraName[3] + AlaemId.ToString());
                    }));
                    break;
            }
        }
        private void dmMessCallBack_5(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)
        {
            msg = msg - 0x8000;
            switch (msg)
            {
                case 0x3051://错误
                    StaticClass.intPtrs_Status[4] = (int)RunningStatus.故障;
                    break;
                case 0x3053://温度数据
                    tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
                    sqlInsert.InsertTemperDataToArrayList(tempMessage);
                    break;
                case 0x3054://报警消息
                    tagAlarm = (DMSDK.tagAlarm)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagAlarm));
                    byte AlaemId = (byte)(tagAlarm.AlarmID);
                    StaticClass.intPtrs_AlarmId[4] = (byte)(StaticClass.intPtrs_AlarmId[4] | (0x80 >> AlaemId));
                    StaticClass.intPtrs_Status[4] = (int)RunningStatus.温度告警;
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        dgvWarning.Rows.Add(DateTime.Now, "测温告警", StaticClass.intPtrs_Ip[4], StaticClass.intPtrs_CameraName[4] + AlaemId.ToString());
                    }));
                    break;
            }
        }
        private void dmMessCallBack_6(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)
        {
            msg = msg - 0x8000;
            switch (msg)
            {
                case 0x3051://错误
                    StaticClass.intPtrs_Status[5] = (int)RunningStatus.故障;
                    break;
                case 0x3053://温度数据
                    tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
                    sqlInsert.InsertTemperDataToArrayList(tempMessage);
                    break;
                case 0x3054://报警消息
                    tagAlarm = (DMSDK.tagAlarm)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagAlarm));
                    byte AlaemId = (byte)(tagAlarm.AlarmID);
                    StaticClass.intPtrs_AlarmId[5] = (byte)(StaticClass.intPtrs_AlarmId[5] | (0x80 >> AlaemId));
                    StaticClass.intPtrs_Status[5] = (int)RunningStatus.温度告警;
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        dgvWarning.Rows.Add(DateTime.Now, "测温告警", StaticClass.intPtrs_Ip[5], StaticClass.intPtrs_CameraName[5] + AlaemId.ToString());
                    }));
                    break;
            }
        }
        private void dmMessCallBack_7(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)
        {
            msg = msg - 0x8000;
            switch (msg)
            {
                case 0x3051://错误
                    StaticClass.intPtrs_Status[6] = (int)RunningStatus.故障;
                    break;
                case 0x3053://温度数据
                    tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
                    sqlInsert.InsertTemperDataToArrayList(tempMessage);
                    break;
                case 0x3054://报警消息
                    tagAlarm = (DMSDK.tagAlarm)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagAlarm));
                    byte AlaemId = (byte)(tagAlarm.AlarmID);
                    StaticClass.intPtrs_AlarmId[6] = (byte)(StaticClass.intPtrs_AlarmId[6] | (0x80 >> AlaemId));
                    StaticClass.intPtrs_Status[6] = (int)RunningStatus.温度告警;
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        dgvWarning.Rows.Add(DateTime.Now, "测温告警", StaticClass.intPtrs_Ip[6], StaticClass.intPtrs_CameraName[6] + AlaemId.ToString());
                    }));
                    break;
            }
        }
        private void dmMessCallBack_8(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)
        {
            msg = msg - 0x8000;
            switch (msg)
            {
                case 0x3051://错误
                    StaticClass.intPtrs_Status[7] = (int)RunningStatus.故障;
                    break;
                case 0x3053://温度数据
                    tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
                    sqlInsert.InsertTemperDataToArrayList(tempMessage);
                    break;
                case 0x3054://报警消息
                    tagAlarm = (DMSDK.tagAlarm)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagAlarm));
                    byte AlaemId = (byte)(tagAlarm.AlarmID);
                    StaticClass.intPtrs_AlarmId[7] = (byte)(StaticClass.intPtrs_AlarmId[7] | (0x80 >> AlaemId));
                    StaticClass.intPtrs_Status[7] = (int)RunningStatus.温度告警;
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        dgvWarning.Rows.Add(DateTime.Now, "测温告警", StaticClass.intPtrs_Ip[7], StaticClass.intPtrs_CameraName[7] + AlaemId.ToString());
                    }));
                    break;
            }
        }
        private void dmMessCallBack_9(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)
        {
            msg = msg - 0x8000;
            switch (msg)
            {
                case 0x3051://错误
                    StaticClass.intPtrs_Status[8] = (int)RunningStatus.故障;
                    break;
                case 0x3053://温度数据
                    tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
                    sqlInsert.InsertTemperDataToArrayList(tempMessage);
                    break;
                case 0x3054://报警消息
                    tagAlarm = (DMSDK.tagAlarm)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagAlarm));
                    byte AlaemId = (byte)(tagAlarm.AlarmID);
                    StaticClass.intPtrs_AlarmId[8] = (byte)(StaticClass.intPtrs_AlarmId[8] | (0x80 >> AlaemId));
                    StaticClass.intPtrs_Status[8] = (int)RunningStatus.温度告警;
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        dgvWarning.Rows.Add(DateTime.Now, "测温告警", StaticClass.intPtrs_Ip[8], StaticClass.intPtrs_CameraName[8] + AlaemId.ToString());
                    }));
                    break;
            }
        }
        private void dmMessCallBack_10(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)
        {
            msg = msg - 0x8000;
            switch (msg)
            {
                case 0x3051://错误
                    StaticClass.intPtrs_Status[9] = (int)RunningStatus.故障;
                    break;
                case 0x3053://温度数据
                    tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
                    sqlInsert.InsertTemperDataToArrayList(tempMessage);
                    break;
                case 0x3054://报警消息
                    tagAlarm = (DMSDK.tagAlarm)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagAlarm));
                    byte AlaemId = (byte)(tagAlarm.AlarmID);
                    StaticClass.intPtrs_AlarmId[9] = (byte)(StaticClass.intPtrs_AlarmId[9] | (0x80 >> AlaemId));
                    StaticClass.intPtrs_Status[9] = (int)RunningStatus.温度告警;
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        dgvWarning.Rows.Add(DateTime.Now, "测温告警", StaticClass.intPtrs_Ip[9], StaticClass.intPtrs_CameraName[9] + AlaemId.ToString());
                    }));
                    break;
            }
        }
        private void dmMessCallBack_11(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)
        {
            msg = msg - 0x8000;
            switch (msg)
            {
                case 0x3051://错误
                    StaticClass.intPtrs_Status[10] = (int)RunningStatus.故障;
                    break;
                case 0x3053://温度数据
                    tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
                    sqlInsert.InsertTemperDataToArrayList(tempMessage);
                    break;
                case 0x3054://报警消息
                    tagAlarm = (DMSDK.tagAlarm)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagAlarm));
                    byte AlaemId = (byte)(tagAlarm.AlarmID);
                    StaticClass.intPtrs_AlarmId[10] = (byte)(StaticClass.intPtrs_AlarmId[10] | (0x80 >> AlaemId));
                    StaticClass.intPtrs_Status[10] = (int)RunningStatus.温度告警;
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        dgvWarning.Rows.Add(DateTime.Now, "测温告警", StaticClass.intPtrs_Ip[10], StaticClass.intPtrs_CameraName[10] + AlaemId.ToString());
                    }));
                    break;
            }
        }
        private void dmMessCallBack_12(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)
        {
            msg = msg - 0x8000;
            switch (msg)
            {
                case 0x3051://错误
                    StaticClass.intPtrs_Status[11] = (int)RunningStatus.故障;
                    break;
                case 0x3053://温度数据
                    tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
                    sqlInsert.InsertTemperDataToArrayList(tempMessage);
                    break;
                case 0x3054://报警消息
                    tagAlarm = (DMSDK.tagAlarm)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagAlarm));
                    byte AlaemId = (byte)(tagAlarm.AlarmID);
                    StaticClass.intPtrs_AlarmId[11] = (byte)(StaticClass.intPtrs_AlarmId[0] | (0x80 >> AlaemId));
                    StaticClass.intPtrs_Status[11] = (int)RunningStatus.温度告警;
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        dgvWarning.Rows.Add(DateTime.Now, "测温告警", StaticClass.intPtrs_Ip[11], StaticClass.intPtrs_CameraName[11] + AlaemId.ToString());
                    }));
                    break;
            }
        }
        private void dmMessCallBack_13(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)
        {
            msg = msg - 0x8000;
            switch (msg)
            {
                case 0x3051://错误
                    StaticClass.intPtrs_Status[12] = (int)RunningStatus.故障;
                    break;
                case 0x3053://温度数据
                    tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
                    sqlInsert.InsertTemperDataToArrayList(tempMessage);
                    break;
                case 0x3054://报警消息
                    tagAlarm = (DMSDK.tagAlarm)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagAlarm));
                    byte AlaemId = (byte)(tagAlarm.AlarmID);
                    StaticClass.intPtrs_AlarmId[12] = (byte)(StaticClass.intPtrs_AlarmId[12] | (0x80 >> AlaemId));
                    StaticClass.intPtrs_Status[12] = (int)RunningStatus.温度告警;
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        dgvWarning.Rows.Add(DateTime.Now, "测温告警", StaticClass.intPtrs_Ip[12], StaticClass.intPtrs_CameraName[12] + AlaemId.ToString());
                    }));
                    break;
            }
        }
        private void dmMessCallBack_14(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)
        {
            msg = msg - 0x8000;
            switch (msg)
            {
                case 0x3051://错误
                    StaticClass.intPtrs_Status[13] = (int)RunningStatus.故障;
                    break;
                case 0x3053://温度数据
                    tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
                    sqlInsert.InsertTemperDataToArrayList(tempMessage);
                    break;
                case 0x3054://报警消息
                    tagAlarm = (DMSDK.tagAlarm)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagAlarm));
                    byte AlaemId = (byte)(tagAlarm.AlarmID);
                    StaticClass.intPtrs_AlarmId[13] = (byte)(StaticClass.intPtrs_AlarmId[13] | (0x80 >> AlaemId));
                    StaticClass.intPtrs_Status[13] = (int)RunningStatus.温度告警;
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        dgvWarning.Rows.Add(DateTime.Now, "测温告警", StaticClass.intPtrs_Ip[13], StaticClass.intPtrs_CameraName[13] + AlaemId.ToString());
                    }));
                    break;
            }
        }
        private void dmMessCallBack_15(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)
        {
            msg = msg - 0x8000;
            switch (msg)
            {
                case 0x3051://错误
                    StaticClass.intPtrs_Status[14] = (int)RunningStatus.故障;
                    break;
                case 0x3053://温度数据
                    tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
                    sqlInsert.InsertTemperDataToArrayList(tempMessage);
                    break;
                case 0x3054://报警消息
                    tagAlarm = (DMSDK.tagAlarm)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagAlarm));
                    byte AlaemId = (byte)(tagAlarm.AlarmID);
                    StaticClass.intPtrs_AlarmId[14] = (byte)(StaticClass.intPtrs_AlarmId[14] | (0x80 >> AlaemId));
                    StaticClass.intPtrs_Status[14] = (int)RunningStatus.温度告警;
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        dgvWarning.Rows.Add(DateTime.Now, "测温告警", StaticClass.intPtrs_Ip[14], StaticClass.intPtrs_CameraName[14] + AlaemId.ToString());
                    }));
                    break;
            }
        }
        private void dmMessCallBack_16(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)
        {
            msg = msg - 0x8000;
            switch (msg)
            {
                case 0x3051://错误
                    StaticClass.intPtrs_Status[15] = (int)RunningStatus.故障;
                    break;
                case 0x3053://温度数据
                    tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
                    sqlInsert.InsertTemperDataToArrayList(tempMessage);
                    break;
                case 0x3054://报警消息
                    tagAlarm = (DMSDK.tagAlarm)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagAlarm));
                    byte AlaemId = (byte)(tagAlarm.AlarmID);
                    StaticClass.intPtrs_AlarmId[15] = (byte)(StaticClass.intPtrs_AlarmId[15] | (0x80 >> AlaemId));
                    StaticClass.intPtrs_Status[15] = (int)RunningStatus.温度告警;
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        dgvWarning.Rows.Add(DateTime.Now, "测温告警", StaticClass.intPtrs_Ip[15], StaticClass.intPtrs_CameraName[15] + AlaemId.ToString());
                    }));
                    break;
            }
        }
        #endregion

        
    }
}
