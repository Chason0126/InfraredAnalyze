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
        ThreadStart DealCallBack;
        Thread thread_CallBack;

        #region//构造函数
        public FrmMain()
        {
            InitializeComponent();
            DMSDK.DM_Init();//关闭时需要释放资源
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
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        private void btnClose_Click(object sender, EventArgs e)//关闭窗体 退出程序
        {
            DMSDK.DM_PlayerCleanup();
            this.Close();
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
                uCPbx = (UCPbx)FromHandle(StaticClass.intPtrs_UCPbx[i]);
                uCPbx.Id = i + 1;
                uCPbx.DoubleClick += new EventHandler(UCPbx_DoubleClick);
                tlpScreen.Controls.Add(uCPbx);
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
                            temp_Node.NodeFont = new Font("微软雅黑", 9, FontStyle.Strikeout);
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
            try
            {
                tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
                StaticClass.StructSM7003Tag structSM7003Tag = new StaticClass.StructSM7003Tag();
                if (tvwSensor.SelectedNode != null)
                {
                    structSM7003Tag = (StaticClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
                    int CameraId = structSM7003Tag.CameraID;
                    int NodeId = structSM7003Tag.NodeID;
                    sqlCreate.Move_Node_Up(NodeId);
                    LoadTreeView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("上移失败：" + ex.Message);
            }
        }
        #endregion

        #region//探测器下移
        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
                StaticClass.StructSM7003Tag structSM7003Tag = new StaticClass.StructSM7003Tag();
                if (tvwSensor.SelectedNode != null)
                {
                    structSM7003Tag = (StaticClass.StructSM7003Tag)tvwSensor.SelectedNode.Tag;
                    int CameraId = structSM7003Tag.CameraID;
                    int NodeId = structSM7003Tag.NodeID;
                    sqlCreate.Move_Node_Down(NodeId);
                    LoadTreeView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("下移失败：" + ex.Message);
            }
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

        #region
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
        private void btnStart_Click(object sender, EventArgs e)
        {
           
            Thread thread = new Thread(showIsRunning);
            thread.Start();
             DealCallBack = new ThreadStart(CallBack);
            thread_CallBack = new Thread(DealCallBack);
            thread_TemperData = new Thread(TemperData);
            try
            {
                temp_arrayList = sqlCreate.Select_All_SMInfraredConfig();//按NodeId降序排列
                if (btnStart.Tag.ToString() == "Start")
                {
                    thread_CallBack.Start();//线程事件开始
                    thread_TemperData.Start();
                    foreach (StaticClass.StructIAnalyzeConfig structIAnalyzeConfig in temp_arrayList)
                    {
                        int Numbering = structIAnalyzeConfig.NodeID - 1;
                        if (DMSDK.DM_CheckOnline(structIAnalyzeConfig.IP, 5000) > 0)
                        {
                            if (structIAnalyzeConfig.Enable == true)//启用
                            {
                                DMSDK.DM_PlayerInit(StaticClass.intPtrs_UCPbx[Numbering]);//初始化视频
                                DMSDK.DM_Init();//初始化连接
                                StaticClass.intPtrs_Connect[Numbering] = DMSDK.DM_OpenMonitor(StaticClass.intPtrs_UCPbx[Numbering], structIAnalyzeConfig.IP, 5000);// 返回值为视频操作句柄
                                StaticClass.intPtrs_Operate[Numbering] = DMSDK.DM_Connect(StaticClass.intPtrs_UCPbx[Numbering], structIAnalyzeConfig.IP, 80);
                                if (StaticClass.intPtrs_Connect[Numbering] < 0 || StaticClass.intPtrs_Operate[Numbering] <= 0)//连接失败
                                {
                                    Control[] controls = Controls.Find(("ucPbx" + structIAnalyzeConfig.NodeID).ToString(), true);
                                    if (controls != null)
                                    {
                                        UCPbx uCPbx = controls[0] as UCPbx;
                                        uCPbx.BackgroundImage = Properties.Resources.disconnectCamera;
                                        UCStatusPanel statusPanel = new UCStatusPanel();
                                        statusPanel.Status = "连接失败";
                                        //flpRunningStatus.Controls.Add(statusPanel);
                                    }
                                }
                                else//连接成功
                                {
                                    UCStatusPanel statusPanel = new UCStatusPanel();
                                    statusPanel.Status = "连接成功";
                                    //flpRunningStatus.Controls.Add(statusPanel);
                                }
                            }
                            else//未启用
                            {
                                Control[] controls = Controls.Find(("ucPbx" + structIAnalyzeConfig.NodeID).ToString(), true);
                                if (controls != null)
                                {
                                    UCPbx uCPbx = controls[0] as UCPbx;
                                    uCPbx.BackgroundImage = Properties.Resources.disableCamera;
                                }
                            }
                        }
                        else//主机离线
                        {
                            UCStatusPanel statusPanel = new UCStatusPanel();
                            statusPanel.Status = "主机离线";
                            statusPanel.Dock = DockStyle.Top;
                            pnlRunningStatus.Controls.Add(statusPanel);
                        }
                    }
                    btnStart.BackgroundImage = Properties.Resources.Pause;
                    btnStart.Tag = "Pause";
                    fMessCallBack_1 = new DMSDK.fMessCallBack(dmMessCallBack_1);//回调函数实例
                    DMSDK.DM_GetTemp(StaticClass.intPtrs_Operate[0], 1);//连续获取测温对象的数据
                    DMSDK.DM_SetAllMessCallBack(fMessCallBack_1, 0);

                    //fMessCallBack_2 = new DMSDK.fMessCallBack(dmMessCallBack_2);//回调函数实例
                    //DMSDK.DM_GetTemp(StaticClass.intPtrs_Operate[1], 1);//连续获取测温对象的数据
                    //DMSDK.DM_SetAllMessCallBack(fMessCallBack_2, 0);
                }
                else if (btnStart.Tag.ToString() == "Pause")
                {
                    foreach (StaticClass.StructIAnalyzeConfig structIAnalyzeConfig in temp_arrayList)
                    {
                        int Numbering = structIAnalyzeConfig.NodeID - 1;
                        DMSDK.DM_CloseMonitor(StaticClass.intPtrs_Connect[Numbering]);//关闭视频连接
                        DMSDK.DM_Disconnect(StaticClass.intPtrs_Operate[Numbering]);//关闭操作连接
                        btnStart.Tag = "Start";
                        btnStart.BackgroundImage = Properties.Resources.start;
                    }
                    thread_CallBack.Abort();
                    thread_TemperData.Abort();
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

        private void showIsRunning()
        {
            FrmIsRunning frmIsRunning = new FrmIsRunning();
            frmIsRunning.ShowDialog();
        }

        private DMSDK.fMessCallBack fMessCallBack_1;
        private DMSDK.fMessCallBack fMessCallBack_2;
        private void CallBack()
        {
            while (false)
            {
                fMessCallBack_1 = new DMSDK.fMessCallBack(dmMessCallBack_1);//回调函数实例
                DMSDK.DM_GetTemp(StaticClass.intPtrs_Operate[0], 1);//连续获取测温对象的数据
                DMSDK.DM_SetAllMessCallBack(fMessCallBack_1, 0);
                Thread.Sleep(1000);
            }
            //while (true)
            //{
            //    foreach (StaticClass.StructIAnalyzeConfig structIAnalyzeConfig in temp_arrayList)//主机启用
            //    {
            //        if (structIAnalyzeConfig.Enable == true)
            //        {
            //            if (DMSDK.DM_CheckOnline(structIAnalyzeConfig.IP, 5000) < 0)//检查主机是否在线
            //            {
            //                MessageBox.Show("主机掉线！");
            //            }
            //        }
            //    }
            //}
        }

        int len;
        DMSDK.tagTemperature tagTemperature;
        DMSDK.tagTempMessage tempMessage;
        DMSDK.tagAlarm tagAlarm;
        ArrayList arrayList_Area = new ArrayList();
        string CameraIP;
        Thread thread_TemperData;
        SqlInsert sqlInsert = new SqlInsert();
        private void dmMessCallBack_1(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)
        {
            msg = msg - 0x8000;
            switch (msg)
            {
                case 0x3051://错误

                    break;
                case 0x3053://温度数据
                    tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
                    sqlInsert.InsertTemperDataToArrayList(tempMessage);
                    break;
                case 0x3054://报警消息
                    tagAlarm = (DMSDK.tagAlarm)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagAlarm));
                    int ErrId = tagAlarm.AlarmID;
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        //dgvWarning.Rows.Add(DateTime.Now, "报警类型", CameraIP, "名称");
                    }));
                    break;
            }
            Thread.Sleep(2000);
        }
        ArrayList arrayList_TemperData;
        private void TemperData()
        {
            arrayList_TemperData = new ArrayList();

        }

      


        //private void dmMessCallBack_2(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)
        //{
        //    msg = msg - 0x8000;
        //    switch (msg)
        //    {
        //        case 0x3051://错误

        //            break;
        //        case 0x3053://温度数据
        //            tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
        //            CameraIP = tempMessage.dvrIP;
        //            len = tempMessage.len;
        //            for (int i = 0; i < len; i++)
        //            {
        //                tagTemperature = new DMSDK.tagTemperature();
        //                tagTemperature = tempMessage.temperInfo[i];
        //                switch (tagTemperature.type)
        //                {
        //                    case 0://点
        //                        int SpotID = tagTemperature.number + 1;
        //                        break;
        //                    case 1://线
        //                        int LineID = tagTemperature.number + 1;
        //                        break;
        //                    case 2://区域
        //                        break;
        //                }
        //            }
        //            break;
        //        case 0x3054://报警消息
        //            tagAlarm = (DMSDK.tagAlarm)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagAlarm));
        //            int ErrId = tagAlarm.AlarmID;
        //            BeginInvoke(new MethodInvoker(delegate
        //            {
        //                //dgvWarning.Rows.Add(DateTime.Now, "报警类型", CameraIP, "名称");
        //            }));
        //            break;
        //    }
        //}
    }
}
